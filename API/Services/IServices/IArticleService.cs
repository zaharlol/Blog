using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Blog.Services.IServices
{
    public interface IArticleService
    {
        IActionResult Create(ArticleViewModel model, HttpContext http);
        List<Article> ViewArticles();
        Article ViewArticle(Guid id);
        Article UpdateArticle(Guid id);
        IActionResult UpdateArticles(Article model);
        IActionResult Delete(Guid id);
    }
}
