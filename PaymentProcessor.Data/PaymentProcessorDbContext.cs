using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PaymentProcessor.Data.Entities;
using System.IO;

namespace PaymentProcessor.Data
{
    public class PaymentProcessorDbContext: DbContext
    {
        public PaymentProcessorDbContext()
        {

        }

        public PaymentProcessorDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<PaymentStatus> PaymentStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("PaymentProcessorDbContext");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Payment>().ToTable("Payment");
            modelBuilder.Entity<PaymentStatus>().ToTable("PaymentStatus");
        }
    }
}
