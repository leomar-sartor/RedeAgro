using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RedeAgro.Models.ViewModels
{
    public class CredenciadoFiltro
    {
        public CredenciadoFiltro()
        {
            Raio = 10;
        }

        [Display(Name = "Raio (Km)")]
        public long Raio { get; set; }
        public string Nome { get; set; }
    }
}
