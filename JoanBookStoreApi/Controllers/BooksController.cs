using JoanBookStoreApi.Model;
using JoanBookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoanBookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        //[HttpGet(Name = "Get all Books")]
        //public ActionResult<List<Book>> Get() =>
        //    _bookService.Get().OrderBy(o => o.Genre).OrderBy(o => o.Author.Split(' ')[1]).OrderBy(o => o.Author.Split(' ')[0]).ToList();

        //[HttpGet("{id:length(24)}", Name = "GetBook")]
        //public ActionResult<Book> Get(string id)
        //{
        //    var book = _bookService.Get(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    return book;
        //}

        [HttpGet]
        public List<Book> Get(string genre = "", String author = "", String title="")
        {
            title = title.Replace("\"", "");
            author = author.Replace("\"", "");
            genre = genre.Replace("\"", "");

            if (genre == "" && author == "" && title == "") return _bookService.Get();

            return _bookService.Filter(genre, author, title);
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _bookService.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{isbn}")]
        public IActionResult Update(string isbn, Book bookIn)
        {
            var book = _bookService.GetByIsbn(isbn);
            //var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            //_bookService.Update(id, bookIn);
            _bookService.UpdateByIsbn(isbn, bookIn);

            return NoContent();
        }

        [HttpPatch("{isbn}")]
        public IActionResult Path(string isbn, Book bookIn)
        {
            var book = _bookService.GetByIsbn(isbn);
            //var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            //_bookService.Update(id, bookIn);
            _bookService.UpdateByIsbn(isbn, bookIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Remove(book.ISBN);

            return NoContent();
        }
    }
}
