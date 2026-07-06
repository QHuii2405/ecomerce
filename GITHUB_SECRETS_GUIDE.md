# Hướng dẫn thiết lập Github Secrets

Dưới đây là danh sách toàn bộ các biến **Repository Secrets** mà bạn cần thiết lập trên Github (Vào `Settings` > `Secrets and variables` > `Actions` > `New repository secret`). 

Mình đã tổng hợp lại các giá trị cũ từng có sẵn trong mã nguồn để bạn copy-paste cho nhanh:

## 1. Các Secrets mới (Vừa được tách ra để bảo mật)

| Tên Secret (Name) | Giá trị cần điền (Value) | Ghi chú |
| :--- | :--- | :--- |
| **`GOOGLE_CLIENT_ID`** | `868316103534-vghj1eq84lqtma1lvknghmc3ifgb73cg.apps.googleusercontent.com` | ID dùng để đăng nhập bằng Google. |
| **`JWT_SECRET`** | `ChaoBanDayLaKeyBiMatSieuCapVipPro2026` | Khóa mã hóa Token (lấy từ appsettings cũ). |
| **`EMAIL_PASSWORD`** | `trki pwqq dahp ucln` | App Password của Gmail `hotaro2405@gmail.com`. |
| **`MOMO_ACCESS_KEY`** | `klm05TvNCzjOaHU1` | Mã Access Key của ví MoMo. |
| **`MOMO_SECRET_KEY`** | `at67qH6mk8g5HI18mI81kvOSaqNeeM3K` | Mã Secret Key của ví MoMo. |

---

## 2. Các Secrets cũ (Bạn có thể đã cài đặt trước đó)

Nếu bạn đã từng thiết lập Deploy thành công qua Git Action thì các biến này có thể đã tồn tại sẵn. Hãy kiểm tra lại xem có thiếu biến nào không:

| Tên Secret (Name) | Giá trị cần điền (Value) |
| :--- | :--- |
| **`DOCKER_USERNAME`** | Tên tài khoản Docker Hub của bạn |
| **`DOCKER_PASSWORD`** | Mật khẩu (hoặc Access Token) Docker Hub của bạn |
| **`SERVER_HOST`** | IP Public của máy chủ AWS EC2 (Ví dụ: `15.134.227.19`) |
| **`SERVER_USERNAME`** | Tên đăng nhập của máy chủ AWS (thường là `ubuntu` hoặc `ec2-user`) |
| **`SERVER_SSH_KEY`** | Nội dung file `.pem` hoặc Private Key để kết nối vào máy chủ |
| **`DB_HOST`** | Endpoint của AWS RDS (hoặc IP Database) |
| **`DB_USER`** | Tên đăng nhập Database (Ví dụ: `admin`) |
| **`DB_PASSWORD`** | Mật khẩu của Database |

---

### Hướng dẫn cách dán vào Github:
1. Đăng nhập vào Github, mở Repository `ecomerce`.
2. Bấm vào tab **Settings** (cài đặt).
3. Ở menu bên trái, cuộn xuống mục **Security**, chọn **Secrets and variables** > **Actions**.
4. Bấm nút màu xanh **New repository secret**.
5. Nhập tên biến vào ô *Name*, copy giá trị tương ứng ở bảng trên vào ô *Secret*, rồi bấm **Add secret**.
6. Lặp lại với tất cả các biến còn thiếu.
