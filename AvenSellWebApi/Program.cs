using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Json;
using Entity.Dtos;
using Entity.Dto;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AvenSellApi", Version = "v1" });
});

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(new JsonFormatter(), "log.json", shared: true)
    .CreateLogger();
builder.Services.AddHttpContextAccessor();
var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

var configuration = configBuilder.Build();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacBusinessModule());
    });

var app = builder.Build();



app.UseCors(x => x.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin());


app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();
