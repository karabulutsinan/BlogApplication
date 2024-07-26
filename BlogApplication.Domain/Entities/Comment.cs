using BlogApplication.Domain.Entities.Abstractions;

namespace BlogApplication.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public required int PostId { get; set; }
        public required int UserId { get; set; }
        public required string Message { get; set; }
        public virtual User User { get; set; } = null!;

    }
}
