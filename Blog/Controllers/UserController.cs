using AutoMapper;
using Blog.Models;
using Blog.Repository;
using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class UserController : Controller
    {
        DataContext db;

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
            {              
                if (ModelState.IsValid)
                {
                    User user = new User()
                    {
                        Id = 1,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PasswordReg = model.PasswordReg
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                return View("Register");
            }
        }
          public void Read() { }
        public void Update() { }
        public void Delete() { }
    }
}
