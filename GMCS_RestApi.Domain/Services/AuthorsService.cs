using GMCS_RestApi.Domain.Contexts;
using GMCS_RestApi.Domain.Models;
using GMCS_RestApi.Domain.Providers;

namespace GMCS_RestApi.Domain.Services
{
    public class AuthorsService : IAuthorsService
    {

        private readonly ApplicationContext _applicationContext;
        private readonly IBooksProvider _booksProvider;

        public AuthorsService(ApplicationContext applicationContext, IBooksProvider booksProvider)
        {
            _applicationContext = applicationContext;
            _booksProvider = booksProvider;
        }

        public async void Post(Author author)
        {
            await _applicationContext.Authors.AddAsync(author);
            await _applicationContext.SaveChangesAsync();
        }

        public async void Delete(Author author)
        {
            _applicationContext.Authors.Remove(author);
            //TODO: Спорный момент, такая логика должна храниться в сервисе или в контроллере?
            var bookToRemove = await _booksProvider.GetBooksByAuthorId(author.Id);

            _applicationContext.RemoveRange(bookToRemove);

            await _applicationContext.SaveChangesAsync();
        }
    }
}