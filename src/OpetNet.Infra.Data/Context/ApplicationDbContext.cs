﻿using Microsoft.EntityFrameworkCore;
using OpetNet.Domain.Models;

namespace OpetNet.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }
}