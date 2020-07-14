using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyDesk.Models;


namespace TinyDesk
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<ProductOrder> ProductOrder { get; set; }
        public DbSet<Register> Register { get; set; }


        // I tried to do this one below so many times. But it doesnt work. 
        // v
        // v
        // v
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Product>().HasKey(t => t.Id);

        //    modelBuilder.Entity<Order>().HasKey(t => t.Id);
        //    modelBuilder.Entity<Order>().HasMany(t => t.Itens).WithOne(t => t.Order);
        //    modelBuilder.Entity<Order>().HasOne(t => t.Register).WithOne(t => t.Order).IsRequired();

        //    modelBuilder.Entity<ProductOrder>().HasKey(t => t.Id);
        //    modelBuilder.Entity<ProductOrder>().HasOne(t => t.Order);
        //    modelBuilder.Entity<ProductOrder>().HasOne(t => t.Product);

        //    / modelBuilder.Entity<Register>().HasKey(t => t.Id);
        //    modelBuilder.Entity<Register>().HasOne(t => t.Order);
        //} 
    }
}
