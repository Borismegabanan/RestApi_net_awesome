using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GMCS_RestAPI.Contracts.Response;
using GMCS_RestAPI.Controllers;
using GMCS_RestApi.Domain.Models;
using GMCS_RestApi.Domain.Providers;
using GMCS_RestApi.Domain.Services;
using GMCS_RestAPI.Mapping;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GMCS_RestApi.UnitTests
{
    public class AuthorsTest
    {

        private static readonly IMapper _mapper = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile())).CreateMapper();
        private static readonly Mock<IAuthorsService> AuthorServiceMock = new Mock<IAuthorsService>();
        private static readonly Mock<IAuthorsProvider> AuthorProviderMock = new Mock<IAuthorsProvider>();


        /// <summary>
        /// Инициализация контроддера для метода GETAllAuthors
        /// </summary>
        /// <returns></returns>
        private async Task<ActionResult<IEnumerable<AuthorResponse>>> ActForAuthorsGet()
        {
            AuthorProviderMock.Setup(x => x.GetAllAuthorsAsync(null)).Returns(GetTestAuthors());
            var controller = new AuthorsController(_mapper, AuthorProviderMock.Object, AuthorServiceMock.Object);
            
            return await controller.Get();
        }

        /// <summary>
        /// Тестирование на выдаваемое количество
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAuthors_ValueCountTestAsync()
        {
            var actionResult = await ActForAuthorsGet();

            var objectResult = (ObjectResult)actionResult.Result;

            Assert.Equal(GetTestAuthors().Result.Count(), ((IEnumerable<AuthorResponse>)objectResult.Value).Count());
        }

        /// <summary>
        /// Тест на тип
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAuthors_TypeTestAsync()
        {
            var actionResult = await ActForAuthorsGet();

            var objectResult = (ObjectResult)actionResult.Result;

            Assert.IsAssignableFrom<IEnumerable<AuthorResponse>>(objectResult.Value);
        }
        
        /// <summary>
        /// Тест на выдачу значения
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAuthors_NotNullTestAsync()
        {
            var actionResult = await ActForAuthorsGet();

             Assert.NotNull(actionResult);
        }

#pragma warning disable 1998
        private async Task<IEnumerable<Author>> GetTestAuthors()
#pragma warning restore 1998
        {
            return new List<Author>
            {
                new Author()
                {
                    BirthDate = new DateTime(1832, 1, 27),
                    Name = "Carroll",
                    MiddleName = "mid",
                    Surname = "Lewis",
                    FullName = "Lewis Carroll"
                },
                new Author()
                {
                    BirthDate = new DateTime(1998, 6, 23),
                    Name = "Борис",
                    MiddleName = "Валентинович",
                    Surname = "Красильников",
                    FullName = "Lewis Carroll"
                }
            };
        }
    }
}
