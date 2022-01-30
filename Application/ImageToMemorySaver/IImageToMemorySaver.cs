using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Application.ImageToMemorySaver
{
    public interface IImageToMemorySaver
    {
        Task<List<SavedImageToMemory>> SaveAsync(List<Image> images);
    }
}
