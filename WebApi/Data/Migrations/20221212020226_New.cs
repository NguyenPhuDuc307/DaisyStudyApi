using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NewUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewId);
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "NewId", "Image", "NewUrl", "Title" },
                values: new object[] { 1, "https://file1.hutech.edu.vn/file/editor/homepage1/686606-hutech-tuyen-sinh-dao-tao-tu-xa.jpg", "https://www.hutech.edu.vn/tuyensinh/tin-tuc/tin-tuyen-sinh/14607496-hutech-thong-bao-nhan-ho-so-xet-tuyen-he-dai-hoc-tu-xa-nam-2022-den-ngay-2012", "HUTECH thông báo nhận hồ sơ xét tuyển hệ Đại học từ xa năm 2022 đến ngày 13/12" });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "NewId", "Image", "NewUrl", "Title" },
                values: new object[] { 2, "https://file1.hutech.edu.vn/file/editor/homepage1/94304-hutech-tuyen-sinh-dai-hoc-tu-xa.jpg", "https://www.hutech.edu.vn/tuyensinh/tin-tuc/tin-tuyen-sinh/14606971-hutech-thong-bao-nhan-ho-so-xet-tuyen-he-dai-hoc-tu-xa-nam-2022-den-ngay-3011", "HUTECH thông báo nhận hồ sơ xét tuyển hệ Đại học từ xa năm 2022 đến ngày 30/11" });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "NewId", "Image", "NewUrl", "Title" },
                values: new object[] { 3, "https://file1.hutech.edu.vn/file/editor/tuyensinh/903160-sinh-vien-hutech-tim-hieu-ve-linh-vuc-fintech-cung-nha-sang-lap-bin-corporation-group16.jpg", "https://www.hutech.edu.vn/homepage/tin-tuc/tin-hutech/14607649-sinh-vien-hutech-tim-hieu-ve-linh-vuc-fintech-cung-nha-sang-lap-bin-corporation-group", "Sinh viên HUTECH tìm hiểu về lĩnh vực FINTECH cùng nhà sáng lập BIN Corporation Group" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News");
        }
    }
}
