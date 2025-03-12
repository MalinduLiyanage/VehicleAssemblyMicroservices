using AdminApi.Attributes;
using AdminApi;
using AdminApi.Services.ValidationService;
using AdminApi.Utilities.EmailServiceUtility;
using AdminApi.Utilities.PasswordHashUtility;
using Microsoft.EntityFrameworkCore;
using AdminApi.Services.AdminService;

var builder = WebApplication.CreateBuilder(args);

var server = Environment.GetEnvironmentVariable("DB_SERVER");
var port = Environment.GetEnvironmentVariable("DB_PORT");
var database = Environment.GetEnvironmentVariable("DB_NAME");
var user = Environment.GetEnvironmentVariable("DB_USER");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
var connectionString = $"Server={server};Port={port};Database={database};User={user};Password={password};";

GlobalAttributes.mySQLConfig.connectionString = connectionString;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add services to the container.
builder.Services.AddScoped<ICreateAdminValidationService, CreateAdminValidationService>();
builder.Services.AddScoped<IPasswordHashUtilityService, PasswordHashUtilityService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
