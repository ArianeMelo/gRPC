using Dommel;
using gRPC.Poc.Emtities;
using System.Data.SqlClient;
using System.Linq.Expressions;
using static gRPC.Poc.Repository.Repo.UsuarioRepository;

namespace gRPC.Poc.Repository.Repo
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public interface IUsuarioRepository
        {
            //Task<Usuario> Buscar(Guid cpf);
            Task<IEnumerable<Usuario>> Buscar(Expression<Func<Usuario, bool>> where);
        }

        private readonly IConfiguration _configuration;
        private readonly string _connection;

        public UsuarioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultString");
        }

        //public async Task<Usuario> Buscar(Guid id)
        //{
        //    using (SqlConnection dbConnection = new SqlConnection(_connection))
        //    {
        //        var result =  await dbConnection.GetAsync<Usuario>(id);
        //        return result;
        //    }
        //}

        public async Task<IEnumerable<Usuario>> Buscar(Expression<Func<Usuario, bool>> where)
        {
            using (SqlConnection dbConnection = new SqlConnection(_connection))
            {
                return await dbConnection.SelectAsync<Usuario>(where);
            }
        }
    }
}
