---
name: react-tailwind-vite
description: Hướng dẫn phát triển giao diện hiện đại, tự nhiên và cao cấp với React 19, Vite và Tailwind CSS v4, loại bỏ hoàn toàn mã nguồn dạng "generic AI".
tags: [react, tailwind, vite, frontend, ui, anti-ai-ux]
---

# 🎨 Kỹ năng: Phát triển React 19 & Tailwind CSS v4 Cao cấp (Chống mã nguồn "Generic AI")

Kỹ năng này định hướng AI Agent cách viết mã nguồn Frontend đạt tiêu chuẩn cao của các lập trình viên Senior, loại bỏ hoàn toàn các đặc trưng thiết kế cẩu thả hoặc rập khuôn thường thấy của AI (như sử dụng màu sắc thuần cơ bản, thiếu hiệu ứng chuyển động, các file component khổng lồ hoặc giao diện tĩnh thiếu sức sống).

---

## 🚫 1. Nhận diện và Tiêu diệt mã nguồn "Generic AI" (Anti-AI-UX Guidelines)

AI tạo giao diện thường mắc các lỗi rập khuôn khiến sản phẩm trông giống như một bản mẫu (template) rẻ tiền. Agent bắt buộc phải tuân thủ các quy tắc sau để tạo ra giao diện tự nhiên, chuyên nghiệp:

### A. Màu sắc: Tuyệt đối không dùng màu cơ bản thuần túy
*❌ Tránh rập khuôn*: Không dùng màu đỏ thuần (`bg-red-500`), xanh lá thuần (`bg-green-500`), hay xanh dương thuần (`bg-blue-600`) cho các nút bấm chính. Chúng làm giao diện trông thô và thiếu tính thẩm mỹ.
*✔️ Giải pháp cao cấp*: Sử dụng các tông màu HSL hoặc các bảng màu trung tính, dịu mắt của Tailwind v4:
- Màu chủ đạo (Primary): `indigo-600`, `violet-600`, `slate-950`
- Màu thành công (Success): `emerald-600`, `teal-600`
- Màu cảnh báo (Warning): `amber-500`
- Nền tối (Dark Background): `slate-900`, `zinc-950` kết hợp với đường viền mảnh `border-white/5`.

### B. Bố cục và Khoảng trống: Chống nhồi nhét
*❌ Tránh rập khuôn*: Gom toàn bộ tính năng vào một trang duy nhất hoặc nhồi nhét quá nhiều thẻ (cards) sát nhau.
*✔️ Giải pháp cao cấp*: Thiết lập khoảng trống (whitespace) rộng rãi để giao diện "thở" (`py-12 md:py-24`, `gap-8 md:gap-12`). Sử dụng các đường ngăn cách cực mảnh (`border-slate-100` hoặc `border-white/10` cho chế độ tối) thay vì các đường kẻ đậm.

### C. Hiệu ứng động (Micro-animations): Làm cho giao diện "sống"
*❌ Tránh rập khuôn*: Các nút bấm và thẻ sản phẩm đứng yên khi hover chuột hoặc thay đổi trạng thái đột ngột không có chuyển tiếp.
*✔️ Giải pháp cao cấp*: Mọi phần tử tương tác bắt buộc phải có hiệu ứng transition mượt mà (`transition-all duration-300 ease-out`):
- Thẻ sản phẩm: Hover sẽ nhấc nhẹ lên (`hover:-translate-y-1 hover:shadow-xl hover:border-indigo-500/20`).
- Nút bấm: Hiệu ứng bấm co giãn nhẹ (`active:scale-95`).
- Sử dụng các lớp phủ Gradient mờ chuyển động khi hover (`bg-gradient-to-r hover:from-indigo-600 hover:to-violet-600`).

### D. Loại bỏ hoàn toàn Placeholders
*❌ Tránh rập khuôn*: Sử dụng các liên kết ảnh placeholder xám xịt hoặc text giả dạng "Lorem Ipsum".
*✔️ Giải pháp cao cấp*: 
- Sử dụng thư viện biểu tượng **Lucide React** một cách thông minh (kích thước mảnh `strokeWidth={1.5}` hoặc `strokeWidth={2}`).
- Sử dụng các khối màu Gradient dạng Glassmorphic tinh tế làm nền thay cho ảnh lỗi.
- Sử dụng hiệu ứng **Skeleton Loading** (màn hình chờ nhấp nháy mảnh) khi dữ liệu đang tải để nâng cao trải nghiệm khách hàng.

