---
name: ef-core-migrations
description: Hướng dẫn tạo, áp dụng và kiểm soát các thay đổi database thông qua Entity Framework Core, đảm bảo an toàn dữ liệu trong môi trường Production.
tags: [dotnet, ef-core, migration, sql-server, database, production-ready]
---

# 🗄️ Kỹ năng: Quản lý Database Migrations với EF Core chuyên nghiệp

Kỹ năng này hướng dẫn AI Agent thực hiện thay đổi cấu trúc cơ sở dữ liệu (schema changes) và quản lý dữ liệu mẫu (data seeding) một cách an toàn, tránh làm gián đoạn hệ thống hoặc mất dữ liệu trong môi trường **Production**.

---

## 🛡️ 1. Quy tắc Vàng bảo vệ dữ liệu khi Migration trên Production

Khi áp dụng thay đổi cấu trúc dữ liệu lên môi trường chạy thực tế (Production), sơ suất nhỏ có thể gây mất mát dữ liệu hoặc làm treo hệ thống. Agent phải tuân thủ nghiêm ngặt các quy tắc sau:

### A. Tuyệt đối không xóa cột (Drop Column) trực tiếp
- **Vấn đề**: Nếu một tính năng mới yêu cầu thay thế cột cũ bằng cột mới, việc chạy lệnh xóa cột cũ ngay lập tức sẽ làm mất toàn bộ dữ liệu lịch sử và gây crash các phiên bản ứng dụng cũ đang chạy song song trong lúc deploy (rolling deployment).
- **Quy trình An toàn 3 bước (Expand and Contract)**:
  1. **Bước 1 (Expand)**: Thêm cột mới (chấp nhận giá trị Null hoặc có giá trị mặc định). Deploy code mới có khả năng ghi dữ liệu vào cả 2 cột.
  2. **Bước 2 (Migrate)**: Chạy một script chuyển đổi dữ liệu lịch sử từ cột cũ sang cột mới.
  3. **Bước 3 (Contract)**: Sau khi hệ thống chạy ổn định hoàn toàn trên cột mới, tiến hành tạo một bản migration mới để xóa cột cũ và loại bỏ thuộc tính đó khỏi code.

### B. Khởi tạo dữ liệu mẫu an toàn (Data Seeding)
- Sử dụng phương thức `HasData` trong `OnModelCreating` của DbContext để gieo dữ liệu danh mục tĩnh (ví dụ: các quyền Admin, Staff, các danh mục sản phẩm mặc định).
- **Lưu ý quan trọng**: Không dùng `HasData` để gieo các dữ liệu thường xuyên thay đổi hoặc dữ liệu động. 
- Đối với tài khoản quản trị mặc định, phải mã hóa mật khẩu bằng BCrypt trước khi gieo:
  ```csharp
  modelBuilder.Entity<User>().HasData(new User
  {
      Id = Guid.Parse("admin-guid-here"),
      Email = "admin@ecommerce.com",
      FullName = "System Administrator",
      PasswordHash = BCrypt.Net.BCrypt.HashPassword("SuperAdminSecurePassword2026"),
      Role = "Admin"
  });
  ```

### C. Đánh chỉ mục (Indexing) hiệu năng cao
- Tất cả các khóa ngoại (Foreign Keys) phải được đánh chỉ mục để tối ưu hóa hiệu năng của các truy vấn liên kết bảng (`Join`). EF Core tự động tạo chỉ mục cho khóa ngoại, nhưng nếu viết SQL Raw hoặc cấu hình quan hệ phức tạp, hãy xác nhận chỉ mục đã được tạo.
- Định nghĩa chỉ mục cụ thể cho các trường thường xuyên tìm kiếm (ví dụ: `Email` của User, `Name` của Product):
  ```csharp
  modelBuilder.Entity<User>()
      .HasIndex(u => u.Email)
      .IsUnique(); // Đảm bảo Email là duy nhất ở mức Database
  ```

---

## 💻 2. Các câu lệnh CLI tiêu chuẩn

Mọi câu lệnh EF Core CLI phải được thực thi từ thư mục gốc của dự án (`d:\Project\ecomerce`) và chỉ rõ dự án chứa DbContext (`src/Infrastructure`) và dự án khởi chạy (`src/WebAPI`).

### A. Tạo mới một Migration
Khi có thay đổi ở tầng Domain Entities:
```powershell
dotnet ef migrations add <TenMigration> --project src/Infrastructure --startup-project src/WebAPI
```
*Yêu cầu*: Tên `<TenMigration>` phải viết theo quy tắc CamelCase và phản ánh chính xác thay đổi (ví dụ: `AddUniqueIndexToUserEmail`, `CreateReviewsTable`).

### B. Áp dụng Migration vào Database
Cập nhật cấu trúc cơ sở dữ liệu thực tế:
```powershell
dotnet ef database update --project src/Infrastructure --startup-project src/WebAPI
```

### C. Sinh tập lệnh SQL để Deploy (Production Script)
Trong môi trường Production thực tế, **không chạy lệnh `dotnet ef database update` trực tiếp**. Thay vào đó, quy trình DevOps chuẩn yêu cầu sinh ra tệp script SQL để đội ngũ Database Administrator kiểm duyệt hoặc để công cụ CI/CD chạy:
```powershell
dotnet ef migrations script --project src/Infrastructure --startup-project src/WebAPI --output db_update_script.sql
```
Lệnh này sẽ sinh ra toàn bộ các câu lệnh SQL thay đổi cấu trúc tương ứng với các bản migration chưa áp dụng.
