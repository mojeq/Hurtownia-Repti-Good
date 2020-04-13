﻿using HurtowniaReptiGood.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models
{
    public class MyContex: DbContext
    {    
        public MyContex(DbContextOptions<MyContex> options)
            :base(options)
        {
        }

        public DbSet<InvoiceAddressEntity> InvoiceAddresses { get; set; }
        public DbSet<ShippingAddressEntity> ShippingAddresses { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<OrderEntity> OrderDetails { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            builder.Entity<OrderEntity>()
                .HasOne(p => p.Customer)
                .WithMany(o => o.OrderList);

            builder.Entity<OrderDetailEntity>()
                .HasOne(p => p.Order)
                .WithMany(o => o.OrderDetails);
        }
    }
}
