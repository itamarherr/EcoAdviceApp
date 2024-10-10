using Microsoft.AspNetCore.Mvc;

namespace EcoAdviceAppApi.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
