using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities
{
    public class Contact
    {
        public int ContactId { set; get; }
        public string? Name { set; get; }
        public string? Email { set; get; }
        public string? PhoneNumber { set; get; }
        public string? Message { set; get; }
    }

    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts");

            builder.HasKey(x => x.ContactId);

            builder.Property(x => x.ContactId)
                   .UseIdentityColumn();
            builder.Property(x => x.Name)
                   .HasMaxLength(200);
            builder.Property(x => x.Email)
                   .HasMaxLength(200);
            builder.Property(x => x.PhoneNumber)
                   .HasMaxLength(200);
            builder.Property(x => x.Message)
                   .IsRequired();
        }
    }
}