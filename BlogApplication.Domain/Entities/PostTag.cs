using BlogApplication.Domain.Entities.Abstractions;

namespace BlogApplication.Domain.Entities
{
    public class PostTag
    {
        public required int PostId { get; set; }
        public required int TagId { get; set; }
    }
}
