using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace DapperEFCorePerformanceBenchmarks.Models
{
    public partial class SportContextEfCore : DbContext
    {
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Sport> Sports { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        public SportContextEfCore(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sport>()
                .HasMany(e => e.Teams);


            modelBuilder.Entity<Team>()
                .HasMany(e => e.Players);
        }
    }
}
