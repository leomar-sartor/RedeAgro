using MongoDB.Bson;
using RedeAgro.Config;
using RedeAgro.Entidade;
using RedeAgro.Intefaces;
using RedeAgro.Object;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace RedeAgro.Repositories
{
    public class CredenciadoRepository : ICredenciadoRepository
    {
        private readonly MgoDbContext _context;
        public CredenciadoRepository(IOptions<MongoDbConfig> config)
        {
            _context = new MgoDbContext(config);
        }

        public async Task AddAsync(Credenciado obj)
        {
            await _context.Credenciados
                .InsertOneAsync(obj);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Credenciados
                .DeleteOneAsync(doc => doc.Id == id);
        }

        public async Task<IEnumerable<Credenciado>> GetAllAsync()
        {
            return await _context.Credenciados
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<Credenciado> GetByIdAsync(Guid id)
        {
            return await _context.Credenciados
                .AsQueryable()
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

        //Colocar um controle de máximo no range do raio
        //nesse método, eu passo como parâmetro a latitude e longitude, do ponto que estou no GPS. Ele utilizará como filtro.
        public async Task<IEnumerable<CredenciadoLocationValueObject>> GetProximityAsync(double latitude, double longitude, int raio, string? name)
        {
            //var filter = new BsonDocument { { "name", new BsonRegularExpression($"/^{name}/") } };
            var pipeline = new BsonDocumentPipelineStageDefinition<Credenciado, CredenciadoLocationValueObject>(new BsonDocument
            {
                { "$geoNear", new BsonDocument
                    {
                        { "near", new BsonDocument
                            {
                                { "type", "Point"},
                                { "coordinates", new BsonArray(new[] { latitude, longitude })}
                            }
                        },
                        //distanceField: Informo ao MongoDb, que quero que retorne a distância, na propriedade Dist.Calculated da minha classe GasStationLocationValueObject.cs
                        { "distanceField", "dist.calculated" },
                        //maxDistance: Informo ao MongoDb, que quero que retorne todos os postos de gasolina em um raio de no máximo de 10 quilômetros.
                        { "maxDistance", 10000 },
                        //query: Adiciono o filtro que quero fazer a consulta.
                        //{ "query" , filter },
                        //includeLocs: Informo ao MongoDb, que quero que retorne a latitude e longitude do posto de gasolina em questão, na propriedade Dist.Location da minha classe GasStationLocationValueObject.cs
                        { "includeLocs", "dist.location" },
                        //spherical: Informo ao MongoDb, que quero que a consulta em quilômetros seja em forma de raio, e não de forma plana.
                        { "spherical", true},
                    }
                }
            });

            try
            {
                return await _context.Credenciados.Aggregate().AppendStage(pipeline).Limit(raio).ToListAsync();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                
            }
            return new List<CredenciadoLocationValueObject>();
        }

        public async Task UpdateAsync(Credenciado obj)
        {
            var filter = Builders<Credenciado>.Filter.Eq(x => x.Id, obj.Id);

            var updateDefinition = Builders<Credenciado>.Update
                .Set(x => x.Id, obj.Id)
                .Set(x => x.Nome, obj.Nome)
                .Set(x => x.Telefone, obj.Telefone)
                .Set(x => x.Email, obj.Email)
                .Set(x => x.Formacoes, obj.Formacoes)
                .Set(x => x.Especialidades, obj.Especialidades)
                .Set(x => x.Atuacoes, obj.Atuacoes)
                .Set(x => x.Servicos, obj.Servicos)
                .Set(x => x.Location, obj.Location);

            await _context.Credenciados.UpdateOneAsync(filter, updateDefinition);
        }
    }
}
