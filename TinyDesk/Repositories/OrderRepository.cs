using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyDesk.Areas.Identity.Data;
using TinyDesk.Models;

namespace TinyDesk.Repositories
{
    public interface IOrderRepository
    {
        Order GetOrder();
        void AddItem(string code);
        CartViewModel GetProductOrder(int id);
    }

    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly UserManager<AppIdentityUser> userManager;

        public OrderRepository(ApplicationContext context,
            IHttpContextAccessor contextAccessor,
            UserManager<AppIdentityUser> userManager) : base(context)
        {
            this.contextAccessor = contextAccessor;
            this.userManager = userManager;
        }

        public void AddItem(string code)
        {
            var product = context.Product.Where(p => p.Code == code).SingleOrDefault();

            if (product == null)
            {
                throw new ArgumentException("Product not found!");
            }

            var order = GetOrder();

            var productOrders = context.ProductOrder.Where(p => p.OrderId == order.Id
            && p.ProductId == product.Id).SingleOrDefault();

            if (productOrders == null)
            {
                var productOrder = new ProductOrder(order.Id, product.Id, 1, product.Price);

                context.ProductOrder.Add(productOrder);
                context.SaveChanges();
            }
   
        }

        

        public Order GetOrder()
        {
            var orderId = GetOrderId();
            var order = context.Order
                .Where(o => o.Id == orderId).SingleOrDefault();
            
            if (order == null)
            {

                order = new Order(GetClientId());
                context.Order.Add(order);
                context.SaveChanges();
                SetOrderId(order.Id);
            }

            return order;
        }

        public CartViewModel GetProductOrder(int id)
        {
            var list = context.ProductOrder.Where(p => p.OrderId == id);
            List<ItemOrderViewModel> listResult = new List<ItemOrderViewModel>();
            foreach(var item in list)
            {
                var productItemId = item.ProductId;
                var productInfos = context.Product.Where(x => x.Id == productItemId).FirstOrDefault();
                var itemOrderViewModel = new ItemOrderViewModel
                    (item.OrderId, productInfos.Id, productInfos.Name, productInfos.Code, 
                    item.Quantity, item.UnitPrice);
                listResult.Add(itemOrderViewModel);
            }
            var cart = new CartViewModel(listResult);
            return cart;
        }

        private string GetClientId()
        {
            var claimsPrincipal = contextAccessor.HttpContext.User;
            var clientId = userManager.GetUserId(claimsPrincipal);
            return clientId;
        }

        private int? GetOrderId()
        {
            return contextAccessor.HttpContext.Session.GetInt32($"orderId_{GetClientId()}");
        }

        private void SetOrderId(int orderId)
        {
            contextAccessor.HttpContext.Session.SetInt32($"orderId_{GetClientId()}", orderId);
        }

       
    }
}
