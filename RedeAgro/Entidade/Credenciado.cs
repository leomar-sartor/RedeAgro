using MongoDbGenericRepository.Attributes;
using RedeAgro.Object;

namespace RedeAgro.Entidade
{
    //[CollectionName("Credenciados")]
    public class Credenciado
    {
        public Credenciado()
        {
            Location = new Local();
            Formacoes = new List<Formacao>();
            Especialidades = new List<Especialidade>();
            Atuacoes = new List<Atuacao>();
            Servicos = new List<Servico>();
        }
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
