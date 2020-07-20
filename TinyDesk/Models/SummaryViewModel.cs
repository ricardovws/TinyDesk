using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyDesk.Models
{
    public class SummaryViewModel
    {
        public Register Register { get; set; }
        public CartViewModel Cart { get; set; }

        public SummaryViewModel(Register register, CartViewModel cart)
        {
            Register = register;
            Cart = cart;
        }
    }
}
