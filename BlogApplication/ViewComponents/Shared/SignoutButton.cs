using BlogApplication.Domain.Entities;
using BlogApplication.Helpers;
using BlogApplication.Persistence.Context;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.ViewComponents.Shared
{
	public class SignoutButtonViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _context;

		public SignoutButtonViewComponent(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			AuthHelper helper = new AuthHelper(HttpContext, _context);

			User? currentUser = helper._GetCurrentUser();

			return View(currentUser);
		}
	}

}
