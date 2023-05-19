namespace RedeAgro.Entidade
{
    public class Servico
    {
        public Servico()
        {
            DataCriacao = DateTime.Today;
        }
        public Guid UserId { get; set; }

        //public long Sequencial { get; set; }

        public DateTime DataCriacao { get; set; }
        public string Descricao { get; set; }
    }
}
