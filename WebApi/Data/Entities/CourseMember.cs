using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities
{
    public class CourseMember
    {
        public int CourseId { set; get; }
        public Course? Course { set; get; }
        public int UserId { set; get; }
        public User? User { set; get; }
    }

    public class CourseMemberConfiguration : IEntityTypeConfiguration<CourseMember>
    {
        public void Configure(EntityTypeBuilder<CourseMember> builder)
        {
            builder.ToTable("CourseMembers");

            builder.HasKey(x => new { x.CourseId , x.UserId});

            builder.HasOne(x => x.Course).WithMany(x => x.CourseMembers).HasForeignKey(x => x.CourseId);
            builder.HasOne(x => x.User).WithMany(x => x.CourseMembers).HasForeignKey(x => x.UserId);
        }
    }
}