using OpetNet.Domain.Models;
using System;

namespace OpetNet.Domain.Interfaces
{
    public interface ICustomerRepository 
    {
        Customer GetByEmailAndPassWord(string email, string passWord);
        Customer GetByEmail(string email);
        Customer GetById(Guid id);
        void Update(Customer customerViewModel);
        void Remove(Guid id);
        void Register(Customer customerViewModel);
    }
}
