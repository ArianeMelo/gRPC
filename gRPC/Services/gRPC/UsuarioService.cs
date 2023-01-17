using gRPC.API.Entities;
using gRPC.Poc.Protos;
using static gRPC.API.Services.gRPC.UsuarioService;

namespace gRPC.API.Services.gRPC
{
    public class UsuarioService : IUsuarioService
    {
        public interface IUsuarioService
        {
            Task<Usuario> ObterUsuarioPorCpf(string id);
            
        }

        private readonly UsuarioProto.UsuarioProtoClient _usuarioProtoClient;

        public UsuarioService(UsuarioProto.UsuarioProtoClient usuarioProtoClient)
        {
            _usuarioProtoClient = usuarioProtoClient;
        }

        public async Task<Usuario> ObterUsuarioPorCpf(string id)
        {
            var usuarioResponse = await ConverterParaUsuarioResponse(id);

            var usuario = new Usuario
            {
                Id = usuarioResponse.Id,
                Nome = usuarioResponse.Nome,
                Cpf = usuarioResponse.Cpf,
            };
           
            return usuario;
        }

        public async Task<ObterUsuarioResponse> ConverterParaUsuarioResponse(string cpf)
        {
            var usuarioRequest = new ObterUsuarioRequest
            {
                Cpf = cpf
            };
            var response = await _usuarioProtoClient.ObterUsuarioAsync(usuarioRequest);

            return response;
        }
    }
}
