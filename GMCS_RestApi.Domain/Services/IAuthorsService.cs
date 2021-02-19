
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Models;

namespace GMCS_RestApi.Domain.Services
{
    public interface IAuthorsService
    {
        /// <summary>
        /// Добавляет запись о новом авторе
        /// </summary>
        /// <param name="author"></param>
        void Post(Author author);
        
        /// <summary>
        /// Удаляет автора и все его книги
        /// </summary>
        /// <param name="author"></param>
        void Delete(Author author);

    }
}
