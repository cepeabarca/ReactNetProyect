using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReactNetProyect.BackEnd.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactNetProyect.BackEnd.Data
{
    public class ReactNetProyectContext : IdentityDbContext
    {
        public ReactNetProyectContext(DbContextOptions<ReactNetProyectContext> options) : base(options) { }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Provider).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Amount).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Date).IsRequired().HasColumnType("date");
                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.HasOne(e => e.Currency)
                    .WithMany(c => c.Receipts)
                    .HasForeignKey(e => e.CurrencyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(3);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);

                entity.HasData(
                new Currency { Id = 1, Code = "USD", Name = "US Dollar" },
                new Currency { Id = 2, Code = "EUR", Name = "Euro" },
                new Currency { Id = 3, Code = "JPY", Name = "Japanese Yen" },
                new Currency { Id = 4, Code = "GBP", Name = "British Pound" },
                new Currency { Id = 5, Code = "AUD", Name = "Australian Dollar" },
                new Currency { Id = 6, Code = "CAD", Name = "Canadian Dollar" },
                new Currency { Id = 7, Code = "CHF", Name = "Swiss Franc" },
                new Currency { Id = 8, Code = "CNY", Name = "Chinese Yuan" },
                new Currency { Id = 9, Code = "HKD", Name = "Hong Kong Dollar" },
                new Currency { Id = 10, Code = "MXN", Name = "Mexican Peso" });
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
