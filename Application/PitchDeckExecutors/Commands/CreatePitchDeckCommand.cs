using Application.ImageToMemorySaver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PitchDeckExecutors.Commands
{
    public class CreatePitchDeckCommand
    {
        public List<SavedImageToMemory> Images { get; set; }

        public static CreatePitchDeckCommand Create(List<SavedImageToMemory> images) => new CreatePitchDeckCommand { Images = images };
    }
}
