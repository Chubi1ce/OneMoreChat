using App.Contracts;
using Infrastructure.Provider;
using System.Net;

namespace Core
{
    public class ChatClient : ChatBase
    {
        private readonly User _user;
        private readonly IPEndPoint _serverEndPoint;
        private readonly IMessageSource _source;
        private IEnumerable<User> _users = [];


        public ChatClient(string userName, IPEndPoint serverEndPoint, IMessageSource source)
        {
            _user = new User { Name = userName};
            _serverEndPoint = serverEndPoint;
            _source = source;
        }

        public string UserName { get; }

        public override async Task Start()
        {
            var join = new Message { Text = _user.Name, Command = Command.Join };
            await _source.Send(join, _serverEndPoint, CancellationToken);
            
            Task.Run(Listener);

            while (!CancellationToken.IsCancellationRequested)
            {
                string input = (await Console.In.ReadLineAsync()) ?? string.Empty;
                Message message;
                if (input.Trim().Equals("/exit", StringComparison.CurrentCultureIgnoreCase))
                {
                    message = new() {SenderId = _user.Id, Command = Command.Exit };
                }
                else
                {
                    message = new() { Text = input, SenderId = _user.Id, Command = Command.None};
                }

                await _source.Send(message, _serverEndPoint, CancellationToken);
            }
        }


        protected override async Task Listener()
        {
            while (!CancellationToken.IsCancellationRequested)
            {
                try
                {
                    ReceiveResult result = await _source.Receive(CancellationToken);
                    if (result.Message is null)
                    {
                        throw new Exception("Message is null");
                    }

                    if (result.Message.Command == Command.Join)
                    {
                        JoinHandler(result.Message);
                    }
                    else if (result.Message.Command == Command.Users)
                    {
                        UsersHandler(result.Message);
                    }
                    else if (result.Message.Command == Command.None)
                    {
                        MessageHandler(result.Message);
                    }
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }
            }
        }

        private void MessageHandler(Message message)
        {
            Console.WriteLine($"{_users.First(u => u.Id == message.SenderId)}: {message.Text}");
        }

        private void UsersHandler(Message message)
        {
            _users = message.Users;
        }

        private void JoinHandler(Message message)
        {
            _user.Id = message.RecipientId;
            Console.WriteLine("Join success");
        }
    }
}
