using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyDesk.Models
{
    public class ProductOrder : BaseModel
    {
        [Required]
        public int OrderId { get; private set; }
        [Required]
        public int ProductId { get; private set; }
        [Required]
        public int Quantity { get; private set; }
        [Required]
        public decimal UnitPrice { get; private set; }

        public ProductOrder()
        {
        }

        public ProductOrder(int orderId, int productId, int quantity, decimal unitPrice)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        internal void RefreshQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}
