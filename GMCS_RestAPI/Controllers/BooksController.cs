using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GMCS_RestAPI.Contracts.Response;
using GMCS_RestApi.Domain.Common;
using GMCS_RestApi.Domain.Enums;
using GMCS_RestApi.Domain.Interfaces;
using GMCS_RestApi.Domain.Models;

namespace GMCS_RestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksProvider _booksProvider;
        private readonly IBooksService _booksService;
        private readonly IMapper _mapper;

        public BooksController(IBooksProvider booksProvider, IBooksService booksService, IMapper mapper)
        {
            this._booksProvider = booksProvider;
            this._booksService = booksService;
            this._mapper = mapper;
        }

        /// <summary>
        /// Получение списка всех книг с полным именем автора.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetAllBooksAsync()
        {
            return new ObjectResult(_mapper.Map<IEnumerable<BookModel>>(await _booksProvider.GetAllBooksAsync()));
        }

        /// <summary>
        /// Получение книг по названию.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBooksByNameAsync(string name)
        {
            var books = await _booksProvider.GetBooksByNameAsync(name);

            if (!books.Any())
            {
                return NotFound();
            }

            return new ObjectResult(_mapper.Map<IEnumerable<BookModel>>(books));
        }


        /// <summary>
        /// Получение книг по названию или Имени, или Фамилии или Отчеству автора
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Search/{metadata}")]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBooksFromMetadataAsync(string metadata)
        {
            var books = await _booksProvider.GetBooksByMetadataAsync(metadata);

            if (!books.Any())
            {
                return NotFound();
            }

            return new ObjectResult(_mapper.Map<IEnumerable<BookModel>>(books));
        }

        /// <summary>
        /// Изменение статуса книги на "В наличии"
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeBookStateToInStockAsync")]
        public async Task<ActionResult<Book>> ChangeBookStateToInStockAsync(int bookId)
        {
            var book = await _booksProvider.GetBookByIdAsync(bookId);

            if (book == null)
            {
                return BadRequest();
            }

            await _booksService.ChangeStateToInStockAsync(book);

            return Ok(book);
        }

        /// <summary>
        /// Изменение статуса книги на "В наличии"
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeBookStateToSoldAsync")]
        public async Task<ActionResult<Book>> ChangeBookStateToSoldAsync(int bookId)
        {
            var book = await _booksProvider.GetBookByIdAsync(bookId);

            if (book == null)
            {
                return BadRequest();
            }

            if (book.BookStateId != (int) BookStates.InStock)
            {
                return BadRequest(DisplayMessages.SoldBookBadRequestErrorMessage);
            }

            await _booksService.ChangeStateToSoldAsync(book);

            return Ok(book);
        }

        /// <summary>
        /// Создание книги
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBookAsync(Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _booksService.CreateBookAsync(book);

            return Ok(book);
        }

        /// <summary>
        /// Удаление книги
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<Book>> RemoveBookByIdAsync(int bookId)
        {
            var book = await _booksProvider.GetBookByIdAsync(bookId);

            if (book == null)
            {
                return NotFound(DisplayMessages.BookNotFoundErrorMessage);
            }

            var isAuthorFound = await _booksProvider.IsBookAuthorExistAsync(book.AuthorId);

            if (!isAuthorFound)
            {
                return NotFound(DisplayMessages.AuthorNotFoundErrorMessage);
            }

            await _booksService.RemoveBookAsync(book);

            return Ok(book);
        }
    }
}