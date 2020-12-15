using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoqProject.Api.Services;

namespace MoqProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CepController : ControllerBase
    {
        private readonly ILogger<CepController> _logger;
        private readonly ICepService _cepService;

        public CepController(ILogger<CepController> logger, ICepService cepService)
        {
            _logger = logger;
            _cepService = cepService;
        }

        [HttpGet("number")]
        public async Task<IActionResult> Get([FromQuery] string number)
            => Ok(await _cepService.FindByCepAsync(number));
    }
}