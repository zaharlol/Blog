using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Services.IServices
{
    public interface IAccountService
    {
        Task<IActionResult> Register(RegisterViewModel model);
        Task<IActionResult> Login(LoginViewModel model);
        IActionResult Account(AccountViewModel model);
        Task<IActionResult> UpdateUsers(User model);
        Task<IActionResult> Delete(User user);
        IActionResult ViewUsers();
        IActionResult UpdateUser(string login);
    }
}
