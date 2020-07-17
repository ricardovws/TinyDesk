using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyDesk.Models
{
    public class ItemOrderViewModel
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; private set; }

        public string ProductCode { get; set; }

        public int Quantity { get; private set; }
        
        public decimal UnitPrice { get; set; }

        public ItemOrderViewModel(int orderId, int productId, string productName, string productCode, int quantity, decimal unitPrice)
        {
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            ProductCode = productCode;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public decimal SubTotal => Quantity * UnitPrice;

    }
}

