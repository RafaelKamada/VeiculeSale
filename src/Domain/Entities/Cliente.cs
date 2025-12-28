using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Cliente
    {
        protected Cliente() { }

        public Cliente(string nome, string cpf, string email, string telefone)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Cpf = new Cpf(cpf);
            Email = new Email(email);
            Telefone = telefone;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public Cpf Cpf { get; private set; }
        public Email Email { get; private set; }
        public string Telefone { get; private set; }

        public virtual ICollection<Venda> Compras { get; private set; } = new List<Venda>();

        public void AtualizarDados(string nome, string email, string telefone)
        {
            Nome = nome;
            Email = new Email(email);
            Telefone = telefone;
        }

    }
}
