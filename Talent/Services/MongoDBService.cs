using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;
using Talent.Configuration;

namespace Talent.Services
{
    public class MongoDBService
    {
        private readonly MongoDBSettings _conf;
        private IMongoClient _client;
        private IMongoDatabase _db;

        public IMongoCollection<Data.Talent> Talents { get; private set; }
        public IMongoCollection<Data.TargetUnit> TargetUnits { get; private set; }

        public MongoDBService(IOptions<MongoDBSettings> conf)
        {
            _conf = conf.Value;
            _client = new MongoClient(_conf.ConnectionString);
            _db = _client.GetDatabase(_conf.Database);

            Talents = _db.GetCollection<Data.Talent>(nameof(Data.Talent).ToLower());
            TargetUnits = _db.GetCollection<Data.TargetUnit>(nameof(Data.TargetUnit).ToLower());
        }

        public void InitDB()
        {
            using var session = _client.StartSession();

            TargetUnits.InsertMany(new[] { new Data.TargetUnit { Department = "d1", UnitNum = "u1" }, new Data.TargetUnit { Department = "d1", UnitNum = "u2" } });
            Talents.InsertMany(new[] {
                new Data.Talent{ FirstName="talent 1", LastName="t1", ResponsibleEmployeeId="77", Phone="050-1234567", TargetUnit="u1"},
                new Data.Talent{ FirstName="talent 2", LastName="t2", ResponsibleEmployeeId="88", Phone="057-4546565", TargetUnit="u2"}
            });
        }
    }

    public static class MongoBuildersExtensions
    {
        public static FilterDefinitionBuilder<TDocument> Filter<TDocument>(this IMongoCollection<TDocument> collection) => Builders<TDocument>.Filter;

        public static IndexKeysDefinitionBuilder<TDocument> IndexKeys<TDocument>(this IMongoCollection<TDocument> collection) => Builders<TDocument>.IndexKeys;

        public static ProjectionDefinitionBuilder<TDocument> Projection<TDocument>(this IMongoCollection<TDocument> collection) => Builders<TDocument>.Projection;

        public static SortDefinitionBuilder<TDocument> Sort<TDocument>(this IMongoCollection<TDocument> collection) => Builders<TDocument>.Sort;

        public static UpdateDefinitionBuilder<TDocument> Update<TDocument>(this IMongoCollection<TDocument> collection) => Builders<TDocument>.Update;
    }
}