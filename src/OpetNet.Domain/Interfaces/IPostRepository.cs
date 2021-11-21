using OpetNet.Domain.Models;
using System.Collections.Generic;
using System;

namespace OpetNet.Domain.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetRecentPost(Guid idUsuario);
        void Register(Post post);
    }
}
