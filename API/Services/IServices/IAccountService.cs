using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Blog.Services.IServices
{
    public interface IAccountService
    {
        Task<IActionResult> Register(RegisterViewModel model, HttpContext http);
        Task<IActionResult> Login(LoginViewModel model, HttpContext http);
        IActionResult Account(AccountViewModel model, HttpContext http);
        Task<IActionResult> UpdateUsers(User model, HttpContext http);
        Task<IActionResult> Delete(User user, HttpContext http);
        IActionResult ViewUsers();
        IActionResult UpdateUser(string login);
    }
}
