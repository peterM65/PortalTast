using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.Infrastructure.Entities;
using Portal.Infrastructure.Extend;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Database
{
    public partial class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {


        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt) { }

    }

    public partial class ApplicationContext 
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
    }
}
