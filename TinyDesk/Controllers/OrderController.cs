using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyDesk.Models;
using TinyDesk.Repositories;

namespace TinyDesk.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IProductOrderRepository productOrderRepository;
        private readonly IRegisterRepository registerRepository;

        public OrderController(IProductRepository productRepository, 
            IOrderRepository orderRepository, 
            IProductOrderRepository productOrderRepository,
            IRegisterRepository registerRepository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.productOrderRepository = productOrderRepository;
            this.registerRepository = registerRepository;
        }

        public IActionResult Carousel()
        {
            return View(productRepository.GetProducts());
        }

        public IActionResult Cart(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                orderRepository.AddItem(code);
            }

            Order order = orderRepository.GetOrder();
            var cart = orderRepository.GetProductOrder(order.Id);
            return View(cart);
        }

        public IActionResult Register()
        {
            var order = orderRepository.GetOrder();

            if (order == null)
            {
                return RedirectToAction("Carousel");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Summary(Register register)
        {
            Order order = orderRepository.GetOrder();
            register.OrderId = order.Id;
            registerRepository.Update(order, register);
            var cart = orderRepository.GetProductOrder(order.Id);
            var summary = new SummaryViewModel(register, cart);
            return View(summary);      
        }

      
        private void UpdateQuantity(ItemOrderViewModel item)
        {
            productOrderRepository.UpdateQuantity(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public UpdateResponse RefreshSomeValues([FromBody]ItemOrderViewModel item)
        {
            UpdateQuantity(item);
            var cart = orderRepository.GetProductOrder(item.OrderId);
            try
            {
                var price = cart.CartList.Where(x => x.ProductId == item.ProductId)
                .FirstOrDefault().UnitPrice;
                item.UnitPrice = price;
            }
            catch(Exception)
            {
                ;
            }
            
            var toUpdate = new UpdateResponse(item, cart);

            return toUpdate;
        }



    }
}
