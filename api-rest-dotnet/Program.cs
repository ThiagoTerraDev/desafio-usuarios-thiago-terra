using Microsoft.EntityFrameworkCore;
using api_rest_dotnet.DataContext;
using api_rest_dotnet.Service.UserService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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

app.MapControllers();

app.Run();
