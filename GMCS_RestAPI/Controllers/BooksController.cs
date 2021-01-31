using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMCS_RestAPI.Classes;
using GMCS_RestAPI.Contexts;
using GMCS_RestAPI.Models;
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
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Book>>> Get()
		{
			//var books= await _context.Books.Join(
			//	_context.Authors,
			//	x => x.AuthorId,
			//	z => z.Id,
			//	(book, author) => new CBook()
			//	{
			//		Id = book.Id,
			//		Name = book.Name,
			//		Author = author.FullName,
			//		InitDate = book.InitDate,
			//		PublishDate = book.PublishDate,
			//		WhoChanged = book.WhoChanged
			//	}).ToListAsync();

			var books = await _context.Books.Include(x => x.Author).ToListAsync();

			return books;
		}

		[HttpGet("{name}")]
		public async Task<ActionResult<IEnumerable<Book>>> Get(string name)
		{
			var books = await _context.Books.Where(x => x.Name.Contains(name)).ToListAsync();
			if (books == null)
			{
				return NotFound();
			}

			return new ObjectResult(books);
		}

		[HttpGet]
		[Route("Search/{metadata}")]
		public async Task<ActionResult<IEnumerable<Book>>> GetFromMetadata(string metadata)
		{
			var books = await _context.Books
				.Join(_context.Authors, x => x.Author.Id, z => z.Id,
					(book, author) => new {book, author}).Where(x =>
					x.book.Name == metadata || x.author.Name == metadata ||
					x.author.Surname == metadata || x.author.MiddleName == metadata).Select(x =>
					new CBook()
					{
						Id = x.book.Id,
						Name = x.book.Name,
						Author = x.author.FullName,
						InitDate = x.book.InitDate,
						PublishDate = x.book.PublishDate,
						WhoChanged = x.book.WhoChanged
					}).ToListAsync();

			if (books == null)
			{
				return NotFound();
			}

			return new ObjectResult(books);
		}

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

			_context.Books.Add(book);
			await _context.SaveChangesAsync();
			return Ok(book);
		}

		[HttpDelete]
		public async Task<ActionResult<Book>> Delete(int bookId)
		{
			var book = _context.Books.FirstOrDefault(x => x.Id == bookId);
			if (book == null)
			{
				return NotFound("не найден автор");
			}

			var isAuthorFound = _context.Authors.Any(x => x.Id == book.Author.Id);

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