using SmartWatts.Models;
using SmartWatts.Repositories;

namespace SmartWatts.Data.Repositories
{
    public interface IResidenciaRepository : IRepository<Residencia>
    {
        Task<IEnumerable<Residencia>> GetResidenciasByUsuarioIdAsync(int usuarioId);
    }
}
