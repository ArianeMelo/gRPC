namespace gRPC.API.Entities
{
    public class Usuario
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }

        public Usuario()
        {
        }

        public Usuario(string? nome, string? cpf)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Cpf = cpf;
        }
    }
}
