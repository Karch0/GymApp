
using ConsoleApp4.Data;
using ConsoleApp4.Data.Models;
using ConsoleApp4.Models;
using ConsoleApp4.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.Data
{
    public class GymContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<GymLocation> GymLocations { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<RewardsShop> RewardsShops { get; set; }

        public GymContext() { }

        public GymContext(DbContextOptions<GymContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Client>()
                .HasOne(c => c.Subscription)
                .WithOne(s => s.Client)
                .HasForeignKey<Subscription>(s => s.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Coach>()
                .HasMany(c => c.Clients)
                .WithOne(c => c.Coach)
                .HasForeignKey(c => c.CoachId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<GymLocation>()
                .HasMany(gl => gl.Coaches)
                .WithOne(c => c.GymLocation)    
                .HasForeignKey(c => c.GymLocationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GymLocation>()
                .HasMany(gl => gl.Subscriptions)
                .WithOne(s => s.GymLocation)
                .HasForeignKey(s => s.GymLocationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
