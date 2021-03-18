using System.Threading.Tasks;

namespace GMCS_RestApi.Domain.Interfaces
{
    public interface IRabbitMessagesProvider
    {
        Task SendMessageToQueueAsync(string message);
    }
}
