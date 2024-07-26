using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogApplication.Persistence.Context;
using BlogApplication.Domain.Entities;
using System.Linq;
using BlogApplication.Web.ViewModels;

namespace BlogApplication.Web.Controllers
{
    public class TagPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TagPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var tag = _context.Tags
                .Include(t => t.Posts)
                .FirstOrDefault(t => t.Id == id);

            if (tag == null)
            {
                return NotFound();
            }

            var viewModel = new TagWithPostsViewModel
            {
                Name = tag.Name,
                PostsCount = tag.Posts.Count,
                Posts = tag.Posts.Where(post => !post.IsDeleted).ToList()
            };

            return View(viewModel);
        }
    }
}
