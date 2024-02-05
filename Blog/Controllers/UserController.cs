using AutoMapper;
using Blog.Models;
using Blog.Repository;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
        public IActionResult Register(RegisterViewModel model)
        {
                if (ModelState.IsValid)
                {
                User user = new User()
                {
                      FirstName = model.FirstName,
                      LastName = model.LastName,
                      PasswordReg = model.PasswordReg,
                      Role = new Role()
                      {
                          Id = 1,
                          Name = "Пользователь"
                      }
                };
                    db.Users.Add(user);
                    db.SaveChanges();

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
            User user = db.Users.FirstOrDefault(s => s.FirstName == model.FirstName);
            var us = mapper.Map<User>(user);

            if (ModelState.IsValid)
            {
                if (model.PasswordReg == us.PasswordReg)
                {
                    return View("Account");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, model.FirstName)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "AppCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
                );

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return View("Login");
        }

        [Route("MyPage")]
        [HttpGet]
        public IActionResult MyPage()
        {
            var user = User;
            var result = mapper.Map<User>(user);
            var model = new UserViewModel(result);

            return View("Account", model);
        }

        public void Read() { }
        public void Update() { }
        public void Delete() { }
    }
}
