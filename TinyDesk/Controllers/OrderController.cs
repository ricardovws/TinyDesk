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

        public OrderController(IProductRepository productRepository,
            IOrderRepository orderRepository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
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
            var productOrders = orderRepository.GetProductOrder(order.Id);
            return View(productOrders);
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
    }
}
