using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult Create()
        {
            return View("NewComment");
        }

        [Route("NewComment")]
        [HttpPost]
        public IActionResult Create(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment()
                {
                    Id = Guid.NewGuid(),
                    Content = model.Content,
                };
                db.Comments.Add(comment);
                db.SaveChanges();

                return View("Article");
            }
            return View("NewComment");
        }
        public void Read() { }
        public void Update() { }
        public void Delete() { }
    }
}
