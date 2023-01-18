using gRPC.Poc.Emtities;
using gRPC.Poc.Protos;
using Grpc.Core;
using System.Linq.Expressions;
using static gRPC.Poc.Repository.Repo.UsuarioRepository;

namespace gRPC.Poc.Services.gRPC
{
    public class UsuarioGrpcService : UsuarioProto.UsuarioProtoBase
    {
        private IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioGrpcService> _logger;

        public UsuarioGrpcService(
            IUsuarioRepository usuarioRepository, 
            ILogger<UsuarioGrpcService> logger)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }
      
        public override async Task<UsuarioResponse> ObterUsuario(ObterUsuarioRequest usuarioRequest, ServerCallContext context)
        {            
            
                var usuario = await _usuarioRepository.ObterUsuario(usuarioRequest.Id);

                if (usuario == null) 
                    throw new RpcException(new Status(StatusCode.NotFound, "Usuario não encontrado"));                
               

                var userResponse = MapUsuarioToUsuarioResponse(usuario);
                _logger.LogInformation("Usuário encontrado.");

                return userResponse;           
           
        }       



        public override async Task<UsuarioResponse> AdicionarUsuario(AdicionarUsuarioRequest usuarioRequest, ServerCallContext context)
        {
            var usuario = MapRequestToUsuario(usuarioRequest.UsuarioResponse.Id, usuarioRequest.UsuarioResponse.Nome, usuarioRequest.UsuarioResponse.Cpf);
           
            var result = await _usuarioRepository.AdicionarUsuario(usuario);

            if (!result)           
                return null;               
                     
            return usuarioRequest.UsuarioResponse;
        }      

        public override async Task<UsuarioResponse> AtualizarUsuario(AtualizarUsuarioRequest usuarioRequest, ServerCallContext context)
        {
            var usuario = MapRequestToUsuario(usuarioRequest.UsuarioResponse.Id, usuarioRequest.UsuarioResponse.Nome, usuarioRequest.UsuarioResponse.Cpf);
           
            var result = await _usuarioRepository.AtualizarUsuario(usuario);

            if (!result)           
                return null;          

            return usuarioRequest.UsuarioResponse;
        }

        public override async Task<DeletarUsuarioResponse> DeletarUsuario(DeletarUsuarioRequest usuarioRequest, ServerCallContext context)
        {           
            var result = await _usuarioRepository.DeletarUsuario(usuarioRequest.Id);

            if (!result)
                return null;
            
            var reponse = new DeletarUsuarioResponse { Success = result };          

            return reponse;
        }

        private static UsuarioResponse MapUsuarioToUsuarioResponse(Usuario usuario)
        {
            
           
            var userResponse = new UsuarioResponse
            {
                Id = usuario.Id.ToString(),
                Nome = usuario.Nome,
                Cpf = usuario.Cpf,
            };

            return userResponse;
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
