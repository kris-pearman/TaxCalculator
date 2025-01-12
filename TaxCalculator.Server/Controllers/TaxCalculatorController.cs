using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Server.Models;

namespace TaxCalculator.Server.Controllers
{
    /// <summary>
    /// An API for calculating tax paid for a given salary.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TaxCalculatorController : ControllerBase
    {
        /// <summary>
        /// Gets details of tax calculations for a given salary.
        /// </summary>
        /// <param name="salary">The annual gross salary.</param>
        /// <returns>An object containing annual and monthly gross and net pay as well as tax paid</returns>
        [HttpGet("{salary}")]
        public IActionResult CalculateTax(int salary)
        {
            return Ok(new TaxCalculatorResponse()
            {
                AnnualGrossPay = 0,
                AnnualNetPay = 0,
                MonthlyGrossPay = 0,
                MonthlyNetPay = 0,
            });
        }
    }
}
