using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpetNet.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OpetNetSocial.UI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ICustomerAppService _customerAppService;
        private readonly IUploadAppService _uploadAppService;
        public ProfileController(ICustomerAppService customerAppService, IUploadAppService uploadAppService)
        {
            _customerAppService = customerAppService;
            _uploadAppService = uploadAppService;
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

        [HttpPost, Route("/upload-imagem-profile")]
        public async Task<IActionResult> UploadImagemPerfil(IFormFile image)
        {
            if (User.Identity.IsAuthenticated)
            {
                _customerAppService.AtualizarFotoDoPerfil(Guid.Parse(User.FindFirst("Id").Value), image);
                return Ok();
            }
            return Unauthorized();
        }

    }
}
