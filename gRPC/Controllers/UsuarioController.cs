using gRPC.API.Entities;
using gRPC.API.Services.gRPC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static gRPC.API.Services.gRPC.UsuarioService;

namespace gRPC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioGrpc;
       
        public UsuarioController(
            IUsuarioService usuarioGrpc)
        {
            _usuarioGrpc = usuarioGrpc;           
        }

        [HttpGet("cpf")]
        public async Task<IActionResult> ObterPorCpf(string cpf)
        {
            Usuario result = new();
            try
            {
                result = await _usuarioGrpc.ObterUsuarioPorCpf(cpf);
            }
            catch (Exception ex)
            {

                throw;
            }                             
            return Ok(result);
        }
       
    }
}
