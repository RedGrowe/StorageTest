using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Nomenclature> Nomenclatures { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=task;Username=postgres;Password=1234");
        }
    }
}
