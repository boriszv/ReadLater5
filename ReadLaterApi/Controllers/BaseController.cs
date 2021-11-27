using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ReadLaterApi.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        protected string UserID
        {
            get => User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
