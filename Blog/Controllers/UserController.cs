using AutoMapper;
using Blog.Models;
using Blog.Repository;
using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class UserController : Controller
    {
        DataContext db;

        public UserController(DataContext data)
        {
            db = data;
        }

        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {             
                if (ModelState.IsValid)
                {
                    User user = new User()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PasswordReg = model.PasswordReg
                    };
                    db.Users.Add(user);
                    db.SaveChanges();

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
        public IActionResult Login(LoginViewModel model)
        {
            User user = db.Users.FirstOrDefault(s => s.FirstName == model.FirstName);
            if (ModelState.IsValid)
            {
                if (model.PasswordReg == user.PasswordReg)
                {
                   return View("Account");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View("Login");
        }


        public void Read() { }
        public void Update() { }
        public void Delete() { }
    }
}
