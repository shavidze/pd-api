using Application.PitchDeckExecutors;
using Application.PitchDeckExecutors.Queries;
using Application.PitchDeckProcessor;
using Application.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace PitchDeckBack.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PitchDeckController : BaseApiController
    {
        private readonly IPitchDeckProccessor _pitchDeckProccessor;

        public PitchDeckController(IPitchDeckProccessor pitchDeckProccessor,
                                   IDispatcher dispatcher) : base(dispatcher)
        {
            _pitchDeckProccessor = pitchDeckProccessor;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync([FromForm] IFormFile file)
        {
            try
            {
                var proccessResult = await _pitchDeckProccessor.ProccessAsync(file);

                if (proccessResult.IsSucceeded)
                    return Ok(proccessResult);

                return BadRequest(proccessResult);
            }
            catch (System.Exception ex)
            {                
                return BadRequest(CommandResult.Error(ex.Message));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return await DispatcheQueryAsync<PitchDeckQuery, PitchDeckQueryResult>(new PitchDeckQuery());
        }
    }
}
