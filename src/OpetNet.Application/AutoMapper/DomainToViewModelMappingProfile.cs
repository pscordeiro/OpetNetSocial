using AutoMapper;
using OpetNet.Application.ViewModels;
using OpetNet.Domain.Models;

namespace OpetNet.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
        }
    }
}
