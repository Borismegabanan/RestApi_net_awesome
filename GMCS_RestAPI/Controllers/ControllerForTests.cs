using AutoMapper;
using GMCS_RestApi.Domain.Common;
using GMCS_RestApi.Domain.Interfaces;
using GMCS_RestAPI.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMCS_RestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControllerForTests : ControllerBase
    {
        private readonly IRabbitMessagesProvider _messagesProvider;
        private readonly IBooksProvider _booksProvider;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public ControllerForTests(IRabbitMessagesProvider messagesProvider, IBooksProvider booksProvider, IMapper mapper, IDistributedCache cache)
        {
            this._messagesProvider = messagesProvider;
            _booksProvider = booksProvider;
            _mapper = mapper;
            _cache = cache;
        }

        /// <summary>
        /// Тест : отправка сообщения на сервис
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("TestSendMessage")]
        public ActionResult SendMessageToQueue(string message)
        {
            _messagesProvider.SendMessageToQueueAsync(message);
            return Ok(message);
        }

        /// <summary>
        /// Получение случайной книги. Ускоренно кэшированием через Redis
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TestRandomBook")]
        public async Task<string> GetRandomBookNameAsync()
        {
            var rand = new Random();
            var key = "RandomBook_" + DateTime.Now.ToString("MMdd_hhmm");
            var isCache = true;

            IEnumerable<ReadModelBook> booksList = await _cache.GetRecordAsync<IEnumerable<ReadModelBook>>(key);

            if (booksList == null)
            {
                booksList = (await _booksProvider.GetAllBooksAsync()).ToList();
                isCache = false;
                await _cache.SetRecordAsync<IEnumerable<ReadModelBook>>(key, booksList);
            }

            var readModelBooks = booksList as ReadModelBook[] ?? booksList.ToArray();
            var sourceText = isCache ? "From cache" : "From provider";
            return $"{sourceText} \r\n {readModelBooks[rand.Next(0, readModelBooks.Length)].Name}";
        }
    }
}
