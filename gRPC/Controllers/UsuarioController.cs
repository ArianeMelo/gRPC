using gRPC.API.Dto;
using gRPC.API.Entities;
using gRPC.API.Services.gRPC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static gRPC.API.Services.gRPC.UsuarioGrpcService;

namespace gRPC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioGrpcService _usuarioGrpc;
       
        public UsuarioController(
            UsuarioGrpcService usuarioGrpc)
        {
            _usuarioGrpc = usuarioGrpc;           
        }

        [HttpGet("id")]
        public async Task<IActionResult> ObterUsuario(Guid id)
        {
            var idUsuario = id.ToString();
            var result = await _usuarioGrpc.ObterUsuario(idUsuario);

            if (result == null)
                return NotFound("Não encontrado");

            return Ok(result); 
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarUsuario(UsuarioDto usuarioDto)
        {
            var usuario = new Usuario(usuarioDto.Nome, usuarioDto.Cpf); 

            var result = await _usuarioGrpc.AdicionarUsuario(usuario);

            if (result == null)
                return BadRequest("Erro ao Adicionar");

            return Ok(usuarioDto);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarUsuario(UsuarioInsertDto usuarioInsertDto)
        {

            var idUsuario = usuarioInsertDto.Id?.ToString();

            var usuarioExiste = await _usuarioGrpc.ObterUsuario(idUsuario);

            if (usuarioExiste == null)
                return NotFound("Usuário não encontrado");

            var usuario = new Usuario 
            { 
                Id = usuarioInsertDto.Id, 
                Nome = usuarioInsertDto.Nome, 
                Cpf = usuarioInsertDto.Cpf 
            };

            var result = await _usuarioGrpc.AtualizarUsuario(usuario);

            if (result == null)
                return BadRequest("Erro ao Atualizar");

            return Ok(usuario);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeletarUsuario(Guid id)
        {
            var idUsuario = id.ToString();
            var usuarioExiste = await _usuarioGrpc.ObterUsuario(idUsuario);

            if (usuarioExiste == null)
                return NotFound("Usuário não encontrado");
            
            var result = await _usuarioGrpc.DeletarUsuario(idUsuario);

            if (result)
                return Ok();

            return BadRequest("Erro ao Excluir");
        }

    }
}
