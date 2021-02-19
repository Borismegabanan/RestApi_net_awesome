using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Classes;
using GMCS_RestApi.Domain.Enums;
using GMCS_RestApi.Domain.Models;
using GMCS_RestApi.Domain.Providers;
using GMCS_RestApi.Domain.Services;

namespace GMCS_RestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksProvider _booksProvider;
        private readonly IBooksService _booksService;

        public BooksController(IBooksProvider booksProvider, IBooksService booksService)
        {
            this._booksProvider = booksProvider;
            this._booksService = booksService;
        }

        /// <summary>
        /// Получение списка всех книг с полным именем автора.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<CBook>> Get()
        {
            return await _booksProvider.GetAllBooksAsync();
        }

        /// <summary>
        /// Получение книг по названию.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Book>>> Get(string name)
        {
            var books = await _booksProvider.GetBooksByNameAsync(name);

            if (!books.Any())
            {
                return NotFound();
            }

            return new ObjectResult(books);
        }


        /// <summary>
        /// Получение книг по названию или Имени, или Фамилии или Отчеству автора
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Search/{metadata}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetFromMetadata(string metadata)
        {
            var books = await _booksProvider.GetBooksByMetadata(metadata);

            if (!books.Any())
            {
                return NotFound();
            }

            return new ObjectResult(books);
        }

        /// <summary>
        /// Изменение статуса книги на "В наличии"
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeStateToInStock")]
        public async Task<ActionResult<Book>> ChangeStateToInStock(int bookId)
        {
            var book = await _booksProvider.GetBookById(bookId);

            if (book == null)
            {
                return BadRequest();
            }

            _booksService.ChangeStateToInStockAsync(book);

            return Ok(book);
        }

        /// <summary>
        /// Изменение статуса книги на "В наличии"
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeStateToSold")]
        public async Task<ActionResult<Book>> ChangeStateToSold(int bookId)
        {
            var book = await _booksProvider.GetBookById(bookId);

            if (book == null)
            {
                return BadRequest();
            }

            if (book.BookStateId != (int) EBookState.InStock)
            {
                return BadRequest("Данной книги нет в налчии");
            }

            _booksService.ChangeStateToSoldAsync(book);

            return Ok(book);
        }

        /// <summary>
        /// Создание книги
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
#pragma warning disable 1998
        public async Task<ActionResult<Book>> Post(Book book)
#pragma warning restore 1998
        {
            if (book == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _booksService.Post(book);

            return Ok(book);
        }

        /// <summary>
        /// Удаление книги
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<Book>> Delete(int bookId)
        {
            var book = await _booksProvider.GetBookById(bookId);

            if (book == null)
            {
                return NotFound("не найдена книга");
            }

            var isAuthorFound = await _booksProvider.IsBookAuthorExist(book.AuthorId);

            if (!isAuthorFound)
            {
                return NotFound("не найден автор");
            }

            _booksService.DeleteAsync(book);

            return Ok(book);
        }
    }
}