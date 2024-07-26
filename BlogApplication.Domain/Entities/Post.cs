using BlogApplication.Domain.Entities.Abstractions;

namespace BlogApplication.Domain.Entities
{
    public class Post : BaseEntity
    {
        public required int UserId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string ContentHTML { get; set; }
        public string? ImageURL { get; set; }
        public bool ShowOnHome { get; set; }

        public required int CategoryId { get; set; }


        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Tag>? Tags { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        //public List<Comment> Comments { get; set; } = new List<Comment>();
        public virtual User User { get; set; } = null!;
    }


}
