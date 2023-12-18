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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AvenSellApi", Version = "v1" });
});

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(new JsonFormatter(), "log.json", shared: true)
    .CreateLogger();
builder.Services.AddHttpContextAccessor();//paytr için eklendi
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
            .WithOrigins("http://188.132.247.40", "https://www.kadimgross.com.tr", "http://78.188.223.33", "http://192.168.1.195:3000", "http://localhost:3000", "http://85.107.90.169:3000", "http://192.168.1.108:3000", "http://188.132.247.54:3000", "http://192.168.1.195:3000", "http://localhost:3000", "http://85.107.90.169:3000", "http://192.168.1.108:3000", "http://78.188.223.33:3000", "http://188.132.247.101:3000"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AvenSellApi v1");
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("https://kadimgross.com.tr/avensellservice/swagger/v1/swagger.json", "AvenSellApi v1");
    });


}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();
