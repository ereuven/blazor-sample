using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Talent.Services
{
    public interface ITalentService
    {
        Task<IList<Data.Talent>> GetTalents(FilterDefinition<Data.Talent> filter, int startIndex = 0, int count = 100, CancellationToken cancellationToken = default);
        Task<long> CountTalents(FilterDefinition<Data.Talent> filter, CancellationToken cancellationToken=default);
    }

    public class TalentService : ITalentService
    {
        private MongoDBService _mongo;

        public TalentService(MongoDBService mongo)
        {
            _mongo = mongo;
        }

        public async Task<long> CountTalents(FilterDefinition<Data.Talent> filter, CancellationToken cancellationToken)
        {
            var count = await _mongo.Talents.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
            return count;
        }

        public async Task<IList<Data.Talent>> GetTalents(FilterDefinition<Data.Talent> filter, int startIndex, int count, CancellationToken cancellationToken)
        {
            var result = await _mongo.Talents.FindAsync(filter, new FindOptions<Data.Talent, Data.Talent>
            {
                Skip = startIndex,
                Limit = count,
                Projection = _mongo.Talents.Projection().Exclude(t => t.Comments)
            }, cancellationToken);


            return await result.ToListAsync(cancellationToken);
        }
    }
}