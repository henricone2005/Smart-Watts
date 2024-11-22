using Microsoft.EntityFrameworkCore;
using SmartWatts.Data;
using SmartWatts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartWatts.Repositories
{
    public class ContaDeLuzRepository : Repository<ContadeLuz>, IContaDeLuzRepository
    {
        public ContaDeLuzRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<ContadeLuz>> GetByMesReferenciaAsync(DateTime mesReferencia)
        {
            return await DbContext.ContasDeLuz
                .Where(c => c.MesReferencia.Month == mesReferencia.Month && c.MesReferencia.Year == mesReferencia.Year)
                .ToListAsync();
        }

        public async Task<IEnumerable<ContadeLuz>> GetByResidenciaIdAsync(int residenciaId)
        {
            return await DbContext.ContasDeLuz
                .Where(c => c.ResidenciaId == residenciaId)
                .ToListAsync();
        }
    }
}
