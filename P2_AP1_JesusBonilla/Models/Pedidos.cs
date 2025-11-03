using System.ComponentModel.DataAnnotations;

namespace P2_AP1_JesusBonilla.Models
{
    public class Pedidos
    {
        [Key]
        public int PedidoId { get; set; }
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "El Nombre del cliente es obligatorio")]
        [MaxLength(70)]
        public string NombreCliente { get; set; }

    }
}
