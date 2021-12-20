using System;
using System.Collections.Generic;
using OpetNet.Application.ViewModels;

namespace OpetNet.Application.Interfaces
{
    public interface ICustomerAppService
    {
        public CustomerViewModel GetForProfile(string urlProfile);
        void Register(CustomerViewModel customerViewModel);
        IEnumerable<CustomerViewModel> GetAll();
        CustomerViewModel GetByEmail(string email);
        CustomerViewModel GetByEmailAndPassWord(string email, string passWord);
        CustomerViewModel GetById(Guid id);
        IEnumerable<CustomerViewModel> GetFriendshipSuggestion(Guid customerId, int take = 10);
        IEnumerable<CustomerViewModel> GetFriends(Guid customerId);
        public void AddFriend(Guid consumerId, Guid friendId);

        void Update(CustomerViewModel customerViewModel);
        void Remove(Guid id);
    }
}
