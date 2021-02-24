using System;
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Classes;
using GMCS_RestApi.Domain.Contexts;
using GMCS_RestApi.Domain.Enums;
using GMCS_RestApi.Domain.Models;

namespace GMCS_RestApi.Domain.Services
{
    public class BooksService : IBooksService
    {

        private readonly ApplicationContext _applicationContext;

        public BooksService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            СStatic.InitDataBase(_applicationContext);
        }

        public async Task ChangeStateToInStockAsync(Book book)
        {
            book.BookStateId = (int)EBookState.InStock;

            book.WhoChanged = Environment.UserName;

            _applicationContext.Update(book);

            await _applicationContext.SaveChangesAsync();
        }

        public async Task ChangeStateToSoldAsync(Book book)
        {
            book.BookStateId = (int)EBookState.Sold;

            book.WhoChanged = Environment.UserName;

            _applicationContext.Update(book);

            await _applicationContext.SaveChangesAsync();
        }

        public async Task AddAsync(Book book)
        {
            await _applicationContext.Books.AddAsync(book);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            _applicationContext.Books.Remove(book);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
