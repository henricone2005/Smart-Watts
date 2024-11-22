using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartWatts.Models;

public class ContadeLuz
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public DateTime MesReferencia { get; set; } // Data não precisa de StringLength

    [Required]
    [Column(TypeName = "decimal(18,2)")] // Define precisão e escala no banco de dados
    public decimal ValorPago { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")] // Configuração para valores decimais
    public decimal ConsumoKwh { get; set; }

    [Required]
    [StringLength(100)] // Aqui é válido porque é uma string
    public string BandeiraTarifaria { get; set; }

    [Required]
    public int UsuarioId { get; set; } // Presumindo que é uma chave estrangeira

    [Required]
    public int ResidenciaId { get; set; }

    [Required]
    public Residencia Residencia { get; set; } // Navegação para a entidade relacionada
}
