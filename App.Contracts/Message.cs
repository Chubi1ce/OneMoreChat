using Domain;
using System.Data;

namespace App.Contracts
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int SenderId { get; set; }
        public int RecipientId { get; set; } = -1;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Command Command { get; set; } = Command.None;
        public IEnumerable<User> Users { get; set; } = [];
        public static Message FromDomain(MessageEntity entity) => new Message
        {
            Id = entity.Id,
            SenderId = entity.SenderId,
            RecipientId = entity.RecipientId,
            CreatedAt = entity.CreatedAt
        };
    }

}
