using Infrastructure.Entities;
using Infrastructure.PitchDeckAppDbContext;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.PitchDeckRepository
{
    public class PitchDeckRepository : IPitchDeckRepository
    {
        private readonly PitchDeckDbContext _dbContext;

        public PitchDeckRepository(PitchDeckDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveAsync(PitchDeck pitchDeck)
        {
            var pitchDeckDb = default(PitchDeckDb);

            if (pitchDeck.Id.HasValue)
            {
                pitchDeckDb = await _dbContext.Set<PitchDeckDb>().FirstOrDefaultAsync(x => x.Id == pitchDeck.Id.Value);
            }

            if (pitchDeckDb == null)
            {
                pitchDeckDb = new PitchDeckDb();

                await _dbContext.Set<PitchDeckDb>().AddAsync(pitchDeckDb);
            }
            else
            {
                pitchDeckDb.Images.Clear();
            }

            pitchDeckDb.Images = pitchDeck.Images.Select(x => new ImageDb
            {
                FullPath = x.Path,
                ImageId = x.ImageId,
                ImageName = x.ImageName,
                ImageType = x.ImageType,
            }).ToList();
        }
    }
}
