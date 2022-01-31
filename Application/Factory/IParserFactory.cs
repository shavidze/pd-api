using Application.AbstractParser;
using Microsoft.AspNetCore.Http;

namespace Application.Factory
{
    public interface IParserFactory
    {
        IParser GetParser(IFormFile file);
    }
}
