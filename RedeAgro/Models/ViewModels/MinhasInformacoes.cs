using RedeAgro.Entidade;

namespace RedeAgro.Models.ViewModels
{
    public class MinhasInformacoes
    {
        public MinhasInformacoes()
        {
            Servicos = new List<Servico>();
        }
        public Guid UserId { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public List<Servico> Servicos { get; set; }
    }
}
