using OpetNet.Domain.Models;
using System;

namespace OpetNet.Domain.Interfaces
{
    public interface IPostsCurtidosRepository
    {
        bool ConsumerLiked(Guid consumerId, int postId);
        void Register(PostsCurtidos postsCurtidos);
    }
}
