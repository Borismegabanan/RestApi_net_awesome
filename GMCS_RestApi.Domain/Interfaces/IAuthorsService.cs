using System.Threading.Tasks;
using GMCS_RestApi.Domain.Commands;
using GMCS_RestApi.Domain.Models;
using GMCS_RestApi.Domain.Queries;

namespace GMCS_RestApi.Domain.Interfaces
{
    public interface IAuthorsService
    {
        /// <summary>
        /// Добавляет запись о новом авторе
        /// </summary>
        /// <param name="authorCommand"></param>
        Task CreateAuthorAsync(CreateAuthorCommand authorCommand);

        /// <summary>
        /// Удаляет автора и все его книги
        /// </summary>
        /// <param name="query"></param>
        Task<Author> RemoveAuthorAsync(AuthorQuery query);

    }
}
