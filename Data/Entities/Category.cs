using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities
{
    public class Category
    {
        public int CategoryId { set; get; }
        public string? CategoryName { set; get; }
        public string? Description { set; get; }
        public ICollection<Course>? Courses { set; get; }
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.CategoryId);
            
            builder.Property(x => x.CategoryId)
                   .UseIdentityColumn();
            builder.Property(x => x.CategoryName)
                   .IsRequired()
                   .HasMaxLength(255);
        }
    }
}

