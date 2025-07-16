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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API BiblioTech",  // Título que você deseja
        Version = "v1",
        Description = "API para sistema de biblioteca"
    });
});


// Configura��o do banco
var connectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// Configura��o do CORS correta
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

// Inje��o de depend�ncia
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

// Aqui aplica a pol�tica CORS correta
app.UseCors("AllowFrontend");

app.UseAuthorization();
app.MapControllers();
app.Run();
