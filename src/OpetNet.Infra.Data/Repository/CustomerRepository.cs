using System;
using OpetNet.Domain.Interfaces;
using OpetNet.Domain.Models;
using OpetNet.Infra.Data.UoW;

namespace OpetNet.Infra.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IUnitOfWorkAdo _unitOfWorkAdo;

        #region Querys
        private const string sp_get_email = @"select 'sonyluz@bne.com.br' as Email;"; 
        #endregion

        public CustomerRepository(IUnitOfWorkAdo unitOfWorkAdo)
        {
            _unitOfWorkAdo = unitOfWorkAdo;
        }

        public Customer GetByEmail(string email)
        {
            try
            {
                var cmd = _unitOfWorkAdo.CreateCommand();
                cmd.CommandText = sp_get_email;
                string getEmail = string.Empty;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        getEmail = dr["Email"].ToString();
                    }
                }
                cmd.Dispose();

                if (!email.Equals(getEmail))
                {
                    return null;
                }
                else
                {
                    return new Customer(
                    new Guid("eff0a55f-b2d4-43fe-8830-5d615af3bd3a"),
                    "Sony Luz",
                    "sonyluz@bne.com.br",
                    Convert.ToDateTime("1990-11-18T21:17:51.763Z")
                    );
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public Customer GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Register(Customer customerViewModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer customerViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
