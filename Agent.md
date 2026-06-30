# 🤖 Hướng dẫn AI Agent & Bản đồ Dự án (Agent.md)

Tài liệu này đóng vai trò là "README dành cho AI Agent" (chẳng hạn như Claude Code, Gemini CLI, Cursor, Copilot). Nó cung cấp ngữ cảnh toàn diện về dự án, quy tắc kiến trúc, tiêu chuẩn mã nguồn và hướng dẫn sử dụng các kỹ năng chuyên biệt (Skills) để phát triển hệ thống.

---

## 📌 1. Tổng quan dự án

Dự án **Ecommerce Order & Inventory System** là một hệ thống quản lý đơn hàng và kho hàng thương mại điện tử, bao gồm:
1. **Backend API**: Xây dựng trên **.NET 10** theo mô hình **Clean Architecture** kết hợp các mẫu thiết kế **Service Layer**, **Unit of Work**, và **Repository**. Tập trung vào xử lý giao dịch đồng thời cao (Concurrency), giữ chỗ kho hàng (Inventory Reservation) để tránh overselling, và tối ưu hóa hiệu năng bằng Redis.
2. **Frontend Web**: Xây dựng trên **React 19**, **Vite**, và **Tailwind CSS v4**. Tương tác với Backend API thông qua Axios.

---

## 🛠 2. Cấu trúc thư mục & Kiến trúc ứng dụng

### 📂 Sơ đồ thư mục chính
```text
ecomerce/
├── src/                         # Mã nguồn Backend (.NET 10)
│   ├── Domain/                  # Tầng nghiệp vụ lõi (Entities, Value Objects)
│   │   ├── Common/              # Thực thể cơ sở (BaseEntity)
│   │   ├── Entities/            # Thực thể nghiệp vụ (Product, Inventory, Order, OrderItem, User)
│   │   └── Interfaces/          # Giao diện lưu trữ lõi (IGenericRepository, IProductRepository, IInventoryRepository, IOrderRepository, IUnitOfWork)
│   ├── Application/             # Tầng ứng dụng (DTOs, Mappers)
│   │   ├── DTOs/                # Các đối tượng yêu cầu đầu vào/đầu ra (Requests, Responses)
│   │   ├── Interfaces/          # Giao diện dịch vụ ứng dụng (IProductService, IOrderService)
│   │   └── Services/            # Thực thi dịch vụ chứa logic nghiệp vụ (ProductService, OrderService)
│   ├── Infrastructure/          # Tầng hạ tầng (DbContext, Migrations, Cache)
│   │   ├── Persistence/         # Cấu hình EF Core DbContext và Migrations
│   │   │   └── Repositories/    # Thực thi cụ thể cho Repositories và UnitOfWork (GenericRepository, ProductRepository, InventoryRepository, OrderRepository, UnitOfWork)
│   │   └── BackgroundServices/  # Dịch vụ chạy ngầm của hệ thống (OrderExpirationService)
│   └── WebAPI/                  # Tầng giao tiếp (Controllers, Program.cs, Configurations)
├── ecommerce-web/               # Mã nguồn Frontend (React 19 + Vite + Tailwind v4)
│   ├── src/
│   │   ├── api/                 # Cấu hình Axios và tích hợp API
│   │   ├── components/          # Component dùng chung (UI, Layouts)
│   │   ├── pages/               # Các trang giao diện (Trang chủ, Sản phẩm, Đơn hàng, Giỏ hàng)
│   │   └── main.jsx
│   └── package.json
├── .agent/                      # Thư mục chứa cấu hình và kỹ năng của AI Agent
│   └── skills/                  # Các kỹ năng chuyên biệt (Agent Skills)
└── docker-compose.yml           # Khởi chạy SQL Server và Redis
```

