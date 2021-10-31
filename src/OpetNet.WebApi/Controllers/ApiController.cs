using OpetNet.Domain.Core.Bus;
using OpetNet.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace OpetNet.WebApi.Controllers
{
    public class ApiController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;

        protected ApiController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator
        )
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => new { n.Message })
            });
        }

        protected new ActionResult<T> Response<T>(HttpStatusCode code, T result)
        {
            if (IsValidOperation())
            {
                return StatusCode((int)code, result);
            }

            switch (code)
            {
                case HttpStatusCode.NotFound:
                    return NotFound(new
                    {
                        success = false,
                        errors = _notifications.GetNotifications().Select(n => n.Message)
                    });
                case HttpStatusCode.Created:
                    return Created(string.Empty, result);
                default:
                    return BadRequest(new
                    {
                        success = false,
                        errors = _notifications.GetNotifications().Select(n => n.Message)
                    });
            }


        }

        protected new IActionResult Response(HttpStatusCode code, object result = null)
        {
            if (IsValidOperation())
            {
                switch (code)
                {
                    case HttpStatusCode.NotFound:
                        return NotFound(result);
                    case HttpStatusCode.Created:
                        return Created(string.Empty, result);
                    default:
                        return Ok(result);
                }
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => new { n.Code, n.Message })
            });
        }

        protected void NotifyModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected void NotifyError(string code, string message)
        {
            _mediator.RaiseEvent(new DomainNotification(code, message));
        }

        protected void AddIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                NotifyError(result.ToString(), error.Description);
            }
        }

    }
}
