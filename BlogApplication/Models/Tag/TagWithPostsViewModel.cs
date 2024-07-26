using BlogApplication.Domain.Entities;

namespace BlogApplication.Web.ViewModels
{
    public class TagWithPostsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostsCount { get; set; }
        public List<Post> Posts { get; set; }
    }
}
