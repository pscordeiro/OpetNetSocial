using OpetNet.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace OpetNet.Infra.Data.UoW
{
    public interface IUnitOfWorkAdo : IUnitOfWork
    {
        IDbTransaction Transaction { get; }
        DbCommand CreateCommand();
    }
}
