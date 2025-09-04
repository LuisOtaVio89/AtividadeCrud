using AtividadeCrud.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AtividadeCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosRepositorio _produtosRepositorio;

        public ProdutosController(IProdutosRepositorio produtosRepositorio)
        {
            _produtosRepositorio = produtosRepositorio;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<List<Produtos>>> BuscarTodos()
        {
            var produtos = await _produtosRepositorio.BuscarTodosProdutos();
            return Ok(produtos);
        }

        // GET: api/Produtos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Produtos>> BuscarPorId(int id)
        {
            var produto = await _produtosRepositorio.BuscarPorId(id);

            if (produto == null)
            {
                return NotFound(new { mensagem = $"Produto com Id {id} não encontrado." });
            }

            return Ok(produto);
        }

        // POST: api/Produtos
        [HttpPost]
        public async Task<ActionResult<Produtos>> Adicionar([FromBody] Produtos produto)
        {
            if (produto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var novoProduto = await _produtosRepositorio.Adicionar(produto);
            return CreatedAtAction(nameof(BuscarPorId), new { id = novoProduto.Id }, novoProduto);
        }

        // PUT: api/Produtos/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Produtos>> Atualizar(int id, [FromBody] Produtos produto)
        {
            if (id <= 0)
            {
                return BadRequest("Id inválido.");
            }

            try
            {
                var produtoAtualizado = await _produtosRepositorio.Atualizar(produto, id);
                return Ok(produtoAtualizado);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        // DELETE: api/Produtos/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Apagar(int id)
        {
            try
            {
                var apagado = await _produtosRepositorio.Apagar(id);

                if (!apagado)
                {
                    return NotFound(new { mensagem = $"Produto com Id {id} não encontrado." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}
