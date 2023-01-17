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

        public UsuarioGrpcService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

      
        public override async Task<ObterUsuarioResponse> ObterUsuario(ObterUsuarioRequest request, ServerCallContext context)
        {
            
            var registro = await _usuarioRepository.Buscar(req => req.Cpf == request.Cpf);

            if (registro == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Não encontrado"));

            var user = MapUsuarioToObterUsuarioResponse(registro);

            return user;
        }  

        private static ObterUsuarioResponse MapUsuarioToObterUsuarioResponse(IEnumerable<Usuario> usuario)
        {
            var um = usuario.FirstOrDefault();
            var user = new ObterUsuarioResponse
            {
                Id = um.Id,
                Nome = um.Nome,
                Cpf = um.Cpf,
            };

            return user;
        }
    }
}
