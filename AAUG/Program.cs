using System.Reflection;
using AAUG.Api.Middlewares;
using AAUG.DomainModels;
using AAUG.Service.Implementations.ViewModelMapper;
using AutoMapper;


using AAUG.ServiceExtentions;
using Microsoft.AspNetCore.Identity;
using AAUG.Context.Context;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using AAUG.DomainModels.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AAUG.Api.ServiceExtensions;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
    // .AddJsonOptions(options =>
    // {
    //     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    // });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDependency(builder.Configuration);

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });
    x.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var jwtSettings = builder.Configuration.GetSection("Jwt");
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(jwtSettings["Key"]!)),
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true
    };
});


MapperConfiguration mapperConfiguration = new MapperConfiguration(mapperConfig =>
{
    mapperConfig.AddProfile<ViewModelAutoMapper>();
    mapperConfig.AddProfile<MappingProfile>();
});

builder.Services.AddSingleton(mapperConfiguration.CreateMapper());

var app = builder.Build();

// app.MapIdentityApi<IdentityUser>();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = AaugRoles.GetAllRoles();

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}
builder.Services.AddHttpContextAccessor();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TokenMiddleware>();

app.MapControllers();

app.Run();
