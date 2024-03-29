using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Blog.Services.IServices
{
    public interface IArticleService
    {
        IActionResult Create(ArticleViewModel model);
        IActionResult ViewArticles();
        IActionResult ViewArticle(Guid id);
        IActionResult UpdateArticle(Guid id);
        IActionResult UpdateArticles(Article model);
        IActionResult Delete(Guid id);
    }
}
