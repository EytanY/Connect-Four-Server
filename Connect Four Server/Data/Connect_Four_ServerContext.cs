using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Connect_Four_Server.Models.Tables;

namespace Connect_Four_Server.Data
{
    public class Connect_Four_ServerContext : DbContext
    {
        public Connect_Four_ServerContext (DbContextOptions<Connect_Four_ServerContext> options)
            : base(options)
        {
        }

        public DbSet<Connect_Four_Server.Models.Tables.PlayersTbl> PlayersTbl { get; set; } = default!;

        public DbSet<Connect_Four_Server.Models.Tables.GamesTbl>? GamesTbl { get; set; }
    }
}
