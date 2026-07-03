import Swal from "sweetalert2";
import React, { useEffect, useState } from 'react';
import api from '../../api/axios';
import {
  FolderTree, Plus, RefreshCw, AlertCircle,
  Tag, X, CheckCircle2, Search, Trash2
} from 'lucide-react';

function AddCategoryModal({ open, onClose, onSuccess }) {
  const [form, setForm] = useState({ name: '', description: '' });
  const [loading, setLoading] = useState(false);
  const [errors, setErrors] = useState({});

  useEffect(() => {
    if (open) { setForm({ name: '', description: '' }); setErrors({}); }
  }, [open]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!form.name.trim()) { setErrors({ name: 'Tên danh mục là bắt buộc' }); return; }
    setLoading(true);
    try {
      await api.post('/categories', { name: form.name.trim(), description: form.description.trim() });
      onSuccess();
      onClose();
    } catch (err) {
      Swal.fire({
        icon: "info",
        text: 'Lỗi: ' + (err.response?.data || err.message)
      });
    } finally {
      setLoading(false);
    }
  };

  if (!open) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-black/50 backdrop-blur-sm">
      <div className="bg-surface border border-outline-variant/30 rounded-3xl w-full max-w-md shadow-2xl">
        <div className="flex items-center justify-between p-6 border-b border-outline-variant/30">
          <h3 className="font-bold text-on-surface text-lg">Thêm danh mục mới</h3>
          <button onClick={onClose} className="text-on-surface-variant hover:text-on-surface transition-colors">
            <X size={20} />
          </button>
        </div>
        <form onSubmit={handleSubmit} className="p-6 space-y-4">
          <div>
            <label className="text-xs font-semibold text-on-surface-variant uppercase tracking-wider">Tên danh mục *</label>
            <input
              className={`w-full mt-1.5 bg-surface-container-lowest border ${errors.name ? 'border-rose-500' : 'border-outline-variant/30'} text-on-surface rounded-xl px-4 py-3 text-sm focus:outline-none focus:border-primary focus:ring-1 focus:ring-primary/30 transition-all`}
              placeholder="VD: Laptop & Máy Tính"
              value={form.name}
              onChange={e => setForm(p => ({ ...p, name: e.target.value }))}
            />
            {errors.name && <p className="text-rose-500 text-xs mt-1">{errors.name}</p>}
          </div>
          <div>
            <label className="text-xs font-semibold text-on-surface-variant uppercase tracking-wider">Mô tả</label>
            <textarea
              className="w-full mt-1.5 bg-surface-container-lowest border border-outline-variant/30 text-on-surface rounded-xl px-4 py-3 text-sm focus:outline-none focus:border-primary focus:ring-1 focus:ring-primary/30 transition-all resize-none"
              rows={2}
              placeholder="Mô tả danh mục..."
              value={form.description}
              onChange={e => setForm(p => ({ ...p, description: e.target.value }))}
            />
          </div>
          <div className="flex gap-3 pt-1">
            <button type="button" onClick={onClose} className="flex-1 py-3 border border-outline-variant/30 text-on-surface-variant rounded-xl text-sm font-semibold hover:bg-surface-container-low transition-all">
              Hủy
            </button>
            <button type="submit" disabled={loading} className="flex-1 py-3 bg-primary text-white rounded-xl text-sm font-semibold hover:bg-primary/90 transition-all active:scale-95 disabled:opacity-60 flex items-center justify-center gap-2">
              {loading ? <><div className="animate-spin rounded-full h-4 w-4 border-b-2 border-white" /> Đang lưu...</> : <><CheckCircle2 size={16} /> Thêm danh mục</>}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

