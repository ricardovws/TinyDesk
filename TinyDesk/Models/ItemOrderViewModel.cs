using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyDesk.Models
{
    public class ItemOrderViewModel
    {
        public int Id { get; set; }

        public string Name { get; private set; }

        public string Code { get; set; }

        public int Quantity { get; private set; }
        
        public decimal UnitPrice { get; private set; }

        public ItemOrderViewModel(int id, string name, string code, int quantity, decimal unitPrice)
        {
            Id = id;
            Name = name;
            Code = code;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
