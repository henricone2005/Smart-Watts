using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWatts.Data;
using SmartWatts.Models;

namespace SmartWatts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidenciasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ResidenciasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Residencias
        [HttpPost]
        public async Task<IActionResult> PostResidencia([FromBody] Residencia residencia)
        {
            if (residencia.UsuarioId == 0)
            {
                return BadRequest("O campo 'UsuarioId' é obrigatório.");
            }

            // Verificar se o usuário existe
            var usuario = await _context.Usuarios.FindAsync(residencia.UsuarioId);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Adiciona a residência no banco de dados
            _context.Residencias.Add(residencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetResidenciaById), new { id = residencia.Id }, residencia);
        }

        // GET: api/Residencias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Residencia>> GetResidenciaById(int id)
        {
            var residencia = await _context.Residencias
                .FirstOrDefaultAsync(r => r.Id == id);

            if (residencia == null)
            {
                return NotFound();
            }

            return residencia;
        }

        // GET: api/Residencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Residencia>>> GetResidencias()
        {
            return await _context.Residencias.ToListAsync();
        }

        // DELETE: api/Residencias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResidencia(int id)
        {
            var residencia = await _context.Residencias.FindAsync(id);
            if (residencia == null)
            {
                return NotFound();
            }

            _context.Residencias.Remove(residencia);
            await _context.SaveChangesAsync();

            return NoContent();  // Retorna 204 No Content após a exclusão
        }
    }
}
