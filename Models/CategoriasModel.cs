using AtividadeCrud.Map;

public class Categorias
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public ICollection<Produtos> Produtos { get; set; } = new List<Produtos>();
}

