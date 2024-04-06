using Blog.Models;
using Blog.Services.IServices;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService comment;

        public CommentController(ICommentService service)
        {
            comment = service;
        }

        [Route("NewComment")]
        [HttpGet]
        public IActionResult CreateG(Guid id)
        {
            return comment.CreateG(id);
        }

        [Route("NewComment")]
        [HttpPost]
        public IActionResult CreateP(CommentViewModel model)
        {
            return comment.CreateP(model);
        }

        [Route("UpdateCom")]
        [HttpGet]
        public IActionResult UpdateComment(Guid id)
        {
            return comment.UpdateComment(id);
        }

        [Route("UpdateCom")]
        [HttpPost]
        public IActionResult UpdateComments(Comment model)
        {
            return comment.UpdateComments(model);
        }

        public IActionResult Delete(Guid id) 
        {
            return comment.Delete(id);            
        }
    }
}