### 📐 Quy tắc Clean Architecture (Backend)
- **Domain**: Không phụ thuộc vào bất kỳ thư viện hay tầng nào khác. Chứa các thực thể chính, các quy tắc nghiệp vụ lõi và định nghĩa toàn bộ **Repository Interfaces** cùng **IUnitOfWork**.
- **Application**: Phụ thuộc vào Domain. Chứa các DTOs, các giao diện dịch vụ (**Service Interfaces**) và triển khai thực tế dịch vụ (**Service Implementations**) chứa logic nghiệp vụ của ứng dụng (Use Cases). Tầng này sử dụng các Repository Interfaces thông qua Dependency Injection.
- **Infrastructure**: Phụ thuộc vào Application và Domain. Thực hiện cấu hình Entity Framework Core, các dịch vụ Redis Cache, dịch vụ chạy ngầm và thực thi cụ thể cho các **Repositories** và **UnitOfWork**.
- **WebAPI**: Phụ thuộc vào Application. Cung cấp các RESTful API endpoints, cấu hình Middleware, JWT Authentication, và Swagger/Scalar. Controllers trong tầng này **chỉ inject các Service Interfaces** tương ứng để điều phối request/response, hoàn toàn không được chứa logic nghiệp vụ hoặc truy cập trực tiếp cơ sở dữ liệu.

---

## ⚙️ 3. Công nghệ & Tham số môi trường

