using System.Collections.Generic;

namespace Infrastructure.Entities
{
    public class PitchDeckDb : BaseEntity
    {
        public ICollection<ImageDb> Images { get; set; } = new HashSet<ImageDb>();
    }
}
