using Microsoft.AspNetCore.Mvc;
using OpetNet.Application.Interfaces;
using OpetNet.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace OpetNetSocial.UI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ICustomerAppService _customerAppService;
        public ProfileController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        [Route("/{urlProfile}")]
        public IActionResult Index(string urlProfile)
        {
            bool aPropria = false;
            var customer = _customerAppService.GetForProfile(urlProfile);
            if (User.Identity.IsAuthenticated)
            {
                
                aPropria = Guid.Parse(User.FindFirst("Id").Value) == customer.Id;
                ViewData["saoAmigos"] = true;
            }

           
            ViewBag.Friends = _customerAppService.GetFriends(customer.Id);
            ViewData["apropria"] = aPropria;
            return View(customer);
        }



    }
}