### 🐳 Hạ tầng Docker
Dự án sử dụng các dịch vụ Docker được cấu hình trong [docker-compose.yml](file:///d:/Project/ecomerce/docker-compose.yml):
- **SQL Server**: Chạy trên cổng mặc định `1433`.
- **Redis**: Chạy trên cổng `6379`.

### 🔑 Xác thực & Bảo mật
- **JWT (JSON Web Token)**: Hỗ trợ phân quyền người dùng theo vai trò (`Admin`, `Staff`, `Customer`). Các API quản trị yêu cầu quyền truy cập cụ thể (`[Authorize(Roles = "Staff,Admin")]`).

---

## 📐 4. Các mẫu thiết kế quan trọng (Design Patterns)

Khi phát triển dự án này, AI Agent phải tuân thủ nghiêm ngặt các mẫu thiết kế đã được thiết lập sẵn dưới đây:

### A. Mẫu thiết kế Repository và Unit of Work
- Mọi hoạt động truy vấn dữ liệu từ cơ sở dữ liệu phải được bọc thông qua các **Repositories**. Không viết LINQ trực tiếp lên `ApplicationDbContext` ở ngoài tầng Infrastructure.
- Sử dụng `IUnitOfWork` để quản lý giao dịch và điều phối hoạt động lưu trữ của nhiều repositories cùng lúc.
```csharp
await _unitOfWork.BeginTransactionAsync();
try
{
    await _unitOfWork.Products.AddAsync(product);
    await _unitOfWork.SaveChangesAsync();
    
    await _unitOfWork.Inventories.AddAsync(inventory);
    await _unitOfWork.SaveChangesAsync();
    
    await _unitOfWork.CommitTransactionAsync();
}
catch
{
    await _unitOfWork.RollbackTransactionAsync();
    throw;
}
```

### B. Quản lý giao dịch và Giữ chỗ kho (Inventory Reservation)
Để ngăn ngừa tình trạng quá bán (overselling) trong môi trường phân tán, hệ thống sử dụng cơ chế cập nhật nguyên tử (atomic update) trực tiếp trong cơ sở dữ liệu, được đóng gói bên trong `IInventoryRepository`:
```csharp
// Trong InventoryRepository.cs
public async Task<bool> ReserveStockAsync(Guid productId, int quantity)
{
    var rowsAffected = await _context.Database.ExecuteSqlRawAsync(
        @"UPDATE Inventories 
          SET ReservedQuantity = ReservedQuantity + {1}
          WHERE ProductId = {0} 
          AND (StockQuantity - ReservedQuantity) >= {1}",
        productId, quantity);

    return rowsAffected > 0;
}
```
- **Quy tắc**: Không cập nhật số lượng giữ chỗ bằng cách đọc dữ liệu lên bộ nhớ rồi tính toán. Phải gọi `ReserveStockAsync` thông qua `IUnitOfWork.Inventories`.

### C. Cơ chế lưu trữ đệm (Cache-Aside Pattern với Redis)
- Các truy vấn đọc dữ liệu tần suất cao (ví dụ: danh sách sản phẩm công khai) được lưu đệm trong Redis với thời gian sống (TTL) là 5 phút, được quản lý ở tầng Service:
- Khi có các hoạt động cập nhật hoặc tạo mới, cache liên quan phải được hủy bỏ ngay lập tức (Cache Invalidation) để tránh dữ liệu không đồng nhất:
```csharp
await _cache.RemoveAsync("products_all");
```

### D. Dịch vụ chạy ngầm (Background Service)
- Sử dụng `BackgroundService` của .NET để thực hiện các tác vụ định kỳ (ví dụ: hủy đơn hàng quá hạn thanh toán sau 2 giờ).
- Vì `DbContext` có vòng đời Scoped, dịch vụ chạy ngầm phải khởi tạo `IServiceScope` theo cách thủ công để truy xuất `ApplicationDbContext`:
```csharp
using (var scope = _serviceProvider.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Thực hiện logic...
}
```

---

## 🛠 5. Quy trình phát triển dành cho AI Agent

Khi nhận được yêu cầu phát triển tính năng hoặc sửa lỗi, Agent thực hiện theo 4 bước sau:

1. **Nghiên cứu & Thiết lập Kế hoạch (Planning)**:
   - Đọc các tệp tin liên quan trực tiếp đến yêu cầu.
   - Viết hoặc cập nhật kế hoạch triển khai trong tài liệu `implementation_plan.md` ở thư mục Artifacts của phiên làm việc.
   - Nhận phản hồi và sự chấp thuận từ lập trình viên trước khi sửa đổi mã nguồn.

2. **Cập nhật Backend (Tầng lõi trước)**:
   - Nếu thay đổi mô hình dữ liệu hoặc thêm nghiệp vụ mới: 
     1. Cập nhật Entities ở tầng `Domain/Entities`.
     2. Định nghĩa giao diện repository đặc thù trong `Domain/Interfaces` (nếu cần).
     3. Khai báo thuộc tính trong `IUnitOfWork` và định nghĩa DTOs tại `Application/DTOs`.
     4. Thực thi repository và UnitOfWork cụ thể ở `Infrastructure/Persistence/Repositories`.
     5. Cập nhật cơ sở dữ liệu nếu có thay đổi cấu trúc (`dotnet ef database update`).
     6. Định nghĩa Service Interface trong `Application/Interfaces` và triển khai trong `Application/Services`.
     7. Viết WebAPI Controllers để tiêm Service Interface và định tuyến.

3. **Cập nhật Frontend (React 19)**:
   - Định nghĩa các hàm gọi API tương ứng trong thư mục `ecommerce-web/src/api`.
   - Tạo hoặc cập nhật các component giao diện sử dụng Tailwind v4.
   - Đảm bảo thiết kế đáp ứng (responsive), giao diện trực quan và trải nghiệm mượt mà.

4. **Kiểm tra & Xác nhận (Verification)**:
   - Khởi động các dịch vụ backend và frontend để chạy thử.
   - Sử dụng công cụ Swagger (`http://localhost:5092/swagger`) để kiểm tra tính đúng đắn của các API endpoints mới.

---

## 🧩 6. Thư viện kỹ năng (Agent Skills)

Để giúp Agent giải quyết các nhiệm vụ chuyên sâu một cách hiệu quả và tiết kiệm mã lực (tokens), dự án cung cấp các kỹ năng chuyên biệt nằm trong thư mục `.agent/skills/`. Agent có thể đọc các kỹ năng này bằng công cụ xem tệp với cờ `IsSkillFile: true`.

| Tên kỹ năng | Thư mục | Mô tả nhiệm vụ |
| :--- | :--- | :--- |
| **Clean Architecture** | [.agent/skills/dotnet-clean-architecture](file:///d:/Project/ecomerce/.agent/skills/dotnet-clean-architecture/SKILL.md) | Quy chuẩn thêm Entity, DTO, Repository, Service và Controller mới theo đúng kiến trúc Clean Architecture. |
| **React & Tailwind v4** | [.agent/skills/react-tailwind-vite](file:///d:/Project/ecomerce/.agent/skills/react-tailwind-vite/SKILL.md) | Kỹ thuật phát triển giao diện hiện đại với React 19, Vite, và hệ thống tiện ích của Tailwind CSS v4. |
| **Database Migrations** | [.agent/skills/ef-core-migrations](file:///d:/Project/ecomerce/.agent/skills/ef-core-migrations/SKILL.md) | Hướng dẫn tạo, áp dụng và xử lý xung đột Entity Framework Core migrations an toàn. |
| **Redis & Concurrency** | [.agent/skills/redis-concurrency](file:///d:/Project/ecomerce/.agent/skills/redis-concurrency/SKILL.md) | Các mẫu mã nguồn tối ưu cho cache-aside, vô hiệu hóa cache, và xử lý tranh chấp kho hàng đồng thời cao. |

---

## 📥 7. Cách tìm kiếm và cài đặt thêm Kỹ năng (Skills) từ Cộng đồng

Hệ thống Kỹ năng của Agent (Agent Skills) được chuẩn hóa theo định dạng mở giúp dễ dàng mở rộng. Lập trình viên có thể tích hợp thêm các kỹ năng từ cộng đồng vào dự án thông qua các bước sau:

### Bước 1: Tìm kiếm kỹ năng phù hợp
Các nguồn kỹ năng phổ biến trên GitHub:
- [VoltAgent/awesome-agent-skills](https://github.com/VoltAgent/awesome-agent-skills): Thư viện tổng hợp hơn 1.000 kỹ năng đa dạng từ Google, Stripe, Vercel và cộng đồng.
- [microsoft/skills](https://github.com/microsoft/skills): Các kỹ năng chuyên sâu về Azure, .NET và bảo mật hệ thống.
- [managedcode/dotnet-skills](https://github.com/managedcode/dotnet-skills): Thư viện bổ trợ các mẫu phát triển .NET tiên tiến.

### Bước 2: Clone hoặc tạo tệp Kỹ năng trong dự án
Để cài đặt một kỹ năng mới từ GitHub vào dự án:
1. Tạo một thư mục con dưới `.agent/skills/<tên-kỹ-năng>`.
2. Tạo tệp `SKILL.md` bên trong thư mục đó để mô tả chi tiết hướng dẫn cho AI Agent.
3. Nếu kỹ năng đi kèm các kịch bản tự động hóa hoặc tệp mẫu (templates), hãy lưu chúng vào `.agent/skills/<tên-kỹ-năng>/scripts/` hoặc `.agent/skills/<tên-kỹ-năng>/templates/`.

*Ví dụ cấu trúc một kỹ năng chuẩn:*
```text
.agent/skills/my-new-skill/
├── SKILL.md                 # Tài liệu hướng dẫn chính (chứa frontmatter mô tả kỹ năng)
├── scripts/                 # (Tùy chọn) Các đoạn mã hỗ trợ (Python, PowerShell, JS)
└── templates/               # (Tùy chọn) Các bản mẫu mã nguồn để AI sao chép nhanh
```

Bằng cách tuân thủ tài liệu hướng dẫn này và sử dụng các Kỹ năng đi kèm, AI Agent sẽ tối ưu hóa được năng lực xử lý, đưa ra những phản hồi chính xác và duy trì chất lượng mã nguồn cao nhất cho dự án.
