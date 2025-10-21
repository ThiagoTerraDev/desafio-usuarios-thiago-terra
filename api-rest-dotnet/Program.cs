using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using api_rest_dotnet.DataContext;
using api_rest_dotnet.Services.UserService;
using api_rest_dotnet.Services.PasswordService;
using api_rest_dotnet.Services.TokenService;
using api_rest_dotnet.Services.AuthService;

var builder = WebApplication.CreateBuilder(args);

// Configura serialização JSON para camelCase (padrão JavaScript/TypeScript)
builder.Services.AddControllers()
  .AddJsonOptions(options =>
  {
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
  });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo 
  { 
    Title = "User Management API", 
    Version = "v1",
    Description = "API para gerenciamento de usuários"
  });

  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Description = "JWT Authorization header usando o esquema Bearer. \r\n\r\n " +
                  "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                  "Exemplo: 'Bearer 12345abcdef'",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer"
  });

  c.AddSecurityRequirement(new OpenApiSecurityRequirement()
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        },
        Scheme = "oauth2",
        Name = "Bearer",
        In = ParameterLocation.Header,
      },
      new List<string>()
    }
  });
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
        .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
      ValidateIssuer = false,
      ValidateAudience = false
    };
  });

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAngularApp", policy =>
  {
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
      .AllowAnyHeader()
      .AllowAnyMethod();
  });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// HTTPS Redirection: Descomente a linha abaixo se quiser forçar HTTPS
// Lembre-se de executar: dotnet dev-certs https --trust
// E alterar os environments do Angular para usar https://localhost:7183/api
// app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");

// Middlewares de autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
