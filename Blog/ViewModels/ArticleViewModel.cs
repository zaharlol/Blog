using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using static System.Collections.Specialized.BitVector32;

namespace Blog.ViewModels
{
    public class ArticleViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display (Name = "title", Prompt = "Заголовок")]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "title", Prompt = "Содержание")]
        public string Content { get; set; }

        public Guid Id { get; set; }

        public List<Article> Articles { get; set; }
    }
}
