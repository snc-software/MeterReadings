using System;
using MeterReadings.Domain;
using MeterReadings.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace MeterReadings.Infrastructure
{
    public class MeterReadingDbContext : DbContext
    {
        public MeterReadingDbContext(DbContextOptions<MeterReadingDbContext> options)
            : base(options)
        {
        }

        public DbSet<MeterReadingModel> MeterReadings { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new MeterReadingConfiguration());
        }
    }
}