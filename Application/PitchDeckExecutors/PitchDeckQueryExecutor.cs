using Application.PitchDeckExecutors.Queries;
using Infrastructure.Entities;
using Infrastructure.PitchDeckAppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.PitchDeckExecutors
{
    public class PitchDeckQueryExecutor : IQueryExecutor<PitchDeckQuery, PitchDeckQueryResult>
    {
        private readonly PitchDeckDbContext _dbContext;

        public PitchDeckQueryExecutor(PitchDeckDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PitchDeckQueryResult> ExecuteAsync(PitchDeckQuery query)
        {
            var queryResult = PitchDeckQueryResult.Empty();

            var lastPitchDeck = await _dbContext.Set<PitchDeckDb>()
                                          .Include(x => x.Images)
                                          .OrderBy(x=>x.CreateDate)
                                          .LastOrDefaultAsync();

            if (lastPitchDeck != null && lastPitchDeck.Images.Any())
            {
                queryResult.Id = lastPitchDeck.Id;
                queryResult.Images = lastPitchDeck.Images.Select(x => new ImageItem
                {
                    FullPath = x.FullPath,
                    Id = x.Id
                });
            }

            return queryResult;
        }
    }

    public class PitchDeckQueryResult
    {
        public int Id { get; set; }

        public IEnumerable<ImageItem> Images { get; set; }

        public static PitchDeckQueryResult Empty() => new PitchDeckQueryResult();
    }

    public class ImageItem
    {
        public int Id { get; set; }
        public string FullPath { get; set; }
    }
}