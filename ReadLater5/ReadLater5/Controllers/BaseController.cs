using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ReadLater5.Controllers
{

    public class BaseController : Controller
    {
        protected string UserID
        {
            get => User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
