using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace GMSC_RestAPI.Infrastructure.ClientBehaviors
{
    public class ClientInspector : IClientMessageInspector
    {
        private readonly ILogger _logger;
        private Stopwatch _stopwatch;


        public ClientInspector(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ClientInspector>();
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            using (var buffer = request.CreateBufferedCopy(int.MaxValue))
            {
                _stopwatch = Stopwatch.StartNew();
                _logger.LogInformation("Service request message: \r\n" + buffer.CreateMessage().ToString());
                request = buffer.CreateMessage();
                return null;
            }
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            using (var buffer = reply.CreateBufferedCopy(int.MaxValue))
            {
                _stopwatch.Stop();
                _logger.LogInformation($"Service response message: \r\n {buffer.CreateMessage().ToString()}\r\n Service Time: {_stopwatch.ElapsedMilliseconds}");

                reply = buffer.CreateMessage();
            }
        }


    }
}