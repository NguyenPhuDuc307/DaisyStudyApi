using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities
{
    public class Lesson
    {
        public int LessonId { set; get; }
        public int CourseId { set; get; }
        public Course? Course { set; get; }
        public string? LessonName { set; get; }
        public string? Content { set; get; }
        public DateTime DateCreated { set; get; }
        public string? ImagePath { set; get; }
        public ICollection<Comment>? Comments { get; set; }
    }

    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lessons");

            builder.HasKey(x => x.LessonId);

            builder.Property(x => x.LessonId)
                   .UseIdentityColumn();
            builder.Property(x => x.LessonName)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.Property(x => x.ImagePath)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.Property(x => x.DateCreated)
                   .IsRequired();

            builder.HasOne(x => x.Course)
                    .WithMany(x => x.Lessons)
                    .HasForeignKey(x => x.CourseId);
        }
    }
}

