namespace AtividadeCrud.Repositorio.Intarfaces
{
    public interface ICategoriasRepositorio
    {
        Task<List<Categorias>> BuscarTodasCategorias();
        Task<Categorias> BuscarPorId(int id);
        Task<Categorias> Adicionar(Categorias categorias);
        Task<Categorias> Atualizar(Categorias categorias, int id);
        Task<bool> Apagar(int id);
    }
}
