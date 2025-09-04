using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtividadeCrud.Map
{
    public class PedidosMap : IEntityTypeConfiguration<Pedidos>
    {
        public void Configure(EntityTypeBuilder<Pedidos> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.EnderecoEntrega).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(50);
            builder.Property(x => x.MetodoPagamento).IsRequired().HasMaxLength(100);

            // Relacionamento: Pedido pertence a 1 Usuário
            builder.HasOne(x => x.Usuario)
                   .WithMany()
                   .HasForeignKey(x => x.UsuarioId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
