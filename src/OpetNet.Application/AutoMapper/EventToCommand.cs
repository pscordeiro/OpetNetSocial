using AutoMapper;
using OpetNet.Domain.Commands;
using OpetNet.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpetNet.Application.AutoMapper
{
    public class EventToCommand : Profile
    {
        public EventToCommand()
        {
            CreateMap<CustomerRegisteredEvent, RegisterNewCustomerCommand>()
                .ConstructUsing(c => new RegisterNewCustomerCommand(c.Name, c.Email, c.BirthDate));
        }
    }
}
