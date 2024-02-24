using Microsoft.EntityFrameworkCore;
using StatsCounter.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace StatsCounter.Data
{
    public class StatsCounterDbContext : DbContext
    {
        public DbSet<RepositoryStatsHistory> RepositoryStatsHistories { get; set; }

        public StatsCounterDbContext(DbContextOptions<StatsCounterDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your model here if needed
            base.OnModelCreating(modelBuilder);
        }

        internal Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}