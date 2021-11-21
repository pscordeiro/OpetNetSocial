using OpetNet.Domain.Interfaces;
using OpetNet.Domain.Models;
using OpetNet.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpetNet.Infra.Data.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly IAmizadesRepository _amizadesRepository;
        public CustomerRepository(ApplicationDbContext dbContext, IAmizadesRepository amizadesRepository ) : base(dbContext)
        {
            _amizadesRepository = amizadesRepository;
        }

        public Customer GetByEmailAndPassWord(string email, string passWord)
        {
            return _context.Customers.FirstOrDefault(x => x.Email == email);
        }
        public Customer GetByEmail(string email)
        {
           return _context.Customers.FirstOrDefault(x => x.Email == email);
        }
        public IEnumerable<Customer> GetFriendshipSuggestion(Guid customerId, int take = 10)
        {
            var listfriends = _amizadesRepository.GetTheFriends(customerId, take).Select(x => x.IdAmigo).ToList();
            return _context.Customers.Where(x => !listfriends.Contains(x.Id) && x.Id != customerId).Take(take).ToList();
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
