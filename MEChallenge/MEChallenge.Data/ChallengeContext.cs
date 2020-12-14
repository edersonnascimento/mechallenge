using MEChallenge.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MEChallenge.Data
{
    public class ChallengeContext : DbContext
    {
        public ChallengeContext(DbContextOptions<ChallengeContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
