using System;
using System.Collections.Generic;
using AutoMapper;
using OpetNet.Application.Interfaces;
using OpetNet.Application.ViewModels;
using OpetNet.Domain.Commands;
using OpetNet.Domain.Core.Bus;
using OpetNet.Domain.Interfaces;
using log4net;
using OpetNet.Domain.Models;

namespace OpetNet.Application.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediatorHandler Bus;
        private readonly ILog _log;

        public CustomerAppService(IMapper mapper,
                                  ICustomerRepository customerRepository,
                                  IMediatorHandler bus,
            ILog log)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            Bus = bus;
            _log = log;
        }


        public CustomerViewModel GetById(Guid id)
        {
            _log.Debug("GetById");

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
            _log.Debug("Register");

            var registerCommand = _mapper.Map<RegisterNewCustomerCommand>(customerViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(CustomerViewModel customerViewModel)
        {
            var updateCommand = _mapper.Map<UpdateCustomerCommand>(customerViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveCustomerCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IEnumerable<CustomerViewModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