export default function AdminCategories() {
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [modalOpen, setModalOpen] = useState(false);
  const [search, setSearch] = useState('');

  useEffect(() => {
    fetchCategories();
  }, []);

  const fetchCategories = async () => {
    setLoading(true);
    setError(null);
    try {
      const res = await api.get('/categories');
      setCategories(res.data || []);
    } catch (err) {
      setError('Không thể tải danh mục: ' + (err.response?.data || err.message));
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (category) => {
    if (!(await Swal.fire({
      title: "X�c nh?n",
      text: `Xoa danh muc "${category.name}"?`,
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "�?ng �",
      cancelButtonText: "H?y"
    })).isConfirmed) return;
    try {
      await api.delete(`/categories/${category.id}`);
      await fetchCategories();
    } catch (err) {
      Swal.fire({
        icon: "info",
        text: 'Loi xoa danh muc: ' + (err.response?.data?.message || err.message)
      });
    }
  };

  const filtered = categories.filter(c =>
    c.name?.toLowerCase().includes(search.toLowerCase())
  );

  return (
    <div className="space-y-6">
      <AddCategoryModal
        open={modalOpen}
        onClose={() => setModalOpen(false)}
        onSuccess={fetchCategories}
      />

      {/* Header */}
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <div>
          <h1 className="text-3xl font-bold text-on-surface tracking-tight">Quản Lý Danh Mục</h1>
          <p className="text-on-surface-variant mt-1">Thêm và quản lý phân loại sản phẩm của cửa hàng.</p>
        </div>
        <div className="flex gap-3">
          <button onClick={fetchCategories} className="p-2.5 bg-surface border border-outline-variant/30 text-on-surface-variant rounded-xl hover:bg-surface-container-low transition-all">
            <RefreshCw size={16} className={loading ? 'animate-spin' : ''} />
          </button>
          <button
            onClick={() => setModalOpen(true)}
            className="flex items-center gap-2 rounded-2xl bg-primary px-4 py-2.5 text-sm font-semibold text-white shadow-lg shadow-primary/20 hover:bg-primary/90 transition-all active:scale-95"
          >
            <Plus size={16} />Thêm danh mục
          </button>
        </div>
      </div>

      {/* Search */}
      <div className="relative max-w-sm">
        <Search size={14} className="absolute left-4 top-1/2 -translate-y-1/2 text-outline" />
        <input
          className="w-full bg-surface border border-outline-variant/30 text-on-surface text-sm rounded-xl pl-10 pr-4 py-2.5 focus:outline-none focus:border-primary transition-all placeholder:text-outline"
          placeholder="Tìm danh mục..."
          value={search}
          onChange={e => setSearch(e.target.value)}
        />
      </div>

      {error && (
        <div className="flex items-center gap-3 p-4 bg-rose-500/10 border border-rose-500/20 rounded-2xl text-rose-400">
          <AlertCircle size={18} /><span className="text-sm">{error}</span>
        </div>
      )}

      {/* Grid of categories */}
      {loading ? (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
          {[1, 2, 3, 4].map(i => (
            <div key={i} className="bg-surface border border-outline-variant/30 rounded-2xl p-5 animate-pulse space-y-3">
              <div className="h-8 w-8 bg-surface-container rounded-xl" />
              <div className="h-4 bg-surface-container rounded w-32" />
              <div className="h-3 bg-surface-container rounded w-24" />
            </div>
          ))}
        </div>
      ) : filtered.length === 0 ? (
        <div className="backdrop-blur-md bg-surface border border-outline-variant/30 p-12 rounded-3xl text-center space-y-4 shadow-sm">
          <div className="h-16 w-16 bg-primary/10 border border-primary/20 text-primary rounded-3xl flex items-center justify-center mx-auto">
            <FolderTree size={28} />
          </div>
          <h3 className="text-lg font-bold text-on-surface">{search ? 'Không tìm thấy danh mục' : 'Chưa có danh mục nào'}</h3>
          <p className="text-on-surface-variant text-sm">{search ? 'Thử từ khóa khác.' : 'Nhấn "Thêm danh mục" để tạo mới.'}</p>
        </div>
      ) : (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
          {filtered.map((cat, i) => (
            <div
              key={cat.id}
              className="backdrop-blur-md bg-surface border border-outline-variant/30 p-5 rounded-2xl hover:border-primary/20 hover:shadow-sm transition-all group shadow-sm"
            >
              <div className="flex items-start justify-between mb-3">
                <div className="w-10 h-10 bg-primary/10 border border-primary/20 text-primary rounded-xl flex items-center justify-center">
                  <Tag size={18} />
                </div>
                <button
                  onClick={() => handleDelete(cat)}
                  className="p-1.5 text-outline hover:text-rose-500 transition-colors opacity-0 group-hover:opacity-100"
                  title="Xoa"
                >
                  <Trash2 size={14} />
                </button>
              </div>
              <h3 className="font-bold text-on-surface text-sm">{cat.name}</h3>
              {cat.description && (
                <p className="text-xs text-on-surface-variant mt-1 line-clamp-2">{cat.description}</p>
              )}
              {cat.createdAt && (
                <p className="text-[10px] text-outline mt-3">
                  Tạo: {new Date(cat.createdAt).toLocaleDateString('vi-VN')}
                </p>
              )}
            </div>
          ))}
        </div>
      )}

      {!loading && filtered.length > 0 && (
        <p className="text-center text-xs text-outline">
          {filtered.length} danh mục
        </p>
      )}
    </div>
  );
}
