using ApiLocadora.DataContexts;
using ApiLocadora.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    x.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do banco
var connectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// Configuração do CORS correta
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173") // Frontend local
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Injeção de dependência
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<EmprestimoService>();
builder.Services.AddScoped<EstoqueService>();
builder.Services.AddScoped<FornecedorService>();
builder.Services.AddScoped<FuncionarioService>();
builder.Services.AddScoped<GeneroService>();
builder.Services.AddScoped<LivroService>();

var app = builder.Build();

// Swagger (opcional)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aqui aplica a política CORS correta
app.UseCors("AllowFrontend");

app.UseAuthorization();
app.MapControllers();
app.Run();
