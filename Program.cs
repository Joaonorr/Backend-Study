using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApplication1.Context;
using WebApplication1.DTOs.Mappings;
using WebApplication1.Repository;
using WebApplication1.services;

var builder = WebApplication.CreateBuilder(args);

// configure method
builder.ConfigureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
// middlewares
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
