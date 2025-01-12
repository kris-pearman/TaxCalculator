using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Server.Models;

namespace TaxCalculator.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxCalculatorController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
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
