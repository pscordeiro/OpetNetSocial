using OpetNet.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace OpetNet.Application.Interfaces
{
    public interface IPostAppService
    {
        /// <summary>
        /// Retorna 10 post dos amigos do usuario e do usuario tbm
        /// </summary>
        /// <param name="idUsuario">Id do usuario que vc quer os post</param>
        /// <returns></returns>
        IEnumerable<PostViewModel> GetRecentPost(Guid idUsuario);
        void Register(PostViewModel postViewModel);
        public void RegisterLikeInPost(Guid customerId, int postId);
    }
}
