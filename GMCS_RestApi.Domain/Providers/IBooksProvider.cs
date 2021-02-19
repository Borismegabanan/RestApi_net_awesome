using System.Collections.Generic;
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Classes;
using GMCS_RestApi.Domain.Models;

namespace GMCS_RestApi.Domain.Providers
{
    public interface IBooksProvider
    {
        /// <summary>
        /// Получение списка всех книг с полным именем автора.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CBook>> GetAllBooksAsync();

        /// <summary>
        /// Получение книг по названию.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IEnumerable<Book>> GetBooksByNameAsync(string name);

        Task<IEnumerable<CBook>> GetBooksByMetadata(string metadata);

        Task<Book> GetBookById(int id);

        Task<bool> IsBookAuthorExist(int authorId);

        Task<List<Book>> GetBooksByAuthorId(int authorId);
    }
}
