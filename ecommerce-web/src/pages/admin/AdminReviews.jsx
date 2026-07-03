import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import Swal from 'sweetalert2';
import api from '../../api/axios';
import { Search, Star, Trash2, MessageSquare, AlertCircle, CornerDownRight, Reply } from 'lucide-react';

export default function AdminReviews() {
  const [reviews, setReviews] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchQuery, setSearchQuery] = useState('');
  const [filterRating, setFilterRating] = useState('all');

  useEffect(() => {
    fetchReviews();
  }, []);

  const fetchReviews = async () => {
    setLoading(true);
    try {
      const res = await api.get('/admin/reviews');
      setReviews(res.data || []);
    } catch (err) {
      console.error(err);
      Swal.fire({
        icon: 'error',
        title: 'Lỗi',
        text: 'Không thể tải danh sách đánh giá'
      });
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    const result = await Swal.fire({
      title: 'Bạn có chắc chắn?',
      text: "Đánh giá này sẽ bị xóa khỏi hệ thống!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#ef4444',
      cancelButtonColor: '#6b7280',
      confirmButtonText: 'Đồng ý, xóa!',
      cancelButtonText: 'Hủy'
    });

    if (result.isConfirmed) {
      try {
        await api.delete(`/admin/reviews/${id}`);
        Swal.fire('Đã xóa!', 'Đánh giá đã được xóa thành công.', 'success');
        fetchReviews();
      } catch (err) {
        console.error(err);
        Swal.fire('Lỗi!', err.response?.data?.message || 'Có lỗi xảy ra khi xóa.', 'error');
      }
    }
  };

  const handleReply = async (review) => {
    const { value: replyText } = await Swal.fire({
      title: 'Trả lời đánh giá',
      input: 'textarea',
      inputLabel: `Khách hàng: ${review.userName}`,
      inputPlaceholder: 'Nhập câu trả lời của bạn...',
      inputValue: review.adminReply || '',
      showCancelButton: true,
      confirmButtonText: 'Gửi trả lời',
      cancelButtonText: 'Hủy',
      inputValidator: (value) => {
        if (!value.trim()) {
          return 'Bạn cần nhập nội dung trả lời!';
        }
      }
    });

    if (replyText) {
      try {
        await api.post(`/admin/reviews/${review.id}/reply`, { reply: replyText });
        Swal.fire('Thành công!', 'Đã gửi câu trả lời.', 'success');
        fetchReviews();
      } catch (err) {
        console.error(err);
        Swal.fire('Lỗi!', err.response?.data?.message || 'Không thể gửi câu trả lời.', 'error');
      }
    }
  };

  const filteredReviews = reviews.filter(r => {
    const matchesSearch = r.userName?.toLowerCase().includes(searchQuery.toLowerCase()) || 
                          r.comment?.toLowerCase().includes(searchQuery.toLowerCase());
    const matchesRating = filterRating === 'all' || r.rating.toString() === filterRating;
    return matchesSearch && matchesRating;
  });

  return (
    <div className="p-8 max-w-7xl mx-auto space-y-8">
      {/* Header */}
      <div className="flex flex-col md:flex-row md:items-center justify-between gap-4">
        <div>
          <h1 className="text-3xl font-black text-on-surface flex items-center gap-3">
            <MessageSquare className="text-primary" size={32} />
            Quản lý Đánh giá
          </h1>
          <p className="text-on-surface-variant mt-2">Theo dõi và quản lý các đánh giá từ khách hàng.</p>
        </div>
      </div>

      {/* Filters */}
      <div className="bg-surface border border-outline-variant/30 p-4 rounded-3xl flex flex-col md:flex-row gap-4">
        <div className="flex-1 relative">
          <Search className="absolute left-4 top-1/2 -translate-y-1/2 text-on-surface-variant" size={20} />
          <input
            type="text"
            placeholder="Tìm theo người dùng, nội dung..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            className="w-full pl-12 pr-4 py-3 bg-surface-container-lowest border border-outline-variant/30 rounded-2xl text-sm focus:border-primary focus:ring-1 focus:ring-primary transition-all"
          />
        </div>
        <select
          value={filterRating}
          onChange={(e) => setFilterRating(e.target.value)}
          className="w-full md:w-48 px-4 py-3 bg-surface-container-lowest border border-outline-variant/30 rounded-2xl text-sm focus:border-primary transition-all cursor-pointer"
        >
          <option value="all">Tất cả số sao</option>
          <option value="5">5 Sao</option>
          <option value="4">4 Sao</option>
          <option value="3">3 Sao</option>
          <option value="2">2 Sao</option>
          <option value="1">1 Sao</option>
        </select>
      </div>

      {/* Content */}
      <div className="bg-surface border border-outline-variant/30 rounded-3xl overflow-hidden shadow-sm">
        <div className="overflow-x-auto">
          <table className="w-full text-left border-collapse min-w-[800px]">
            <thead>
              <tr className="bg-surface-container-low/50">
                <th className="py-4 px-6 font-bold text-xs text-on-surface-variant uppercase tracking-wider">Khách hàng & Sản phẩm</th>
                <th className="py-4 px-6 font-bold text-xs text-on-surface-variant uppercase tracking-wider">Đánh giá</th>
                <th className="py-4 px-6 font-bold text-xs text-on-surface-variant uppercase tracking-wider w-1/2">Nội dung</th>
                <th className="py-4 px-6 font-bold text-xs text-on-surface-variant uppercase tracking-wider text-right">Hành động</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-outline-variant/20">
              {loading ? (
                <tr>
                  <td colSpan="4" className="text-center py-12 text-on-surface-variant">Đang tải dữ liệu...</td>
                </tr>
              ) : filteredReviews.length === 0 ? (
                <tr>
                  <td colSpan="4" className="text-center py-12">
                    <div className="flex flex-col items-center justify-center text-on-surface-variant opacity-60">
                      <AlertCircle size={48} className="mb-4" />
                      <p>Không tìm thấy đánh giá nào.</p>
                    </div>
                  </td>
                </tr>
              ) : (
                filteredReviews.map((r) => (
                  <tr key={r.id} className="hover:bg-surface-container-lowest/50 transition-colors">
                    <td className="py-4 px-6">
                      <p className="font-bold text-sm text-on-surface">{r.userName}</p>
                      <Link to={`/product/${r.productId}?tab=reviews`} className="text-xs text-primary font-semibold mt-0.5 hover:underline block w-fit" title={r.productName}>
                        Sản phẩm: {r.productName?.length > 25 ? r.productName.substring(0, 25) + '...' : r.productName}
                      </Link>
                      <p className="text-xs text-on-surface-variant mt-1">
                        {new Date(r.createdAt).toLocaleString('vi-VN', {
                          day: '2-digit', month: '2-digit', year: 'numeric',
                          hour: '2-digit', minute: '2-digit'
                        })}
                      </p>
                    </td>
                    <td className="py-4 px-6">
                      <div className="flex items-center gap-1">
                        {[1, 2, 3, 4, 5].map((s) => (
                          <Star key={s} size={14} className={s <= r.rating ? 'fill-amber-500 text-amber-500' : 'text-on-surface-variant/30'} />
                        ))}
                      </div>
                    </td>
                    <td className="py-4 px-6">
                      <p className="text-sm text-on-surface line-clamp-2" title={r.comment}>{r.comment}</p>
                      {r.adminReply && (
                        <div className="mt-2 bg-primary/5 p-3 rounded-xl border border-primary/10 flex gap-2">
                          <CornerDownRight size={16} className="text-primary shrink-0 mt-0.5" />
                          <div>
                            <p className="text-xs font-bold text-primary">Phản hồi từ Admin:</p>
                            <p className="text-xs text-on-surface-variant mt-1">{r.adminReply}</p>
                          </div>
                        </div>
                      )}
                    </td>
                    <td className="py-4 px-6 text-right space-x-2 whitespace-nowrap">
                      <button
                        onClick={() => handleReply(r)}
                        className="p-2 text-blue-500 hover:bg-blue-500/10 rounded-xl transition-colors"
                        title="Trả lời đánh giá"
                      >
                        <Reply size={18} />
                      </button>
                      <button
                        onClick={() => handleDelete(r.id)}
                        className="p-2 text-rose-500 hover:bg-rose-500/10 rounded-xl transition-colors"
                        title="Xóa đánh giá"
                      >
                        <Trash2 size={18} />
                      </button>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}
