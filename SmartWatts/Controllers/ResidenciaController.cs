using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWatts.Data;
using SmartWatts.Models;

namespace SmartWatts.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResidenciaController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ResidenciaController(ApplicationDbContext context)
    {
        _context = context;
    }

    // DTOs
    public class CreateResidenciaDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade de moradores deve ser pelo menos 1.")]
        public int QuantidadeMoradores { get; set; }

        [Required]
        [StringLength(200)]
        public string Endereco { get; set; }

        [Required]
        public int UsuarioId { get; set; }
    }

    public class UpdateResidenciaDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade de moradores deve ser pelo menos 1.")]
        public int QuantidadeMoradores { get; set; }

        [Required]
        [StringLength(200)]
        public string Endereco { get; set; }
    }

    public class ResidenciaDto
    {
        public int Id { get; set; }
        public int QuantidadeMoradores { get; set; }
        public string Endereco { get; set; }
        public decimal ConsumoTotal { get; set; }
        public int UsuarioId { get; set; }
    }

    // Endpoints
    [HttpPost]
    public async Task<IActionResult> CreateResidencia([FromBody] CreateResidenciaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
        if (usuario == null)
        {
            return NotFound($"Usuário com ID {dto.UsuarioId} não encontrado.");
        }

        var residencia = new Residencia
        {
            QuantidadeMoradores = dto.QuantidadeMoradores,
            Endereco = dto.Endereco,
            UsuarioId = dto.UsuarioId,
            Usuario = usuario
        };

        _context.Residencias.Add(residencia);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetResidenciaById), new { id = residencia.Id }, residencia);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResidenciaDto>>> GetResidencias()
    {
        var residencias = await _context.Residencias
            .Include(r => r.ContasDeLuz)
            .Select(r => new ResidenciaDto
            {
                Id = r.Id,
                QuantidadeMoradores = r.QuantidadeMoradores,
                Endereco = r.Endereco,
                ConsumoTotal = r.ContasDeLuz.Sum(c => c.ConsumoKwh),
                UsuarioId = r.UsuarioId
            })
            .ToListAsync();

        return Ok(residencias);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResidenciaDto>> GetResidenciaById(int id)
    {
        var residencia = await _context.Residencias
            .Include(r => r.ContasDeLuz)
            .Select(r => new ResidenciaDto
            {
                Id = r.Id,
                QuantidadeMoradores = r.QuantidadeMoradores,
                Endereco = r.Endereco,
                ConsumoTotal = r.ContasDeLuz.Sum(c => c.ConsumoKwh),
                UsuarioId = r.UsuarioId
            })
            .FirstOrDefaultAsync(r => r.Id == id);

        if (residencia == null)
        {
            return NotFound($"Residência com ID {id} não encontrada.");
        }

        return Ok(residencia);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateResidencia(int id, [FromBody] UpdateResidenciaDto dto)
    {
        var residencia = await _context.Residencias.FindAsync(id);
        if (residencia == null)
        {
            return NotFound($"Residência com ID {id} não encontrada.");
        }

        residencia.QuantidadeMoradores = dto.QuantidadeMoradores;
        residencia.Endereco = dto.Endereco;

        _context.Entry(residencia).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteResidencia(int id)
    {
        var residencia = await _context.Residencias
            .Include(r => r.ContasDeLuz)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (residencia == null)
        {
            return NotFound($"Residência com ID {id} não encontrada.");
        }

        if (residencia.ContasDeLuz.Any())
        {
            return BadRequest("Não é possível excluir uma residência com contas de luz associadas.");
        }

        _context.Residencias.Remove(residencia);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
