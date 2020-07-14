using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyDesk.Repositories;

namespace TinyDesk.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductRepository productRepository;

        public OrderController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Carousel()
        {
            return View(productRepository.GetProducts());
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
