using App.Contracts;
using System.Net;

namespace Infrastructure.Provider
{
    public interface IMessageSource
    {
        Task<ReceiveResult> Receive(CancellationToken token);
        Task Send(Message message, IPEndPoint endPoint, CancellationToken cancellationToken);
        //IPEndPoint CreateEndPoint(string addres, int port);
        //IPEndPoint GetServerEnPoint();
    }
}
