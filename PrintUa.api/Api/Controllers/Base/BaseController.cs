using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace Api.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected Guid GetUserId()
        {
            return Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
