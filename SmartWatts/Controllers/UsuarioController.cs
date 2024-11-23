using Microsoft.AspNetCore.Mvc;
using SmartWatts.Models;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IRepository<Usuario> _usuarioRepository;

    public UsuarioController(IRepository<Usuario> usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    // GET: api/usuario
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        return Ok(usuarios);
    }

    // POST: api/usuario
    [HttpPost]
    public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
    {
        await _usuarioRepository.AddAsync(usuario);
        return CreatedAtAction(nameof(GetUsuarios), new { id = usuario.Id }, usuario);
    }

    // PUT: api/usuario/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
    {
        if (id != usuario.Id)
        {
            return BadRequest();
        }

        await _usuarioRepository.UpdateAsync(usuario);

        return NoContent();
    }

    // DELETE: api/usuario/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        await _usuarioRepository.DeleteAsync(id);
        return NoContent();
    }
}
