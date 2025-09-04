using AtividadeCrud.Data;
using AtividadeCrud.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AtividadeCrud.Repositorio
{
    public class PedidosProdutosRepositorio : IPedidosProdutosRepositorio
    {
        private readonly AtividadeCrudDbContext _dbContext;

        public PedidosProdutosRepositorio(AtividadeCrudDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PedidosProdutos> Adicionar(PedidosProdutos pedidoProduto)
        {
            await _dbContext.PedidosProdutos.AddAsync(pedidoProduto);
            await _dbContext.SaveChangesAsync();

            return pedidoProduto;
        }

        public async Task<bool> Apagar(int id)
        {
            PedidosProdutos pedidoProdutoPorId = await BuscarPorId(id);

            if (pedidoProdutoPorId == null)
            {
                throw new Exception("Tarefa não encontrada");
            }

            _dbContext.PedidosProdutos.Remove(pedidoProdutoPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<PedidosProdutos> Atualizar(PedidosProdutos pedidoProduto, int id)
        {
            PedidosProdutos pedidoProdutoPorId = await BuscarPorId(id);

            if (pedidoProdutoPorId == null)
            {
                throw new Exception($"Tarefa do Id: {id} não encontrada.");
            }

            pedidoProdutoPorId.Quantidade = pedidoProduto.Quantidade;
            pedidoProdutoPorId.PrecoUnitario = pedidoProduto.PrecoUnitario;
            //pedidoProdutoPorId.UsuarioId = pedidoProduto.UsuarioId;

            await _dbContext.SaveChangesAsync();

            return pedidoProdutoPorId;
        }

        public async Task<PedidosProdutos> BuscarPorId(int id)
        {
            return await _dbContext.PedidosProdutos
                //.Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<PedidosProdutos>> BuscarTodosPedidosProdutos()
        {
            return await _dbContext.PedidosProdutos
               //.Include(x => x.Usuario)
               .ToListAsync();
        }
    }
}
