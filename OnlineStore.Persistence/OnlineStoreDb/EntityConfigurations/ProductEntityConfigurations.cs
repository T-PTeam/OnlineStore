using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Core.Domain.Products.Models;

namespace OnlineStore.Persistence.OnlineStoreDb.EntityConfigurations;

public class ProductEntityConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Price)
            .IsRequired()
            .HasMaxLength(8)
            .HasColumnType("decimal(8, 2)");
    }
}
