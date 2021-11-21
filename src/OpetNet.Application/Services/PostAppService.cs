using AutoMapper;
using OpetNet.Application.Interfaces;
using OpetNet.Application.ViewModels;
using OpetNet.Domain.Interfaces;
using OpetNet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OpetNet.Application.Services
{
    public class PostAppService : IPostAppService
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        public PostAppService(IPostRepository postRepository, IMapper mapper)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }
        public IEnumerable<PostViewModel> GetRecentPost(Guid idUsuario)
        {
            return _mapper.Map<IEnumerable<PostViewModel>>(_postRepository.GetRecentPost(idUsuario));
        }

        public void Register(PostViewModel postViewModel)
        {
            postViewModel.Mensagem = Regex.Replace(postViewModel.Mensagem, @"<.*>", "").ToString();
            _postRepository.Register(_mapper.Map<Post>(postViewModel));
        }
    }
}
