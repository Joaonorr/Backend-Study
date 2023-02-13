using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApplication1.Context;
using WebApplication1.DTOs.Mappings;
using WebApplication1.Repository.UnitWork;

namespace WebApplication1.services;

public static class Services
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.

        builder.Services.AddControllers().AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var MappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });
        IMapper mapper = MappingConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);

        // Configure DataBase Connection
        var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
