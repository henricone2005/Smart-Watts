using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWatts.Data;
using SmartWatts.Models;

namespace SmartWatts.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContadeLuzController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ContadeLuzController(ApplicationDbContext context)
    {
        _context = context;
    }

    // DTOs
    public class CreateContaDeLuzDto
    {
        [Required]
        public DateTime MesReferencia { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor pago deve ser maior que zero.")]
        public decimal ValorPago { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O consumo em kWh deve ser maior que zero.")]
        public decimal ConsumoKwh { get; set; }

        [Required]
        [StringLength(100)]
        public string BandeiraTarifaria { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int ResidenciaId { get; set; }
    }

    public class ContaDeLuzDto
    {
        public int Id { get; set; }
        public DateTime MesReferencia { get; set; }
        public decimal ValorPago { get; set; }
        public decimal ConsumoKwh { get; set; }
        public string BandeiraTarifaria { get; set; }
        public int UsuarioId { get; set; }
        public int ResidenciaId { get; set; }
    }

    // Endpoints
    [HttpPost]
    public async Task<IActionResult> CreateContaDeLuz([FromBody] CreateContaDeLuzDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Verificar se a residência e o usuário existem
        var residencia = await _context.Residencias.FindAsync(dto.ResidenciaId);
        if (residencia == null)
        {
            return NotFound($"Residência com ID {dto.ResidenciaId} não encontrada.");
        }

        var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
        if (usuario == null)
        {
            return NotFound($"Usuário com ID {dto.UsuarioId} não encontrado.");
        }

        var contaDeLuz = new ContadeLuz
        {
            MesReferencia = dto.MesReferencia,
            ValorPago = dto.ValorPago,
            ConsumoKwh = dto.ConsumoKwh,
            BandeiraTarifaria = dto.BandeiraTarifaria,
            UsuarioId = dto.UsuarioId,
            ResidenciaId = dto.ResidenciaId,
            Residencia = residencia
        };

        _context.ContasDeLuz.Add(contaDeLuz);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetContaDeLuzById), new { id = contaDeLuz.Id }, contaDeLuz);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContaDeLuzDto>>> GetContasDeLuz()
    {
        var contas = await _context.ContasDeLuz
            .Include(c => c.Residencia)
            .Select(c => new ContaDeLuzDto
            {
                Id = c.Id,
                MesReferencia = c.MesReferencia,
                ValorPago = c.ValorPago,
                ConsumoKwh = c.ConsumoKwh,
                BandeiraTarifaria = c.BandeiraTarifaria,
                UsuarioId = c.UsuarioId,
                ResidenciaId = c.ResidenciaId
            })
            .ToListAsync();

        return Ok(contas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContaDeLuzDto>> GetContaDeLuzById(int id)
    {
        var conta = await _context.ContasDeLuz
            .Include(c => c.Residencia)
            .Select(c => new ContaDeLuzDto
            {
                Id = c.Id,
                MesReferencia = c.MesReferencia,
                ValorPago = c.ValorPago,
                ConsumoKwh = c.ConsumoKwh,
                BandeiraTarifaria = c.BandeiraTarifaria,
                UsuarioId = c.UsuarioId,
                ResidenciaId = c.ResidenciaId
            })
            .FirstOrDefaultAsync(c => c.Id == id);

        if (conta == null)
        {
            return NotFound($"Conta de luz com ID {id} não encontrada.");
        }

        return Ok(conta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContaDeLuz(int id, [FromBody] CreateContaDeLuzDto dto)
    {
        var conta = await _context.ContasDeLuz.FindAsync(id);
        if (conta == null)
        {
            return NotFound($"Conta de luz com ID {id} não encontrada.");
        }

        // Atualizar os campos
        conta.MesReferencia = dto.MesReferencia;
        conta.ValorPago = dto.ValorPago;
        conta.ConsumoKwh = dto.ConsumoKwh;
        conta.BandeiraTarifaria = dto.BandeiraTarifaria;

        // Verificar residência e usuário
        var residencia = await _context.Residencias.FindAsync(dto.ResidenciaId);
        if (residencia == null)
        {
            return NotFound($"Residência com ID {dto.ResidenciaId} não encontrada.");
        }

        var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
        if (usuario == null)
        {
            return NotFound($"Usuário com ID {dto.UsuarioId} não encontrado.");
        }

        conta.UsuarioId = dto.UsuarioId;
        conta.ResidenciaId = dto.ResidenciaId;
        conta.Residencia = residencia;

        _context.Entry(conta).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContaDeLuz(int id)
    {
        var conta = await _context.ContasDeLuz.FindAsync(id);
        if (conta == null)
        {
            return NotFound($"Conta de luz com ID {id} não encontrada.");
        }

        _context.ContasDeLuz.Remove(conta);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
