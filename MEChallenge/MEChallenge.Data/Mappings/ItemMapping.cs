using MEChallenge.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MEChallenge.Data.Mappings
{
    public class ItemMapping : AbstractMapping<Item>
    {
        public override void Map(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(i => i.Description);
            builder.Property(i => i.Description)
                .HasMaxLength(60);
        }
    }
}
