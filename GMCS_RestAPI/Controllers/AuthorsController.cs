using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GMCS_RestAPI.Contracts.Request;
using GMCS_RestAPI.Contracts.Response;
using GMCS_RestApi.Domain.Commands;
using GMCS_RestApi.Domain.Common;
using GMCS_RestApi.Domain.Interfaces;
using GMCS_RestApi.Domain.Models;

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
        public async Task<ActionResult<IEnumerable<AuthorModel>>> Get()
        {
            var authors = await _authorsProvider.GetAllAuthorsAsync();

            return new ObjectResult(_mapper.Map<IEnumerable<AuthorModel>>(authors));
        }

        /// <summary>
        /// Получение автора по его имени.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<AuthorModel>>> Get(string name)
        {
            var authors = await _authorsProvider.GetAllAuthorsAsync(name);
            if (!authors.Any())
            {
                return NotFound();
            }

            return new ObjectResult(_mapper.Map<IEnumerable<AuthorModel>>(authors));
        }

        /// <summary>
        /// Создание автора.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AuthorRequest>> CreateAuthorAsync(AuthorRequest author)
        {
            if (author == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authorsService.CreateAuthorAsync(_mapper.Map<CreateAuthorCommand>(author));

            return Ok(author);
        }

        /// <summary>
        /// Удаление автора и всех его книг.
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<AuthorModel>> DeleteAuthorAsync(int authorId)
        {
            var author = _authorsProvider.GetTheAuthorAsync(authorId).Result;

            if (author == null)
            {
                return NotFound(DisplayMessages.AuthorNotFoundErrorMessage);
            }

            await _authorsService.RemoveAuthorAsync(author);

            return Ok(_mapper.Map<AuthorModel>(author));
        }
    }
}
