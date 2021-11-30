using OpetNet.Domain.Interfaces;
using OpetNet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using OpetNet.Infra.Data.Context;
using System;
using System.Linq;

namespace OpetNet.Infra.Data.Repository
{
    public class PostsCurtidosRepository : BaseRepository<PostsCurtidos>, IPostsCurtidosRepository
    {
        public PostsCurtidosRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public bool ConsumerLiked(Guid customerId, int postId)
        {
            var a = _context.PostsCurtidos.FirstOrDefault(x => x.CustomerId == customerId && x.PostId == postId);
            return a != null;
        }

        public void Register(PostsCurtidos postsCurtidos)
        {
            Add(postsCurtidos);
        }
    }
}
