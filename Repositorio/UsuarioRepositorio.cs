using AtividadeCrud.Data;
using AtividadeCrud.Repositorio.Intarfaces;
using AtividadeCrud.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AtividadeCrud.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AtividadeCrudDbContext _dbContext;

        public UsuarioRepositorio(AtividadeCrudDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception("Tarefa não encontrada");
            }

            _dbContext.Usuarios.Remove(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"Tarefa do Id: {id} não encontrada.");
            }

            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;


            await _dbContext.SaveChangesAsync();

            return usuarioPorId;
        }

        public async Task<UsuarioModel> BuscarPorId(int id)
        {
            return await _dbContext.Usuarios

                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios

               .ToListAsync();
        }
    }
}
