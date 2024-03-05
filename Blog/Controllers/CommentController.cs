using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

            List<Comment> comments = new List<Comment>();

            CommentViewModel comment = new CommentViewModel("", comments, article);

            return View("NewComment", comment);
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

                return RedirectToAction("ViewArticle", "Article", new { id = model.Article.Id }); 
            }
            return View("NewComment");
        }

        [Route("UpdateCom")]
        [HttpGet]
        public IActionResult UpdateComment(Guid id)
        {
            Comment comment = db.Comments.FirstOrDefault(s => s.Id == id);

            return View("UpdateCom", comment);
        }

        [Route("UpdateCom")]
        [HttpPost]
        public IActionResult UpdateComments(Comment model)
        {
            Comment comment = db.Comments.Include(s => s.Article).FirstOrDefault(s => s.Id == model.Id);

            comment.Content = model.Content;
            db.Comments.Update(comment);
            db.SaveChanges();


            return RedirectToAction("ViewArticle", "Article", new { id = comment.Article.Id });

        }

        public IActionResult Delete(Guid id) 
        {
            var art = db.Comments.Include(s => s.Article).FirstOrDefault(x => x.Id == id);
            db.Comments.Remove(art);
            db.SaveChanges();

            return RedirectToAction("ViewArticle", "Article", new { id = art.Article.Id });
        }
    }
}
