using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWatts.Data;
using SmartWatts.Models;

namespace SmartWatts.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UsuarioController(ApplicationDbContext context)
    {
        _context = context;
    }

    // DTOs
    public class CreateUsuarioDto
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        public List<int> ResidenciaIds { get; set; } = new List<int>();
    }

    public class UpdateUsuarioDto
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<int> ResidenciaIds { get; set; } = new List<int>();
    }

    // Endpoints
    [HttpPost]
    public async Task<IActionResult> CreateUsuario([FromBody] CreateUsuarioDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email
        };

        if (dto.ResidenciaIds.Any())
        {
            var residencias = await _context.Residencias
                .Where(r => dto.ResidenciaIds.Contains(r.Id))
                .ToListAsync();

            if (residencias.Count != dto.ResidenciaIds.Count)
            {
                return BadRequest("Um ou mais IDs de residência são inválidos.");
            }

            foreach (var residencia in residencias)
            {
                usuario.Residencias.Add(residencia);
            }
        }

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.Id }, usuario);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
    {
        var usuarios = await _context.Usuarios
            .Include(u => u.Residencias)
            .Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                ResidenciaIds = u.Residencias.Select(r => r.Id).ToList()
            })
            .ToListAsync();

        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioDto>> GetUsuarioById(int id)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Residencias)
            .Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                ResidenciaIds = u.Residencias.Select(r => r.Id).ToList()
            })
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null)
        {
            return NotFound($"Usuário com ID {id} não encontrado.");
        }

        return Ok(usuario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UpdateUsuarioDto dto)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return NotFound($"Usuário com ID {id} não encontrado.");
        }

        usuario.Nome = dto.Nome;
        usuario.Email = dto.Email;

        _context.Entry(usuario).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return NotFound($"Usuário com ID {id} não encontrado.");
        }

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
