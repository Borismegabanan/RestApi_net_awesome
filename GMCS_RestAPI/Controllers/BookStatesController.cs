using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Contexts;
using GMCS_RestApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GMCS_RestAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BookStatesController : Controller
	{
		private readonly ApplicationContext _context;

		public BookStatesController(ApplicationContext context)
		{
			_context = context;
		}

		/// <summary>
		///  Получение списка состояний книги.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<BookStateDbRecord>>> Get()
		{
			return await _context.BookStates.ToListAsync();
		}

	}
}
