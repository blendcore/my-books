using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Repositories;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BooksService _booksService;
        private readonly IServiceBus _serviceBus;
        public BooksController(BooksService booksService, IServiceBus serviceBus)
        {
            _booksService = booksService;
            _serviceBus = serviceBus;
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var allBooks = _booksService.GetAllBooks();
            return Ok(allBooks);
        }

        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById(int id) 
        {
            var book = _booksService.GetBookById(id);
            return Ok(book);
        }

        [HttpPost("add-book-with-authors")]
        public IActionResult AddBook([FromBody]BookVM book) { 
            _booksService.AddBookWithAuthors(book);
            _serviceBus.SendMessageAsync(book);
            return Ok();
        }

        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] BookVM book) 
        { 
            var updatedBook = _booksService.UpdateBooKById(id, book);
            _serviceBus.SendMessageAsync(book);
            return Ok(updatedBook);
        }
        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id) 
        {
            _booksService.DeleteBookById(id);
            return Ok();
        }
    }
}
