using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Galactus.Models;

namespace Galactus.Data
{
    public class GalactusContext : DbContext
    {
        public GalactusContext (DbContextOptions<GalactusContext> options)
            : base(options)
        {
        }

        public DbSet<Galactus.Models.Planeta> Planeta { get; set; }

        public DbSet<Galactus.Models.TimeControle> TimeControle { get; set; }

    
    }
}
