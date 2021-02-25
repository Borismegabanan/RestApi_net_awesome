using System.Collections.Generic;
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Models;

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
    }
}
