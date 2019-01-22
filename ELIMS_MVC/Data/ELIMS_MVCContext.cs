using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ELIMS_MVC.Models
{
    public class ELIMS_MVCContext : DbContext
    {
        public ELIMS_MVCContext (DbContextOptions<ELIMS_MVCContext> options)
            : base(options)
        {
        }

        public DbSet<ELIMS_MVC.Models.Request> Request { get; set; }
    }
}
