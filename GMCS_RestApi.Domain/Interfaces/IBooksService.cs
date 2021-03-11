using GMCS_RestApi.Domain.Commands;
using GMCS_RestApi.Domain.Models;
using GMCS_RestApi.Domain.Queries;
using System.Threading.Tasks;

namespace GMCS_RestApi.Domain.Interfaces
{
    public interface IBooksService
    {
        /// <summary>
        /// Сменяет статус книги на "В наличии"
        /// </summary>
        /// <param name="bookId"></param>
        Task ChangeStateToInStockAsync(int bookId);

        /// <summary>
        /// Сменяет статус книги на "продана"
        /// </summary>
        /// <param name="bookId"></param>
        Task ChangeStateToSoldAsync(int bookId);

        /// <summary>
        /// Добавляет запись о новой книге
        /// </summary>
        /// <param name="bookCommand"></param>
        Task<int> CreateBookAsync(CreateBookCommand bookCommand);

        /// <summary>
        /// Удаляет книгу
        /// </summary>
        /// <param name="bookQuery"></param>
        Task<Book> RemoveBookAsync(BookQuery bookQuery);
    }
}
