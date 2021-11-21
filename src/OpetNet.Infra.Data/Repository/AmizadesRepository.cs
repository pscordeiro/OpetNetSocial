using OpetNet.Domain.Interfaces;
using OpetNet.Domain.Models;
using OpetNet.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpetNet.Infra.Data.Repository
{
    public class AmizadesRepository : BaseRepository<Amizades>, IAmizadesRepository
    {
        public AmizadesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<Amizades> GetTheFriends(Guid customerId, int take = 10)
        {
            return _context.Amizades.Where(x => x.IdUsuario == customerId).Take(take).ToList();
        }

        public IEnumerable<Amizades> GetTheUnfriends(Guid customerId, int take = 10)
        {
            return _context.Amizades.Where(x => x.IdUsuario != customerId).Take(take).ToList();
        }
    }
}
