using OpetNet.Domain.Interfaces;
using OpetNet.Domain.Models;
using OpetNet.Infra.Data.Context;
using System;
using System.Linq;

namespace OpetNet.Infra.Data.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext ) : base(dbContext)
        {

        }

        public Customer GetByEmail(string email)
        {
           return _context.Customers.FirstOrDefault(x => x.Email == email);
        }

        public Customer GetById(Guid id)
        {
            return _context.Customers.Find(id);
        }

        public void Register(Customer customerViewModel)
        {
            Add(customerViewModel);
        }

        public void Remove(Guid id)
        {
            var customer = GetById(id);
            base.Remove(customer);
        }
    }
}
