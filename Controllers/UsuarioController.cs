using AtividadeCrud.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AtividadeCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarTodos()
        {
            var usuarios = await _usuarioRepositorio.BuscarTodosUsuarios();
            return Ok(usuarios);
        }

        // GET: api/Usuario/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> BuscarPorId(int id)
        {
            var usuario = await _usuarioRepositorio.BuscarPorId(id);

            if (usuario == null)
            {
                return NotFound(new { mensagem = $"Usuário com Id {id} não encontrado." });
            }

            return Ok(usuario);
        }

        // POST: api/Usuario
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Adicionar([FromBody] UsuarioModel usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var novoUsuario = await _usuarioRepositorio.Adicionar(usuario);
            return CreatedAtAction(nameof(BuscarPorId), new { id = novoUsuario.Id }, novoUsuario);
        }

        // PUT: api/Usuario/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> Atualizar(int id, [FromBody] UsuarioModel usuario)
        {
            if (id <= 0)
            {
                return BadRequest("Id inválido.");
            }

            try
            {
                var usuarioAtualizado = await _usuarioRepositorio.Atualizar(usuario, id);
                return Ok(usuarioAtualizado);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        // DELETE: api/Usuario/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Apagar(int id)
        {
            try
            {
                var apagado = await _usuarioRepositorio.Apagar(id);

                if (!apagado)
                {
                    return NotFound(new { mensagem = $"Usuário com Id {id} não encontrado." });
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
