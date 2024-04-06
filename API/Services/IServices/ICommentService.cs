using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Blog.Services.IServices
{
    public interface ICommentService
    {
        IActionResult CreateG(Guid id);
        IActionResult CreateP(CommentViewModel model);
        IActionResult UpdateComment(Guid id);
        IActionResult UpdateComments(Comment model);
        IActionResult Delete(Guid id);
    }
}
