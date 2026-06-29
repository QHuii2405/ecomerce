---
name: git-commit
description: Hướng dẫn quy chuẩn Git Commit đẹp, rõ ràng và chuẩn hóa theo Conventional Commits kết hợp Gitmoji dành cho AI Agent khi hoàn thành nhiệm vụ.
tags: [git, commit, workflow, developer-productivity, standard, best-practice]
---

# 📜 Kỹ năng: Quy chuẩn Git Commit Đẹp & Rõ Ràng (Conventional Commits & Gitmoji)

Kỹ năng này hướng dẫn AI Agent thực hiện commit code một cách chuyên nghiệp, sạch sẽ và có tổ chức sau khi hoàn thành nhiệm vụ. Việc tuân thủ quy chuẩn giúp lịch sử Git trực quan, dễ tra cứu và hỗ trợ tự động hóa sinh changelog.

---

## 🚨 1. Các Quy Tắc Vàng (Golden Rules)

Trước khi thực hiện bất kỳ lệnh commit nào, AI Agent bắt buộc phải đảm bảo các điều kiện sau:

1. **Chỉ commit khi code chạy được**: Tuyệt đối không commit code lỗi cú pháp, chưa biên dịch được hoặc không vượt qua bộ test. Luôn chạy thử lệnh build (`dotnet build` cho backend, `npm run build` cho frontend) để kiểm chứng.
2. **Commit đơn nhiệm (Single Responsibility Commit)**: Mỗi commit chỉ nên giải quyết một vấn đề hoặc tính năng duy nhất. Tránh gộp chung việc sửa lỗi (bug fix) và thêm tính năng mới (feature) vào cùng một commit.
3. **Không commit file rác / thông tin nhạy cảm**: 
   - Kiểm tra kỹ `git status` trước khi thêm file.
   - Tuyệt đối không commit file build (`bin/`, `obj/`, `node_modules/`, `.next/`, `dist/`).
   - Tuyệt đối không commit file cấu hình môi trường chứa thông tin nhạy cảm (`.env`, `appsettings.json` chứa mật khẩu thực, API Keys, connection strings).
4. **Viết commit message bằng tiếng Việt hoặc tiếng Anh một cách nhất quán**: Dưới đây sẽ hướng dẫn cấu trúc chuẩn hóa cho cả hai ngôn ngữ.

---

## ✍️ 2. Cấu Trúc Commit Message Chuẩn Hóa

Thông điệp commit (Commit Message) phải tuân theo cấu trúc **Conventional Commits** kết hợp **Gitmoji**:

```text
<gitmoji> <type>(<scope>): <description>

[body - giải thích chi tiết lý do và thay đổi quan trọng nếu cần]

[footer - mã ticket/issue hoặc breaking changes nếu có]
```

### Chi tiết các thành phần:

* **`<gitmoji>`**: Biểu tượng cảm xúc (Emoji) giúp phân biệt nhanh loại commit bằng mắt thường.
* **`<type>`**: Loại thay đổi (bắt buộc), bao gồm các từ khóa sau:
  - `feat`: Tính năng mới (Feature).
  - `fix`: Sửa lỗi (Bug fix).
  - `docs`: Tài liệu (Documentation).
  - `style`: Định dạng code (khoảng trắng, dấu chấm phẩy, format code - không ảnh hưởng logic).
  - `refactor`: Tái cấu trúc code (không thêm tính năng mới, không sửa lỗi).
  - `perf`: Tối ưu hiệu năng (Performance).
  - `test`: Thêm hoặc sửa test cases.
  - `build`: Thay đổi hệ thống build hoặc package dependencies (ví dụ: cập nhật NuGet, npm packages).
  - `ci`: Thay đổi cấu hình CI/CD (ví dụ: GitHub Actions workflow).
  - `chore`: Các công việc vặt khác (cập nhật cấu hình dự án, `.gitignore`, v.v.).
  - `revert`: Hoàn tác một commit trước đó.
* **`<scope>`**: Phạm vi/Thành phần bị ảnh hưởng (tùy chọn nhưng khuyến khích). Ví dụ: `auth`, `order`, `inventory`, `cart`, `payment`, `ui`.
* **`<description>`**: Mô tả ngắn gọn về thay đổi:
  - Viết ở dạng hiện tại, thể mệnh lệnh (ví dụ: dùng `thêm api thanh toán` thay vì `đã thêm api thanh toán`).
  - Không viết hoa chữ cái đầu tiên (trừ danh từ riêng).
  - Không đặt dấu chấm (`.`) ở cuối mô tả.

