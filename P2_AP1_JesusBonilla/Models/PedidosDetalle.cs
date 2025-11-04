namespace P2_AP1_JesusBonilla.Models;

public class PedidosDetalle
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public int ComponenteId { get; set; }
    public int Cantidad { get; set; }
    public double Precio { get; set; }
}
