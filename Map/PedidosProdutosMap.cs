using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtividadeCrud.Map
{
    public class PedidosProdutosMap : IEntityTypeConfiguration<PedidosProdutos>
    {
        public void Configure(EntityTypeBuilder<PedidosProdutos> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Quantidade).IsRequired();
            builder.Property(x => x.PrecoUnitario).IsRequired().HasColumnType("decimal(18,2)");

            // Relacionamentos
            builder.HasOne(x => x.Produtos)
                   .WithMany()
                   .HasForeignKey(x => x.ProdutoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Pedidos)
                   .WithMany()
                   .HasForeignKey(x => x.PedidoId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
