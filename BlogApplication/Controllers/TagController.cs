using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogApplication.Persistence.Context;
using BlogApplication.Domain.Entities;
using BlogApplication.Web.ViewModels;
using System.Linq;

namespace BlogApplication.Web.Controllers
{
    public class TagController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TagController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tags = _context.Tags
                .Include(t => t.Posts)
                .ToList();

            var viewModel = tags.Select(tag => new TagWithPostsViewModel
            {
                Id = tag.Id,
                Name = tag.Name,
                PostsCount = tag.Posts.Count(post => !post.IsDeleted),
                Posts = tag.Posts.Where(post => !post.IsDeleted).ToList()
            }).ToList();

            return View(viewModel);
        }
    }
}
