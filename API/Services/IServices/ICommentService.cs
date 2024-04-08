using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Blog.Services.IServices
{
    public interface ICommentService
    {
        CommentViewModel CreateG(Guid id);
        IActionResult CreateP(CommentViewModel model);
        Comment UpdateComment(Guid id);
        IActionResult UpdateComments(Comment model);
        IActionResult Delete(Guid id);
    }
}
