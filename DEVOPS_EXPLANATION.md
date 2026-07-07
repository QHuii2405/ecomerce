# Giải thích Chi tiết các Kỹ thuật DevOps đã áp dụng

Tài liệu này giải thích sâu về lý thuyết, cơ chế hoạt động và lý do lựa chọn các công cụ DevOps đã được đưa vào dự án E-commerce. Bạn có thể dùng kiến thức này để hiểu rõ bản chất và trình bày thuyết phục với nhà tuyển dụng.

---

## 1. Infrastructure as Code (IaC) với Terraform

### Lý thuyết & Cách hoạt động
**IaC (Infrastructure as Code)** là phương pháp quản lý và cung cấp hạ tầng máy tính thông qua các file cấu hình (code) có thể đọc được bằng máy, thay vì phải cấu hình thủ công qua giao diện (GUI).
- **Terraform** hoạt động theo cơ chế **Khai báo (Declarative)**: Bạn chỉ cần mô tả *kết quả cuối cùng* bạn muốn (ví dụ: "Tôi muốn 1 máy ảo EC2, 1 mạng VPC"), Terraform sẽ tự tính toán các bước cần thiết để gọi API của AWS và tạo ra kết quả đó.
- Terraform sử dụng **State file (`terraform.tfstate`)** để ghi nhớ trạng thái của hạ tầng hiện tại. Mỗi lần bạn thay đổi code, nó sẽ so sánh code với State file để tìm ra sự khác biệt (diff) và chỉ thay đổi những phần cần thiết.

### Tại sao lại chọn Terraform?
- **Tái sử dụng & Tự động hóa:** Thay vì mất 30 phút click chuột trên AWS để tạo mạng (VPC, Subnet, Internet Gateway) và máy chủ EC2, ta chỉ cần chạy 1 lệnh `terraform apply` trong 2 phút.
- **Quản lý phiên bản:** Code hạ tầng có thể được lưu trữ trên Git, giúp team DevOps review lỗi trước khi triển khai và dễ dàng rollback (quay lại bản cũ) nếu hạ tầng bị sập.
- **Tránh sai sót con người (Human Error):** Click nhầm cấu hình mạng có thể gây ra lỗ hổng bảo mật nghiêm trọng. Code giúp mọi thứ chuẩn xác 100% qua mỗi lần chạy.

---

## 2. DevSecOps với Trivy (Security Scanning)

### Lý thuyết & Cách hoạt động
**DevSecOps** là việc đưa bảo mật (Security) vào ngay từ giai đoạn đầu của vòng đời phát triển phần mềm (DevOps).
- **Trivy** là một công cụ quét lỗ hổng bảo mật mã nguồn mở, hoạt động bằng cách tải xuống cơ sở dữ liệu các lỗ hổng đã biết (CVE - Common Vulnerabilities and Exposures).
- Khi CI/CD build xong Docker Image, Trivy sẽ quét từng layer (lớp) của Image đó, tìm kiếm các hệ điều hành cũ (vd: Alpine bản cũ) hoặc các thư viện (Nodejs/.NET packages) có chứa lỗ hổng bảo mật và báo cáo lại.

### Tại sao lại chọn Trivy?
- Tốc độ cực nhanh và dễ dàng tích hợp vào GitHub Actions.
- Việc phát hiện lỗi ngay tại CI/CD Pipeline (Shift-Left Security) giúp lập trình viên sửa lỗi trước khi image độc hại được đẩy lên Server, tránh rủi ro bị hacker khai thác.

---

## 3. Monitoring & Observability (Prometheus & Grafana)

### Lý thuyết & Cách hoạt động
- **Monitoring** là việc quan sát xem hệ thống có đang hoạt động hay không. **Observability** là khả năng hiểu được *tại sao* hệ thống lại không hoạt động dựa trên dữ liệu.
- **Prometheus** hoạt động theo cơ chế **Pull Model**: Thay vì đợi ứng dụng gửi dữ liệu tới, Prometheus sẽ chủ động "đến gõ cửa" (gọi HTTP request) các ứng dụng (targets) mỗi 15 giây để kéo số liệu (CPU, RAM, số lượng request) về và lưu trữ vào Time-Series Database.
- **Grafana** là công cụ trực quan hóa. Nó kết nối vào Prometheus, lấy dữ liệu khô khan và biến chúng thành các biểu đồ đẹp mắt, dễ hiểu.

