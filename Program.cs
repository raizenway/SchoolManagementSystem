using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Configurations;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using SchoolManagementSystem.Modules.Users.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// 🔒 Load .env
DotNetEnv.Env.Load();
builder.Configuration["JWT_KEY"] = Environment.GetEnvironmentVariable("JWT_KEY");

// 🔑 Ambil secret key dari .env
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? "supersecurekey123456789!";

// 🔌 Koneksi ke PostgreSQL
var connStr = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
              $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
              $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
              $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
              $"Password={Environment.GetEnvironmentVariable("DB_PASS")};" +
              $"SslMode=Require;Trust Server Certificate=true";

// 💾 Registrasi DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connStr));

// 👤 Tambahkan Identity + Role support
builder.Services.AddIdentity<AppUser, IdentityRole<int>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// 📦 Tambahkan layanan MVC dan Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "SchoolManagementSystem API", Version = "v1" });

    // Tambah dukungan JWT
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Masukkan token JWT dengan format: Bearer {token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// 🚀 Bangun aplikasi
var app = builder.Build();

// 🧭 Middleware pipeline
app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

    string[] roles = new[] { "Admin", "Teacher", "Student" };

    foreach (var role in roles)
    {
        var exists = await roleManager.RoleExistsAsync(role);
        if (!exists)
            await roleManager.CreateAsync(new IdentityRole<int>(role));
    }
}

app.UseAuthentication(); // ⬅️ Harus sebelum UseAuthorization
app.UseAuthorization();

app.MapControllers();
app.Run();
