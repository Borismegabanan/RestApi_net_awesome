using AutoMapper;
using GMCS_RestApi.Domain.Commands;
using GMCS_RestApi.Domain.Contexts;
using GMCS_RestApi.Domain.Contexts.Tools;
using GMCS_RestApi.Domain.Enums;
using GMCS_RestApi.Domain.Interfaces;
using GMCS_RestApi.Domain.Models;
using GMCS_RestApi.Domain.Queries;
using System;
using System.Threading.Tasks;

namespace GMCS_RestApi.Domain.Services
{
    public class BooksService : IBooksService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public BooksService(ApplicationContext applicationContext, IMapper mapper)
        {
            _mapper = mapper;
            _applicationContext = applicationContext;
            ContextInitializer.InitDataBase(_applicationContext);
        }

        public async Task ChangeStateToInStockAsync(int bookId)
        {
            var book = await _applicationContext.Books.FindAsync(bookId);


            book.BookStateId = (int)BookStates.InStock;

            book.WhoChanged = Environment.UserName;

            _applicationContext.Update(book);

            await _applicationContext.SaveChangesAsync();
        }

        public async Task ChangeStateToSoldAsync(int bookId)
        {
            var book = await _applicationContext.Books.FindAsync(bookId);

            book.BookStateId = (int)BookStates.Sold;

            book.WhoChanged = Environment.UserName;

            _applicationContext.Update(book);

            await _applicationContext.SaveChangesAsync();
        }

        public async Task<int> CreateBookAsync(CreateBookCommand bookCommand)
        {
            var book = _mapper.Map<Book>(bookCommand);

            await _applicationContext.Books.AddAsync(book);
            await _applicationContext.SaveChangesAsync();

            return book.Id;
        }

        public async Task<Book> RemoveBookAsync(BookQuery bookQuery)
        {

            var book = await _applicationContext.Books.FindAsync(bookQuery.Id);

            _applicationContext.Books.Remove(book);
            await _applicationContext.SaveChangesAsync();

            return book;
        }
    }
}
