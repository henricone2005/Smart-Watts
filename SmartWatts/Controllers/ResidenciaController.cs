using Microsoft.AspNetCore.Mvc;
using SmartWatts.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartWatts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidenciaController : ControllerBase
    {
        private readonly IRepository<Residencia> _residenciaRepository;
        private readonly IRepository<ContadeLuz> _contaDeLuzRepository;

        public ResidenciaController(IRepository<Residencia> residenciaRepository, IRepository<ContadeLuz> contaDeLuzRepository)
        {
            _residenciaRepository = residenciaRepository;
            _contaDeLuzRepository = contaDeLuzRepository;
        }

        // POST: api/residencia
        [HttpPost]
        public async Task<ActionResult<Residencia>> PostResidencia([FromBody] Residencia residencia)
        {
            if (residencia == null)
            {
                return BadRequest();
            }

            // Adiciona a residência
            await _residenciaRepository.AddAsync(residencia);
            return CreatedAtAction(nameof(GetResidencia), new { id = residencia.Id }, residencia);
        }

        // GET: api/residencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Residencia>>> GetResidencias()
        {
            var residencias = await _residenciaRepository.GetAllAsync();
            return Ok(residencias);
        }

        // GET: api/residencia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Residencia>> GetResidencia(int id)
        {
            var residencia = await _residenciaRepository.GetByIdAsync(id);
            if (residencia == null)
            {
                return NotFound();
            }
            return Ok(residencia);
        }

        // PUT: api/residencia/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResidencia(int id, [FromBody] Residencia residencia)
        {
            if (id != residencia.Id)
            {
                return BadRequest();
            }

            var existingResidencia = await _residenciaRepository.GetByIdAsync(id);
            if (existingResidencia == null)
            {
                return NotFound();
            }

            await _residenciaRepository.UpdateAsync(residencia);
            return NoContent();
        }

        // DELETE: api/residencia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResidencia(int id)
        {
            var residencia = await _residenciaRepository.GetByIdAsync(id);
            if (residencia == null)
            {
                return NotFound();
            }

            // Remover as contas de luz associadas antes de remover a residência
            var contasDeLuz = await _contaDeLuzRepository.GetAllAsync();
            foreach (var conta in contasDeLuz)
            {
                if (conta.ResidenciaId == id)
                {
                    await _contaDeLuzRepository.DeleteAsync(conta.Id);
                }
            }

            // Remover a residência
            await _residenciaRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
