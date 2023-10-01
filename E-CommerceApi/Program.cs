using Application;
using Application.Features.TokenIdentity.TokenService;
using Application.TokenService.Dto;
using Core;
using Domain.AgregateModels.UserModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Option;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));
builder.Services.Configure<Client>(builder.Configuration.GetSection("Clients"));
//Identity k�t�phanesine y�nelik tan�mlamar
builder.Services.AddIdentity<User, IdentityRole>(Opt =>
{
    Opt.User.RequireUniqueEmail = true;    //Emailin tekilli�i i�in
    Opt.Password.RequireNonAlphanumeric = false;  //* , - gibi ifadelerin zorunlu olarak kullan�lmamas� 
    Opt.Password.RequiredLength = 10;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); // �ifre s�f�rlama gibi i�lemlerde default bir token �retiyor 


//Kimlik Do�rulama
builder.Services.AddAuthentication(options =>   // Uygulamam�zda kullan�ca��m�z �emay� vericez
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  // �yelik kullanan �emay� tan�ml�yoruz 
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;     //Jwt den gelen token �emas�  
    //Token bazl� doo�rulama yapmak i�in a�a��daki kodlar� yazaca��z
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{

    var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();//Appsetting de belirledi�imiz de�erleri almadk i�in kulland�k  
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters() // verece�imiz Parametleri burada belirtece�iz
    {
        ValidIssuer = tokenOptions.Issuer, //  "Issuer": "www.authserver.com" de�eri ile gelen de�eri kar��la�t�racak
        ValidAudience = tokenOptions.Audience[0], //  "Audience": [ "www.authserver.com" ],
        IssuerSigningKey =SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),    //"SecurityKey": "mysecuritykeymysecuritykeymysecuritykeymysecuritykey"

        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero    // Tokenlar�n �m�rlerini do�rularken token �m�rlerine default olarak eklenen bir s�re vard�r bunu s�f�rl�yoruz 
    };
});





builder.Services.AddDbContext<AppDbContext>(x => 
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {

        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

//APplication a y�nelik b�t�n tan�mlamalar burada yap�lacak
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices();
builder.Services.AddCoreServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
