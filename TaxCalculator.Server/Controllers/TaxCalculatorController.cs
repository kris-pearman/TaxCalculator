using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Server.Models;
using TaxCalculator.Server.Services;

namespace TaxCalculator.Server.Controllers
{
    /// <summary>
    /// An API for calculating tax paid for a given salary.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TaxCalculatorController : ControllerBase
    {
        public ITaxCalculatorService _taxCalculatorService;
        public TaxCalculatorController(ITaxCalculatorService taxCalculatorService)
        {
            _taxCalculatorService = taxCalculatorService;
        }
        /// <summary>
        /// Gets details of tax calculations for a given salary.
        /// </summary>
        /// <param name="salary">The annual gross salary.</param>
        /// <returns>An object containing annual and monthly gross and net pay as well as tax paid</returns>
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
                throw new NotImplementedException();
            }
            
        }
    }
}
