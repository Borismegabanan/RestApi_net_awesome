using AutoMapper;
using GMCS_RestApi.Domain.Commands;
using GMCS_RestApi.Domain.Common;
using GMCS_RestApi.Domain.Interfaces;
using GMCS_RestApi.Domain.Queries;
using GMCS_RestAPI.Contracts.Request;
using GMCS_RestAPI.Contracts.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<AuthorDisplayModel>>> GetAllAuthorsAsync()
        {
            var authors = await _authorsProvider.GetAllAuthorsAsync();

            return new ObjectResult(_mapper.Map<IEnumerable<AuthorDisplayModel>>(authors));
        }

        /// <summary>
        /// Получение автора по его имени.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<AuthorDisplayModel>>> GetAuthorByNameAsync(string name)
        {
            var authors = await _authorsProvider.GetAllAuthorsAsync(name);
            if (!authors.Any())
            {
                return NotFound();
            }

            return new ObjectResult(_mapper.Map<IEnumerable<AuthorDisplayModel>>(authors));
        }

        /// <summary>
        /// Создание автора.
        /// </summary>
        /// <param name="createAuthor"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AuthorDisplayModel>> CreateAuthorAsync(CreateAuthorRequest createAuthor)
        {
            if (createAuthor == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authorsService.CreateAuthorAsync(_mapper.Map<CreateAuthorCommand>(createAuthor));

            return Ok(_mapper.Map<AuthorDisplayModel>(createAuthor));
        }

        /// <summary>
        /// Удаление автора и всех его книг.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<AuthorDisplayModel>> RemoveAuthorAsync(int id)
        {
            var query = new AuthorQuery() { Id = id };
            if (!await _authorsProvider.IsAuthorExistAsync(query))
            {
                return NotFound(DisplayMessages.AuthorNotFoundErrorMessage);
            }

            var removedAuthor = await _authorsService.RemoveAuthorAsync(query);

            return Ok(_mapper.Map<AuthorDisplayModel>(removedAuthor));
        }
    }
}
