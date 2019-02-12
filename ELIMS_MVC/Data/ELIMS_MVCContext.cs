using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ELIMS_MVC.Models;

namespace ELIMS_MVC.Models
{
    public class ELIMS_MVCContext : DbContext
    {
        public ELIMS_MVCContext (DbContextOptions<ELIMS_MVCContext> options)
            : base(options)
        {
        }

        public DbSet<ELIMS_MVC.Models.Request> Request { get; set; }
        public DbSet<ELIMS_MVC.Models.ContactForm> ContactForm { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Request>()
                .Property(a => a.RequestMade)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Request>()
                .Property(b => b.RequestStatus)
                .HasDefaultValue("Pending");

            modelBuilder.Entity<ContactForm>()
                .Property(c => c.ContactDate)
                .HasDefaultValueSql("getdate()");
        }

    }
}
