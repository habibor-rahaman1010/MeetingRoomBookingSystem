using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class MeetingRoomController : Controller
    {
        public IActionResult Index()
        {
            var model = new MeetingRoomCreateModel();
            return View(model);
        }

        
    }
}
