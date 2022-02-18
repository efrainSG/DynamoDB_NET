using Microsoft.AspNetCore.Mvc;

namespace MovieRank.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
