using GMCS_RestApi.Domain.Commands;
using GMCS_RestApi.Domain.Models;
using System.Threading.Tasks;

namespace GMCS_RestApi.Domain.Interfaces
{
    public interface IBooksService
    {
        /// <summary>
        /// Сменяет статус книги на "В наличии"
        /// </summary>
        /// <param name="book"></param>
        Task ChangeStateToInStockAsync(Book book);

        /// <summary>
        /// Сменяет статус книги на "продана"
        /// </summary>
        /// <param name="book"></param>
        Task ChangeStateToSoldAsync(Book book);

        /// <summary>
        /// Добавляет запись о новой книге
        /// </summary>
        /// <param name="bookCommand"></param>
        Task<int> CreateBookAsync(CreateBookCommand bookCommand);

        /// <summary>
        /// Удаляет книгу
        /// </summary>
        /// <param name="book"></param>
        Task RemoveBookAsync(Book book);
    }
}
