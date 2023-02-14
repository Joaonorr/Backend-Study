using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApplication1.Context;
using WebApplication1.DTOs.Mappings;
using WebApplication1.Middlewares;
using WebApplication1.Repository;
using WebApplication1.services;

var builder = WebApplication.CreateBuilder(args);

// configure Services
builder.ConfigureServices();

var app = builder.Build();

// Configure Middlewares
app.ConfigureMiddlewares();

app.Run();
