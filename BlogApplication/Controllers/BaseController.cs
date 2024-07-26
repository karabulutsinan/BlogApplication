using BlogApplication.Domain.Entities;
using BlogApplication.Helpers;
using BlogApplication.Persistence.Context;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers
{
	public class BaseController : Controller
	{
		private readonly ApplicationDbContext _context;
		private User _currentUser;
		public BaseController(ApplicationDbContext context)
		{
			_context = context;
		}

		public User? _GetCurrentUser()
		{
			AuthHelper authHelper = new AuthHelper(HttpContext, _context);
			User? currentUser = authHelper._GetCurrentUser();
			return currentUser;


        }
	}
}
