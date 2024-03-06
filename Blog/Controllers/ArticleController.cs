using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class ArticleController : Controller
    {
        DataContext db;

        public ArticleController(DataContext _db)
        {
            db = _db;
        }

        [Route("NewArticle")]
        [HttpGet]
        public IActionResult Create()
        {
            return View("NewArticle");
        }

        [Route("NewArticle")]
        [HttpPost]
        public IActionResult Create(ArticleViewModel model)
        {

            User currentUser = db.Users.FirstOrDefault(u => u.FirstName == User.Identity.Name);

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

                return ViewArticles();
            }
            return View("NewArticle");
        }

        [Route("Arts")]
        [HttpGet]
        public IActionResult ViewArticles()
        {
            List<Article> articles = db.Articles.Include(s => s.User).Include(s => s.Comments).ToList();

            return View("Arts", articles);

        }

        [Route("Article")]
        [HttpGet]
        public IActionResult ViewArticle(Guid id)
        {
            Article artical = db.Articles.Include(s => s.User).Include(s => s.Comments).ThenInclude(s => s.User).FirstOrDefault(x => x.Id == id);

            return View("Article", artical);

        }

        [Route("UpdateArt")]
        [HttpGet]
        public IActionResult UpdateArticle(Guid id) 
        {
            Article artical = db.Articles.FirstOrDefault(x => x.Id == id);

            return View("UpdateArt", artical);
        }

        [Route("UpdateArt")]
        [HttpPost]
        public IActionResult UpdateArticles(Article model)
        {
            Article article = db.Articles.FirstOrDefault(x => x.Id == model.Id);

            article.Title = model.Title;
            article.Content = model.Content;

            db.Articles.Update(article);
            db.SaveChanges();

            return RedirectToAction("", "Account");
        }

        public IActionResult Delete(Guid id)
        {
            var art = db.Articles.Include(s => s.Comments).FirstOrDefault(x => x.Id == id);
            db.Articles.Remove(art);
            db.SaveChanges();

            return RedirectToAction("", "Account");
        }
    }
}
