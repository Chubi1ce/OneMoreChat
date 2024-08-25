using App.Contracts;
using System.Net;

namespace Infrastructure.Provider
{
    public record ReceiveResult(IPEndPoint EndPoint, Message? Message);

}
