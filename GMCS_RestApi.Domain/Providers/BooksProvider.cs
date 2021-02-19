using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Classes;
using GMCS_RestApi.Domain.Contexts;
using GMCS_RestApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GMCS_RestApi.Domain.Providers
{
    public class BooksProvider : IBooksProvider
    {
        private readonly ApplicationContext _applicationContext;

        public BooksProvider(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<IEnumerable<CBook>> GetAllBooksAsync()
        {
            return await _applicationContext.Books.Join(_applicationContext.BookStates, x => x.BookStateId, z => z.Id, (book, state) => new { book, state })
                .Join(
                    _applicationContext.Authors,
                    x => x.book.AuthorId,
                    z => z.Id,
                    (record, author) => new CBook
                    {
                        Id = record.book.Id,
                        Name = record.book.Name,
                        Author = author.FullName,
                        BookState = record.state.Name,
                        InitDate = record.book.InitDate,
                        PublishDate = record.book.PublishDate,
                        WhoChanged = record.book.WhoChanged
                    }).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByNameAsync(string name)
        {
            return await _applicationContext.Books.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        public async Task<IEnumerable<CBook>> GetBooksByMetadata(string metadata)
        {
            metadata = metadata.ToLower();

            return await _applicationContext.Books
                .Join(_applicationContext.Authors, x => x.AuthorId, z => z.Id,
                    (book, author) => new {book, author}).Where(x =>
                    x.book.Name.ToLower() == metadata || x.author.Name.ToLower() == metadata ||
                    x.author.Surname.ToLower() == metadata || x.author.MiddleName.ToLower() == metadata).Select(x =>
                    new CBook
                    {
                        Id = x.book.Id,
                        Name = x.book.Name,
                        Author = x.author.FullName,
                        //Todo: Normal Enum
                        BookState = x.book.BookStateId.ToString(),
                        InitDate = x.book.InitDate,
                        PublishDate = x.book.PublishDate,
                        WhoChanged = x.book.WhoChanged
                    }).ToListAsync();
        }

        public async Task<bool> IsBookAuthorExist(int authorId)
        {
            return await _applicationContext.Authors.AnyAsync(x => x.Id == authorId);
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _applicationContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Book>> GetBooksByAuthorId(int authorId)
        {
            return await _applicationContext.Books.Where(x => x.AuthorId == authorId).ToListAsync();
        }
    }
}
