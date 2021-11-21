using AutoMapper;
using OpetNet.Application.ViewModels;
using OpetNet.Domain.Models;

namespace OpetNet.Application.AutoMapper
{
    public class PostToPostViewModelProfile : Profile
    {
        public PostToPostViewModelProfile()
        {
            CreateMap<Post, PostViewModel>();
        }
    }
}
