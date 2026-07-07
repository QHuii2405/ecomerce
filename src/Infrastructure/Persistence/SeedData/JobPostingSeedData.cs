using Domain.Entities;

namespace Infrastructure.Persistence.SeedData;

public static class JobPostingSeedData
{
    public static IEnumerable<JobPosting> GetJobPostings()
    {
        var seedDate = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc);
        return new List<JobPosting>
        {
            new JobPosting
            {
                Id = Guid.Parse("b1000000-0000-0000-0000-000000000001"),
                Title = "Nhân Viên Bán Hàng Công Nghệ",
                Department = "Kinh Doanh",
                Location = "TP. Hồ Chí Minh",
                EmploymentType = "Toàn thời gian",
                SalaryRange = "12 - 18 triệu VNĐ + Hoa hồng",
                Description = "Tư vấn và bán các sản phẩm công nghệ cao cấp tại showroom iLuminaty Shop. Hỗ trợ khách hàng chọn sản phẩm phù hợp, xử lý đơn hàng và chăm sóc sau bán.",
                Requirements = "• Tốt nghiệp CĐ/ĐH các ngành liên quan\n• Yêu thích công nghệ, am hiểu sản phẩm điện tử\n• Kỹ năng giao tiếp và thuyết trình tốt\n• Ưu tiên có kinh nghiệm bán lẻ công nghệ",
                Benefits = "• Lương cứng + hoa hồng hấp dẫn\n• Bảo hiểm đầy đủ theo luật\n• Giảm giá 30% sản phẩm nội bộ\n• Đào tạo sản phẩm hàng tháng",
                IsActive = true,
                PostedAt = new DateTime(2026, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = seedDate,
                IsDeleted = false
            },
            new JobPosting
            {
                Id = Guid.Parse("b1000000-0000-0000-0000-000000000002"),
                Title = "Lập Trình Viên Full-Stack (.NET + React)",
                Department = "Công Nghệ",
                Location = "TP. Hồ Chí Minh / Remote",
                EmploymentType = "Toàn thời gian",
                SalaryRange = "25 - 45 triệu VNĐ",
                Description = "Phát triển và bảo trì nền tảng thương mại điện tử iLuminaty Shop. Xây dựng API .NET, giao diện React, tích hợp thanh toán và quản lý kho.",
                Requirements = "• 2+ năm kinh nghiệm .NET Core / ASP.NET\n• Thành thạo React, TypeScript/JavaScript\n• Hiểu biết SQL Server, Redis, REST API\n• Có kinh nghiệm Clean Architecture là lợi thế",
                Benefits = "• Môi trường startup năng động\n• Làm việc linh hoạt (hybrid/remote)\n• MacBook Pro + màn hình 4K\n• Thưởng dự án theo quý",
                IsActive = true,
                PostedAt = new DateTime(2026, 6, 20, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = seedDate,
                IsDeleted = false
            },
            new JobPosting
            {
                Id = Guid.Parse("b1000000-0000-0000-0000-000000000003"),
                Title = "Chuyên Viên Marketing Digital",
                Department = "Marketing",
                Location = "TP. Hồ Chí Minh",
                EmploymentType = "Toàn thời gian",
                SalaryRange = "15 - 22 triệu VNĐ",
                Description = "Lên kế hoạch và triển khai chiến dịch marketing online cho iLuminaty Shop. Quản lý social media, chạy quảng cáo Facebook/Google, phân tích hiệu quả.",
                Requirements = "• 1+ năm kinh nghiệm marketing digital\n• Thành thạo Facebook Ads, Google Ads\n• Kỹ năng viết content và thiết kế cơ bản\n• Hiểu biết về ngành công nghệ/điện tử",
                Benefits = "• Ngân sách marketing thử nghiệm\n• Tham gia sự kiện công nghệ lớn\n• Team trẻ, sáng tạo\n• Review lương 2 lần/năm",
                IsActive = true,
                PostedAt = new DateTime(2026, 6, 25, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = seedDate,
                IsDeleted = false
            },
            new JobPosting
            {
                Id = Guid.Parse("b1000000-0000-0000-0000-000000000004"),
                Title = "Thực Tập Sinh Kho & Logistics",
                Department = "Vận Hành",
                Location = "TP. Hồ Chí Minh",
                EmploymentType = "Thực tập",
                SalaryRange = "5 - 7 triệu VNĐ + Phụ cấp",
                Description = "Hỗ trợ quản lý kho hàng, nhập xuất sản phẩm, đóng gói và theo dõi vận chuyển. Cơ hội học hỏi quy trình logistics thương mại điện tử.",
                Requirements = "• Sinh viên năm 3-4 các ngành QTKD, Logistics\n• Cẩn thận, trách nhiệm cao\n• Biết sử dụng Excel cơ bản\n• Có thể làm việc ít nhất 4 tháng",
                Benefits = "• Cơ hội chính thức hóa sau thực tập\n• Được đào tạo hệ thống quản lý kho\n• Môi trường chuyên nghiệp\n• Phụ cấp ăn trưa",
                IsActive = true,
                PostedAt = new DateTime(2026, 6, 28, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = seedDate,
                IsDeleted = false
            }
        };
    }
}
