using Dapper;
using Dommel;
using gRPC.Poc.Emtities;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq.Expressions;
using static gRPC.Poc.Repository.Repo.UsuarioRepository;

namespace gRPC.Poc.Repository.Repo
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public interface IUsuarioRepository
        {
            Task<Usuario> ObterUsuario(string id);
            Task<bool> AdicionarUsuario(Usuario usuario);
            Task<bool> AtualizarUsuario(Usuario usuario);
            Task<bool> DeletarUsuario(string id);

        }

        private readonly IConfiguration _configuration;
        private readonly string _connection;

        public UsuarioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DataBase");
        }


        public async Task<Usuario> ObterUsuario(string id)
        {
            var idUsuario = Guid.Parse(id);
            
            using (SqlConnection dbConnection = new SqlConnection(_connection))
            {

                var result = await dbConnection.GetAsync<Usuario>(idUsuario);

                if (result == null)
                    return null;

                return result;
            }
        }

        public async Task<bool> AdicionarUsuario(Usuario usuario)
        {
            var sql  = $"INSERT INTO USUARIOS VALUES ('{usuario.Id}', '{usuario.Nome}', '{usuario.Cpf}')"; 
            using (SqlConnection dbConnection = new SqlConnection(_connection))
            {
                try
                {
                    var result = await dbConnection.ExecuteAsync(sql); //Usa ExecuteAsync = retorna int (Quantas linhas afetadas)

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }

                return true;
            }
        }

        public async Task<bool> AtualizarUsuario(Usuario usuario)
        {
            using (SqlConnection dbConnection = new SqlConnection(_connection))
            {
                var result = await dbConnection.UpdateAsync(usuario); //Usa ExecuteAsync = retorna int (Quantas linhas afetadas)

                return result;
            }
        }

        public async Task<bool> DeletarUsuario(string id)
        {
            var idUsuario = Guid.Parse(id);
            var sql = $"DELETE FROM USUARIOS WHERE ID = '{id}'";           

            using (SqlConnection dbConnection = new SqlConnection(_connection))
            {

                try
                {
                    var result = await dbConnection.ExecuteAsync(sql); //Usa ExecuteAsync = retorna int (Quantas linhas afetadas)

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }

                return true;
            }
        }
    }
}
