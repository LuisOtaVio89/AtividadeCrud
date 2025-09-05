using AtividadeCrud.Map;
using Microsoft.EntityFrameworkCore;

namespace AtividadeCrud.Data
{
    public class AtividadeCrudDbContext : DbContext
    {
        public AtividadeCrudDbContext(DbContextOptions<AtividadeCrudDbContext> options)
    : base(options)
        {
        }

        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<PedidosProdutos> PedidosProdutos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
