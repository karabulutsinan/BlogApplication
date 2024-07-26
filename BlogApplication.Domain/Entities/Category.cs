using BlogApplication.Domain.Entities.Abstractions;

namespace BlogApplication.Domain.Entities
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
