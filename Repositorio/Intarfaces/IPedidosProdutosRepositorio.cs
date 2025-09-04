namespace AtividadeCrud.Repositorio.Interfaces
{
    public interface IPedidosProdutosRepositorio
    {
        Task<List<PedidosProdutos>> BuscarTodosPedidosProdutos();
        Task<PedidosProdutos> BuscarPorId(int id);
        Task<PedidosProdutos> Adicionar(PedidosProdutos pedidosProdutos);
        Task<PedidosProdutos> Atualizar(PedidosProdutos pedidosProdutos, int id);
        Task<bool> Apagar(int id);
    }
}
