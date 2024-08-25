using Domain;
using System.Net;
using System.Text.Json.Serialization;

namespace App.Contracts
{
    public record User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime LastOnLine { get; set; } = DateTime.Now;

        [JsonIgnore]
        public IPEndPoint? EndPoint { get; set; }
        public static User FromDomain(UserEntity userEntity) => new ()
        {
            Id = userEntity.Id,
            Name = userEntity.Name,
            LastOnLine = userEntity.LastOnLine,
        };
    }
}
