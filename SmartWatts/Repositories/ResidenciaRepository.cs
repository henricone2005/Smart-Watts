using Microsoft.EntityFrameworkCore;
using SmartWatts.Models;
using SmartWatts.Repositories;

namespace SmartWatts.Data.Repositories
{
    public class ResidenciaRepository : Repository<Residencia>, IResidenciaRepository
    {
        public ResidenciaRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Residencia>> GetResidenciasByUsuarioIdAsync(int usuarioId)
        {
            return await DbContext.Residencias
                .Where(r => r.UsuarioId == usuarioId)
                .ToListAsync();
        }
    }
}
