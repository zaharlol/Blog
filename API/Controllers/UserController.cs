﻿using AutoMapper;
using Blog;
using Blog.Models;
using Blog.Services.IServices;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
using Blog.Services;

namespace Blog.Controllers
{
    public class UserController : Controller
    {
        private readonly IAccountService account;

        public UserController(IAccountService accountService)
        {
            account = accountService;
        }

        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return StatusCode(200);
        }


        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            return await account.Register(model, HttpContext);                     
        }

        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return StatusCode(200);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            return await account.Login(model, HttpContext);
        }

        [Authorize] 
        [Route("Account")]
        [HttpGet]
        public User Account(AccountViewModel model)
        {
            return account.Account(model, HttpContext);
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
        public List<User> ViewUsers() 
        {
            return account.ViewUsers();            
        }

        [Route("UpdateUs")]
        [HttpGet]
        public User UpdateUser(string login) 
        {
            return account.UpdateUser(login);            
        }

        [Route("UpdateUs")]
        [HttpPost]
        public async Task<IActionResult> UpdateUsers(User model)
        {
            return await account.UpdateUsers(model, HttpContext);            
        }

        [Route("DeleteUser")]
        [HttpPost]
        public async Task<IActionResult> Delete(User user) 
        {        
            return await account.Delete(user, HttpContext);           
        }
    }
}
