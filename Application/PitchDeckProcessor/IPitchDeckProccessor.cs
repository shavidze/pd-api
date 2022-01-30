using Application.Shared;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Application.PitchDeckProcessor
{
    public interface IPitchDeckProccessor
    {
        Task<CommandResult> ProccessAsync(IFormFile formFiles);
    }
}
