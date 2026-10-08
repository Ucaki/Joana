using System.Text;
using Joana.API.Middleware;
using Joana.Application.Interfaces;
using Joana.Application.Services;
using Joana.Infrastructure.Data;
using Joana.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

const string BlazorClientCorsPolicy = "BlazorClient";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<JoanaDbContext>(options =>options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Joana.Web radi na http://localhost:5076
builder.Services.AddCors(options =>
{
    options.AddPolicy(BlazorClientCorsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:5076")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//Repos
builder.Services.AddScoped<IProizvodRepository, ProizvodRepository>();
builder.Services.AddScoped<IKupacRepository, KupacRepository>();
builder.Services.AddScoped<IAdministratorRepository, AdministratorRepository>();
builder.Services.AddScoped<ICenaProizvodaRepository, CenaProizvodaRepository>();
builder.Services.AddScoped<IKategorijaRepository, KategorijaRepository>();
builder.Services.AddScoped<INarudzbenicaRepository, NarudzbenicaRepository>();
builder.Services.AddScoped<IObradaNarudzRepository, ObradaNarudzRepository>();
builder.Services.AddScoped<IStatusNarudzbeniceRepository, StatusNarudzbeniceRepository>();
builder.Services.AddScoped<IStavkaNarudzRepository, StavkaNarudzRepository>();

//Service
builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<ICenaProizvodaService, CenaProizvodaService>();
builder.Services.AddScoped<IKategorijaService, KategorijaService>();
builder.Services.AddScoped<IKupacService, KupacService>();
builder.Services.AddScoped<INarudzbenicaService, NarudzbenicaService>();
builder.Services.AddScoped<IObradaNarudzbeniceService, ObradaNarudzbeniceService>();
builder.Services.AddScoped<IProizvodService, ProizvodService>();
builder.Services.AddScoped<IStatusNarudzbeniceService, StatusNarudzbeniceService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
//mojsije

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();


var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSection["Key"] ?? throw new InvalidOperationException("Jwt:Key nije konfigurisan.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            RoleClaimType = "role"
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Joana API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Unesite JWT token dobijen od /api/auth/login (bez 'Bearer ' prefiksa)."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors(BlazorClientCorsPolicy);

app.UseAuthentication(); //jwt
app.UseAuthorization();
app.MapControllers();
app.Run();
