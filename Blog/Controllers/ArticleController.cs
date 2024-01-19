using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

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
            if (ModelState.IsValid)
            {
                Article article = new Article()
                {
                    Id = Guid.NewGuid(),
                    Title = model.Title,
                    Content = model.Content,
                };
                db.Articles.Add(article);
                db.SaveChanges();

                return View("NewArticle");
            }
            return View("NewArticle");
        }
        public void Read() { }
        public void Update() { }
        public void Delete() { }
    }
}
