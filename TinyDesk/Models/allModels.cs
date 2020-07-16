using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TinyDesk.Models
{

    [DataContract]
    public class BaseModel
    {
        [DataMember]
        public int Id { get; protected set; }
    }

    public class Product : BaseModel
    {
        public Product()
        {

        }

        [Required]
        public string Code { get; private set; }
        [Required]
        public string Name { get; private set; }
        [Required]
        public decimal Price { get; private set; }

        public Product(string code, string name, decimal price)
        {
            Code = code;
            Name = name;
            Price = price;
        }
    }

    public class Register : BaseModel
    {
        public Register()
        {
        }

        public virtual int OrderId { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string Phone { get; set; } = "";
        [Required]
        public string Address { get; set; } = "";
        [Required]
        public string Address2 { get; set; } = "";
        [Required]
        public string City { get; set; } = "";
        [Required]
        public string State { get; set; } = "";
        [Required]
        public string Country { get; set; } = "";
        [Required]
        public string ZipCode { get; set; } = "";
    }

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

    public class Order : BaseModel
    {
        //public Order()
        //{
        //    Register = new Register();
        //}

        //public Order(Register register)
        //{
        //    Register = register;
        //}

        //public List<ProductOrder> Itens { get; private set; } = new List<ProductOrder>();
        //[Required]
        public virtual int RegisterId{ get; private set; }
    }

}
