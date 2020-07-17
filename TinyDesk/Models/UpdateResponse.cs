using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyDesk.Models
{
    public class UpdateResponse
    {
        public ItemOrderViewModel UpdatedOne { get; }
        public object Cart { get; }

        public UpdateResponse(ItemOrderViewModel updatedOne, object cart)
        {
            UpdatedOne = updatedOne;
            Cart = cart;
        }
    }
}
