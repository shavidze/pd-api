using Application.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PitchDeckBack.Controllers
{
    public class BaseApiController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public BaseApiController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task<IActionResult> DispatcheCommandAsync<T>(T command) where T : class
        {
            var commandResult = await _dispatcher.DispatcheCommandAsync(command);

            if (commandResult.IsSucceeded)
                return Ok(commandResult);

            return BadRequest(commandResult);
        }

        public async Task<IActionResult> DispatcheQueryAsync<TQuery, TResult>(TQuery query)
        {
            try
            {
                var queryResult = await _dispatcher.DispatcheQueryAsync<TQuery, TResult>(query);

                return Ok(queryResult);

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
