using BlogApplication.Domain.Entities.Abstractions;
using System.Text.Json.Serialization;

namespace BlogApplication.Domain.Entities
{
    public class User : BaseEntity
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required bool IsAdmin { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        [JsonIgnore]
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
    }
}
