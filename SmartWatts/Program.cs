using Microsoft.EntityFrameworkCore;
using SmartWatts.Data;
using SmartWatts.Models;
using SmartWatts.Repositories;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Registra o DbContext com a conexão do banco de dados
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Registra os repositórios genéricos (se necessário, pode adicionar mais repositórios)
        builder.Services.AddScoped<IRepository<Usuario>, Repository<Usuario>>();  // Exemplo de repositório genérico para Usuario
        builder.Services.AddScoped<IRepository<Residencia>, Repository<Residencia>>();  // Exemplo para Residência
        builder.Services.AddScoped<IRepository<ContadeLuz>, Repository<ContadeLuz>>();  // Exemplo para ContaDeLuz

        // Registra os controllers da API
        builder.Services.AddControllers();

        // Configuração do Swagger
        builder.Services.AddSwaggerGen();  // Adiciona o Swagger no projeto

        var app = builder.Build();

        // Ativa o Swagger para a aplicação
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        });

        // Usar HTTPS e redirecionamento
        app.UseHttpsRedirection();

        // Mapear os controllers
        app.MapControllers();

        app.Run();
    }
}
