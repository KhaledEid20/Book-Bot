global using Microsoft.EntityFrameworkCore;
global using BookBot;
global using BookBot.Repository;
global using BookBot.Repository.Base;
global using BookBot.Models;
global using BookBot.DTOs;
global using AutoMapper;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using BookBot.Helper;
using Microsoft.OpenApi.Models;
using BookBot.Extensions;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Books API", Version = "v1" });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenJwtAuth();

//Dependency injection in Repository pattern
builder.Services.AddScoped<IUnitOfWork , Unitofwork>();
builder.Services.AddScoped<IAuthorRepo, AuthorRepo>();
builder.Services.AddScoped<IBookRepo , BookRepo>();
builder.Services.AddScoped<HelperClass>();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;            // No digit required in the password
    options.Password.RequiredLength = 6;              // Minimum password length
    options.Password.RequireNonAlphanumeric = false;  // No non-alphanumeric character required
    options.Password.RequireUppercase = false;        // No uppercase character required
    options.Password.RequireLowercase = false;        // No lowercase character required
    options.Password.RequiredUniqueChars = 1;         // Minimum unique characters in the password
})
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCustomJwtAuth(builder.Configuration);

builder.Services.AddAuthorization(options =>
        options.AddPolicy("AdminPolicy", policy =>
            policy.RequireClaim("Admin")),
        options.AddPolicy("AppUserPolicy" , policy=>
            policy.RequireClaim("AppUser"))
    );
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Books API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
