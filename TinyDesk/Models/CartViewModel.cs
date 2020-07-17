using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyDesk.Models
{
    public class CartViewModel
    {
        public List<ItemOrderViewModel> CartList { get; }

        public CartViewModel(List<ItemOrderViewModel> cartList)
        {
            CartList = cartList;
        }

        public decimal Total => CartList.Sum(s => s.SubTotal);

        public int TotalItens => CartList.Sum(s => s.Quantity);
    }
}
