using Microsoft.AspNetCore.Http;

namespace Application.RequestModel
{
    public class PdfModel : PitchDeckModel
    {

        public static PdfModel Create(IFormFile file) => new PdfModel { File = file };
    }
}
