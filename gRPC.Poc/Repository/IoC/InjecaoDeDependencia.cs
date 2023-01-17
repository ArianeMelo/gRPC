using gRPC.Poc.Repository.Repo;
using static gRPC.Poc.Repository.Repo.UsuarioRepository;

namespace gRPC.Poc.Repository.IoC
{
    public class InjecaoDeDependencia
    {
        public static void RegistrarInjecaoDependencia(IServiceCollection services)
        {
            Container.RegistrarMapeamentoDapper();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        }
    }
}
