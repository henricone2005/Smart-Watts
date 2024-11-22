using SmartWatts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartWatts.Repositories
{
    public interface IContaDeLuzRepository : IRepository<ContadeLuz>
    {
        Task<IEnumerable<ContadeLuz>> GetByMesReferenciaAsync(DateTime mesReferencia);
        Task<IEnumerable<ContadeLuz>> GetByResidenciaIdAsync(int residenciaId);
    }
}
