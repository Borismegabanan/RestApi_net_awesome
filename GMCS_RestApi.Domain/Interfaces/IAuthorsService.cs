using System.Threading.Tasks;
using GMCS_RestApi.Domain.Models;

namespace GMCS_RestApi.Domain.Interfaces
{
    public interface IAuthorsService
    {
        /// <summary>
        /// Добавляет запись о новом авторе
        /// </summary>
        /// <param name="author"></param>
        Task CreateAuthorAsync(Author author);
        
        /// <summary>
        /// Удаляет автора и все его книги
        /// </summary>
        /// <param name="author"></param>
        Task RemoveAuthorAsync(Author author);

    }
}
