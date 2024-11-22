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
            .HasOne(r => r.Usuario)
            .WithMany(u => u.Residencias)
            .HasForeignKey(r => r.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade); // Cascata: ao excluir o usuário, remove as residências

        // Configuração para ContadeLuz -> Residencia
        modelBuilder.Entity<ContadeLuz>()
            .HasOne(cl => cl.Residencia)
            .WithMany(r => r.ContasDeLuz)
            .HasForeignKey(cl => cl.ResidenciaId)
            .OnDelete(DeleteBehavior.Cascade); // Cascata: ao excluir a residência, remove as contas de luz

        base.OnModelCreating(modelBuilder);
    }
}
