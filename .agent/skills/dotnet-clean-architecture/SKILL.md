---
name: dotnet-clean-architecture
description: Hướng dẫn phát triển các tính năng backend theo mô hình Clean Architecture sử dụng .NET 10 kết hợp Repository, Unit of Work, Service và các tiêu chuẩn vận hành sản phẩm (Production Standards).
tags: [dotnet, csharp, clean-architecture, backend, production-ready, SOLID]
---

# 📐 Kỹ năng: Phát triển .NET 10 Clean Architecture Đạt Chuẩn Production

Kỹ năng này thiết lập các nguyên tắc phát triển mã nguồn Backend có tính an toàn cao, dễ mở rộng và vận hành ổn định trong môi trường **Production**. AI Agent bắt buộc phải tuân thủ nghiêm ngặt các quy chuẩn này để tránh tạo ra mã nguồn dạng "chạy thử nghiệm" (experiment/prototype).

---

## 🛡️ 1. Các tiêu chuẩn vận hành sản phẩm (Production-Ready Standards)

### A. Quản lý cấu hình & Bảo mật thông tin
- **Không bao giờ viết cứng (Hardcode)**: Các thông tin nhạy cảm như chuỗi kết nối Database, khóa bí mật JWT, API key của bên thứ ba tuyệt đối không được viết cứng trong code.
- **Sử dụng Configuration**: Phải đọc thông qua `IConfiguration` hoặc áp dụng **Options Pattern** (`IOptions<JwtSettings>`) để ánh xạ cấu hình từ `appsettings.json` hoặc biến môi trường (Environment Variables).
  ```csharp
  var jwtSettings = _configuration.GetSection("JwtSettings");
  var secret = jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret chưa được cấu hình.");
  ```

### B. Ghi nhật ký cấu trúc (Structured Logging)
- Sử dụng `ILogger<T>` để ghi lại các sự kiện vận hành.
- **Quy tắc quan trọng**: Không sử dụng phép nội suy chuỗi (string interpolation `$""`) trong chuỗi định dạng log. Phải sử dụng **Structured Logging** (Message Templates) để các hệ thống gom log tập trung (như ELK Stack, Seq, Grafana Loki) có thể lập chỉ mục (index) và tìm kiếm các tham số hiệu quả.
  ```csharp
  // ❌ SAI: Không hỗ trợ phân tích tham số
  _logger.LogInformation($"Đã đặt hàng thành công đơn {order.Id} cho khách {userId}");

  // ✔️ ĐÚNG: Hỗ trợ tìm kiếm theo thuộc tính OrderId và UserId trong Kibana/Seq
  _logger.LogInformation("Đặt hàng thành công. OrderId: {OrderId}, UserId: {UserId}", order.Id, userId);
  ```

### C. Xử lý ngoại lệ tập trung (Global Exception Handling)
- Không viết quá nhiều khối `try-catch` lặp đi lặp lại trong các Controllers để bắt lỗi hệ thống.
- **Giải pháp Production**: Sử dụng cơ chế `IExceptionHandler` (được giới thiệu từ .NET 8) hoặc viết một **Custom Middleware** để bắt toàn bộ các lỗi không mong muốn ở mức toàn cục, ghi lại log chi tiết kèm StackTrace, và trả về định dạng phản hồi chuẩn **RFC 7807 Problem Details** cho Client:
  ```json
  {
    "type": "https://tools.ietf.org/html/rfc7807",
    "title": "An unexpected error occurred",
    "status": 500,
    "detail": "Chi tiết lỗi đã được ghi nhận trong hệ thống.",
    "instance": "/api/orders"
  }
  ```

### D. Kiểm tra dữ liệu đầu vào tự động (Automatic Validation)
- Giữ cho các lớp Service và Controller hoàn toàn sạch bóng các câu lệnh kiểm tra dữ liệu tẻ nhạt (`if (string.IsNullOrEmpty(name)) ...`).
- **Giải pháp Production**: Tích hợp thư viện **FluentValidation**. Định nghĩa các lớp validator riêng biệt và cấu hình một Pipeline Behavior trong MediatR hoặc một Action Filter trong ASP.NET Core để tự động validate mọi Request DTO trước khi nó chạm tới Controller. Nếu lỗi, tự động trả về mã `400 Bad Request` kèm chi tiết các ô nhập liệu bị sai.

### E. Tối ưu hóa truy vấn EF Core (Performance Tuning)
- **Read-Only Queries**: Đối với các truy vấn chỉ đọc dữ liệu hiển thị (không cập nhật), bắt buộc phải sử dụng phương thức `.AsNoTracking()` để EF Core không tốn tài nguyên theo dõi trạng thái thực thể (Change Tracking), giúp tăng tốc độ truy vấn và tiết kiệm RAM:
  ```csharp
  var products = await _context.Products
      .AsNoTracking()
      .Where(p => !p.IsDeleted)
      .ToListAsync();
  ```
- **Chỉ mục (Indexes)**: Mọi thuộc tính khóa ngoại (Foreign Keys) hoặc các thuộc tính thường xuyên nằm trong điều kiện tìm kiếm (`Where`) hoặc sắp xếp (`OrderBy`) bắt buộc phải được đánh chỉ mục trong cơ sở dữ liệu để tối ưu hiệu năng đọc.

---

## 📐 2. Luồng phát triển tính năng chuẩn Clean Architecture & SOLID

Khi thêm một thực thể nghiệp vụ mới (ví dụ: `ProductReview`), hãy thực hiện theo đúng quy trình 5 bước sau để đảm bảo tính lỏng lẻo (Loose Coupling) và dễ viết unit test:

```text
[Domain Entities] ──► [Domain Interfaces] ──► [Infrastructure Repositories] ──► [Application Services] ──► [WebAPI Controllers]
```

1. **Tầng Domain (Lõi nghiệp vụ)**:
   - Tạo thực thể tại `src/Domain/Entities/` (kế thừa `BaseEntity`).
   - Định nghĩa các interface repository đặc thù tại `src/Domain/Interfaces/` (ví dụ: `IProductReviewRepository : IGenericRepository<ProductReview>`).
2. **Tầng Infrastructure (Hạ tầng lưu trữ)**:
   - Triển khai cụ thể repository tại `src/Infrastructure/Persistence/Repositories/` (`ProductReviewRepository`).
   - Khai báo và khởi tạo repository mới trong `IUnitOfWork` để quản lý tập trung.
   - Tạo và áp dụng Migration để đồng bộ cấu trúc SQL Server.
3. **Tầng Application (Logic ứng dụng)**:
   - Định nghĩa Request/Response DTOs tại `src/Application/DTOs/`.
   - Định nghĩa giao diện dịch vụ tại `src/Application/Interfaces/` (`IProductReviewService`).
   - Triển khai dịch vụ tại `src/Application/Services/` (`ProductReviewService`), inject `IUnitOfWork` để thực thi logic và phối hợp transaction.
4. **Tầng WebAPI (Giao tiếp HTTP)**:
   - Tạo API Controller tại `src/WebAPI/Controllers/`, chỉ inject `IProductReviewService`.
   - Đăng ký phụ thuộc (DI) trong `Program.cs` cho các dịch vụ mới.
