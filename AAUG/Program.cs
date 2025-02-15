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
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using AAUG.DomainModels.Dtos.Email;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateConverter());
});
// .AddJsonOptions(options =>
// {
//     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
// });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDependency(builder.Configuration);

builder.Services.AddIdentityCore<IdentityUser>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredUniqueChars = 0;
    })
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
builder.Services.AddHttpContextAccessor();

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

app.UseCors("AllowAll");

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();



app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TokenMiddleware>();

app.MapControllers();

// app.UseSpa(spa =>
// {
//     spa.Options.SourcePath = "wwwroot";
//     if (app.Environment.IsDevelopment())
//     {
//         spa.UseReactDevelopmentServer(npmScript: "start");
//     }
// });

app.Run();
