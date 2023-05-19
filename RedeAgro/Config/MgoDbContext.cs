using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using RedeAgro.Entidade;
using RedeAgro.Models;

namespace RedeAgro.Config
{
    public class MgoDbContext
    {
        private readonly IMongoDatabase _mgoDatabase;
        public MgoDbContext(IOptions<MongoDbConfig> config)
        {
            try
            {
                MongoClientSettings setting = MongoClientSettings
                    .FromUrl(new MongoUrl(config.Value.ConnectionString));

                if (config.Value.IsSSL)
                {
                    setting.SslSettings = new SslSettings
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    };
                }

                var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
                ConventionRegistry.Register("camelCase", conventionPack, t => true);

                var client = new MongoClient(setting);
                //new MongoClient(config.Value.ConnectionString);
                _mgoDatabase = client.GetDatabase(config.Value.NameDb);

                CheckIndexContext();
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível conectar!");
            }
        }

        public IMongoCollection<Credenciado> Credenciados =>
            _mgoDatabase.GetCollection<Credenciado>("Credenciados");

        public void CheckIndexContext()
        {
            IMongoCollection<Credenciado> collection = _mgoDatabase.GetCollection<Credenciado>("Credenciados");

            collection.Indexes.CreateOne(new IndexKeysDefinitionBuilder<Credenciado>().Geo2DSphere(x => x.Location));
        }
    }
}