### Tại sao lại chọn combo này?
- Đây là tiêu chuẩn vàng (Industry Standard) trong thế giới Cloud Native. Hầu hết các công ty lớn đều dùng.
- Giúp team DevOps chủ động thiết lập cảnh báo (Alert) khi CPU > 80% trước khi Server thực sự bị sập, thay vì đợi khách hàng gọi điện than phiền.

---

## 4. Nginx (Reverse Proxy & Load Balancing)

### Lý thuyết & Cách hoạt động
- **Reverse Proxy** là một máy chủ đứng trước các máy chủ backend/frontend. Khi Client (trình duyệt) gửi request, nó không nói chuyện trực tiếp với Backend, mà gửi tới Nginx. Nginx nhận request, xem xét và chuyển tiếp (forward) request đó đến đúng container.
- Nginx xử lý theo cơ chế **Event-driven, Asynchronous**, cho phép nó xử lý hàng chục ngàn kết nối đồng thời với lượng RAM rất nhỏ.

### Tại sao lại chọn Nginx?
- **Bảo mật:** Ứng dụng Backend không bị phơi bày trực tiếp ra Internet. Hacker chỉ thấy Nginx.
- **Quản lý tập trung:** Dễ dàng cấu hình HTTPS/SSL (chứng chỉ bảo mật) tại 1 nơi duy nhất là Nginx thay vì phải cấu hình riêng lẻ cho Frontend và Backend.
- Nếu ứng dụng to lên, Nginx có thể làm **Load Balancer** (chia đều tải cho 3 cái backend thay vì 1).

---

## 5. Kubernetes (K8s) Manifests

### Lý thuyết & Cách hoạt động
**Kubernetes (K8s)** là một hệ thống quản lý, điều phối (Orchestration) các container.
- Thay vì bạn phải tự khởi động lại container khi nó chết (như dùng Docker Compose), K8s có **Control Plane** liên tục giám sát.
- Bạn viết file YAML (Manifests) khai báo: "Tôi muốn ứng dụng backend luôn có 2 bản sao (Replicas) chạy song song". K8s sẽ đảm bảo điều đó. Nếu 1 cái sập, K8s tự động tạo cái mới thay thế (Self-healing).

### Tại sao K8s lại quan trọng?
- Docker Compose chỉ phù hợp cho 1 server duy nhất. Khi website E-commerce của bạn cần chạy trên cụm 10 Server khác nhau để phục vụ hàng triệu người, Docker Compose trở nên vô dụng. K8s là giải pháp tối thượng để Scale (mở rộng) hệ thống.

---

## HƯỚNG DẪN: Bạn Cần Làm Gì Để Áp Dụng Chúng Vào Project Thực Tế?

1. **Với Terraform (Hạ tầng):**
   - Đăng ký 1 tài khoản AWS (Free Tier).
   - Tải và cài đặt **Terraform CLI** và **AWS CLI** trên máy tính của bạn.
   - Chạy lệnh `aws configure` để đăng nhập.
   - Mở terminal tại thư mục `terraform/`, chạy lần lượt: `terraform init` -> `terraform plan` -> `terraform apply`. Bạn sẽ thấy EC2 tự động hiện lên trên AWS!

2. **Với CI/CD & DevSecOps (GitHub Actions):**
   - Không cần làm gì ở máy local. Khi bạn `git push` code mới lên nhánh `main`, hãy vào tab **Actions** trên GitHub repository của bạn.
   - Nhớ cấu hình các Secrets trong repository settings (`DOCKER_USERNAME`, `SERVER_SSH_KEY`, v.v.).
   - Bạn sẽ thấy job quét Trivy chạy. Nếu bạn muốn pipeline thất bại khi gặp lỗi CRITICAL, hãy đổi `exit-code: '0'` thành `exit-code: '1'` trong file `ci.yml`.

3. **Với Monitoring & Nginx (Trên Server):**
   - Khi Server đã cấu hình xong, bạn SSH vào Server, chuyển tới thư mục chứa code và chạy lệnh:
     `docker compose -f docker-compose.prod.yml up -d`
   - Truy cập `http://<IP_SERVER>:9090` để xem Prometheus và `http://<IP_SERVER>:3001` (user: admin/admin) để cấu hình Grafana.
   - Lúc này, truy cập IP Server qua cổng 80, Nginx sẽ tự động định tuyến giao diện Frontend cho bạn.

4. **Với Kubernetes (Học tập):**
   - Để chạy thử các file `k8s/`, bạn hãy tải **Minikube** hoặc kích hoạt tính năng Kubernetes có sẵn trong Docker Desktop.
   - Chạy lệnh: `kubectl apply -f k8s/` để xem K8s tạo các Pods như thế nào.
