using Microsoft.Extensions.Options;

namespace RedeAgro.Config
{
    public class MongoDbConfig
    { 
        public string NameDb { get; init; }
        public string Host { get; init; }
        public int Port { get; init; }
        //public string ConnectionString 
        //    => $"mongodb://{Host}:{Port}";
        //                  localhost:27017

        //mongodb://writecode01
        //:Mudar123@mongodb.writecode.com.br:
        //27017/?tls=false&authMechanism=DEFAULT
        //&authSource=writecode01
        public string ConnectionString
            => $"mongodb://writecode01:Mudar123@mongodb.writecode.com.br:27017/?tls=false&authMechanism=SCRAM-SHA-256&authSource=writecode01";

        public string Password { get; init; }

        public bool IsSSL { get; set; } = false;
    }
}
