using Microsoft.EntityFrameworkCore;
using SmartWatts.Models;

namespace SmartWatts.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ContadeLuz> ContasDeLuz { get; set; }
        public DbSet<Residencia> Residencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração para Residencia -> ContasDeLuz
            modelBuilder.Entity<ContadeLuz>()
                .HasOne(cl => cl.Residencia)  // Uma conta de luz tem uma residência
                .WithMany(r => r.ContasDeLuz)  // Uma residência tem muitas contas de luz
                .HasForeignKey(cl => cl.ResidenciaId)  // Chave estrangeira
                .OnDelete(DeleteBehavior.Cascade); // Quando a residência é excluída, as contas de luz também são excluídas.

            base.OnModelCreating(modelBuilder);
        }
    }
}
