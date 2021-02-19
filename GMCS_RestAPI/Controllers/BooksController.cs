using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Classes;
using GMCS_RestApi.Domain.Contexts;
using GMCS_RestApi.Domain.Enums;
using GMCS_RestApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GMCS_RestAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BooksController : ControllerBase
	{
		private readonly ApplicationContext _context;

		public BooksController(ApplicationContext context)
		{
			_context = context;

			СStatic.InitDataBase(_context);
		}

		/// <summary>
		/// Получение списка всех книг с полным именем автора.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<CBook>>> Get()
		{
			return await _context.Books.Join(_context.BookStates, x=>x.BookStateId, z=>z.Id,(book,state) => new {book,state})
				.Join(
				_context.Authors,
				x => x.book.AuthorId,
				z => z.Id,
				(record, author) => new CBook
				{
					Id = record.book.Id,
					Name = record.book.Name,
					Author = author.FullName,
					BookState = record.state.Name,
					InitDate = record.book.InitDate,
					PublishDate = record.book.PublishDate,
					WhoChanged = record.book.WhoChanged
				}).ToListAsync();
		}

		/// <summary>
		/// Получение книг по названию.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		[HttpGet("{name}")]
		public async Task<ActionResult<IEnumerable<Book>>> Get(string name)
		{
			var books = await _context.Books.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync();
			
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
			metadata = metadata.ToLower();
			var books = await _context.Books
				.Join(_context.Authors, x => x.AuthorId, z => z.Id,
					(book, author) => new {book, author}).Where(x =>
					x.book.Name.ToLower() == metadata || x.author.Name.ToLower() == metadata ||
					x.author.Surname.ToLower() == metadata || x.author.MiddleName.ToLower() == metadata).Select(x =>
					new CBook
					{
						Id = x.book.Id,
						Name = x.book.Name,
						Author = x.author.FullName,
						InitDate = x.book.InitDate,
						PublishDate = x.book.PublishDate,
						WhoChanged = x.book.WhoChanged
					}).ToListAsync();

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
			var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == bookId);

			if (book == null)
			{
				return BadRequest();
			}

			book.BookStateId = (int)EBookState.InStock;

			book.WhoChanged = Environment.UserName;

			_context.Update(book);

			await _context.SaveChangesAsync();

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
			var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == bookId);

			if (book == null)
			{
				return BadRequest();
			}

			if (book.BookStateId != (int)EBookState.InStock)
			{
				return BadRequest("Данной книги нет в налчии");
			}

			book.BookStateId = (int)EBookState.Sold;

			book.WhoChanged = Environment.UserName;

			_context.Update(book);

			await _context.SaveChangesAsync();

			return Ok(book);
		}

		/// <summary>
		/// Создание книги
		/// </summary>
		/// <param name="book"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult<Book>> Post(Book book)
		{
			if (book == null)
			{
				return BadRequest();
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			await _context.Books.AddAsync(book);
			await _context.SaveChangesAsync();
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
			var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == bookId);
			if (book == null)
			{
				return NotFound("не найден автор");
			}

			var isAuthorFound = _context.Authors.Any(x => x.Id == book.AuthorId);

			if (!isAuthorFound)
			{
				return NotFound("не найдена книга");
			}

			_context.Books.Remove(book);

			await _context.SaveChangesAsync();
			return Ok(book);
		}
	}
}