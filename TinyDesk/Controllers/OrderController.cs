using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyDesk.Areas.Identity.Data;
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
        private readonly UserManager<AppIdentityUser> userManager;

        public OrderController(IProductRepository productRepository, 
            IOrderRepository orderRepository, 
            IProductOrderRepository productOrderRepository,
            IRegisterRepository registerRepository,
            UserManager<AppIdentityUser> userManager)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.productOrderRepository = productOrderRepository;
            this.registerRepository = registerRepository;
            this.userManager = userManager;
        }

        public IActionResult Carousel()
        {
            return View(productRepository.GetProducts());
        }

        [Authorize]
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

        [Authorize]
        public async Task<IActionResult> Register()
        {
            var order = orderRepository.GetOrder();

            if (order == null)
            {
                return RedirectToAction("Carousel");
            }

            var user = await userManager.GetUserAsync(this.User);

            //Catch the last register with the same email user

            var register = registerRepository.CatchRegister(user.Email);

            return View(register);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Summary(Register register)
        {
            var user = await userManager.GetUserAsync(this.User);
            var registerDB = registerRepository.CatchRegister(user.Email);

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
        [Authorize]
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
