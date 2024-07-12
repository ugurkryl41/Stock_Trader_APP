using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=KARAYEL;database=SuperTraderDB;integrated security=true;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<PortfolioItem>()
                .HasKey(pi => pi.Id);

            modelBuilder.Entity<Stock>()
                .Property(s => s.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.Property(e => e.userBalance).HasColumnType("decimal(18,2)");
            });

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);

        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Stocks
            modelBuilder.Entity<Stock>().HasData(
                new Stock { Id = 1, Symbol = "ABC", Price = 100.00m },
                new Stock { Id = 2, Symbol = "DEF", Price = 200.00m },
                new Stock { Id = 3, Symbol = "GHI", Price = 300.00m },
                new Stock { Id = 4, Symbol = "JKL", Price = 400.00m },
                new Stock { Id = 5, Symbol = "MNO", Price = 500.00m }
            );

            // Seed Portfolios
            modelBuilder.Entity<Portfolio>().HasData(
                new Portfolio { Id = 1, UserId = "user1", userBalance = 1000.00m },
                new Portfolio { Id = 2, UserId = "user2", userBalance = 2000.00m },
                new Portfolio { Id = 3, UserId = "user3", userBalance = 3000.00m },
                new Portfolio { Id = 4, UserId = "user4", userBalance = 4000.00m },
                new Portfolio { Id = 5, UserId = "user5", userBalance = 5000.00m }
            );

            // Seed Portfolio Items
            modelBuilder.Entity<PortfolioItem>().HasData(
                new PortfolioItem { Id = 1, PortfolioId = 1, StockId = 1, Quantity = 10 },
                new PortfolioItem { Id = 2, PortfolioId = 2, StockId = 2, Quantity = 20 },
                new PortfolioItem { Id = 3, PortfolioId = 3, StockId = 3, Quantity = 30 },
                new PortfolioItem { Id = 4, PortfolioId = 4, StockId = 4, Quantity = 40 },
                new PortfolioItem { Id = 5, PortfolioId = 5, StockId = 5, Quantity = 50 }
            );
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }

    }
}
