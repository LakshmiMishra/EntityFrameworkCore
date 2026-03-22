using DbOperationsWithEFCore.Data;
using DbOperationsWithEFCore.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
