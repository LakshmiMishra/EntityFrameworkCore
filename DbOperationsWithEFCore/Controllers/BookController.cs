using DbOperationsWithEFCore.Data;
using DbOperationsWithEFCore.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DbOperationsWithEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(AppDbContext appDbContext) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddNewBook([FromBody] Book book)
        {
            await appDbContext.Books.AddAsync(book);
            await appDbContext.SaveChangesAsync();
            return Ok(book);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> BulkInsertBooks([FromBody] List<Book> books)
        {
            await appDbContext.AddRangeAsync(books);
            await appDbContext.SaveChangesAsync();
            return Ok(books);
        }

    }
}
