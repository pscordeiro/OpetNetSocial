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
        private readonly IPostsCurtidosRepository _postsCurtidosRepository;
        public PostAppService(IPostRepository postRepository, IMapper mapper,
            IPostsCurtidosRepository postsCurtidosRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _postsCurtidosRepository = postsCurtidosRepository;
        }
        public IEnumerable<PostViewModel> GetRecentPost(Guid idUsuario)
        {
            var listaParaEditar = _mapper.Map<List<PostViewModel>>(_postRepository.GetRecentPost(idUsuario));
            foreach (var post in listaParaEditar)
            {
                post.Liked = _postsCurtidosRepository.ConsumerLiked(idUsuario, post.Id);
            }
            return listaParaEditar;
        }

        public void Register(PostViewModel postViewModel)
        {
            postViewModel.Mensagem = Regex.Replace(postViewModel.Mensagem, @"<.*>", "").ToString();
            _postRepository.Register(_mapper.Map<Post>(postViewModel));
        }
        public void RegisterLikeInPost(Guid customerId, int postId)
        {
            _postsCurtidosRepository.Register(new PostsCurtidos { CustomerId = customerId, PostId = postId });
        }
    }
}
