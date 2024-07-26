using BlogApplication.Domain.Entities;
using BlogApplication.Models.Auth;
using BlogApplication.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.ConstrainedExecution;

namespace BlogApplication.Controllers
{
	public class AuthController : BaseController
	{
		private readonly ApplicationDbContext _context;

		public AuthController(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
        public IActionResult Register(RegisterDTO request)
        {
			User user = new User() {
				Username = request.UserName,
				Email = request.Email,
				Password = request.Password,
				IsActive = true,
				IsDeleted = false,
				IsAdmin = false,
				Name= request.Name,
				Surname=request.Surname,
				CreatedDateUTC = DateTime.UtcNow,
			};
			//burada kullanıcı varsa hata mesajı gönder.
            
            _context.Users.Add(user);
			
            int isSuccessed =_context.SaveChanges();
			

            if (isSuccessed==0)
            {
				ViewBag.ErrorMessage = "Kayıt Oluşturulken Hata Oluştu.";
				return View();	
            }

            else
            {
                ViewBag.Message = "Başarılı";
				return RedirectToAction("Login");
            }
        }
        

        [HttpGet]
		public IActionResult Login()
		{
			User? currentUser = _GetCurrentUser();

			if (currentUser != null)
			{
				return RedirectToAction("Index", "Dashboard");
			}

			return View();
		}

		[HttpPost]
		public IActionResult Logout()
		{
			HttpContext.Response.Cookies.Delete("user");
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public IActionResult Login(LoginDTO request)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.ErrorMessage = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).FirstOrDefault();

				return View();
			}

			var user = _context.Users.Where(x => !x.IsDeleted && x.IsActive)
									 .Where(x => x.Username == request.UsernameOrEmail || x.Email == request.UsernameOrEmail)
									 .Where(x => x.Password == request.Password).FirstOrDefault();

			if (user == null)
			{
				ViewBag.ErrorMessage = "Kullanıcı adı veya parola hatalı";
			}

			else
			{
				// add cookie user id

				HttpContext.Response.Cookies.Append("user", user.Id.ToString());
				ViewBag.Message = "Başarılı";
			}

			return View();
		}
	}
}
