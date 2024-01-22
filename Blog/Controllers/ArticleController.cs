using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient.DataClassification;
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

                return View("Arts");
            }
            return View("NewArticle");
        }

        [Route("Arts")]
        [HttpGet]
        public IActionResult ViewArticles() 
        {
            var model = db.Articles.ToList();
            return View("Arts", model);

        }

        [Route("Article")]
        [HttpGet]
        public IActionResult ViewArticle()
        {
           
            return View("Article");

        }

        public void Update() { }
        public void Delete(ArticleViewModel model) 
        {
            var art = db.Articles.FirstOrDefault(x => x.Id == model.Id);
            db.Articles.Remove(art);
        }
    }
}
