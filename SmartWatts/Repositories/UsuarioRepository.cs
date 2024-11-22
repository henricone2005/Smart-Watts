using Microsoft.EntityFrameworkCore;
using SmartWatts.Data;
using SmartWatts.Models;
using System.Threading.Tasks;

namespace SmartWatts.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Usuario?> GetUsuarioByEmailAsync(string email)
        {
            return await DbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
