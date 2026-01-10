using CleanArchitecture.Domain.Common.EntityConstraints;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.HasKey(c => c.ID);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(CategoryConsts.CategoryNameMaxLength);

            builder.Property(c => c.Description)
                .HasMaxLength(CategoryConsts.CategoryDescriptionMaxLength);

            builder.HasIndex(c => c.Name);
        }
    }
}
