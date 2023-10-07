using Application;
using Core;
using Domain.AgregateModels.UserModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//Identity kütüphanesine yönelik tanýmlamar
builder.Services.AddIdentity<User, IdentityRole>(Opt =>
{
    Opt.User.RequireUniqueEmail = true;    //Emailin tekilliði için
    Opt.Password.RequireNonAlphanumeric = false;  //* , - gibi ifadelerin zorunlu olarak kullanýlmamasý 
    Opt.Password.RequiredLength = 10;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); // þifre sýfýrlama gibi iþlemlerde default bir token üretiyor 







builder.Services.AddDbContext<AppDbContext>(x => 
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {

        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

//APplication a yönelik bütün tanýmlamalar burada yapýlacak
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
