using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class UserEntity
    {
        [Key]public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime LastOnLine { get; set; }
    }
}
