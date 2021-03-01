using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GMCS_RestAPI.Contracts.Response;
using GMCS_RestAPI.Controllers;
using GMCS_RestApi.Domain.Contexts;
using GMCS_RestApi.Domain.Contexts.Tools;
using GMCS_RestApi.Domain.Interfaces;
using GMCS_RestApi.Domain.Providers;
using GMCS_RestApi.Domain.Services;
using GMCS_RestAPI.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GMCS_RestApi.UnitTests
{
    public class BooksControllerTest
    {
        private static readonly IMapper Mapper =
            new MapperConfiguration(mc => mc.AddProfile(new MappingProfile())).CreateMapper();
        private static readonly ApplicationContext TestContext = new TestDbContext();

        private static readonly IBooksProvider BooksProvider = new BooksProvider(TestContext);
        private static readonly IBooksService BooksService = new BooksService(TestContext, Mapper);

        public BooksControllerTest()
        {
            ContextInitializer.InitDataBase(TestContext);
        }

        [Fact]
        public async Task GetAllBooksTest()
        {
            var controller = new BooksController(BooksProvider, BooksService, Mapper);
            var actionResult = await controller.GetAllBooksAsync();
            var objectResult = (ObjectResult)actionResult.Result;
            var models = (IEnumerable<BookDisplayModel>)objectResult.Value;

            Assert.Equal(models.Count(), TestContext.Books.Count());
        }

        [Fact]
        public async Task GetBooksByNameTest()
        {
            var firstName = (await TestContext.Books.FirstOrDefaultAsync()).Name;
            var partOfName = firstName.Remove(3);

            var controller = new BooksController(BooksProvider, BooksService, Mapper);
            var actionResult = await controller.GetBooksByNameAsync(partOfName);
            var objectResult = (ObjectResult)actionResult.Result;
            var models = (IEnumerable<BookDisplayModel>)objectResult.Value;
            var model = models.FirstOrDefault();

            Assert.True(model != null && model.Name == firstName);
        }

        [Fact]
        public async Task GetBooksByMetadataTest()
        {
            var author = (await TestContext.Authors.FirstOrDefaultAsync());
            var authorMetadata = author.Name;

            var controller = new BooksController(BooksProvider, BooksService, Mapper);
            var actionResult = await controller.GetBooksFromMetadataAsync(authorMetadata);
            var objectResult = (ObjectResult)actionResult.Result;
            var models = (IEnumerable<BookDisplayModel>)objectResult.Value;

            var books = TestContext.Books.Where(x => x.AuthorId == author.Id);
            //authors can have same names
            Assert.True(models.Count() >= books.Count());
            //separated name from fullname, to check
            Assert.True(models.All(x => x.Author.Split(' ')[1] == authorMetadata));
        }

        public async Task ChangeBookStateToInStockTest()
        {

        }
        public async Task ChangeBookStateToSoldTest()
        {

        }
        public async Task CreateBookTest()
        {

        }

        public async Task RemoveBookByIdTest()
        {

        }

    }
