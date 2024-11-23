using SmartWatts.Models;

public class Residencia
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public ICollection<ContadeLuz> ContasDeLuz { get; set; } = new List<ContadeLuz>(); // Certifique-se que o nome está correto
}
