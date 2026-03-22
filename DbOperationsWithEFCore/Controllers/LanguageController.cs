using DbOperationsWithEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationsWithEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        public readonly AppDbContext _appDbContext;
        public LanguageController(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetLanguages()
        {
            var languages = await _appDbContext.Languages.ToListAsync();
            return Ok(languages);
        }
    }
}