---

## 📂 2. Tổ chức cấu trúc mã nguồn chuẩn Product

Mã nguồn Frontend phải được cấu trúc rõ ràng thành các thành phần nhỏ, độc lập, dễ bảo trì và kiểm thử.

### A. Phân tách Logic và Giao diện (Separation of Concerns)
- **Component hiển thị (Presentational Components)**: Chỉ nhận props, hiển thị giao diện và phát sự kiện (ví dụ: `ProductCard.jsx`, `Button.jsx`).
- **Component container / Pages**: Quản lý trạng thái, gọi API thông qua Axios Client, điều phối logic nghiệp vụ và truyền dữ liệu xuống các component con.

### B. Ví dụ cấu trúc một Page chuẩn: Trang Danh sách sản phẩm (`ProductList.jsx`)
```jsx
import React, { useEffect, useState } from 'react';
import axiosClient from '../api/axiosClient';
import ProductCard from '../components/ProductCard';
import { LayoutGrid, AlertCircle, RefreshCw } from 'lucide-react';

export default function ProductList() {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchProducts();
  }, []);

  const fetchProducts = async () => {
    setLoading(true);
    setError(null);
    try {
      const response = await axiosClient.get('/products');
      setProducts(response.data.data);
    } catch (err) {
      setError(err.response?.data?.message || 'Không thể tải danh sách sản phẩm. Vui lòng thử lại.');
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return (
      <div className="flex min-h-[400px] flex-col items-center justify-center gap-4">
        <RefreshCw className="animate-spin text-indigo-600" size={32} />
        <p className="text-sm font-medium text-slate-500 animate-pulse">Đang tải danh sách sản phẩm...</p>
      </div>
    );
  }

  if (error) {
    return (
      <div className="mx-auto max-w-md rounded-2xl border border-red-100 bg-red-50/50 p-6 text-center backdrop-blur-md">
        <AlertCircle className="mx-auto text-red-500" size={32} />
        <h3 className="mt-3 text-base font-semibold text-slate-900">Đã xảy ra lỗi</h3>
        <p className="mt-2 text-sm text-red-600">{error}</p>
        <button
          onClick={fetchProducts}
          className="mt-4 inline-flex items-center gap-2 rounded-xl bg-red-600 px-4 py-2 text-sm font-medium text-white shadow-sm hover:bg-red-500 active:scale-95 transition-all"
        >
          Thử lại
        </button>
      </div>
    );
  }

  return (
    <div className="mx-auto max-w-7xl px-4 py-12 sm:px-6 lg:px-8">
      <div className="flex items-center justify-between border-b border-slate-100 pb-5">
        <div>
          <h2 className="text-2xl font-bold tracking-tight text-slate-950">Bộ Sưu Tập Sản Phẩm</h2>
          <p className="mt-1 text-sm text-slate-500">Khám phá các sản phẩm chất lượng cao của chúng tôi</p>
        </div>
        <LayoutGrid className="text-slate-400" size={20} />
      </div>

      <div className="mt-8 grid grid-cols-1 gap-x-6 gap-y-10 sm:grid-cols-2 lg:grid-cols-4 xl:gap-x-8">
        {products.map((product) => (
          <ProductCard key={product.id} product={product} />
        ))}
      </div>
    </div>
  );
}
```

---

## 🛡️ 3. Quản lý trạng thái và Xác thực biểu mẫu (Forms)
- Sử dụng **React Hooks** (`useState`, `useReducer`, `useCallback`) một cách khoa học để tránh hiện tượng re-render không cần thiết.
- Mọi biểu mẫu nhập liệu (như Đăng ký, Đăng nhập, Đặt hàng) bắt buộc phải được xác thực dữ liệu đầu vào phía Client trước khi gửi request lên server. Hiển thị thông báo lỗi ngay dưới mỗi ô nhập liệu bằng màu sắc tinh tế (`text-rose-500 text-xs mt-1`), và tắt nút submit nếu biểu mẫu không hợp lệ.
- Lưu trữ token xác thực (`accessToken`) an toàn trong `localStorage` hoặc cấu hình cơ chế cookie `HttpOnly` cho môi trường production để chống các cuộc tấn công XSS.
