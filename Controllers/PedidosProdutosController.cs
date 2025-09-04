using AtividadeCrud.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AtividadeCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosProdutosController : ControllerBase
    {
        private readonly IPedidosProdutosRepositorio _pedidosProdutosRepositorio;

        public PedidosProdutosController(IPedidosProdutosRepositorio pedidosProdutosRepositorio)
        {
            _pedidosProdutosRepositorio = pedidosProdutosRepositorio;
        }

        // GET: api/PedidosProdutos
        [HttpGet]
        public async Task<ActionResult<List<PedidosProdutos>>> BuscarTodos()
        {
            var lista = await _pedidosProdutosRepositorio.BuscarTodosPedidosProdutos();
            return Ok(lista);
        }

        // GET: api/PedidosProdutos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidosProdutos>> BuscarPorId(int id)
        {
            var pedidoProduto = await _pedidosProdutosRepositorio.BuscarPorId(id);

            if (pedidoProduto == null)
            {
                return NotFound(new { mensagem = $"PedidoProduto com Id {id} não encontrado." });
            }

            return Ok(pedidoProduto);
        }

        // POST: api/PedidosProdutos
        [HttpPost]
        public async Task<ActionResult<PedidosProdutos>> Adicionar([FromBody] PedidosProdutos pedidoProduto)
        {
            if (pedidoProduto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var novoPedidoProduto = await _pedidosProdutosRepositorio.Adicionar(pedidoProduto);
            return CreatedAtAction(nameof(BuscarPorId), new { id = novoPedidoProduto.Id }, novoPedidoProduto);
        }

        // PUT: api/PedidosProdutos/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<PedidosProdutos>> Atualizar(int id, [FromBody] PedidosProdutos pedidoProduto)
        {
            if (id <= 0)
            {
                return BadRequest("Id inválido.");
            }

            try
            {
                var pedidoProdutoAtualizado = await _pedidosProdutosRepositorio.Atualizar(pedidoProduto, id);
                return Ok(pedidoProdutoAtualizado);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        // DELETE: api/PedidosProdutos/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Apagar(int id)
        {
            try
            {
                var apagado = await _pedidosProdutosRepositorio.Apagar(id);

                if (!apagado)
                {
                    return NotFound(new { mensagem = $"PedidoProduto com Id {id} não encontrado." });
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
