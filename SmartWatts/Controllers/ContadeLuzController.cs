using Microsoft.AspNetCore.Mvc;
using SmartWatts.Data;
using SmartWatts.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartWatts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContadeLuzController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContadeLuzController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ContasDeLuz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContadeLuz>>> GetContasDeLuz()
        {
            return await _context.ContasDeLuz
                .Include(c => c.Residencia)  // Incluir residência associada
                .ToListAsync();
        }

        // GET: api/ContasDeLuz/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContadeLuz>> GetContadeLuz(int id)
        {
            var contadeLuz = await _context.ContasDeLuz
                .Include(c => c.Residencia)  // Incluir residência associada
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contadeLuz == null)
            {
                return NotFound();
            }

            return contadeLuz;
        }

        // POST: api/ContasDeLuz
        [HttpPost]
        public async Task<ActionResult<ContadeLuz>> PostContadeLuz(ContadeLuz contadeLuz)
        {
            _context.ContasDeLuz.Add(contadeLuz);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContadeLuz), new { id = contadeLuz.Id }, contadeLuz);
        }

        // PUT: api/ContasDeLuz/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContadeLuz(int id, ContadeLuz contadeLuz)
        {
            if (id != contadeLuz.Id)
            {
                return BadRequest();
            }

            _context.Entry(contadeLuz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContadeLuzExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ContasDeLuz/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContadeLuz(int id)
        {
            var contadeLuz = await _context.ContasDeLuz.FindAsync(id);
            if (contadeLuz == null)
            {
                return NotFound();
            }

            _context.ContasDeLuz.Remove(contadeLuz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContadeLuzExists(int id)
        {
            return _context.ContasDeLuz.Any(e => e.Id == id);
        }
    }
}
