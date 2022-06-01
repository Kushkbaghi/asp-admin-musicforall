using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicForAllAdmin.Models;

namespace MusicForAllAdmin.Data
{
    public class MusicForAllAdminContext : DbContext
    {
        public MusicForAllAdminContext (DbContextOptions<MusicForAllAdminContext> options)
            : base(options)
        {
        }

        public DbSet<MusicForAllAdmin.Models.Product>? Product { get; set; }
    }
}
