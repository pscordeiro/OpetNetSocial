using System;
using OpetNet.Domain.Models;
using System.Collections.Generic;

namespace OpetNet.Domain.Interfaces
{
    public interface IAmizadesRepository
    {
        IEnumerable<Amizades> GetTheFriends(Guid customerId, int take = 10);
        IEnumerable<Amizades> GetTheUnfriends(Guid customerId, int take = 10);
    }
}
