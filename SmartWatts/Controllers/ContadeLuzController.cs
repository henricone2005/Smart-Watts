using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWatts.Data;
using SmartWatts.Models;

namespace SmartWatts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContasdeLuzController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContasdeLuzController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/ContasDeLuz
        [HttpPost]
        public async Task<IActionResult> PostContaDeLuz([FromBody] ContadeLuz contaDeLuz)
        {
            // Verificar se a residência existe
            var residencia = await _context.Residencias.FindAsync(contaDeLuz.ResidenciaId);
            if (residencia == null)
            {
                return NotFound("Residência não encontrada.");
            }

            // Adiciona a conta de luz no banco de dados
            _context.ContasDeLuz.Add(contaDeLuz);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContaDeLuzById), new { id = contaDeLuz.Id }, contaDeLuz);
        }

        // GET: api/ContasDeLuz/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContadeLuz>> GetContaDeLuzById(int id)
        {
            var contaDeLuz = await _context.ContasDeLuz
                .FirstOrDefaultAsync(cl => cl.Id == id);

            if (contaDeLuz == null)
            {
                return NotFound();
            }

            return contaDeLuz;
        }

        // GET: api/ContasDeLuz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContadeLuz>>> GetContasDeLuz()
        {
            return await _context.ContasDeLuz.ToListAsync();
        }

        // DELETE: api/ContasDeLuz/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContaDeLuz(int id)
        {
            var contaDeLuz = await _context.ContasDeLuz.FindAsync(id);
            if (contaDeLuz == null)
            {
                return NotFound();
            }

            _context.ContasDeLuz.Remove(contaDeLuz);
            await _context.SaveChangesAsync();

            return NoContent();  // Retorna 204 No Content após a exclusão
        }
    }
}
