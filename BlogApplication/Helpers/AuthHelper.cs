using BlogApplication.Domain.Entities;
using BlogApplication.Persistence.Context;
using Microsoft.AspNetCore.Http;

namespace BlogApplication.Helpers
{
	public class AuthHelper
	{
		private readonly HttpContext _httpContext;
		private readonly ApplicationDbContext _context;

		public AuthHelper(HttpContext httpContext, ApplicationDbContext context)
		{
			_httpContext = httpContext;
			_context = context;
		}

		public User? _GetCurrentUser()
		{
			User? _currentUser = null;
			string currentUserId;
			_httpContext.Request.Cookies.TryGetValue("user", out currentUserId);
			if (_currentUser == null && !string.IsNullOrEmpty(currentUserId))
			{
				_currentUser = _context.Users
					.Where(x => x.Id.ToString() == currentUserId)
					.IsAvailable()
					.FirstOrDefault();
			}
			return _currentUser;
		}
	}
}
