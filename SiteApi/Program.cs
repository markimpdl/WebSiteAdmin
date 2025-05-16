using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SiteApi.Controllers;
using SiteApi.Data;
using SiteApi.Models;
using SiteApi.Repositories;
using SiteApi.Services;

var builder = WebApplication.CreateBuilder(args);
//Postgres Configuration
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Identity Configuration
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApiDbContext>()
    .AddDefaultTokenProviders();

//JWT configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(); // necessário também

builder.Services.AddControllers()
    .AddApplicationPart(typeof(AdminController).Assembly); // 👈 força carregar o controller

//Services and repositories
// Repositórios e Serviços
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<IImageUploadService, ImageUploadService>();
builder.Services.AddScoped<IHomeService, HomeService>(); // novo serviço agregado

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Site API",
        Version = "v1"
    });
   

    // Corrige suporte a IFormFile no Swagger
    options.OperationFilter<FileUploadOperationFilter>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
