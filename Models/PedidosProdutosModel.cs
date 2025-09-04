﻿using AtividadeCrud.Map;

public class PedidosProdutos
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public int PedidoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }

    public Produtos Produtos { get; set; }
    public Pedidos Pedidos { get; set; }
}
