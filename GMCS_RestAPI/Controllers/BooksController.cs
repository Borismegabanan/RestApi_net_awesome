using AutoMapper;
using GMCS_RestApi.Domain.Commands;
using GMCS_RestApi.Domain.Common;
using GMCS_RestApi.Domain.Enums;
using GMCS_RestApi.Domain.Interfaces;
using GMCS_RestAPI.Contracts.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Queries;

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
        public async Task<ActionResult<IEnumerable<BookDisplayModel>>> GetAllBooksAsync()
        {
            return new ObjectResult(_mapper.Map<IEnumerable<BookDisplayModel>>(await _booksProvider.GetAllBooksAsync()));
        }

        /// <summary>
        /// Получение книг по названию.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<BookDisplayModel>>> GetBooksByNameAsync(string name)
        {
            var books = await _booksProvider.GetBooksByNameAsync(name);

            if (!books.Any())
            {
                return NotFound();
            }

            return new ObjectResult(_mapper.Map<IEnumerable<BookDisplayModel>>(books));
        }


        /// <summary>
        /// Получение книг по названию или Имени, или Фамилии или Отчеству автора
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Search/{metadata}")]
        public async Task<ActionResult<IEnumerable<BookDisplayModel>>> GetBooksFromMetadataAsync(string metadata)
        {
            var books = await _booksProvider.GetBooksByMetadataAsync(metadata);

            if (!books.Any())
            {
                return NotFound();
            }

            return new ObjectResult(_mapper.Map<IEnumerable<BookDisplayModel>>(books));
        }

        /// <summary>
        /// Изменение статуса книги на "В наличии"
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeBookStateToInStockAsync")]
        public async Task<ActionResult<BookDisplayModel>> ChangeBookStateToInStockAsync(int bookId)
        {
            var book = await _booksProvider.GetBookReadModelByIdAsync(bookId);

            if (book == null)
            {
                return BadRequest();
            }

            await _booksService.ChangeStateToInStockAsync(book.Id);

            return Ok(_mapper.Map<BookDisplayModel>(book));
        }

        /// <summary>
        /// Изменение статуса книги на "Продано"
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeBookStateToSoldAsync")]
        public async Task<ActionResult<BookDisplayModel>> ChangeBookStateToSoldAsync(int bookId)
        {
            var book = await _booksProvider.GetBookReadModelByIdAsync(bookId);

            if (book == null)
            {
                return BadRequest();
            }

            if (book.BookStateId != (int)BookStates.InStock)
            {
                return BadRequest(DisplayMessages.SoldBookBadRequestErrorMessage);
            }

            await _booksService.ChangeStateToSoldAsync(book.Id);

            return Ok(_mapper.Map<BookDisplayModel>(book));
        }

        /// <summary>
        /// Создание книги
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BookDisplayModel>> CreateBookAsync(CreateBookCommand book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //todo 2 test for create and view
            var newBookId = await _booksService.CreateBookAsync(book);

            var createdBook = await _booksProvider.GetBookReadModelByIdAsync(newBookId);

            return Ok(_mapper.Map<BookDisplayModel>(createdBook));
        }

        /// <summary>
        /// Удаление книги
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<BookDisplayModel>> RemoveBookByIdAsync(int bookId)
        {
            if (!await _booksProvider.IsBookExist(bookId))
            {
                return NotFound(DisplayMessages.BookNotFoundErrorMessage);
            }

            if (!await _booksProvider.IsBookAuthorExistAsync(bookId))
            {
                return NotFound(DisplayMessages.AuthorNotFoundErrorMessage);
            }
            //Todo automapper?
            var bookQuery = new BookQuery() { Id = bookId };

            var removedBook = await _booksService.RemoveBookAsync(bookQuery);

            return Ok(_mapper.Map<BookDisplayModel>(removedBook));
        }
    }
}