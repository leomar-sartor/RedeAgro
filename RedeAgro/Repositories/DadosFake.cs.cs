using RedeAgro.Entidade;
using System.Collections.Generic;

namespace RedeAgro.Repositories
{
    public class DadosFake
    {
        public string nome { get; set; }

        public string endereco { get; set; }

        public List<double> local { get; set; }

        public string setor { get; set; }

        public List<DadosFake> GetDados()
        {
            var registros = new List<DadosFake>();

            registros.Add(new DadosFake()
            {
                nome = "Teatro São Pedro",
                endereco = "Teste",
                local = new List<double>() { -51.23040500, -30.03211600 },
                setor = "Poder Público"
            });

            registros.Add(new DadosFake()
            {
                nome = "Usina do Gasômetro",
                endereco = "Teste",
                local = new List<double>() { -51.24125800, -30.03389300 },
                setor = "Poder Público"
            });


            registros.Add(new DadosFake()
            {
                nome = "Usina do Gasômetro",
                endereco = "Teste",
                local = new List<double>() { -51.24089200, -30.03373700 },
                setor = "Poder Público"
            });


            registros.Add(new DadosFake()
            {
                nome = "Parque Harmonia",
                endereco = "Teste",
                local = new List<double>() { -51.23656500, -30.04030000 },
                setor = "Poder Público"
            });


            registros.Add(new DadosFake()
            {
                nome = "Parque Harmonia",
                endereco = "Teste",
                local = new List<double>() { -51.23662100, -30.04070500 },
                setor = "Poder Público"
            });


            registros.Add(new DadosFake()
            {
                nome = "Teatro São Pedro",
                endereco = "Teste",
                local = new List<double>() { -51.22966900, -30.04925500 },
                setor = "Poder Público"
            });


            registros.Add(new DadosFake()
            {
                nome = "Teatro São Pedro",
                endereco = "Teste",
                local = new List<double>() { -51.23179600, -30.05184000 },
                setor = "Poder Público"
            });

            registros.Add(new DadosFake()
            {
                nome = "Teatro São Pedro",
                endereco = "Teste",
                local = new List<double>() { -51.20005900, -30.02597700 },
                setor = "Poder Público"
            });

            registros.Add(new DadosFake()
            {
                nome = "Teatro São Pedro",
                endereco = "Teste",
                local = new List<double>() { -51.13752000, -30.15400300 },
                setor = "Poder Público"
            });


            registros.Add(new DadosFake()
            {
                nome = "Teatro São Pedro",
                endereco = "Teste",
                local = new List<double>() { -51.22588800, -30.03803300 },
                setor = "Poder Público"
            });


            registros.Add(new DadosFake()
            {
                nome = "Teatro São Pedro",
                endereco = "Teste",
                local = new List<double>() { -51.17989400, -30.01787500 },
                setor = "Poder Público"
            });


            registros.Add(new DadosFake()
            {
                nome = "Teatro São Pedro",
                endereco = "Teste",
                local = new List<double>() { -51.18326200, -29.98779300 },
                setor = "Poder Público"
            });


            registros.Add(new DadosFake()
            {
                nome = "Teatro São Pedro",
                endereco = "Teste",
                local = new List<double>() { -51.23126900, -30.02974000 },
                setor = "Poder Público"
            });


            registros.Add(new DadosFake()
            {
                nome = "Teatro São Pedro",
                endereco = "Teste",
                local = new List<double>() { -51.22760300, -30.02743300 },
                setor = "Poder Público"
            });

            registros.Add(new DadosFake()
            {
                nome = "Teatro São Pedro",
                endereco = "Teste",
                local = new List<double>() { -51.22813500, -30.02759500 },
                setor = "Poder Público"
            });



            return registros;
        }
    }
}
