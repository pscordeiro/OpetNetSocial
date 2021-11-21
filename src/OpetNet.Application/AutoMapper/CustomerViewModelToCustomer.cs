using AutoMapper;
using OpetNet.Application.ViewModels;
using OpetNet.Domain.Models;

namespace OpetNet.Application.AutoMapper
{
    public class CustomerViewModelToCustomer : Profile
    {
        public CustomerViewModelToCustomer()
        {
            CreateMap<CustomerViewModel, Customer>();
        }
    }
}
