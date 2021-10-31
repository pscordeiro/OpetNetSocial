using OpetNet.Domain.Core.Bus;
using OpetNet.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OpetNet.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class HomeController : ApiController
    {
        public HomeController(INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("index", Name = "index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok();
        }
    }
}
