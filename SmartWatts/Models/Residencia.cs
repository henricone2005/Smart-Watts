using SmartWatts.Models;

public class Residencia
{
    public int Id { get; set; }
    public string Endereco { get; set; }

    // Lista de contas de luz associadas à residência
    public ICollection<ContadeLuz> ContasDeLuz { get; set; } = new List<ContadeLuz>();
}
