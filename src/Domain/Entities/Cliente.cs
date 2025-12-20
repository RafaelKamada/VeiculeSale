namespace Domain.Entities
{
    public class Cliente
    {
        public Cliente(string nome, string cpf, string email)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Cpf = cpf;
            Email = email;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }

        // Relação com Vendas (opcional na modelagem, útil para o EF Core)
        public virtual ICollection<Venda> Compras { get; private set; } = new List<Venda>();
    }
}
