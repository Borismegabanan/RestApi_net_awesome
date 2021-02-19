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

        /// <summary>
        /// Получение книг по названию или Имени, или Фамилии или Отчеству автора
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        Task<IEnumerable<CBook>> GetBooksByMetadata(string metadata);

        /// <summary>
        /// Получение книги по индефикатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Book> GetBookById(int id);

        /// <summary>
        /// Показывает есть ли запись в Бд об авторе книги
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>
        Task<bool> IsBookAuthorExist(int authorId);
        
        /// <summary>
        /// Получение книг об индефикатору автора
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>
        Task<List<Book>> GetBooksByAuthorId(int authorId);
    }
}
