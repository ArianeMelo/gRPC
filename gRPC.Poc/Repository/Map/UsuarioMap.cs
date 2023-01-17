using Dapper.FluentMap.Dommel.Mapping;
using gRPC.Poc.Emtities;

namespace gRPC.Poc.Repository.Map
{
    public class UsuarioMap : DommelEntityMap<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("Usuarios");
            Map(cliente => cliente.Id).ToColumn("Id").IsKey();
            Map(cliente => cliente.Nome).ToColumn("Nome");
            Map(cliente => cliente.Cpf).ToColumn("Cpf");

        }
    }
}
