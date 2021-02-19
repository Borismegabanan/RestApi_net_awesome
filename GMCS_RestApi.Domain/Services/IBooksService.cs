using GMCS_RestApi.Domain.Models;

namespace GMCS_RestApi.Domain.Services
{
    public interface IBooksService
    {
        void ChangeStateToInStockAsync(Book book);

        void ChangeStateToSoldAsync(Book book);

        void Post(Book book);

        void DeleteAsync(Book book);
    }
}
