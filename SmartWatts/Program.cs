using Microsoft.EntityFrameworkCore;
using SmartWatts.Data;
using SmartWatts.Data.Repositories;
using SmartWatts.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurando o DbContext com a string de conexão
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração padrão
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro dos repositórios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IContaDeLuzRepository, ContaDeLuzRepository>();
builder.Services.AddScoped<IResidenciaRepository, ResidenciaRepository>();

var app = builder.Build();

// Configuração do pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
