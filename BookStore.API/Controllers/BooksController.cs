using BookStore.API.Contracts;
using BookStore.Application.Applicatoin;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }
        [HttpGet]
        public async Task<ActionResult<List<BookResponse>>> GetBook()
        {
            var book = await bookService.GetAllBooks();

            var response = book.Select(b => new BookResponse(b.Id, b.Title, b.Description, b.Price));

            return Ok(response);
        }
    }
}
