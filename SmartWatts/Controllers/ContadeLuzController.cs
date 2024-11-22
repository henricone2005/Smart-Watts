using Microsoft.AspNetCore.Mvc;
using SmartWatts.Models;
using SmartWatts.Repositories;

namespace SmartWatts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContadeLuzController : ControllerBase
    {
        private readonly IContaDeLuzRepository _contaDeLuzRepository;

        public ContadeLuzController(IContaDeLuzRepository contaDeLuzRepository)
        {
            _contaDeLuzRepository = contaDeLuzRepository;
        }

        // Criar uma nova conta de luz
        [HttpPost]
        public async Task<IActionResult> CreateContaDeLuz([FromBody] ContadeLuz contaDeLuz)
        {
            if (contaDeLuz == null)
                return BadRequest("Conta de luz não pode ser nula.");

            await _contaDeLuzRepository.AddAsync(contaDeLuz);
            return CreatedAtAction(nameof(GetContaDeLuzById), new { id = contaDeLuz.Id }, contaDeLuz);
        }

        // Obter uma conta de luz pelo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContaDeLuzById(int id)
        {
            var contaDeLuz = await _contaDeLuzRepository.GetByIdAsync(id);

            if (contaDeLuz == null)
                return NotFound($"Conta de luz com ID {id} não encontrada.");

            return Ok(contaDeLuz);
        }

        // Obter todas as contas de luz
        [HttpGet]
        public async Task<IActionResult> GetAllContasDeLuz()
        {
            var contasDeLuz = await _contaDeLuzRepository.GetAllAsync();
            return Ok(contasDeLuz);
        }

        // Atualizar uma conta de luz
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContaDeLuz(int id, [FromBody] ContadeLuz contaDeLuz)
        {
            if (contaDeLuz == null || id != contaDeLuz.Id)
                return BadRequest("Dados inválidos.");

            var contaDeLuzExistente = await _contaDeLuzRepository.GetByIdAsync(id);

            if (contaDeLuzExistente == null)
                return NotFound($"Conta de luz com ID {id} não encontrada.");

            await _contaDeLuzRepository.UpdateAsync(contaDeLuz);
            return NoContent();
        }

        // Deletar uma conta de luz
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContaDeLuz(int id)
        {
            var contaDeLuz = await _contaDeLuzRepository.GetByIdAsync(id);

            if (contaDeLuz == null)
                return NotFound($"Conta de luz com ID {id} não encontrada.");

            await _contaDeLuzRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
