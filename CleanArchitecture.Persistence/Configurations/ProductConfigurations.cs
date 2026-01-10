using CleanArchitecture.Domain.Common.EntityConstraints;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasKey(p => p.ID);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(ProductConsts.ProductNameMaxLength);

            builder.Property(p => p.Description)
                .HasMaxLength(ProductConsts.ProductDescriptionMaxLength);

            builder.Property(p => p.Price)
                .HasColumnType(ProductConsts.ProductPriceColumnType);

            builder.Property(p => p.Stock)
                .IsRequired();

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId); 

            builder.HasIndex(p => p.Name);
        }
    }
}
