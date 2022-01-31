using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Application.AbstractParser
{
    public interface IParser
    {
        Task<List<Image>> ParseAsync();
    }
}
