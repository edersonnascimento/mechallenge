using MEChallenge.Data.Mappings;
using MEChallenge.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MEChallenge.Data
{
    public class ChallengeContext : DbContext
    {
        public ChallengeContext(DbContextOptions<ChallengeContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new ItemMapping());
            modelBuilder.AddConfiguration(new OrderMapping());

            base.OnModelCreating(modelBuilder); 
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
