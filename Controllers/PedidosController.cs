using AtividadeCrud.Repositorio.Intarfaces;
using AtividadeCrud.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AtividadeCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidosRepositorio _pedidosRepositorio;

        public PedidosController(IPedidosRepositorio pedidosRepositorio)
        {
            _pedidosRepositorio = pedidosRepositorio;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<List<Pedidos>>> BuscarTodos()
        {
            var pedidos = await _pedidosRepositorio.BuscarTodosPedidos();
            return Ok(pedidos);
        }

        // GET: api/Pedidos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedidos>> BuscarPorId(int id)
        {
            var pedido = await _pedidosRepositorio.BuscarPorId(id);

            if (pedido == null)
            {
                return NotFound(new { mensagem = $"Pedido com Id {id} não encontrado." });
            }

            return Ok(pedido);
        }

        // POST: api/Pedidos
        [HttpPost]
        public async Task<ActionResult<Pedidos>> Adicionar([FromBody] Pedidos pedido)
        {
            if (pedido == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var novoPedido = await _pedidosRepositorio.Adicionar(pedido);
            return CreatedAtAction(nameof(BuscarPorId), new { id = novoPedido.Id }, novoPedido);
        }

        // PUT: api/Pedidos/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Pedidos>> Atualizar(int id, [FromBody] Pedidos pedido)
        {
            if (id <= 0)
            {
                return BadRequest("Id inválido.");
            }

            try
            {
                var pedidoAtualizado = await _pedidosRepositorio.Atualizar(pedido, id);
                return Ok(pedidoAtualizado);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        // DELETE: api/Pedidos/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Apagar(int id)
        {
            try
            {
                var apagado = await _pedidosRepositorio.Apagar(id);

                if (!apagado)
                {
                    return NotFound(new { mensagem = $"Pedido com Id {id} não encontrado." });
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
