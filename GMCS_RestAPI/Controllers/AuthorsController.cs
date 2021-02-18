using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Contexts;
using GMCS_RestApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GMCS_RestAPI.Controllers
{

	[ApiController]
	[Route("[controller]")]
	public class AuthorsController : Controller
	{
		private readonly ApplicationContext _context;

		public AuthorsController(ApplicationContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Получение списка авторов.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Author>>> Get()
		{
			return await _context.Authors.ToListAsync();
		}

		/// <summary>
		/// Получение автора по его имени.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		[HttpGet("{name}")]
		public async Task<ActionResult<IEnumerable<Author>>> Get(string name)
		{
			var authors = await _context.Authors.Where(x => x.FullName.ToLower().Contains(name.ToLower())).ToListAsync();

			if (!authors.Any())
			{
				return NotFound();
			}

			return new ObjectResult(authors);
		}

		/// <summary>
		/// Создание автора.
		/// </summary>
		/// <param name="author"></param>
		/// <returns></returns>
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

			await _context.Authors.AddAsync(author);
			await _context.SaveChangesAsync();
			return Ok(author);
		}

		/// <summary>
		/// Удаление автора и всех его книг.
		/// </summary>
		/// <param name="authorId"></param>
		/// <returns></returns>
		[HttpDelete]
		public async Task<ActionResult<Author>> Delete(int authorId)
		{
			var author = _context.Authors.FirstOrDefault(x => x.Id == authorId);
			if (author == null)
			{
				return NotFound("не найден автор");
			}
			
			_context.Authors.Remove(author);

			var booksToRemove = _context.Books.Where(x => x.AuthorId == authorId).ToList();

			_context.Books.RemoveRange(booksToRemove);

			await _context.SaveChangesAsync();
			return Ok(author);
		}
	}
}
