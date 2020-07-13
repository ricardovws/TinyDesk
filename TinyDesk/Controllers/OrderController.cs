using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyDesk.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Carousel()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Summary()
        {
            return View();
        }
    }
}
