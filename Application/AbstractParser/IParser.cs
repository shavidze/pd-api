using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Application.AbstractParser
{
    public interface IParser<T> where T : class
    {
        Task<List<Image>> ParseAsync(T model);
    }
}
