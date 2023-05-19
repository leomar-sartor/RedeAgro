using RedeAgro.Entidade;

namespace RedeAgro.Inputs
{
    public class CredenciadoInput
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public List<Formacao> Formacoes { get; set; }

        public List<Especialidade> Especialidades { get; set; }

        public List<Atuacao> Atuacoes { get; set; }

        public List<Servico> Servicos { get; set; }
        public Local Location { get; set; }
    }
}
