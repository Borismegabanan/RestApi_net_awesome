using System;
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

        public async void ChangeStateToInStockAsync(Book book)
        {
            book.BookStateId = (int)EBookState.InStock;

            book.WhoChanged = Environment.UserName;

            _applicationContext.Update(book);

            await _applicationContext.SaveChangesAsync();
        }

        public async void ChangeStateToSoldAsync(Book book)
        {
            book.BookStateId = (int)EBookState.Sold;

            book.WhoChanged = Environment.UserName;

            _applicationContext.Update(book);

            await _applicationContext.SaveChangesAsync();
        }

        public async void Post(Book book)
        {
            await _applicationContext.Books.AddAsync(book);
            await _applicationContext.SaveChangesAsync();
        }

        public async void DeleteAsync(Book book)
        {
            _applicationContext.Books.Remove(book);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
