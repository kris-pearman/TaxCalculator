using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaxCalculator.Server.Services;

namespace TaxCalculator.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //PaySummaryController?
    public class TaxCalculatorController : ControllerBase
    {
        public ITaxCalculatorService _taxCalculatorService;
        private readonly ILogger<TaxCalculatorController> _logger;

        public TaxCalculatorController(ITaxCalculatorService taxCalculatorService, ILogger<TaxCalculatorController> logger)
        {
            _taxCalculatorService = taxCalculatorService;
            _logger = logger;
        }

        [HttpGet("{salary}")]
        //Add in HTTP Response codes here
        //SummarisePay?
        public async Task<IActionResult> CalculateTax(int salary)
        {
            try
            {
                if (salary < 0)
                {
                    _logger.LogWarning("Negative salary received by API. Return Bad Request");
                    return BadRequest();
                }
                var taxCalculationResult = await _taxCalculatorService.CalculateTotalTax(salary);
                return Ok(taxCalculationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error ocurred whilst calculating tax. " + ex.ToString(), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            
        }
    }
}
