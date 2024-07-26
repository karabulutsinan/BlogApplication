using BlogApplication.Domain.Entities.Abstractions;

namespace BlogApplication.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public required string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
