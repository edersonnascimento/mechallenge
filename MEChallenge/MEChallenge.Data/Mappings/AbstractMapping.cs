using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MEChallenge.Data.Mappings
{
    public abstract class AbstractMapping<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}
