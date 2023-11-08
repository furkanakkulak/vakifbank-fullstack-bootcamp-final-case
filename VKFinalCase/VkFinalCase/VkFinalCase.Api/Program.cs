using System.Reflection;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using StackExchange.Redis;
using VkFinalCase.Api.Middleware;
using VkFinalCase.Base.Logger;
using VkFinalCase.Base.Token;
using VkFinalCase.Data.Context;
using VkFinalCase.Data.Uow;
using VkFinalCase.Operation.Cqrs;
using VkFinalCase.Operation.Mapper;
using VkFinalCase.Operation.Validation;




var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSerilog();
builder.Services.AddCors(o => o.AddPolicy("AllowAllOrigins", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

// Add services to the container.
string connection = builder.Configuration.GetConnectionString("MsSqlConnection");
builder.Services.AddDbContext<VkDbContext>(opts => opts.UseSqlServer(connection));

var JwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(typeof(CreateUserCommand).GetTypeInfo().Assembly);

var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperConfig()); });
builder.Services.AddSingleton(config.CreateMapper());

builder.Services.AddControllers().AddFluentValidation(x =>
{
    x.RegisterValidatorsFromAssemblyContaining<BaseValidator>();
});
builder.Services.AddMemoryCache();


builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var redisConfig = new ConfigurationOptions();
redisConfig.EndPoints.Add(builder.Configuration["Redis:Host"], Convert.ToInt32(builder.Configuration["Redis:Port"]));
redisConfig.DefaultDatabase = 0;
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.ConfigurationOptions = redisConfig;
    opt.InstanceName = builder.Configuration["Redis:InstanceName"];
});

builder.Services.AddControllersWithViews(options =>
    options.CacheProfiles.Add("Cache100", new CacheProfile
    {
        Duration = 100,
        Location = ResponseCacheLocation.Any,
    }));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "VkApi Management for IT Company",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new string[] { } }
    });
});

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = true;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = JwtConfig.Issuer,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtConfig.Secret)),
            ValidAudience = JwtConfig.Audience,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(2)
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAllOrigins"); 


app.UseMiddleware<ErrorHandlerMiddleware>();
Action<RequestProfilerModel> requestResponseHandler = requestProfilerModel =>
{
    Log.Information("<-------------#  Request-Begin   #------------->");
    Log.Information(requestProfilerModel.Request);
    Log.Information(Environment.NewLine);
    Log.Information(requestProfilerModel.Response);
    Log.Information("<-------------#   Request-End    #------------->");
};
app.UseMiddleware<RequestLoggingMiddleware>(requestResponseHandler);


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();