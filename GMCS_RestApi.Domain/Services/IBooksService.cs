using GMCS_RestApi.Domain.Models;

namespace GMCS_RestApi.Domain.Services
{
    public interface IBooksService
    {
        /// <summary>
        /// Сменяет статус книги на "В наличии"
        /// </summary>
        /// <param name="book"></param>
        void ChangeStateToInStockAsync(Book book);

        /// <summary>
        /// Сменяет статус книги на "продана"
        /// </summary>
        /// <param name="book"></param>
        void ChangeStateToSoldAsync(Book book);

        /// <summary>
        /// Добавляет запись о новой книге
        /// </summary>
        /// <param name="book"></param>
        void Post(Book book);

        /// <summary>
        /// Удаляет книгу
        /// </summary>
        /// <param name="book"></param>
        void DeleteAsync(Book book);
    }
}
