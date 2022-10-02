using CachacasCanuto.Application.Common.Interfaces;
using CachacasCanuto.Application.Common;
using CachacasCanuto.Application.Services.Interfaces;
using CachacasCanuto.Application.Services;
using CachacasCanuto.Infrastructure.ExternalResources.HttpServices.Interfaces;
using CachacasCanuto.Infrastructure.ExternalResources.HttpServices;
using Microsoft.OpenApi.Models;
using System.Reflection;
using AutoMapper;
using CachacasCanuto.Application.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMessageHandler, MessageHandler>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IProductHttpService, ProductHttpService>();
builder.Services.AddScoped<ICustomerHttpService, CustomerHttpService>();
builder.Services.AddScoped<ISaleHttpService, SaleHttpService>();
builder.Services.AddScoped<IClientHttpService, ClientHttpService>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Cachaças Canuto",
                        Version = "v1",
                        Description = "API desenvolvida para gerenciamento de um negócio de chacaças",
                        Contact = new OpenApiContact
                        {
                            Name = "Pablo Souza",
                            Url = new Uri("https://github.com/souzapablo")
                        }
                    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
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
