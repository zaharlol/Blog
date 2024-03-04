using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Blog.Controllers
{
    public class CommentController : Controller
    {
        DataContext db;

        public CommentController(DataContext _db)
        {
            db = _db;
        }

        [Route("NewComment")]
        [HttpGet]
        public IActionResult CreateG(Guid id)
        {
            Article article = db.Articles.FirstOrDefault(s => s.Id == id);
            
            return View("NewComment", article);
        }

        [Route("NewComment")]
        [HttpPost]
        public IActionResult CreateP(CommentViewModel model)
        {
            User currentUser = db.Users.FirstOrDefault(u => u.FirstName == User.Identity.Name);

            Article article = db.Articles.FirstOrDefault(s => s.Id == model.Article.Id);

            if (ModelState.IsValid)
            {
                Comment comment = new Comment()
                {
                    Id = Guid.NewGuid(),
                    Content = model.Content,
                    Article = article,
                    User = currentUser
                };
                db.Comments.Add(comment);
                db.SaveChanges();

                return RedirectToAction("ViewArticle", "Article");
            }
            return View("NewComment");
        }
        public void Read() { }
        public void Update() { }
        public void Delete(Comment comment) 
        {
            var art = db.Comments.FirstOrDefault(x => x.Id == comment.Id);
            db.Comments.Remove(art);
        }
    }
}
