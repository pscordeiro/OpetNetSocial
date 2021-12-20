using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OpetNet.Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> query);
    }
}
