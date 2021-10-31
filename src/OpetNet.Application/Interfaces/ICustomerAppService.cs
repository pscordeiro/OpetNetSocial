using System;
using System.Collections.Generic;
using OpetNet.Application.ViewModels;

namespace OpetNet.Application.Interfaces
{
    public interface ICustomerAppService
    {
        void Register(CustomerViewModel customerViewModel);
        IEnumerable<CustomerViewModel> GetAll();
        CustomerViewModel GetById(Guid id);
        void Update(CustomerViewModel customerViewModel);
        void Remove(Guid id);
    }
}
