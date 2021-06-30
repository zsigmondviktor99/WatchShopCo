using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using webshop_gyakorlas.Models;

namespace webshop_gyakorlas.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //TODO: Get the connectionString from the JSON...
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-webshop_gyakorlas-8A29A5CD-3B6B-4288-8BCD-A3FEC36273C9;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        public DbSet<Watch> Wathces { get; set; }
        public DbSet<Brand> Brands { get; set; }
    }
}
