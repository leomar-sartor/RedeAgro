using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using RedeAgro.Entidade;
using RedeAgro.Object;

namespace RedeAgro.Models
{
    [CollectionName("Users")]
    public class UserApp : MongoIdentityUser<Guid>
    {
        public UserApp()
        {
            Local = new Local();
        }

        public Local Local { get; set; }

        //public List<Formacao> Formacoes { get; set; }

        //public List<Especialidade> Especialidades { get; set; }

        //public List<Atuacao> Atuacoes { get; set; }

        //public List<Servico> Servicos { get; set; }
    }
}
