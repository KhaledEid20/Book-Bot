global using Microsoft.EntityFrameworkCore;
global using BookBot;
global using BookBot.Repository;
global using BookBot.Repository.Base;
global using BookBot.Models;
var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    
builder.Services.AddSingleton<IUnitOfWork , Unitofwork>();
builder.Services.AddSingleton<IAuthorRepo, AuthorRepo>();
builder.Services.AddSingleton<IBookRepo , BookRepo>();


var app = builder.Build();

//Dependency injection in Repository pattern

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
