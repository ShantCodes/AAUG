using System.Reflection;
using AAUG.Api.Middlewares;
using AAUG.DomainModels;
using AAUG.Service.Implementations.ViewModelMapper;
using AutoMapper;


using AAUG.ServiceExtentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDependency(builder.Configuration);
builder.Services.AddSwaggerGen();
MapperConfiguration mapperConfiguration = new MapperConfiguration(mapperConfig => {
    mapperConfig.AddProfile<ViewModelAutoMapper>();
    mapperConfig.AddProfile<MappingProfile>();
});

builder.Services.AddSingleton(mapperConfiguration.CreateMapper());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
