using System;
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Contexts;
using GMCS_RestApi.Domain.Contexts.Tools;
using GMCS_RestApi.Domain.Enums;
using GMCS_RestApi.Domain.Interfaces;
using GMCS_RestApi.Domain.Models;

namespace GMCS_RestApi.Domain.Services
{
    public class BooksService : IBooksService
    {

        private readonly ApplicationContext _applicationContext;

        public BooksService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            ContextInitializer.InitDataBase(_applicationContext);
        }

        public async Task ChangeStateToInStockAsync(Book book)
        {
            book.BookStateId = (int)BookStates.InStock;

            book.WhoChanged = Environment.UserName;

            _applicationContext.Update(book);

            await _applicationContext.SaveChangesAsync();
        }

        public async Task ChangeStateToSoldAsync(Book book)
        {
            book.BookStateId = (int)BookStates.Sold;

            book.WhoChanged = Environment.UserName;

            _applicationContext.Update(book);

            await _applicationContext.SaveChangesAsync();
        }

        public async Task CreateBookAsync(Book book)
        {
            await _applicationContext.Books.AddAsync(book);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task RemoveBookAsync(Book book)
        {
            _applicationContext.Books.Remove(book);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
