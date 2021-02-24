using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GMCS_RestAPI.Contracts.Response;
using GMCS_RestApi.Domain.Models;
using GMCS_RestApi.Domain.Providers;
using GMCS_RestApi.Domain.Services;

namespace GMCS_RestAPI.Controllers
{

	[ApiController]
	[Route("[controller]")]
	public class AuthorsController : Controller
	{
		private readonly IAuthorsProvider _authorsProvider;
		private readonly IAuthorsService _authorsService;
		private readonly IMapper _mapper;

		public AuthorsController(IMapper mapper, IAuthorsProvider authorsProvider, IAuthorsService authorsService)
		{
			_authorsProvider = authorsProvider;
			_authorsService = authorsService;
			_mapper = mapper;
		}

		/// <summary>
		/// Получение списка авторов.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<AuthorResponse>>> Get()
		{
			var authors = await _authorsProvider.GetAllAuthorsAsync();

			return new ObjectResult( _mapper.Map<IEnumerable<AuthorResponse>>(authors));
		}

		/// <summary>
		/// Получение автора по его имени.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		[HttpGet("{name}")]
		public async Task<ActionResult<IEnumerable<AuthorResponse>>> Get(string name)
		{
			var authors = await _authorsProvider.GetAllAuthorsAsync(name);
			if (!authors.Any())
			{
				return NotFound();
			}

			return new ObjectResult(_mapper.Map<IEnumerable<AuthorResponse>>(authors));
		}

		/// <summary>
		/// Создание автора.
		/// </summary>
		/// <param name="author"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult<AuthorResponse>> Post(Author author)
		{
			if (author == null)
			{
				return BadRequest();
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			author.FullName ??= $"{author.Surname} {author.Name} {author.MiddleName}";

			await _authorsService.AddAsync(author);

			return Ok(author);
		}

		/// <summary>
		/// Удаление автора и всех его книг.
		/// </summary>
		/// <param name="authorId"></param>
		/// <returns></returns>
		[HttpDelete]
		public async Task<ActionResult<AuthorResponse>> Delete(int authorId)
		{
			var author = _authorsProvider.GetTheAuthorAsync(authorId).Result;

			if (author == null)
			{
				return NotFound("не найден автор");
			}

			await _authorsService.RemoveAsync(author);

			return Ok(_mapper.Map<AuthorResponse>(author));
		}
	}
}
