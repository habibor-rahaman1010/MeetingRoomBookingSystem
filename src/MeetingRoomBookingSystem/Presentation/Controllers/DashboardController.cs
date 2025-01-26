using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var model = new MeetingRoomDashboardModel();
            return View(model);
        }
    }
}
