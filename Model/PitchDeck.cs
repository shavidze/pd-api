using System.Collections.Generic;

namespace Model
{
    public class PitchDeck
    {
        public int? Id { get; set; }

        public ICollection<PitchDeckImage> Images { get; set; } = new HashSet<PitchDeckImage>();
    }
}
