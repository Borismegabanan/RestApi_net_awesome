using System.Collections.Generic;
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Models;

namespace GMCS_RestApi.Domain.Providers
{
    public interface IAuthorsProvider
    {
        /// <summary>
        /// Получает всех авторов
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IEnumerable<Author>> GetAllAuthorsAsync(string name = null);

        Task<Author> GetTheAuthorAsync(int id);
    }
}
