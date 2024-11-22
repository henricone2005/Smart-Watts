using Microsoft.AspNetCore.Mvc;
using SmartWatts.Data.Repositories;
using SmartWatts.Models;

namespace SmartWatts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidenciasController : ControllerBase
    {
        private readonly IResidenciaRepository _residenciaRepository;

        public ResidenciasController(IResidenciaRepository residenciaRepository)
        {
            _residenciaRepository = residenciaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetResidencias()
        {
            var residencias = await _residenciaRepository.GetAllAsync();
            return Ok(residencias);
        }

        [HttpGet("{usuarioId}/usuario")]
        public async Task<IActionResult> GetResidenciasByUsuarioId(int usuarioId)
        {
            var residencias = await _residenciaRepository.GetResidenciasByUsuarioIdAsync(usuarioId);
            return Ok(residencias);
        }

        [HttpPost]
        public async Task<IActionResult> CreateResidencia([FromBody] Residencia residencia)
        {
            await _residenciaRepository.AddAsync(residencia);
            return CreatedAtAction(nameof(GetResidencias), new { id = residencia.Id }, residencia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResidencia(int id, [FromBody] Residencia residencia)
        {
            if (id != residencia.Id)
            {
                return BadRequest();
            }

            await _residenciaRepository.UpdateAsync(residencia);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResidencia(int id)
        {
            await _residenciaRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
