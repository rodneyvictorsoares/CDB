using CDB.Server.Interfaces;
using CDB.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace CDB.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CdbController(ICdbCalculatorService calculatorService) : ControllerBase
    {
        private readonly ICdbCalculatorService _calculatorService = calculatorService;

        [HttpPost("calcular")]
        public ActionResult<CdbCalculoResponse> Calcular([FromBody] CdbCalculoRequest request)
        {
            var result = _calculatorService.Calcular(request);
            return Ok(result);
        }
    }
}
