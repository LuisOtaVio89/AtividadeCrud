public class Pedidos
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string EnderecoEntrega { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string MetodoPagamento { get; set; } = string.Empty;
    public Usuario Usuario { get; set; }
    public ICollection<PedidosProdutos> PedidoProdutos { get; set; } = new List<PedidosProdutos>();
}
