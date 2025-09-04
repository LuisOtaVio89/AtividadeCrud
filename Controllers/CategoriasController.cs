using AtividadeCrud.Repositorio.Intarfaces;
using Microsoft.AspNetCore.Mvc;

namespace AtividadeCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriasRepositorio _categoriasRepositorio;

        public CategoriasController(ICategoriasRepositorio categoriasRepositorio)
        {
            _categoriasRepositorio = categoriasRepositorio;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<List<Categorias>>> BuscarTodas()
        {
            var categorias = await _categoriasRepositorio.BuscarTodasCategorias();
            return Ok(categorias);
        }

        // GET: api/Categorias/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Categorias>> BuscarPorId(int id)
        {
            var categoria = await _categoriasRepositorio.BuscarPorId(id);

            if (categoria == null)
            {
                return NotFound(new { mensagem = $"Categoria com Id {id} não encontrada." });
            }

            return Ok(categoria);
        }

        // POST: api/Categorias
        [HttpPost]
        public async Task<ActionResult<Categorias>> Adicionar([FromBody] Categorias categoria)
        {
            if (categoria == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var novaCategoria = await _categoriasRepositorio.Adicionar(categoria);
            return CreatedAtAction(nameof(BuscarPorId), new { id = novaCategoria.Id }, novaCategoria);
        }

        // PUT: api/Categorias/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Categorias>> Atualizar(int id, [FromBody] Categorias categoria)
        {
            if (id <= 0)
            {
                return BadRequest("Id inválido.");
            }

            try
            {
                var categoriaAtualizada = await _categoriasRepositorio.Atualizar(categoria, id);
                return Ok(categoriaAtualizada);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        // DELETE: api/Categorias/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Apagar(int id)
        {
            try
            {
                var apagado = await _categoriasRepositorio.Apagar(id);

                if (!apagado)
                {
                    return NotFound(new { mensagem = $"Categoria com Id {id} não encontrada." });
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
