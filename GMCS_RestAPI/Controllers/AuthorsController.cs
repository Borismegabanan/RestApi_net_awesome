using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMCS_RestAPI.Contexts;
using GMCS_RestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GMCS_RestAPI.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class AuthorsController : Controller
	{
		private readonly ApplicationContext _context;

		public AuthorsController(ApplicationContext context)
		{
			_context = context;
			
		}


		[HttpGet("{name}")]
		public async Task<ActionResult<IEnumerable<Author>>> Get(string name)
		{
			var authors = await _context.Authors.Where(x => x.FullName.Contains(name)).ToListAsync();
			if (authors == null)
			{
				return NotFound();
			}

			return new ObjectResult(authors);
		}

		[HttpPost]
		public async Task<ActionResult<Author>> Post(Author author)
		{
			if (author == null)
			{
				return BadRequest();
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (author.FullName == null)
			{
				author.FullName = $"{author.Surname} {author.Name} {author.MiddleName}";
			}

			_context.Authors.Add(author);
			await _context.SaveChangesAsync();
			return Ok(author);
		}

		[HttpDelete]
		public async Task<ActionResult<Author>> Delete(int authorId)
		{
			var author = _context.Authors.FirstOrDefault(x => x.Id == authorId);
			if (author == null)
			{
				return NotFound("не найден автор");
			}
			
			_context.Authors.Remove(author);

			var booksToRemove = _context.Books.Where(x => x.Author.Id == authorId).ToList();

			_context.Books.RemoveRange(booksToRemove);

			await _context.SaveChangesAsync();
			return Ok(author);
		}

	}
}
