using AtividadeCrud.Data;
using AtividadeCrud.Repositorio.Intarfaces;
using Microsoft.EntityFrameworkCore;

namespace AtividadeCrud.Repositorio
{
    public class PedidosRepositorio : IPedidosRepositorio
    {
        private readonly AtividadeCrudDbContext _dbContext;

        public PedidosRepositorio(AtividadeCrudDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Pedidos> Adicionar(Pedidos pedido)
        {
            await _dbContext.Pedidos.AddAsync(pedido);
            await _dbContext.SaveChangesAsync();

            return pedido;
        }

        public async Task<bool> Apagar(int id)
        {
            Pedidos pedidoPorId = await BuscarPorId(id);

            if (pedidoPorId == null)
            {
                throw new Exception("Tarefa não encontrada");
            }

            _dbContext.Pedidos.Remove(pedidoPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Pedidos> Atualizar(Pedidos pedido, int id)
        {
            Pedidos pedidoPorId = await BuscarPorId(id);

            if (pedidoPorId == null)
            {
                throw new Exception($"Tarefa do Id: {id} não encontrada.");
            }

            pedidoPorId.EnderecoEntrega = pedido.EnderecoEntrega;
            pedidoPorId.Status = pedido.Status;
            //pedidoPorId.UsuarioId = pedido.UsuarioId;

            await _dbContext.SaveChangesAsync();

            return pedidoPorId;
        }

        public async Task<Pedidos> BuscarPorId(int id)
        {
            return await _dbContext.Pedidos
                //.Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Pedidos>> BuscarTodosPedidos()
        {
            return await _dbContext.Pedidos
               //.Include(x => x.Usuario)
               .ToListAsync();
        }
    }
}
