using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMCS_RestAPI.Classes;
using GMCS_RestAPI.Contexts;
using GMCS_RestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GMCS_RestAPI.Controllers
{
	public class SearchController : Controller
	{
		private readonly ApplicationContext _context;

		public SearchController(ApplicationContext context)
		{
			_context = context;
		}

		[HttpGet("{metadata}")]
		public async Task<ActionResult<IEnumerable<Book>>> GetBookFromMetadata(string metadata)
		{
			var books = await _context.Books
				.Join(_context.Authors, x => x.Author.Id, z => z.Id,
					(book, author) => new { book, author }).Where(x =>
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

	}
}
