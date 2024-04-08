using Blog;
using Blog.Models;
using Blog.Services.IServices;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace Blog.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService article;

        public ArticleController(IArticleService service)
        {
            article = service;
        }

        [Route("NewArticle")]
        [HttpGet]
        public IActionResult Create()
        {
            return StatusCode(200);
        }

        [Route("NewArticle")]
        [HttpPost]
        public IActionResult Create(ArticleViewModel model)
        {
            return article.Create(model, HttpContext);           
        }

        [Route("Arts")]
        [HttpGet]
        public List<Article> ViewArticles()
        {
            return article.ViewArticles();   
        }

        [Route("Article")]
        [HttpGet]
        public Article ViewArticle(Guid id)
        {
            return article.ViewArticle(id);           
        }

        [Route("UpdateArt")]
        [HttpGet]
        public Article UpdateArticle(Guid id) 
        {
            return article.UpdateArticle(id);            
        }

        [Route("UpdateArt")]
        [HttpPost]
        public IActionResult UpdateArticles(Article model)
        {
            return article.UpdateArticles(model);            
        }

        [Route("DeleteArt")]
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            return article.Delete(id);            
        }
    }
}
