using MEChallenge.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MEChallenge.Data.Mappings
{
    public class OrderMapping : AbstractMapping<Order>
    {
        public override void Map(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .HasMaxLength(20);
        }
    }
}
