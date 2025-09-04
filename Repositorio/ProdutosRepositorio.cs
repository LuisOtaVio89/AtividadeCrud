using AtividadeCrud.Data;
using AtividadeCrud.Repositorio.Intarfaces;
using AtividadeCrud.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AtividadeCrud.Repositorio
{
    public class ProdutosRepositorio : IProdutosRepositorio
    {
        private readonly AtividadeCrudDbContext _dbContext;

        public ProdutosRepositorio(AtividadeCrudDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Produtos> Adicionar(Produtos produto)
        {
            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();

            return produto;
        }

        public async Task<bool> Apagar(int id)
        {
            Produtos produtoPorId = await BuscarPorId(id);

            if (produtoPorId == null)
            {
                throw new Exception("Tarefa não encontrada");
            }

            _dbContext.Produtos.Remove(produtoPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Produtos> Atualizar(Produtos produto, int id)
        {
            Produtos produtoPorId = await BuscarPorId(id);

            if (produtoPorId == null)
            {
                throw new Exception($"Tarefa do Id: {id} não encontrada.");
            }

            produtoPorId.Nome = produto.Nome;
            //produtoPorId.UsuarioId = produto.UsuarioId;

            await _dbContext.SaveChangesAsync();

            return produtoPorId;
        }

        public async Task<Produtos> BuscarPorId(int id)
        {
            return await _dbContext.Produtos
                //.Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Produtos>> BuscarTodosProdutos()
        {
            return await _dbContext.Produtos
               //.Include(x => x.Usuario)
               .ToListAsync();
        }
    }
}
