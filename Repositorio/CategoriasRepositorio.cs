using AtividadeCrud.Data;
using AtividadeCrud.Repositorio.Intarfaces;
using Microsoft.EntityFrameworkCore;

namespace AtividadeCrud.Repositorio
{
    public class CategoriasRepositorio : ICategoriasRepositorio
    {
        private readonly AtividadeCrudDbContext _dbContext;

        public CategoriasRepositorio(AtividadeCrudDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Categorias> Adicionar(Categorias categoria)
        {
            await _dbContext.Categorias.AddAsync(categoria);
            await _dbContext.SaveChangesAsync();

            return categoria;
        }

        public async Task<bool> Apagar(int id)
        {
            Categorias tarefaPorId = await BuscarPorId(id);

            if (tarefaPorId == null)
            {
                throw new Exception("Tarefa não encontrada");
            }

            _dbContext.Categorias.Remove(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Categorias> Atualizar(Categorias tarefa, int id)
        {
            Categorias tarefaPorId = await BuscarPorId(id);

            if (tarefaPorId == null)
            {
                throw new Exception($"Tarefa do Id: {id} não encontrada.");
            }

            tarefaPorId.Nome = tarefa.Nome;
            tarefaPorId.Status = tarefa.Status;
            //tarefaPorId.UsuarioId = tarefa.UsuarioId;

            await _dbContext.SaveChangesAsync();

            return tarefaPorId;
        }

        public async Task<Categorias> BuscarPorId(int id)
        {
            return await _dbContext.Categorias
                //.Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Categorias>> BuscarTodasCategorias()
        {
            return await _dbContext.Categorias
               //.Include(x => x.Usuario)
               .ToListAsync();
        }
    }
}
