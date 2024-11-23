using Microsoft.EntityFrameworkCore;
using SmartWatts.Models;

namespace SmartWatts.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // DbSets das entidades
    public DbSet<ContadeLuz> ContasDeLuz { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Residencia> Residencias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração para Residencia -> Usuario
        modelBuilder.Entity<Residencia>()
            .HasOne<Usuario>()  // Um usuário tem muitas residências
            .WithMany()  // Relacionamento de um para muitos (sem navegação no lado do usuário)
            .HasForeignKey(r => r.UsuarioId)  // Chave estrangeira no lado da residência
            .OnDelete(DeleteBehavior.Cascade); // Excluir residências quando o usuário for excluído

        // Configuração para ContadeLuz -> Residencia
        modelBuilder.Entity<ContadeLuz>()
            .HasOne<Residencia>()  // Uma conta de luz tem uma residência
            .WithMany(r => r.ContasDeLuz)  // Uma residência tem muitas contas de luz
            .HasForeignKey(cl => cl.ResidenciaId)  // Chave estrangeira no lado da conta de luz
            .OnDelete(DeleteBehavior.Cascade); // Excluir contas de luz quando a residência for excluída

        base.OnModelCreating(modelBuilder);
    }
}
