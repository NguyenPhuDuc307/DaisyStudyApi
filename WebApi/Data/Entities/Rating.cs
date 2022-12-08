using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities
{
    public class Rating
    {
        public int CourseId { set; get; }
        public Course? Course { set; get; }
        public int UserId { set; get; }
        public User? User { set; get; }
        public int Stars { set; get; }
        public string? Message { set; get; }
        public DateTime DateCreated { set; get; }
    }

    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Ratings");

            builder.HasKey(x => new { x.CourseId, x.UserId });

            builder.Property(x => x.Message)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.Property(x => x.Stars)
                   .IsRequired();
            builder.Property(x => x.DateCreated)
                   .IsRequired();
        }
    }
}

