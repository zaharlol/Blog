using AutoMapper;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Blog
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, LoginViewModel>();
        }
    }
}
