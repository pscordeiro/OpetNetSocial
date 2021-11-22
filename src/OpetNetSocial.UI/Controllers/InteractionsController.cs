using Microsoft.AspNetCore.Mvc;
using OpetNet.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace OpetNetSocial.UI.Controllers
{
    public class InteractionsController : Controller
    {
        private readonly ICustomerAppService _customerAppService;

        public InteractionsController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        public async Task<IActionResult> AddFriend(Guid friendId)
        {
            Guid customerId = Guid.Parse(User.FindFirst("Id").Value);
            _customerAppService.AddFriend(customerId, friendId);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LikeAPost(int idPost)
        {
            return Ok();
        }
    }
}
