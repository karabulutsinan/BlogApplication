using BlogApplication.Domain.Entities;
using BlogApplication.Helpers;
using BlogApplication.Persistence.Context;
using BlogApplication.Persistence.Migrations;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace BlogApplication.Controllers
{
	public class DashboardController : BaseController
	{
		private readonly ApplicationDbContext _context;

		public DashboardController(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			User? currentUser = _GetCurrentUser();

			if (currentUser == null)
			{
				return RedirectToAction("Login", "Auth");
			}

			var query = _context.Posts.IsAvailable();

			if (!currentUser.IsAdmin)
			{
				query = query.Where(x => x.UserId == currentUser.Id);
			}

			List<Post> posts = query.ToList();

			return View(posts);
		}

		public IActionResult DeletePost(int Id)
		{
			Post post = _context.Posts.Where(x => x.Id == Id).FirstOrDefault();
			if (post != null)
			{
				post.IsDeleted = true;
				_context.Posts.Update(post);

				_context.SaveChanges();

			}
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult CreatePost()
		{
			List<Category> categories = _context.Categories.IsAvailable().ToList();
			List<Tag> tags = _context.Tags.IsAvailable().ToList();
			ViewBag.Categories = categories;
			ViewBag.Tags = tags;
			return View();

		}
		[HttpPost]
		public IActionResult CreatePost(string Title, string ContentHTML, string Description, string ImageURL, bool ShowOnHome, int CategoryId, List<int> Tags)
		{
			Post post = new Post()
			{
				UserId = _GetCurrentUser().Id,
				Title = Title,
				ContentHTML = ContentHTML,
				Description = Description,
				ImageURL = ImageURL,
				ShowOnHome = ShowOnHome,
				CategoryId = CategoryId,
				IsActive = true,
				CreatedDateUTC = DateTime.UtcNow,
			};

			_context.Posts.Add(post);
			_context.SaveChanges();

			foreach (var item in Tags)
			{
				PostTag pt = new PostTag() { PostId = post.Id, TagId = item };
				_context.PostTags.Add(pt);
			}

			_context.SaveChanges();


			return RedirectToAction("Index");
		}

        [HttpGet]
        public IActionResult PostDetails(int Id)
        {
            Post post = _context.Posts.Find(Id);

            if (post == null)
            {
                return NotFound();
            }

            List<Category> categories = _context.Categories.ToList();
            List<Tag> tags = _context.Tags.ToList();
            ViewBag.Categories = categories;
            ViewBag.Tags = tags;

            
            return View(post);
        }

        [HttpPost]
        public IActionResult PostDetails(int Id, string Title, string ContentHTML, string Description, string ImageURL, bool ShowOnHome, int CategoryId, List<int> Tags)
        {
            Post post = _context.Posts.Find(Id);

            if (post == null)
            {
                return NotFound();
            }

            post.Title = Title;
            post.ContentHTML = ContentHTML;
            post.Description = Description;
            post.ImageURL = ImageURL;
            post.ShowOnHome = ShowOnHome;
            post.CategoryId = CategoryId;

            
            _context.PostTags.RemoveRange(_context.PostTags.Where(pt => pt.PostId == post.Id));

            
            foreach (var tagId in Tags)
            {
                PostTag pt = new PostTag() { PostId = post.Id, TagId = tagId };
                _context.PostTags.Add(pt);
            }

            _context.Posts.Update(post);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }

		

	
}
