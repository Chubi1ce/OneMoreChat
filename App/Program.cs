using Core;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Provider;
using System.Net;
using System.Net.Sockets;

namespace App
{
    public class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint serverEndPoint = new (IPAddress.Parse("127.0.0.1"), 12000);
            IMessageSource messageSource;

            if (args.Length == 0)
            {
                //server
                messageSource = new MessageSource(new UdpClient(serverEndPoint));

                var chat = new ChatServer(messageSource, new ChatContext());
                chat.Start();
            }
            else
            {
                //client
                messageSource = new MessageSource(new UdpClient());
                var chat = new ChatClient(args[0], serverEndPoint, messageSource);
                chat.Start();
            }
        }
    }
}
