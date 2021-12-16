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
        private readonly IAmizadesRepository _amizadesRepository;
        public PostRepository(ApplicationDbContext dbContext, IAmizadesRepository amizadesRepository) : base(dbContext)
        {
            _amizadesRepository = amizadesRepository;
        }

        public IEnumerable<Post> GetRecentPost(Guid idUsuario)
        {
            var listaAmigosUsuario = _amizadesRepository.GetTheFriends(idUsuario).Select(x => x.IdAmigo);

            return _context.Posts.Include(x => x.Customer)
                .Where(x => listaAmigosUsuario.Contains(x.CustomerId) || x.CustomerId == idUsuario)
                .OrderByDescending(x => x.DataPublicacao).Take(10).ToList();
        }
        public void Register(Post post)
        {
            Add(post);
        }
    }
}
