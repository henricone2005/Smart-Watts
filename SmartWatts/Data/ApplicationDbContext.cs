using Microsoft.EntityFrameworkCore;
using SmartWatts.Models;

namespace SmartWatts.Data;

public class ApplicationDbContext : DbContext
{

 public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

 public DbSet<ContadeLuz>  ContasDeLuz {get; set;}

  public DbSet<Usuario>  Usuarios {get; set;}

   public DbSet<Residencia>  Residencias {get; set;}



}
