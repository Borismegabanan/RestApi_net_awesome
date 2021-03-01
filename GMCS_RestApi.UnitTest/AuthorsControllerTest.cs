using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GMCS_RestAPI.Contracts.Request;
using GMCS_RestAPI.Contracts.Response;
using GMCS_RestAPI.Controllers;
using GMCS_RestApi.Domain.Contexts;
using GMCS_RestApi.Domain.Interfaces;
using GMCS_RestApi.Domain.Models;
using GMCS_RestApi.Domain.Providers;
using GMCS_RestApi.Domain.Services;
using GMCS_RestAPI.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GMCS_RestApi.UnitTests
{
    public class AuthorsControllerTest
    {
        private static readonly IMapper Mapper =
            new MapperConfiguration(mc => mc.AddProfile(new MappingProfile())).CreateMapper();
        private static readonly ApplicationContext TestContext = new TestDbContext();

        private static readonly IAuthorsProvider AuthorsProvider = new AuthorsProvider(TestContext);
        private static readonly IAuthorsService AuthorsService = new AuthorsService(TestContext, new BooksProvider(TestContext), Mapper);

        private static readonly IEnumerable<Author> DefaultValues = GetTestValueForTestDb();

        /// <summary>
        /// Тестирование получения авторов
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAuthors_TestAsync()
        {
            var controller = new AuthorsController(Mapper, AuthorsProvider, AuthorsService);
            var actionResult = await controller.GetAllAuthorsAsync();
            var objectResult = (ObjectResult)actionResult.Result;

            Assert.IsAssignableFrom<IEnumerable<AuthorDisplayModel>>(objectResult.Value);
            Assert.NotNull(actionResult);
            Assert.Equal(DefaultValues.Count(), ((IEnumerable<AuthorDisplayModel>)objectResult.Value).Count());
        }


        /// <summary>
        /// Тест на не найденого пользователя
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAuthorsByName_NotFoundResultTestAsync()
        {
            var controller = new AuthorsController(Mapper, AuthorsProvider, AuthorsService);
            var actionResult = await controller.GetAuthorByNameAsync("namename");

            Assert.Equal(new NotFoundResult().StatusCode, ((NotFoundResult)actionResult.Result).StatusCode);

        }

        /// <summary>
        /// Тест на нахождение
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAuthorByName_FoundTestAsync()
        {
            var controller = new AuthorsController(Mapper, AuthorsProvider, AuthorsService);
            var actionResult = await controller.GetAuthorByNameAsync(DefaultValues.First().Name);
            var objectValue = (ObjectResult)actionResult.Result;
            var models = (IEnumerable<AuthorDisplayModel>)objectValue.Value;

            Assert.Equal(DefaultValues.First().FullName, models.First().FullName);
        }

        /// <summary>
        /// тест на добавление автора в БД
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddAuthor_SuccessTestAsync()
        {
            var controller = new AuthorsController(Mapper, AuthorsProvider, AuthorsService);
            var newModel = new CreateAuthorRequest()
            { BirthDate = DateTime.Now, MiddleName = "test", Surname = "testSurname", Name = "testName" };

            var actionResult = await controller.CreateAuthorAsync(Mapper.Map<CreateAuthorRequest>(newModel));
            var objectResult = (ObjectResult)actionResult.Result;
            var model = (AuthorDisplayModel)objectResult.Value;

            Assert.NotNull(model);
        }
        /// <summary>
        /// Тест на удачное удаление
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task RemoveAuthor_SuccessTestAsync()
        {
            var idToDelete = DefaultValues.Count();
            var controller = new AuthorsController(Mapper, AuthorsProvider, AuthorsService);
            var actionResult = await controller.RemoveAuthorAsync(idToDelete);
            var oldModel = (AuthorDisplayModel)((ObjectResult)actionResult.Result).Value;

            Assert.True(oldModel.FullName == DefaultValues.Last().FullName);
            Assert.False(await TestContext.Authors.AnyAsync(x => x.Id == idToDelete));

        }

        //Todo non async. В теории может выбиваться 
        private static IEnumerable<Author> GetTestValueForTestDb()
        {
            var listOfAuthors = new List<Author>()
            {
                new Author()
                {
                    BirthDate = DateTime.Now,
                    Name = "Test",
                    Surname = "SurnameTest",
                    MiddleName = "MiddTest",
                    FullName = "SurnameTest Test MiddTest"
                },
                new Author()
                {
                    BirthDate = DateTime.Now,
                    Name = "Test2",
                    Surname = "SurnameTest2",
                    MiddleName = "MiddTest2",
                    FullName = "SurnameTest2 Test2 MiddTest2"
                }
            };

            TestContext.Authors.AddRange(listOfAuthors);
            TestContext.SaveChanges();

            return listOfAuthors;
        }

    }
}
