using AutoMapper;
using OpetNet.Application.ViewModels;
using OpetNet.Domain.Models;

namespace OpetNet.Application.AutoMapper
{
    public class PostViewModelToPostProfile : Profile
    {
        public PostViewModelToPostProfile()
        {
            CreateMap<PostViewModel, Post>();
        }
    }
}
