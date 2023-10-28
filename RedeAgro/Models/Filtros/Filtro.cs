namespace RedeAgro.Models.Filtros
{
    public class Filtro<T>
    {
        public int TotalLinhas { get; set; }
        public List<T> Itens { get; set; }

        public Filtro()
        {
            this.Itens = new List<T>();
        }


        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;





        public int PageNumber { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
    }
}
