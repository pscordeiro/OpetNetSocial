using Microsoft.EntityFrameworkCore;
using OpetNet.Domain.Interfaces;
using OpetNet.Domain.Models;
using OpetNet.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpetNet.Infra.Data.Repository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<Post> GetRecentPost(Guid idUsuario)
        {
            var listaAmigosUsuario = _context.Amizades.Where(x => x.IdUsuario == idUsuario)
                .Select(x=> x.IdAmigo).ToList();

            return _context.Posts.Include(x => x.Customer)
                .Where(x => listaAmigosUsuario.Contains(x.CustomerId) || x.CustomerId == idUsuario)
                .Take(10)
                .OrderByDescending(x => x.DataPublicacao).ToList();
        }
        public void Register(Post post)
        {
            Add(post);
        }
    }
}
