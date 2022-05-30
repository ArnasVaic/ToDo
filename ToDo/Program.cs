using AutoMapper;
using AutoMapper;
using ToDo.Dependencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDo.EntityFramework;
using ToDo.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGenericRepository<ToDoEntity>, ToDoRepositoryDb>();
builder.Services.AddScoped<IGenericRepository<ProjectEntity>, ProjectRepositoryDb>();
builder.Services.AddAutoMapper(typeof(OrganizationProfile));

string connectionString = "Server=DESKTOP-SP9PGN8;Initial Catalog=ToDo;Integrated Security=True;MultipleActiveResultSets=True;";
builder.Services.AddDbContext<ProjectContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

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


public class OrganizationProfile : Profile
{
    public OrganizationProfile()
    {
        CreateMap<ToDoViewModel, ToDoEntity>();
        CreateMap<ToDoEntity, ToDoPostModel>();
        CreateMap<ToDoPostModel, ToDoViewModel>();
    }
}