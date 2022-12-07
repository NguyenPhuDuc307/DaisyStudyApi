using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public DateTime Dob { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<CourseMember>? CourseMembers { set; get; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.UserId);

            builder.Property(x => x.FullName)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.Property(x => x.Dob)
                   .IsRequired();
            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.Property(x => x.Password)
                   .IsRequired()
                   .HasMaxLength(255);
        }
    }
}