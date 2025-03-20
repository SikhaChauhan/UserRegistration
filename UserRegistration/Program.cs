using BusinessLayer;
using BusinessLayer.Interface;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//JWT Token
var key = Encoding.ASCII.GetBytes("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
        };
    });

// Add services to the container
builder.Services.AddControllers();

// Dependency Injection for Business and Repository Layers
builder.Services.AddScoped<IUserRergistrationBL, UserRegistrationBL>();
builder.Services.AddScoped<IUserRegistrationRL, UserRegistrationRL>();

// Database Configuration
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Database connection string is missing.");
}

builder.Services.AddDbContext<RegistrationAppContext>(options =>
    options.UseSqlServer(connectionString));

// Swagger Setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Global Exception Handling Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

// Enable Swagger in Development Mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware Pipeline
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
