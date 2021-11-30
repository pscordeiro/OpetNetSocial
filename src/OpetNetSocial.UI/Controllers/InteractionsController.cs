using Microsoft.AspNetCore.Mvc;
using OpetNet.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace OpetNetSocial.UI.Controllers
{
    public class InteractionsController : Controller
    {
        private readonly ICustomerAppService _customerAppService;
        private readonly IPostAppService _postAppService;

        public InteractionsController(ICustomerAppService customerAppService, IPostAppService postAppService)
        {
            _customerAppService = customerAppService;
            _postAppService = postAppService;
        }

        public async Task<IActionResult> AddFriend(Guid friendId)
        {
            Guid customerId = Guid.Parse(User.FindFirst("Id").Value);
            _customerAppService.AddFriend(customerId, friendId);

            return RedirectToAction("Index", "Home");
        }

        public async Task LikeAPost(int postId)
        {
            Guid customerId = Guid.Parse(User.FindFirst("Id").Value);
             _postAppService.RegisterLikeInPost(customerId, postId);
        }
    }
}
