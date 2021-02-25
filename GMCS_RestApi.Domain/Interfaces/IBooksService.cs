using System.Threading.Tasks;
using GMCS_RestApi.Domain.Models;

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
        /// <param name="book"></param>
        Task CreateBookAsync(Book book);

        /// <summary>
        /// Удаляет книгу
        /// </summary>
        /// <param name="book"></param>
        Task RemoveBookAsync(Book book);
    }
}
