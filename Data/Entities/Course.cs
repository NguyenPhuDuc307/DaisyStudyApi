using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities
{
    public class Course
    {
        public int CourseId { set; get; }
        public int CategoryId { set; get; }
        public Category? Category { set; get; }
        public string? CourseName { set; get; }
        public string? Description { set; get; }
        public DateTime DateCreated { set; get; }
        public string? ImagePath { set; get; }
        public ICollection<Lesson>? Lessons { set; get; }
        public ICollection<CourseMember>? CourseMembers { set; get; }
    }

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(x => x.CourseId);

            builder.Property(x => x.CourseId)
                   .UseIdentityColumn();
            builder.Property(x => x.CourseName)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.Property(x => x.ImagePath)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.Property(x => x.DateCreated)
                   .IsRequired();

            builder.HasOne(x => x.Category)
                   .WithMany(x => x.Courses)
                   .HasForeignKey(x => x.CategoryId);
        }
    }
}

