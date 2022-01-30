using Microsoft.AspNetCore.Http;

namespace Application.RequestModel
{
    public class PPtModel : PitchDeckModel
    {
        public static PPtModel Create(IFormFile file) => new PPtModel { File = file };

    }
}
