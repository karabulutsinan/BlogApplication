using BlogApplication.Domain.Entities;
using BlogApplication.Helpers;
using BlogApplication.Models;
using BlogApplication.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var post = _context.Posts
                                .Include(p => p.User)
                                .Include(p => p.Category)
                                .Include(p => p.Tags)
                                .Include(p => p.Comments)
                                    .ThenInclude(c => c.User)
                                .FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            var currentUser = _GetCurrentUser();
            ViewBag.CurrentUser = currentUser;
            ViewBag.IsAdmin = currentUser != null && currentUser.IsAdmin;

            post.Comments = post.Comments.Where(c => !c.IsDeleted).ToList();

            return View(post);
        }
        [HttpGet]
        public IActionResult CreateComment()
        {

            return View();

        }
        [HttpPost]
        public IActionResult CreateComment(int postId, string comment)
        {
            Comment newComment = new Comment()
            {
                PostId = postId,
                UserId = _GetCurrentUser().Id,
                CreatedDateUTC = DateTime.Now,
                Message = comment,
            };

            _context.Comments.Add(newComment);
            _context.SaveChanges();

            var allComments = _context.Comments
                .Where(x => x.PostId == postId && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedDateUTC)
                .Select(x => new 
                {
                    x.Id,
                    x.Message, 
                    x.CreatedDateUTC, 
                    UserFullName = $"{x.User.Name} {x.User.Surname} " 
                }).
                ToList();
            return Json(allComments);


        }

        [HttpPost]
        public IActionResult DeleteComment(int commentId)
        {
            var comment = _context.Comments.Find(commentId);
            if (comment == null)
            {
                return NotFound();
            }

            comment.IsDeleted = true;
            _context.Comments.Update(comment);
            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}