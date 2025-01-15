using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaxCalculator.Server.Services;

namespace TaxCalculator.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxCalculatorController : ControllerBase
    {
        public ITaxCalculatorService _taxCalculatorService;
        public TaxCalculatorController(ITaxCalculatorService taxCalculatorService)
        {
            _taxCalculatorService = taxCalculatorService;
        }
        [HttpGet("{salary}")]
        public async Task<IActionResult> CalculateTax(int salary)
        {
            try
            {
                var taxCalculationResult = await _taxCalculatorService.CalculateTotalTax(salary);
                return Ok(taxCalculationResult);
            }
            catch (Exception ex)
            {
                //TODO: This needs a logger to log the error
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            
        }
    }
}
