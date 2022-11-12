using Microsoft.AspNetCore.Mvc;

namespace RedisSearchDemo.Controllers
{
    public class HelloRedisController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
