using RedeAgro.Entidade;
using System.ComponentModel.DataAnnotations;

namespace RedeAgro.Models.Filtros
{
    public class SearchFilter : Filtro<Credenciado>
    {
        public SearchFilter()
        {

        }

        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}
