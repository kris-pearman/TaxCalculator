using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaxCalculator.Server.Services;

namespace TaxCalculator.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaySummaryController : ControllerBase
    {
        public IPaySummaryService _paySummaryService;
        private readonly ILogger<PaySummaryController> _logger;

        public PaySummaryController(IPaySummaryService paySummaryService, ILogger<PaySummaryController> logger)
        {
            _paySummaryService = paySummaryService;
            _logger = logger;
        }

        [HttpGet("{salary}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SummarisePay(int salary)
        {
            try
            {
                if (salary < 0)
                {
                    _logger.LogError("Invalid salary passed in, salary has to be a positive whole number");
                    return BadRequest();
                }
                var result = await _paySummaryService.Create(salary);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error ocurred whilst calculating tax. " + ex.ToString(), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            
        }
    }
}
