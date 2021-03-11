using GMCS_RestApi.Domain.Models;
using GMCS_RestApi.Domain.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GMCS_RestApi.Domain.Interfaces
{
    public interface IAuthorsProvider
    {
        /// <summary>
        /// Получает всех авторов
        /// </summary>
        /// <param name="name">фильтрация по имени</param>
        /// <returns></returns>
        Task<IEnumerable<Author>> GetAllAuthorsAsync(string name = null);

        /// <summary>
        /// Получает автора по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Author> GetTheAuthorAsync(int id);

        /// <summary>
        /// Существует ли автор в БД
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<bool> IsAuthorExistAsync(AuthorQuery query);
    }
}
