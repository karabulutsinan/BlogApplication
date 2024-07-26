using BlogApplication.Helpers;
using BlogApplication.Persistence.Context;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.ViewComponents
{
	public class PostsViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _context;

		public PostsViewComponent(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var posts = _context.Posts.IsAvailable()
										.OrderByDescending(x=>x.CreatedDateUTC)
									  .ToList();

			return View(posts);
		}
	}

}
