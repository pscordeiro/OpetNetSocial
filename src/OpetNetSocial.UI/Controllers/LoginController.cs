using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OpetNet.Application.Interfaces;
using OpetNet.Application.ViewModels;
using OpetNetSocial.UI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace OpetNetSocial.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ICustomerAppService _customerAppService;

        public LoginController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Login_DTO login_DTO)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }
            CustomerViewModel usuario = _customerAppService.GetByEmailAndPassWord(login_DTO.Email, login_DTO.PassWord);

            if(usuario is null)
            {
                ViewBag.Erro = "Usuario ou senha incorretos";
                return View();
            }


            Login(usuario);
            return RedirectToAction("Home", "Index");
        }
        private async void Login(CustomerViewModel usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim(ClaimTypes.Role, "Usuario_Comum"),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.SerialNumber, usuario.Id.ToString())
            };

            var identidadeDeUsuario = new ClaimsIdentity(claims, "Email");

            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identidadeDeUsuario);

            var propriedadesDeAutenticacao = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(2),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, propriedadesDeAutenticacao);

            /*
             Primeiro criamos uma lista de Claims, que é onde nós iremos atribuir as informações do usuário.
            Depois criamos uma ClaimsIdentity, que define o tempo de autenticação.
            Então nós criamos a ClaimPrincipal que é a junção das duas anteriores que será passado como parâmetro para autenticação.
            Após isso criei um AuthenticationProperties que é onde contém as propriedades de autenticação contendo algumas informações de persistência de dados e afins, entre elas, tempo de expiração, dados persistentes após fechamento do navegador e continuará autenticado mesmo se atualizar a página.
            E por fim nós autenticamos utilizando as informações da ClaimPrincipal e as configurações de AuthenticationProperties.
             */
        }
    }
}
