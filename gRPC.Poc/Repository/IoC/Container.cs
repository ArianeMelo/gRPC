using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using gRPC.Poc.Repository.Map;

namespace gRPC.Poc.Repository.IoC
{
    public static class Container
    {
        public static void RegistrarMapeamentoDapper()
        {
            FluentMapper.Initialize(configurar =>
            {
                configurar.AddMap(new UsuarioMap());
                configurar.ForDommel();
            });
        }
    }
}
