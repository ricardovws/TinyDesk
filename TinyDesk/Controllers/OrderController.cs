﻿using Microsoft.AspNetCore.Mvc;
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

        public OrderController(IProductRepository productRepository, 
            IOrderRepository orderRepository, 
            IProductOrderRepository productOrderRepository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.productOrderRepository = productOrderRepository;
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
            return View();
        }

        public IActionResult Summary()
        {
            Order order = orderRepository.GetOrder();
            return View(orderRepository.GetProductOrder(order.Id));      
        }

        [HttpPost]
        public void UpdateQuantity([FromBody]ItemOrderViewModel item)
        {
            productOrderRepository.UpdateQuantity(item);
        }

        [HttpPost]
        public UpdateResponse RefreshSomeValues([FromBody]ItemOrderViewModel item)
        {
            UpdateQuantity(item);
            var cart = orderRepository.GetProductOrder(item.OrderId);
            item.UnitPrice = productOrderRepository.GetPrice(item);
            var toUpdate = new UpdateResponse(item, cart);

            return toUpdate;
        }



    }
}