---

## 🎨 3. Danh Sách Gitmoji Phổ Biến

| Gitmoji | Type | Ý nghĩa / Khi nào sử dụng |
| :--- | :--- | :--- |
| ✨ `:sparkles:` | `feat` | Thêm một tính năng mới |
| 🐛 `:bug:` | `fix` | Sửa một lỗi/bug trong code |
| 📝 `:memo:` | `docs` | Cập nhật hoặc viết mới tài liệu (ReadMe, API docs) |
| ♻️ `:recycle:` | `refactor` | Tái cấu trúc code để cải thiện chất lượng/độ sạch |
| ⚡ `:zap:` | `perf` | Tối ưu hóa hiệu suất hoặc tốc độ chạy code |
| 🎨 `:art:` | `style`/`ui` | Định dạng code, cải thiện UI/CSS hoặc cấu trúc file |
| 🧪 `:test_tube:`| `test` | Thêm, sửa đổi hoặc cải thiện unit test / integration test |
| 🔧 `:wrench:` | `chore` / `config`| Cập nhật cấu hình dự án, bổ sung package |
| 🔒 `:lock:` | `security` | Khắc phục các vấn đề liên quan đến bảo mật |
| 💥 `:boom:` | `breaking` | Thay đổi lớn gây lỗi tương thích (Breaking Change) |
| ⏪ `:rewind:` | `revert` | Hoàn tác (revert) các thay đổi từ một commit khác |

---

## 💡 4. Ví Dụ Cụ Thể

### Ví dụ về Commit ngắn gọn (Chỉ có Header):
* `✨ feat(order): thêm api tạo đơn hàng và tích hợp thanh toán VnPay`
* `🐛 fix(auth): sửa lỗi token hết hạn không tự động logout người dùng`
* `♻️ refactor(inventory): tối ưu hóa truy vấn Sql Server bằng Dapper`
* `📝 docs(readme): cập nhật hướng dẫn chạy môi trường docker-compose`
* `🎨 style(web-ui): sửa lỗi căn lề nút đặt hàng trên thiết bị di động`
* `🔧 chore(gitignore): bổ sung bỏ qua các file tạm sinh ra bởi Visual Studio`

### Ví dụ về Commit chi tiết (Có Header, Body và Footer):
```text
✨ feat(auth): tích hợp đăng nhập bằng Google OAuth2

- Thêm cấu hình GoogleClientId và GoogleClientSecret vào appsettings.json
- Viết mới dịch vụ GoogleAuthService xử lý verify ID Token từ Google
- Thêm endpoint `/api/auth/google` trong WebAPI
- Cập nhật giao diện trang đăng nhập React để hiển thị nút "Sign in with Google"

Ref: #104
```

---

## 🛠️ 5. Quy Trình Commit An Toàn Cho AI Agent

Khi hoàn thành công việc, hãy thực hiện lần lượt các bước sau:

1. **Kiểm tra trạng thái**:
   ```bash
   git status
   ```
   Xem kỹ danh sách file thay đổi và file chưa theo dõi (untracked files). Đảm bảo không có file rác.

2. **So sánh sự thay đổi (Review Diff)**:
   ```bash
   git diff --cached
   ```
   Hoặc tự review các dòng code vừa viết để chắc chắn không còn các đoạn code debug tạm thời (ví dụ: `console.log`, `Console.WriteLine`, code hardcode test).

3. **Build & Test**:
   - Chạy lệnh build của dự án để chắc chắn không lỗi compile.
   - Chạy bộ unit test (nếu có).

4. **Thêm file thay đổi vào Staging**:
   - Thêm từng file cụ thể: `git add src/WebAPI/Controllers/OrderController.cs`
   - **Tuyệt đối tránh** dùng `git add .` bừa bãi khi chưa lọc kỹ các file không mong muốn.

5. **Commit với thông điệp chuẩn**:
   ```bash
   git commit -m "✨ feat(order): thêm api tạo đơn hàng và tích hợp thanh toán VnPay"
   ```

6. **Kiểm tra lại lịch sử**:
   ```bash
   git log -n 1
   ```
   Xác nhận commit message đã hiển thị đúng định dạng và không bị lỗi hiển thị ký tự đặc biệt/tiếng Việt.
