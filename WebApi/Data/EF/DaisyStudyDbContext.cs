using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.EF
{
    public class DaisyStudyDbContext : DbContext
    {
        public DaisyStudyDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new CourseMemberConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());

        }

        public DbSet<User> Users { set; get; } = default!;
        public DbSet<Category> Categories { set; get; } = default!;
        public DbSet<Comment> Comments { set; get; } = default!;
        public DbSet<Contact> Contacts { set; get; } = default!;
        public DbSet<Course> Courses { set; get; } = default!;
        public DbSet<CourseMember> CourseMembers { set; get; } = default!;
        public DbSet<Lesson> Lessons { set; get; } = default!;
        public DbSet<Rating> Ratings { set; get; } = default!;

    }
}