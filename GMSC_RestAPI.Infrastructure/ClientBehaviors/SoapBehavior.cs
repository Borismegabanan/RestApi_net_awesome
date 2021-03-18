using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace GMSC_RestAPI.Infrastructure.ClientBehaviors
{
    /// <summary>
    /// Кастомное поведение для клиента сервисаю
    /// </summary>
    public class SoapBehavior : IEndpointBehavior
    {
        public IClientMessageInspector MessageInspector { get; }

        public SoapBehavior(IClientMessageInspector messageInspector)
        {
            MessageInspector = messageInspector;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(MessageInspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
