using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyDesk.Models
{
    public class Order : BaseModel
    {
        public virtual int RegisterId { get; private set; }

        [Required]
        public string ClientId { get; set; }

        internal void InsertRegisterId(int Id)
        {
            RegisterId = Id;
        }

        public Order(string clientId)
        {
            ClientId = clientId;
        }
    }
}
