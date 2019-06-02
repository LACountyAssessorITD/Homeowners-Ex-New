using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HomeOwners_Exemption.Models
{
    public class HOXContext:DbContext
    {
        public HOXContext(DbContextOptions<HOXContext> options) : base(options)
        { }

        public HOXContext()
        {
        }

        public DbSet<Claim> claim { get; set; }
    }
}
