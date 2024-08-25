using System.Reflection.Metadata;
using System.Text.Json;

namespace Core
{
    public abstract class ChatBase
    {
        protected CancellationTokenSource CancellationTokenSource { get; set; } = new CancellationTokenSource();
        protected CancellationToken CancellationToken => CancellationTokenSource.Token;
        protected abstract Task Listener();
        public abstract Task Start();
    }
}
