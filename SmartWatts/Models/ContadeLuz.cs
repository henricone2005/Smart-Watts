namespace SmartWatts.Models;

public class ContadeLuz
{
      public int Id { get; set; }
    public DateTime MesReferencia { get; set; }
    public decimal ValorPago { get; set; }
    public decimal ConsumoKwh { get; set; }
    public string BandeiraTarifaria { get; set; }
    public int ResidenciaId { get; set; }
    public Residencia Residencia { get; set; }
}
