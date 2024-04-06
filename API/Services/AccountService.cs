using AutoMapper;
using Blog;
using Blog.Models;
using Blog.Services.IServices;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static  Microsoft.AspNetCore.Mvc.Controller;


namespace Blog.Services
{
    public class AccountService : Controller, IAccountService
    {
        private readonly Logger _logger;
        DataContext db;

        public AccountService(Logger logger, DataContext data)
        {
            _logger = logger;
            db = data;
        }

        public IActionResult Account(AccountViewModel model, HttpContext http)
        {
            if (http.User.Identity.IsAuthenticated)
            {
                User user = db.Users.FirstOrDefault(u => u.FirstName == http.User.Identity.Name);
                if (user == null)
                {
                    _logger.Error("Пользователь не найден");
                    http.SignOutAsync();
                    return View("Login");
                }
                else
                {
                    model.User = user;
                    model.Name = user.LastName + " " + user.FirstName;
                    model.Articles = db.Articles.Where(s => s.UserId == user.Id).ToList();

                    return View("Account", model);
                }
            }
            else
            {
                return View("ErrorMes");
            }
        }

        public async Task<IActionResult> Delete(User user, HttpContext http)
        {
            var del = db.Users.FirstOrDefault(x => x.Id == user.Id);
            await http.SignOutAsync();
            db.Users.Remove(del);
            await db.SaveChangesAsync();

            _logger.Trace("Пользователь {0} удалён", user.Id);

            return View("Login");
        }

        public async Task<IActionResult> Login(LoginViewModel model, HttpContext http)
        {
                User user = await db.Users.Include(s => s.Role).FirstOrDefaultAsync(s => s.FirstName == model.FirstName);

                if (model.PasswordReg == user.PasswordReg)
                {
                    await Authenticate(user, http);
                    return RedirectToAction("", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
                
            return View("Login");
        }

        public async Task<IActionResult> Register(RegisterViewModel model, HttpContext http)
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
                    db.Roles.Add(new Role
                    {
                        Id = 2,
                        Name = "Администратор"
                    });
                    db.Roles.Add(new Role
                    {
                        Id = 3,
                        Name = "Модератор"
                    });
                }


                user.Role = role;

                db.Users.Add(user);


                db.SaveChanges();

                await Authenticate(user, http);

                _logger.Trace("Зарегестрировался пользователь {0}", user.Id);

                return View("Login");
            }
            return View("Register");
        }

        public async Task<IActionResult> UpdateUsers(User model, HttpContext http)
        {
            User user = db.Users.Include(s => s.Role).FirstOrDefault(s => s.FirstName == http.User.Identity.Name);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PasswordReg = model.PasswordReg;

            if (model.RoleId != 0)
            {
                Role role = db.Roles.FirstOrDefault(role => role.Id == model.RoleId);

                if (role == null) role = new Role { Id = model.RoleId };

                if (model.RoleId == 1) role.Name = "Пользователь";
                if (model.RoleId == 2) role.Name = "Администратор";
                if (model.RoleId == 3) role.Name = "Модератор";

                user.Role = role;
            }

            db.Users.Update(user);
            db.SaveChanges();

            await Authenticate(user, http);

            _logger.Trace("Пользователь {0} обновлён", user.Id);

            return RedirectToAction("Account", "User");
        }

        public IActionResult ViewUsers()
        {
            List<User> users = db.Users.Include(s => s.Articles).ToList();

            return View("Users", users);
        }

        public IActionResult UpdateUser(string login)
        {
            User user = db.Users.Include(s => s.Role).FirstOrDefault(s => s.FirstName == login);
            return View("UpdateUs", user);
        }

        public async Task Authenticate(User user, HttpContext http)
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

            await http.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            _logger.Trace("Пользователь {0} аутентифицировался", user.Id);
        }
    }
}
