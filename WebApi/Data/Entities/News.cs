using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities
{
    public class New
    {
        public int NewId { set; get; }
        public string? Title { set; get; }
        public string? Image { set; get; }
        public string? NewUrl { set; get; }
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<New>().HasData(new New
            {
                NewId = 1,
                Title = "HUTECH thông báo nhận hồ sơ xét tuyển hệ Đại học từ xa năm 2022 đến ngày 13/12",
                Image = "https://file1.hutech.edu.vn/file/editor/homepage1/686606-hutech-tuyen-sinh-dao-tao-tu-xa.jpg",
                NewUrl = "https://www.hutech.edu.vn/tuyensinh/tin-tuc/tin-tuyen-sinh/14607496-hutech-thong-bao-nhan-ho-so-xet-tuyen-he-dai-hoc-tu-xa-nam-2022-den-ngay-2012",
            });
            modelBuilder.Entity<New>().HasData(new New
            {
                NewId = 2,
                Title = "HUTECH thông báo nhận hồ sơ xét tuyển hệ Đại học từ xa năm 2022 đến ngày 30/11",
                Image = "https://file1.hutech.edu.vn/file/editor/homepage1/94304-hutech-tuyen-sinh-dai-hoc-tu-xa.jpg",
                NewUrl = "https://www.hutech.edu.vn/tuyensinh/tin-tuc/tin-tuyen-sinh/14606971-hutech-thong-bao-nhan-ho-so-xet-tuyen-he-dai-hoc-tu-xa-nam-2022-den-ngay-3011",
            });
            modelBuilder.Entity<New>().HasData(new New
            {
                NewId = 3,
                Title = "Sinh viên HUTECH tìm hiểu về lĩnh vực FINTECH cùng nhà sáng lập BIN Corporation Group",
                Image = "https://file1.hutech.edu.vn/file/editor/tuyensinh/903160-sinh-vien-hutech-tim-hieu-ve-linh-vuc-fintech-cung-nha-sang-lap-bin-corporation-group16.jpg",
                NewUrl = "https://www.hutech.edu.vn/homepage/tin-tuc/tin-hutech/14607649-sinh-vien-hutech-tim-hieu-ve-linh-vuc-fintech-cung-nha-sang-lap-bin-corporation-group",
            });
        }
    }

    public class NewConfiguration : IEntityTypeConfiguration<New>
    {
        public void Configure(EntityTypeBuilder<New> builder)
        {
            builder.ToTable("News");

            builder.HasKey(x => x.NewId);

            builder.Property(x => x.NewId)
                   .UseIdentityColumn();
            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.Property(x => x.Image)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.Property(x => x.NewUrl)
                   .IsRequired()
                   .HasMaxLength(255);
        }
    }
}

