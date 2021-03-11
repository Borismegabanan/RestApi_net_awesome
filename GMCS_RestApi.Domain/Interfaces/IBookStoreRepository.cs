using GMCS_RestApi.Domain.Commands;
using System.Threading.Tasks;

namespace GMCS_RestApi.Domain.Interfaces
{
    //ANSWER: должен наследоваться от IBookStore?
    public interface IBookStoreRepository
    {
        /// <summary>
        /// Создаёт экземпляр книги в БД.
        /// </summary>
        /// <param name="createBookRequest">экземпляр книги</param>
        /// <returns></returns>
        Task<BookDisplayServiceResponse> CreateBookAsync(CreateBookServiceRequest createBookRequest);

        /// <summary>
        /// Удаляет книгу.
        /// </summary>
        /// <param name="removeBookRequest"></param>
        /// <returns></returns>
        Task<BookDisplayServiceResponse> RemoveBookAsync(RemoveBookServiceRequest removeBookRequest);
    }
}