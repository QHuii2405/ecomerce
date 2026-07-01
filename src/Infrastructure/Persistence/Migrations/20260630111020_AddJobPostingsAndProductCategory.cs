using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddJobPostingsAndProductCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobPostings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmploymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalaryRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Benefits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PostedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPostings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "JobPostings",
                columns: new[] { "Id", "Benefits", "CreatedAt", "Department", "Description", "EmploymentType", "IsActive", "IsDeleted", "Location", "PostedAt", "Requirements", "SalaryRange", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("b1000000-0000-0000-0000-000000000001"), "• Lương cứng + hoa hồng hấp dẫn\n• Bảo hiểm đầy đủ theo luật\n• Giảm giá 30% sản phẩm nội bộ\n• Đào tạo sản phẩm hàng tháng", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Kinh Doanh", "Tư vấn và bán các sản phẩm công nghệ cao cấp tại showroom iLuminaty Shop. Hỗ trợ khách hàng chọn sản phẩm phù hợp, xử lý đơn hàng và chăm sóc sau bán.", "Toàn thời gian", true, false, "TP. Hồ Chí Minh", new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), "• Tốt nghiệp CĐ/ĐH các ngành liên quan\n• Yêu thích công nghệ, am hiểu sản phẩm điện tử\n• Kỹ năng giao tiếp và thuyết trình tốt\n• Ưu tiên có kinh nghiệm bán lẻ công nghệ", "12 - 18 triệu VNĐ + Hoa hồng", "Nhân Viên Bán Hàng Công Nghệ", null },
                    { new Guid("b1000000-0000-0000-0000-000000000002"), "• Môi trường startup năng động\n• Làm việc linh hoạt (hybrid/remote)\n• MacBook Pro + màn hình 4K\n• Thưởng dự án theo quý", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Công Nghệ", "Phát triển và bảo trì nền tảng thương mại điện tử iLuminaty Shop. Xây dựng API .NET, giao diện React, tích hợp thanh toán và quản lý kho.", "Toàn thời gian", true, false, "TP. Hồ Chí Minh / Remote", new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Utc), "• 2+ năm kinh nghiệm .NET Core / ASP.NET\n• Thành thạo React, TypeScript/JavaScript\n• Hiểu biết SQL Server, Redis, REST API\n• Có kinh nghiệm Clean Architecture là lợi thế", "25 - 45 triệu VNĐ", "Lập Trình Viên Full-Stack (.NET + React)", null },
                    { new Guid("b1000000-0000-0000-0000-000000000003"), "• Ngân sách marketing thử nghiệm\n• Tham gia sự kiện công nghệ lớn\n• Team trẻ, sáng tạo\n• Review lương 2 lần/năm", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Marketing", "Lên kế hoạch và triển khai chiến dịch marketing online cho iLuminaty Shop. Quản lý social media, chạy quảng cáo Facebook/Google, phân tích hiệu quả.", "Toàn thời gian", true, false, "TP. Hồ Chí Minh", new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), "• 1+ năm kinh nghiệm marketing digital\n• Thành thạo Facebook Ads, Google Ads\n• Kỹ năng viết content và thiết kế cơ bản\n• Hiểu biết về ngành công nghệ/điện tử", "15 - 22 triệu VNĐ", "Chuyên Viên Marketing Digital", null },
                    { new Guid("b1000000-0000-0000-0000-000000000004"), "• Cơ hội chính thức hóa sau thực tập\n• Được đào tạo hệ thống quản lý kho\n• Môi trường chuyên nghiệp\n• Phụ cấp ăn trưa", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Vận Hành", "Hỗ trợ quản lý kho hàng, nhập xuất sản phẩm, đóng gói và theo dõi vận chuyển. Cơ hội học hỏi quy trình logistics thương mại điện tử.", "Thực tập", true, false, "TP. Hồ Chí Minh", new DateTime(2026, 6, 28, 0, 0, 0, 0, DateTimeKind.Utc), "• Sinh viên năm 3-4 các ngành QTKD, Logistics\n• Cẩn thận, trách nhiệm cao\n• Biết sử dụng Excel cơ bản\n• Có thể làm việc ít nhất 4 tháng", "5 - 7 triệu VNĐ + Phụ cấp", "Thực Tập Sinh Kho & Logistics", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobPostings");
        }
    }
}
