﻿using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRMSystem.Infrastructure
{
    public class TContext : DbContext
    {

        public TContext(DbContextOptions<TContext> options):base(options)
        { 
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Cart>().Property(p => p.Amount)
            //    .HasColumnType("decimal(18,4)");

        }



        public DbSet<User> AppUsers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }


    }
}
