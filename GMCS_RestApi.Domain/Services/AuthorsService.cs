using AutoMapper;
using GMCS_RestApi.Domain.Commands;
using GMCS_RestApi.Domain.Contexts;
using GMCS_RestApi.Domain.Interfaces;
using GMCS_RestApi.Domain.Models;
using GMCS_RestApi.Domain.Queries;
using System.Threading.Tasks;

namespace GMCS_RestApi.Domain.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IBooksProvider _booksProvider;
        private readonly IMapper _mapper;

        public AuthorsService(ApplicationContext applicationContext, IBooksProvider booksProvider, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _booksProvider = booksProvider;
            _mapper = mapper;
        }

        public async Task CreateAuthorAsync(CreateAuthorCommand authorCommand)
        {
            var author = _mapper.Map<Author>(authorCommand);

            await _applicationContext.Authors.AddAsync(author);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Author> RemoveAuthorAsync(AuthorQuery query)
        {
            var authorModel = await _applicationContext.Authors.FindAsync(query.Id);
            _applicationContext.Authors.Remove(authorModel);

            var bookToRemove = await _booksProvider.GetBooksByAuthorIdAsync(query.Id);

            _applicationContext.RemoveRange(bookToRemove);
            await _applicationContext.SaveChangesAsync();

            return authorModel;
        }
    }
}