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
        Task<(IEnumerable<Data.Talent>, long)> GetTalents(int startIndex = 0, int count = 100, CancellationToken cancellationToken = default);
    }

    public class TalentService : ITalentService
    {
        private MongoDBService _mongo;

        public TalentService(MongoDBService mongo)
        {
            _mongo = mongo;
        }

        public async Task<(IEnumerable<Data.Talent>, long)> GetTalents(int startIndex, int count, CancellationToken cancellationToken)
        {
            var resultTask = _mongo.Talents.FindAsync(_mongo.Talents.Filter().Empty, new FindOptions<Data.Talent, Data.Talent>
            {
                Skip = startIndex,
                Limit = count,
                Projection = _mongo.Talents.Projection().Exclude(t => t.Comments)
            }, cancellationToken);


            return ((await resultTask).ToEnumerable(), await _mongo.Talents.CountDocumentsAsync(_mongo.Talents.Filter().Empty, cancellationToken: cancellationToken));
        }
    }
}