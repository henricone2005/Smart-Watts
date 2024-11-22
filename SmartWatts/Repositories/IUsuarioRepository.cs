using SmartWatts.Models;
using System.Threading.Tasks;

namespace SmartWatts.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario?> GetUsuarioByEmailAsync(string email);
    }
}
