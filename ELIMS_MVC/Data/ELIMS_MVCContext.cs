using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ELIMS_MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ELIMS_MVC.Data;

namespace ELIMS_MVC.Models
{
    public class ELIMS_MVCContext : IdentityDbContext<ApplicationUser>
    {
        public ELIMS_MVCContext (DbContextOptions<ELIMS_MVCContext> options)
            : base(options)
        {
        }


        public DbSet<ELIMS_MVC.Models.Request> Request { get; set; }
        public DbSet<ELIMS_MVC.Models.ContactForm> ContactForm { get; set; }
        //public DbSet<ELIMS_MVC.Models.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Request>()
                .Property(a => a.RequestMade)
                .HasDefaultValueSql("getdate()");

            //modelBuilder.Entity<Request>()
            //    .Property(b => b.Status)
            //    .HasDefaultValue("Pending");

            modelBuilder.Entity<ContactForm>()
                .Property(c => c.ContactDate)
                .HasDefaultValueSql("getdate()");

            
        }
        //public DbSet<ELIMS_MVC.Models.User> Users { get; set; }

        public DbSet<ELIMS_MVC.Models.Inventory> Inventory { get; set; }

    }
}
