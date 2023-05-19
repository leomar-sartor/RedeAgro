using Microsoft.Extensions.Options;

namespace RedeAgro.Config
{
    public class MongoDbConfig
    { 
        public string NameDb { get; init; }
        public string Host { get; init; }
        public int Port { get; init; }
        public string ConnectionString => $"mongodb://{Host}:{Port}";

        public bool IsSSL { get; set; } = false;
    }
}
