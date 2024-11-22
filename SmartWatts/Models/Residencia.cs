namespace SmartWatts.Models;

public class Residencia
{
     public int Id { get; set; }
    public int QuantidadeMoradores { get; set; }
    public string Endereco { get; set; }
    public decimal ConsumoTotal { get; set; } // Calculado com base nas contas de luz
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public ICollection<ContadeLuz> ContasDeLuz { get; set; } = new List<ContadeLuz>();
}
