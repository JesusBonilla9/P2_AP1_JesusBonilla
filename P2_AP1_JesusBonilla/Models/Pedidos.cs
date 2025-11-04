using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2_AP1_JesusBonilla.Models;

public class Pedidos
{
    [Key]
    public int PedidoId { get; set; }
    [Required(ErrorMessage = "La fecha es obligatoria.")]
    public DateTime Fecha { get; set; }
    [Required(ErrorMessage = "El Nombre del cliente es obligatorio")]
    [MaxLength(70)]
    public string NombreCliente { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public double Importe { get; set; }
    [ForeignKey("PedidoId")]
    public virtual ICollection<PedidosDetalle> Detalles { get; set; } = new List<PedidosDetalle>();
}

