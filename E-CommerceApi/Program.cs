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
//Identity kütüphanesine yönelik tanýmlamar
builder.Services.AddIdentity<User, IdentityRole>(Opt =>
{
    Opt.User.RequireUniqueEmail = true;    //Emailin tekilliði için
    Opt.Password.RequireNonAlphanumeric = false;  //* , - gibi ifadelerin zorunlu olarak kullanýlmamasý 
    Opt.Password.RequiredLength = 10;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); // þifre sýfýrlama gibi iþlemlerde default bir token üretiyor 


//Kimlik Doðrulama
builder.Services.AddAuthentication(options =>   // Uygulamamýzda kullanýcaðýmýz þemayý vericez
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  // üyelik kullanan þemayý tanýmlýyoruz 
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;     //Jwt den gelen token þemasý  
    //Token bazlý dooðrulama yapmak için aþaðýdaki kodlarý yazacaðýz
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{

    var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();//Appsetting de belirlediðimiz deðerleri almadk için kullandýk  
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters() // vereceðimiz Parametleri burada belirteceðiz
    {
        ValidIssuer = tokenOptions.Issuer, //  "Issuer": "www.authserver.com" deðeri ile gelen deðeri karþýlaþtýracak
        ValidAudience = tokenOptions.Audience[0], //  "Audience": [ "www.authserver.com" ],
        IssuerSigningKey =SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),    //"SecurityKey": "mysecuritykeymysecuritykeymysecuritykeymysecuritykey"

        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero    // Tokenlarýn ömürlerini doðrularken token ömürlerine default olarak eklenen bir süre vardýr bunu sýfýrlýyoruz 
    };
});





builder.Services.AddDbContext<AppDbContext>(x => 
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {

        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

//APplication a yönelik bütün tanýmlamalar burada yapýlacak
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
