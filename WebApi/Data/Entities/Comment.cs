using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities
{
    public class Comment
    {
        public int CommentId { set; get; }
        public int LessonId { set; get; }
        public Lesson? Lesson { set; get; }
        public int UserId { set; get; }
        public User? AppUser { set; get; }
        public string? Content { set; get; }
        public DateTime DateTimeCreated { set; get; }
    }

    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(x => x.CommentId);

            builder.Property(x => x.CommentId)
                   .UseIdentityColumn();
            builder.Property(x => x.UserId)
                   .IsRequired();
            builder.Property(x => x.LessonId)
                   .IsRequired();
            builder.Property(x => x.Content)
                   .IsRequired();
            builder.Property(x => x.DateTimeCreated)
                   .IsRequired();

            builder.HasOne(x => x.Lesson)
                   .WithMany(x => x.Comments)
                   .HasForeignKey(x => x.LessonId);
            builder.HasOne(x => x.AppUser)
                   .WithMany(x => x.Comments)
                   .HasForeignKey(x => x.UserId);
        }
    }
}
