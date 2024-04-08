using Blog.Models;
using Blog.Services.IServices;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Services
{
    public class CommentService : Controller, ICommentService
    {
        DataContext db;
        private readonly Logger logger;

        public CommentService(DataContext _db, Logger _logger)
        {
            db = _db;
            logger = _logger;
        }

        public CommentViewModel CreateG(Guid id)
        {
            Article article = db.Articles.FirstOrDefault(s => s.Id == id);

            List<Comment> comments = new List<Comment>();

            CommentViewModel comment = new CommentViewModel("", comments, article);

            return comment;
        }

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

                logger.Trace("Комментарий {0} добавлен", comment.Id);

                return StatusCode(200);
            }
            return StatusCode(204);
        }

        public IActionResult Delete(Guid id)
        {
            var comment = db.Comments.Include(s => s.Article).FirstOrDefault(x => x.Id == id);
            db.Comments.Remove(comment);
            db.SaveChanges();

            logger.Trace("Комментарий {0} удалён", comment.Id);

            return StatusCode(200);
        }

        public Comment UpdateComment(Guid id)
        {
            Comment comment = db.Comments.FirstOrDefault(s => s.Id == id);

            return comment;
        }

        public IActionResult UpdateComments(Comment model)
        {
            Comment comment = db.Comments.Include(s => s.Article).FirstOrDefault(s => s.Id == model.Id);

            comment.Content = model.Content;
            db.Comments.Update(comment);
            db.SaveChanges();

            logger.Trace("Комментарий {0} обновлён", comment.Id);

            return StatusCode(200);
        }
    }
}
