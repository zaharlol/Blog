using Blog.Models;
using Blog.Services.IServices;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Blog.Services
{
    public class ArticleService : Controller, IArticleService
    {
        DataContext db;
        private readonly Logger logger;

        public ArticleService(DataContext _db, Logger _logger)
        {
            db = _db;
            logger = _logger;
        }

        public IActionResult Create(ArticleViewModel model, HttpContext http)
        {
            User currentUser = db.Users.FirstOrDefault(u => u.FirstName == http.User.Identity.Name);

            if (ModelState.IsValid)
            {
                Article article = new Article()
                {
                    Id = Guid.NewGuid(),
                    Title = model.Title,
                    Content = model.Content,
                    User = currentUser,
                };
                db.Articles.Add(article);
                db.SaveChanges();

                logger.Trace("Статья {0} добавлена", article.Id);

                return StatusCode(200);
            }
            return StatusCode(204);
        }

        public IActionResult Delete(Guid id)
        {
            var art = db.Articles.Include(s => s.Comments).FirstOrDefault(x => x.Id == id);
            db.Articles.Remove(art);
            db.SaveChanges();

            logger.Trace("Статья {0} удалена" + art.Id);

            return StatusCode(200);
        }

        public Article UpdateArticle(Guid id)
        {
            Article artical = db.Articles.FirstOrDefault(x => x.Id == id);

            return artical;
        }

        public IActionResult UpdateArticles(Article model)
        {
            Article article = db.Articles.FirstOrDefault(x => x.Id == model.Id);

            article.Title = model.Title;
            article.Content = model.Content;

            db.Articles.Update(article);
            db.SaveChanges();

            logger.Trace("Статья {0} обновлена" + article.Id);

            return StatusCode(200);
        }

        public Article ViewArticle(Guid id)
        {
            Article artical = db.Articles.Include(s => s.User).Include(s => s.Comments).ThenInclude(s => s.User).FirstOrDefault(x => x.Id == id);

            return artical;
        }

        public List<Article> ViewArticles()
        {
            List<Article> articles = db.Articles.Include(s => s.User).Include(s => s.Comments).ToList();

            return articles;
        }        
    }
}
