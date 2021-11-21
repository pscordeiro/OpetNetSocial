using AutoMapper;
using OpetNet.Application.Interfaces;
using OpetNet.Application.ViewModels;
using OpetNet.Domain.Commands;
using OpetNet.Domain.Interfaces;
using OpetNet.Domain.Models;
using System;
using System.Collections.Generic;

namespace OpetNet.Application.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomerAppService(IMapper mapper,
                                  ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }


        public CustomerViewModel GetById(Guid id)
        {

            return _mapper.Map<CustomerViewModel>(_customerRepository.GetById(id));
        }
        public CustomerViewModel GetByEmailAndPassWord(string email, string passWord)
        {
            return _mapper.Map<CustomerViewModel>(_customerRepository.GetByEmail(email));
        }
        public CustomerViewModel GetByEmail(string email)
        {
           return _mapper.Map<CustomerViewModel>(_customerRepository.GetByEmail(email));
        }
        public void Register(CustomerViewModel customerViewModel)
        {
            var customer = _mapper.Map<CustomerViewModel, Customer>(customerViewModel);
            _customerRepository.Register(customer);
        }
        public IEnumerable<CustomerViewModel> GetFriendshipSuggestion(Guid customerId, int take = 10)
        {
            return _mapper.Map<IEnumerable<CustomerViewModel>>(_customerRepository.GetFriendshipSuggestion(customerId, take));
        }
        public void Update(CustomerViewModel customerViewModel)
        {
            var updateCommand = _mapper.Map<UpdateCustomerCommand>(customerViewModel);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveCustomerCommand(id);
        }

        public IEnumerable<CustomerViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

    }
}
