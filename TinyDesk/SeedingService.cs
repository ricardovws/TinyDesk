using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TinyDesk.Models;

namespace TinyDesk
{
    public class SeedingService
    {
        private readonly ApplicationContext context;

        public SeedingService(ApplicationContext context)
        {
            this.context = context;
        }

        public void Seed()
        {
            if(context.Order.Any()
                ||
                context.Product.Any()
                ||
                context.ProductOrder.Any()
                ||
                context.Register.Any())
            {
                return; //DB has been seeded.
            }

            var json = File.ReadAllText("books.json");
            var books = JsonConvert.DeserializeObject<List<Book>>(json);

            foreach(var book in books)
            {
                context.Set<Product>().Add(new Product(book.Code, book.Name, book.Price));
            }
            context.SaveChanges();
        }
    }

    class Book
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
