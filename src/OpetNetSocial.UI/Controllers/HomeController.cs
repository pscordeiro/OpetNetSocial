using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpetNet.Application.Interfaces;
using OpetNet.Application.ViewModels;
using OpetNetSocial.UI.Models;
using System;
using System.Diagnostics;

namespace OpetNetSocial.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerAppService _customerAppService;
        private readonly IPostAppService _postAppService;
        public HomeController(ILogger<HomeController> logger, ICustomerAppService customerAppService,
            IPostAppService postAppService)
        {
            _logger = logger;
            _customerAppService = customerAppService;
            _postAppService = postAppService;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.PostsRecentes = _postAppService.GetRecentPost(Guid.Parse(User.FindFirst("Id").Value));
                return View("HomeLogado");
            }

            return View();
        }
        public IActionResult CreatePost(PostViewModel postViewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            postViewModel.CustomerId = Guid.Parse(User.FindFirst("Id").Value);
            Console.WriteLine("USE ID " + postViewModel.CustomerId);
            _postAppService.Register(postViewModel);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
