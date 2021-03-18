using GMCS_RestApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GMCS_RestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControllerForTests : ControllerBase
    {
        private readonly IRabbitMessagesProvider _messagesProvider;

        public ControllerForTests(IRabbitMessagesProvider messagesProvider)
        {
            this._messagesProvider = messagesProvider;
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
    }
}
