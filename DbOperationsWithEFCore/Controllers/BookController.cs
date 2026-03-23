using DbOperationsWithEFCore.Data;
using DbOperationsWithEFCore.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPut("{bookid}")]
        public async Task<IActionResult> UpdateBook([FromRoute]int bookid,[FromBody] Book bookupdate)
        {
           // find the book whose valuse need to be update
            var book = await appDbContext.Books.FindAsync(bookid);
            if (book is null)
            {
                return NotFound();
            }
            book.Title = bookupdate.Title;
            book.Description = bookupdate.Description;
            book.NoOfPages = bookupdate.NoOfPages;
            book.IsActive = bookupdate.IsActive;

            //update the book with new values   
            //var book = appDbContext.Books.Update(bookupdate);
            await appDbContext.SaveChangesAsync();
            return Ok(book);

        }


        [HttpPut("bulkupdate")]
        public async Task<IActionResult> BulkUpdate()
        {
            await appDbContext.Books.Where(book=>book.NoOfPages==20)
                .ExecuteUpdateAsync(book => book
                .SetProperty(book => book.Description, "bulk updated")
                .SetProperty(book => book.LanguageId, 2)
                .SetProperty(book=>book.NoOfPages,100)
             );
         
            return Ok();

        }
    }
}
