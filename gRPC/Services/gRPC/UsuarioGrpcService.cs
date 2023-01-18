using gRPC.API.Entities;
using gRPC.Poc.Protos;
using static gRPC.API.Services.gRPC.UsuarioGrpcService;

namespace gRPC.API.Services.gRPC
{
    public class UsuarioGrpcService   
    {       
        private readonly UsuarioProto.UsuarioProtoClient _usuarioProtoClient;

        public UsuarioGrpcService(UsuarioProto.UsuarioProtoClient usuarioProtoClient)
        {
            _usuarioProtoClient = usuarioProtoClient;
        }

        public async Task<Usuario> ObterUsuario(string? id)
        {
            var idUsuario = new ObterUsuarioRequest { Id = id };
            
            var result  =  await _usuarioProtoClient.ObterUsuarioAsync(idUsuario);

            var usuario = MapRequestToUsuario(result.Id, result.Nome, result.Cpf);

            return usuario; 
        }

        public async Task<bool> DeletarUsuario(string id)
        {
            var idUsuario = new DeletarUsuarioRequest { Id = id };

            var result = await _usuarioProtoClient.DeletarUsuarioAsync(idUsuario);

            if (result == null)
                return false;

            return true;
        }


        public async Task<Usuario> AdicionarUsuario(Usuario usuario)
        {
            var usuarioResponse = new UsuarioResponse
            {  
                Id = usuario.Id.ToString(),
                Nome = usuario.Nome,
                Cpf = usuario.Cpf
            };

            var usuarioRequest = new AdicionarUsuarioRequest
            {
                UsuarioResponse = usuarioResponse
            };          

            var response = await _usuarioProtoClient.AdicionarUsuarioAsync(usuarioRequest);

            var usuarioConvert = MapRequestToUsuario(response.Id, response.Nome, response.Cpf);
         
            return usuario;
        }

 

        public async Task<Usuario> AtualizarUsuario(Usuario usuario)
        {
            var usuarioResponse = new UsuarioResponse
            {
                Id = usuario.Id.ToString(),
                Nome = usuario.Nome,
                Cpf = usuario.Cpf
            };

            var usuarioRequest = new AtualizarUsuarioRequest
            {
                UsuarioResponse = usuarioResponse
            };           

            var response = await _usuarioProtoClient.AtualizarUsuarioAsync(usuarioRequest);
            var usuarioConvert = MapRequestToUsuario(response.Id, response.Nome, response.Cpf);

         
            return usuario;
        }

    
        private static Usuario MapRequestToUsuario(string id, string nome, string cpf)
        {
            var usuario = new Usuario
            {
                Id = Guid.Parse(id),
                Nome = nome,
                Cpf = cpf
            };

            return usuario;
        }       
    }
}
