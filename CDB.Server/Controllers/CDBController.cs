using CDB.Server.Interfaces;
using CDB.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace CDB.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CDBController(ICDBCalculatorService calculatorService) : ControllerBase
    {
        private readonly ICDBCalculatorService _calculatorService = calculatorService;

        [HttpPost("calcular")]
        public ActionResult<CDBCalculoResponse> Calcular([FromBody] CDBCalculoRequest request)
        {
            var result = _calculatorService.Calcular(request);
            return Ok(result);
        }
    }
}
