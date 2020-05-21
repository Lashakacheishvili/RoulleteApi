using Microsoft.AspNetCore.Mvc;
using RoulleteApi.Common;
using RoulleteApi.Services.Interfaces;
using RoulleteApi.Services.Models;
using System.Threading.Tasks;

namespace RoulleteApi.Controllers
{
    [Route("api/jackpot")]
    [ApiController]
    public class JackpotController : BaseAPiController
    {
        private readonly IJackpotService _jackpotService;

        public JackpotController(IJackpotService jackpotService)
        {
            _jackpotService = jackpotService;
        }

        [HttpGet]
        [ProducesResponseType(404, Type = typeof(ServiceResponse))]
        [ProducesResponseType(200, Type = typeof(ServiceResponse<JackpotModel>))]
        public async Task<IActionResult> GetCurrentJackpotAmount()
            => HandleResult(await _jackpotService.GetAsync());
    }
}
