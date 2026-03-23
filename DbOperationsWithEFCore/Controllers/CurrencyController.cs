using DbOperationsWithEFCore.Data;
using DbOperationsWithEFCore.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DbOperationsWithEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        public readonly AppDbContext _appDbContext;
        public CurrencyController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }

        [HttpGet]
        public async Task<IActionResult> GetCurrencies()
        {
            var currencies = await _appDbContext.Currencies.ToListAsync();
            return Ok(currencies);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCurrencyById([FromRoute] int id)
        {
            var currencyDetails = await _appDbContext.Currencies.FindAsync(id);//finding data by using primary key
            if (currencyDetails == null)
                return NotFound("Currency Do Not Exists");
            else
                return Ok(currencyDetails);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCurrencyByName([FromRoute] string name, [FromQuery] string? description)
        {
            // var currencyDetails = await _appDbContext.Currencies.Where(cur => cur.Title == name).FirstOrDefaultAsync();
            // var currencyDetails = await _appDbContext.Currencies.FirstOrDefaultAsync(cur => cur.Title == name);
            //   var currencyDetails = await _appDbContext.Currencies.SingleOrDefaultAsync(cur => cur.Title == name && cur.Description == description);
            var currencyDetails = await _appDbContext.Currencies.
                FirstOrDefaultAsync(cur => cur.Title == name
                && (string.IsNullOrEmpty(description) || cur.Description == description));
            if (currencyDetails == null)
                return NotFound("Currency Do Not Exists");
            else
                return Ok(currencyDetails);
        }
        [HttpPost("all")]
        public async Task<IActionResult> GetCurrencyByIds([FromBody]List<int> Ids)
        {
            // var currencyDetails = await _appDbContext.Currencies.Where(cur => cur.Title == name).FirstOrDefaultAsync();
            // var currencyDetails = await _appDbContext.Currencies.FirstOrDefaultAsync(cur => cur.Title == name);
            //   var currencyDetails = await _appDbContext.Currencies.SingleOrDefaultAsync(cur => cur.Title == name && cur.Description == description);
            var currencies = await _appDbContext.Currencies
                .Where(cur => Ids.Contains(cur.Id))
                .Select(cur=>new Currency
                {
                    Id = cur.Id,
                    Title = cur.Title
                }).ToListAsync();
            if (currencies == null)
                return NotFound("Currency Ids Do Not Exists");
            else
                return Ok(currencies);
        }
    }
}
