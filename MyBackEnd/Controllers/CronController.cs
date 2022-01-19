using Microsoft.AspNetCore.Mvc;

namespace MyBackEnd.Controllers
{
    [ApiController]
    public class CronController : ControllerBase
    {
        [HttpPost("/cron")]
        public IActionResult Index()
        {
            Console.WriteLine($"hello from the cron input binding................{DateTime.Now.ToString("yyyyMMdd H:m:s")}");
            return Ok();
        }
    }
}
