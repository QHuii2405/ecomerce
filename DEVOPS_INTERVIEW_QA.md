# Bộ Câu Hỏi Phỏng Vấn DevOps (Cho dự án E-commerce)

Đây là các câu hỏi mà nhà tuyển dụng rất có thể sẽ hỏi khi bạn ghi các công nghệ này vào CV. Hãy đọc kỹ, hiểu rõ và trả lời một cách tự tin.

---

### PHẦN 1: TỔNG QUAN & CI/CD

**Câu 1: DevOps là gì theo cách hiểu của em?**
* **Trả lời:** DevOps không phải là một công cụ hay chức danh, mà là sự kết hợp giữa triết lý văn hóa, quy trình và công cụ nhằm tự động hóa và tích hợp giữa nhóm Phát triển (Dev) và nhóm Vận hành (Ops). Mục đích là để đưa phần mềm từ lúc viết code đến tay người dùng nhanh hơn, an toàn hơn và chất lượng hơn.

**Câu 2: Khác biệt giữa CI (Continuous Integration) và CD (Continuous Deployment/Delivery) là gì? Trong project của em, quy trình này diễn ra như thế nào?**
* **Trả lời:** 
  - **CI:** Là quá trình tích hợp code liên tục. Khi code được push lên, nó sẽ tự động được build và test. (Trong project, em dùng GitHub Actions để cài đặt .NET/Node.js, build code để đảm bảo code không bị lỗi cú pháp).
  - **CD (Delivery/Deployment):** Là bước tiếp theo, tự động đóng gói và triển khai ứng dụng. (Trong project, CD của em là build thành Docker Image, đẩy lên Docker Hub, và SSH vào AWS EC2 để kéo image mới về chạy).

**Câu 3: Em có xử lý bảo mật nào trong CI/CD không?**
* **Trả lời:** Có ạ. Em áp dụng DevSecOps bằng cách sử dụng **Trivy**. Ngay sau khi Docker Image được build, Trivy sẽ quét tự động để tìm kiếm các lỗ hổng hệ điều hành và thư viện. Nếu em đổi cấu hình `exit-code` thành 1, pipeline sẽ tự động báo lỗi và dừng lại nếu phát hiện lỗ hổng nghiêm trọng (Critical), ngăn chặn việc triển khai mã độc hại.

---

### PHẦN 2: DOCKER & CONTAINER

**Câu 4: Lợi ích của Docker so với máy ảo (Virtual Machine) truyền thống?**
* **Trả lời:** Docker nhẹ và nhanh hơn VM rất nhiều vì nó dùng chung hệ điều hành lõi (Host Kernel) của máy vật lý, trong khi VM yêu cầu phải cài cả một hệ điều hành riêng (Guest OS). Container khởi động trong vài giây, tiết kiệm RAM/CPU và giải quyết được vấn đề "Code chạy tốt trên máy em nhưng lỗi trên server" nhờ việc đóng gói toàn bộ môi trường vào image.

**Câu 5: Trong `docker-compose.prod.yml`, em dùng thẻ `depends_on`. Điều gì xảy ra nếu em không có thẻ này?**
* **Trả lời:** Thẻ `depends_on` dùng để quy định thứ tự khởi động. Ví dụ, Backend cần Database (SQL Server/Redis) phải chạy trước thì mới kết nối được. Nếu không có `depends_on`, Docker sẽ khởi động tất cả container cùng lúc. Khi đó Backend khởi động xong trước Database và sẽ bị văng lỗi Connection Refused, dẫn đến việc container backend bị sập (crash).

---

### PHẦN 3: INFRASTRUCTURE AS CODE (TERRAFORM)

**Câu 6: Tại sao em lại dùng Terraform thay vì vào console AWS tạo bằng tay cho nhanh? Project nhỏ thì có đáng dùng không?**
* **Trả lời:** Với project nhỏ, làm bằng tay có thể nhanh lúc đầu. Nhưng điểm mạnh của Terraform là tính **tái sử dụng và tự liệu hóa (self-documenting)**. Nó giúp toàn bộ hạ tầng được lưu trữ dưới dạng code (IaC), dễ dàng quản lý phiên bản trên Git, làm việc nhóm, và có thể khôi phục lại hạ tầng giống hệt trong 2 phút nếu xảy ra thảm họa mất data, điều mà làm bằng tay rất mất thời gian và dễ sai sót.

**Câu 7: Terraform State file là gì? Nó quan trọng như thế nào?**
* **Trả lời:** State file (`.tfstate`) là file dùng để Terraform ghi nhớ những tài nguyên vật lý nào (như IP, ID của EC2) đã được nó tạo ra và liên kết chúng với các đoạn code khai báo. Nếu không có file này, lần sau chạy lệnh apply, Terraform sẽ không biết là EC2 đã tồn tại và sẽ báo lỗi hoặc cố gắng tạo ra các EC2 trùng lặp. (Trong thực tế làm việc nhóm, file này thường phải được lưu từ xa trên AWS S3 và khóa bằng DynamoDB).

---

### PHẦN 4: MONITORING & NGINX

**Câu 8: Tại sao lại cần cả Prometheus và Grafana? Một cái không đủ sao?**
* **Trả lời:** Chúng có vai trò khác nhau và bù trừ cho nhau. Prometheus là một cơ sở dữ liệu chuỗi thời gian (Time-series DB) cực kỳ giỏi trong việc thu thập và lưu trữ số liệu (metrics) từ server. Tuy nhiên, giao diện mặc định của nó rất nghèo nàn. Grafana là công cụ trực quan hóa (Visualization), nó dùng dữ liệu từ Prometheus để vẽ ra các biểu đồ (Dashboard) đẹp, dễ theo dõi và tích hợp tốt các chức năng cảnh báo (Gửi tin nhắn Slack/Email).

**Câu 9: Nginx đóng vai trò gì trong dự án của em?**
* **Trả lời:** Nginx đóng vai trò là một **Reverse Proxy**. Thay vì mở trực tiếp cổng của Frontend (ví dụ 3000) hay Backend (8080) ra Internet (điều này nguy hiểm và khó quản lý), em chỉ mở cổng 80/443 của Nginx. Nginx đứng mũi chịu sào, che giấu kiến trúc bên trong, định tuyến request (ví dụ: `/api` chuyển về backend, `/` chuyển về frontend). Nó cũng tạo tiền đề để em cài đặt SSL/HTTPS (chứng chỉ bảo mật) và Load Balancing sau này.

---

### PHẦN 5: KUBERNETES (K8S)

**Câu 10: Em có thư mục k8s/, vậy sự khác nhau giữa Kubernetes và Docker Compose là gì? Khi nào thì dùng cái nào?**
* **Trả lời:** 
  - **Docker Compose** phù hợp cho môi trường phát triển (Dev) hoặc triển khai trên một máy chủ (Single-node). Nó dễ thiết lập nhưng không có khả năng tự phục hồi mạnh mẽ hay phân tán tài nguyên.
  - **Kubernetes (K8s)** là công cụ cho môi trường Production lớn (Multi-node). Nó có các tính năng cao cấp như tự động thay thế container chết (Self-healing), tự động tăng giảm số lượng container tùy theo lượng truy cập (Auto-scaling), và không làm gián đoạn hệ thống khi cập nhật phiên bản mới (Zero-downtime deployment). Khi hệ thống lớn, bắt buộc phải chuyển từ Compose sang K8s.
