using EFCodeFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EFCodeFirst.Models.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("connString") { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Employee2> Employees2 { get; set; }
        public DbSet<Designation> Designations { get; set; }
    }
}