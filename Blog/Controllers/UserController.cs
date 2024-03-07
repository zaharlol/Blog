using AutoMapper;
using Blog.Models;
using Blog.Repository;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Blog.Controllers
{
    public class UserController : Controller
    {
        DataContext db;
        IMapper mapper;

        public UserController(DataContext data, IMapper mapper)
        {
            db = data;
            this.mapper = mapper;
        }

        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [ValidateAntiForgeryToken]
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PasswordReg = model.PasswordReg,
                };

                Role role = db.Roles.FirstOrDefault(r => r.Id == 1);

                if (role == null)
                {
                    role = new Role
                    {
                        Id = 1,
                        Name = "Пользователь"
                    };
                }

                user.Role = role;

                db.Users.Add(user);
                db.SaveChanges();
                await Authenticate(user);

                mapper.Map<User>(user);

                return View("Login");
            }
            return View("Register");
        }


        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                User user = await db.Users.Include(s => s.Role).FirstOrDefaultAsync(s => s.FirstName == model.FirstName);

                if (model.PasswordReg == user.PasswordReg)
                {
                    await Authenticate(user);
                    return RedirectToAction("", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }

            return View("Login");
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.FirstName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "AppCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
                );

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        [Authorize]
        [Route("Account")]
        [HttpGet]
        public IActionResult Account(AccountViewModel model)
        {
            User user = db.Users.FirstOrDefault(u => u.FirstName == User.Identity.Name);

            model.User = user;
            model.Name = user.LastName + " " + user.FirstName;
            model.Articles = db.Articles.Where(s => s.UserId == user.Id).ToList();

            return View("Account", model);
        }

        [Route("Logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        { 
            await HttpContext.SignOutAsync();
            return View("Login"); 
        }

        [Route("Users")]
        [HttpGet]
        public IActionResult ViewUsers() 
        {
            List<User> users = db.Users.Include(s => s.Articles).ToList();

            return View("Users", users);
        }


        [Route("UpdateUs")]
        [HttpGet]
        public IActionResult UpdateUser(string login) 
        {
            User user = db.Users.Include(s => s.Role).FirstOrDefault(s => s.FirstName == login);
            return View("UpdateUs", user);
        }

        [Route("UpdateUs")]
        [HttpPost]
        public async Task<IActionResult> UpdateUsers(User model)
        {
            User user = db.Users.Include(s => s.Role).FirstOrDefault(s => s.FirstName == User.Identity.Name);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PasswordReg = model.PasswordReg;

            Role role = db.Roles.FirstOrDefault(role => role.Id == model.RoleId);

            if (role == null)
            {
                role = new Role { Id = model.RoleId };
                if (model.RoleId == 1) role.Name = "Пользователь";
                if (model.RoleId == 2) role.Name = "Администратор";
                if (model.RoleId == 3) role.Name = "Модератор";
                
            }
 
                user.Role = role;

            db.Users.Update(user);
            db.SaveChanges();

            await Authenticate(user);

            return RedirectToAction("Account", "User");
        }


        public async Task<IActionResult> Delete(User user) 
        {        
            var del = db.Users.FirstOrDefault(x => x.Id == user.Id);
            await HttpContext.SignOutAsync();
            db.Users.Remove(del);
            await db.SaveChangesAsync();

            return View("Login");
        }
    }
}
