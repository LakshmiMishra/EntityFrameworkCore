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
        [HttpGet]
        public async Task<IActionResult> GetBooksAsync()
        {
            var books = await appDbContext.
                Books.
                Include(x=>x.Author)
               // .Include(x=>x.Language)
                .ToListAsync();//eager loading
            return Ok(books);
        }


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
        public async Task<IActionResult> UpdateBook([FromRoute] int bookid, [FromBody] Book bookupdate)
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
            await appDbContext.Books.Where(book => book.NoOfPages == 20)
                .ExecuteUpdateAsync(book => book
                .SetProperty(book => book.Description, "bulk updated")
                .SetProperty(book => book.LanguageId, 2)
                .SetProperty(book => book.NoOfPages, 100)
             );

            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            //var book = await appDbContext.Books.FindAsync(id);
            //if(book is null)
            //    return NotFound();
            //else
            //{
            //    appDbContext.Remove(book);
            //  var res= await appDbContext.SaveChangesAsync();
            //}
            //Above commented code hits DB twice, first to find the record and second to delete the record. Below code hits DB only once.

            //We can do this in 1 DB call using ExecuteDeleteAsync method.
            //It will delete the record without fetching it first.or by changing the trcakingstate

            // await appDbContext.Books.Where(book => book.Id == id).ExecuteDeleteAsync();
            var book = new Book { Id = id };
            appDbContext.Entry(book).State = EntityState.Deleted;
            await appDbContext.SaveChangesAsync();

            return Ok("Record deleted successfully");
        }

        [HttpDelete("bulkdelete")]
        public async Task<IActionResult> BulkDelete()
        {
            //Bulk delete with 2 DB hits
            //var books = await appDbContext.Books.Where(b => b.IsActive == false).ToListAsync();
            //appDbContext.Books.RemoveRange(books);// change tracking happens
            //await appDbContext.SaveChangesAsync();

            //Bulk delete with 1 db hit
            await appDbContext.Books.Where(b=>b.NoOfPages== 100).ExecuteDeleteAsync();

            return Ok("Bulk deleted successfully with 1 Db hit");
        }
    }
}
