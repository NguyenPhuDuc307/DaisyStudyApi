using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public DateTime Dob { get; set; }
        public int Code { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirm { get; set; }
        public string? Password { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<CourseMember>? CourseMembers { set; get; }

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                FullName = "Nguyễn Phú Đức",
                Dob = new DateTime(2001,07,30),
                Code = 562765,
                Email = "nguyenphuduc62001@gmail.com",
                EmailConfirm = true,
                Password = "FE9989D5012230C4C8DD97BD7D209DEF"
            });
        }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.UserId);

            builder.Property(x => x.UserId)
                   .UseIdentityColumn();
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