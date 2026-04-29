# 🛒 Ecommerce Order & Inventory System (Clean Architecture)

Hệ thống Backend quản lý đơn hàng và kho hàng được xây dựng bằng **.NET 10**, tập trung vào xử lý giao dịch (Transaction) và tối ưu hiệu năng.

## ✨ Tính năng chính
- **Clean Architecture**: Phân tách rõ ràng các tầng Domain, Application, Infrastructure và WebAPI.
- **Inventory Management**: Cơ chế **Reserve Stock** (giữ chỗ kho) khi đặt hàng để tránh lỗi overselling.
- **Security**: Xác thực JWT (Access & Refresh Token), mã hóa mật khẩu BCrypt.
- **Performance**: Tích hợp **Redis Cache** giúp tăng tốc độ truy vấn sản phẩm gấp 10 lần.
- **Automation**: **Background Service** tự động quét và hủy đơn hàng quá hạn thanh toán sau 2 giờ.

## 🛠 Công nghệ sử dụng
- **Backend**: .NET 10, Entity Framework Core.
- **Database**: SQL Server (Docker).
- **Caching**: Redis (Docker).
- **Security**: JWT Bearer, Authentication/Authorization.
- **Tools**: Swagger UI, EF Migrations.

## 🚀 Hướng dẫn chạy dự án
1. Khởi động Docker: `docker-compose up -d`
2. Cập nhật Database: `dotnet ef database update --project src/Infrastructure --startup-project src/WebAPI`
3. Chạy ứng dụng: `dotnet run --project src/WebAPI`
4. Truy cập Swagger: `http://localhost:5092/swagger`