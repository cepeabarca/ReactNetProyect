using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactNetProyect.BackEnd.Data.Models;
using ReactNetProyect.BackEnd.Service;

namespace ReactNetProyect.BackEnd.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetAllCurrenciesAsync()
        {
            var currencies = await _currencyService.GetAllCurrenciesAsync();
            return Ok(currencies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Currency>> GetCurrencyByIdAsync(int id)
        {
            var currency = await _currencyService.GetCurrencyByIdAsync(id);
            if (currency == null)
            {
                return NotFound();
            }
            return Ok(currency);
        }
    }
}
