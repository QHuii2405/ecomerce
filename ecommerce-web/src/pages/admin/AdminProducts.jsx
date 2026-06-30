import React, { useEffect, useState } from 'react';
import api from '../../api/axios';
import {
  Package, Plus, X, AlertCircle, RefreshCw,
  Search, Edit2, Trash2, CheckCircle2, Tag
} from 'lucide-react';

const INIT_FORM = {
  name: '', description: '', price: '', categoryId: '',
  initialStock: 10
};

function ProductModal({ open, onClose, onSuccess, categories, editProduct }) {
  const [form, setForm] = useState(INIT_FORM);
  const [loading, setLoading] = useState(false);
  const [errors, setErrors] = useState({});

  useEffect(() => {
    if (editProduct) {
      setForm({
        name: editProduct.name || '',
        description: editProduct.description || '',
        price: editProduct.price || '',
        categoryId: editProduct.categoryId || '',
        initialStock: editProduct.inventory?.stockQuantity ?? editProduct.inventory?.availableQuantity ?? 0
      });
    } else {
      setForm(INIT_FORM);
    }
    setErrors({});
  }, [open, editProduct]);

  const validate = () => {
    const e = {};
    if (!form.name.trim()) e.name = 'Tên sản phẩm là bắt buộc';
    if (!form.price || isNaN(form.price) || Number(form.price) <= 0) e.price = 'Giá phải lớn hơn 0';
    if (!form.categoryId) e.categoryId = 'Vui lòng chọn danh mục';
    setErrors(e);
    return Object.keys(e).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validate()) return;
    setLoading(true);
    try {
      const payload = {
        name: form.name.trim(),
        description: form.description.trim(),
        price: Number(form.price),
        categoryId: form.categoryId,
        initialStock: Number(form.initialStock) || 0,
        stockQuantity: Number(form.initialStock) || 0
      };
      if (editProduct) {
        await api.put(`/products/${editProduct.id}`, payload);
      } else {
        await api.post('/products', payload);
      }
      onSuccess();
      onClose();
    } catch (err) {
      alert('Lỗi: ' + (err.response?.data || err.message));
    } finally {
      setLoading(false);
    }
  };

  if (!open) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-black/70 backdrop-blur-sm">
      <div className="bg-slate-900 border border-white/10 rounded-3xl w-full max-w-lg shadow-2xl">
        <div className="flex items-center justify-between p-6 border-b border-white/5">
          <h3 className="font-bold text-white text-lg">{editProduct ? 'Sửa sản phẩm' : 'Thêm sản phẩm mới'}</h3>
          <button onClick={onClose} className="text-slate-400 hover:text-white transition-colors">
            <X size={20} />
          </button>
        </div>
        <form onSubmit={handleSubmit} className="p-6 space-y-4">
          <div>
            <label className="text-xs font-semibold text-slate-400 uppercase tracking-wider">Tên sản phẩm *</label>
            <input
              className={`w-full mt-1.5 bg-white/5 border ${errors.name ? 'border-rose-500' : 'border-white/10'} text-white rounded-xl px-4 py-3 text-sm focus:outline-none focus:border-indigo-500 focus:ring-1 focus:ring-indigo-500/30 transition-all`}
              placeholder="VD: Laptop Pro X1 2024"
              value={form.name}
              onChange={e => setForm(p => ({ ...p, name: e.target.value }))}
            />
            {errors.name && <p className="text-rose-400 text-xs mt-1">{errors.name}</p>}
          </div>

          <div>
            <label className="text-xs font-semibold text-slate-400 uppercase tracking-wider">Mô tả</label>
            <textarea
              className="w-full mt-1.5 bg-white/5 border border-white/10 text-white rounded-xl px-4 py-3 text-sm focus:outline-none focus:border-indigo-500 focus:ring-1 focus:ring-indigo-500/30 transition-all resize-none"
              rows={3}
              placeholder="Mô tả ngắn về sản phẩm..."
              value={form.description}
              onChange={e => setForm(p => ({ ...p, description: e.target.value }))}
            />
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="text-xs font-semibold text-slate-400 uppercase tracking-wider">Giá (VNĐ) *</label>
              <input
                type="number"
                className={`w-full mt-1.5 bg-white/5 border ${errors.price ? 'border-rose-500' : 'border-white/10'} text-white rounded-xl px-4 py-3 text-sm focus:outline-none focus:border-indigo-500 focus:ring-1 focus:ring-indigo-500/30 transition-all`}
                placeholder="VD: 15000000"
                value={form.price}
                onChange={e => setForm(p => ({ ...p, price: e.target.value }))}
              />
              {errors.price && <p className="text-rose-400 text-xs mt-1">{errors.price}</p>}
            </div>
            <div>
              <label className="text-xs font-semibold text-slate-400 uppercase tracking-wider">Số lượng kho</label>
              <input
                type="number"
                min="0"
                className="w-full mt-1.5 bg-white/5 border border-white/10 text-white rounded-xl px-4 py-3 text-sm focus:outline-none focus:border-indigo-500 focus:ring-1 focus:ring-indigo-500/30 transition-all"
                value={form.initialStock}
                onChange={e => setForm(p => ({ ...p, initialStock: e.target.value }))}
              />
            </div>
          </div>

          <div>
            <label className="text-xs font-semibold text-slate-400 uppercase tracking-wider">Danh mục *</label>
            <select
              className={`w-full mt-1.5 bg-slate-800 border ${errors.categoryId ? 'border-rose-500' : 'border-white/10'} text-white rounded-xl px-4 py-3 text-sm focus:outline-none focus:border-indigo-500 transition-all`}
              value={form.categoryId}
              onChange={e => setForm(p => ({ ...p, categoryId: e.target.value }))}
            >
              <option value="">-- Chọn danh mục --</option>
              {categories.map(cat => (
                <option key={cat.id} value={cat.id}>{cat.name}</option>
              ))}
            </select>
            {errors.categoryId && <p className="text-rose-400 text-xs mt-1">{errors.categoryId}</p>}
          </div>

          <div className="flex gap-3 pt-2">
            <button
              type="button"
              onClick={onClose}
              className="flex-1 py-3 border border-white/10 text-slate-400 rounded-xl text-sm font-semibold hover:bg-white/5 transition-all"
            >
              Hủy
            </button>
            <button
              type="submit"
              disabled={loading}
              className="flex-1 py-3 bg-indigo-600 text-white rounded-xl text-sm font-semibold hover:bg-indigo-500 transition-all active:scale-95 disabled:opacity-60 flex items-center justify-center gap-2"
            >
              {loading ? <><div className="animate-spin rounded-full h-4 w-4 border-b-2 border-white" /> Đang lưu...</> : <><CheckCircle2 size={16} /> {editProduct ? 'Cập nhật' : 'Thêm mới'}</>}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

export default function AdminProducts() {
  const [products, setProducts] = useState([]);
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [modalOpen, setModalOpen] = useState(false);
  const [editProduct, setEditProduct] = useState(null);
  const [search, setSearch] = useState('');

  useEffect(() => {
    fetchAll();
  }, []);

  const fetchAll = async () => {
    setLoading(true);
    setError(null);
    try {
      const [prodRes, catRes] = await Promise.all([
        api.get('/products'),
        api.get('/categories')
      ]);
      setProducts(prodRes.data.data || []);
      setCategories(catRes.data || []);
    } catch (err) {
      setError('Không thể tải dữ liệu: ' + (err.response?.data || err.message));
    } finally {
      setLoading(false);
    }
  };

  const categoryNameById = Object.fromEntries(categories.map(c => [c.id, c.name]));

  const handleDelete = async (product) => {
    if (!window.confirm(`Xoa san pham "${product.name}"?`)) return;
    try {
      await api.delete(`/products/${product.id}`);
      await fetchAll();
    } catch (err) {
      alert('Loi xoa san pham: ' + (err.response?.data?.message || err.message));
    }
  };

  const filteredProducts = products.filter(p =>
    p.name?.toLowerCase().includes(search.toLowerCase()) ||
    categoryNameById[p.categoryId]?.toLowerCase().includes(search.toLowerCase())
  );

  const getStockStatus = (product) => {
    const available = product.inventory?.availableQuantity ?? product.inventory?.stockQuantity ?? 0;
    if (available <= 0) return { label: 'Hết hàng', cls: 'text-rose-400 bg-rose-500/10 border-rose-500/20' };
    if (available < 5) return { label: `Sắp hết (${available})`, cls: 'text-amber-400 bg-amber-500/10 border-amber-500/20' };
    return { label: `Còn ${available}`, cls: 'text-emerald-400 bg-emerald-500/10 border-emerald-500/20' };
  };

  return (
    <div className="space-y-6">
      <ProductModal
        open={modalOpen}
        onClose={() => { setModalOpen(false); setEditProduct(null); }}
        onSuccess={fetchAll}
        categories={categories}
        editProduct={editProduct}
      />

      {/* Header */}
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <div>
          <h1 className="text-3xl font-bold text-white tracking-tight">Quản Lý Sản Phẩm</h1>
          <p className="text-slate-400 mt-1">Thêm, chỉnh sửa và quản lý sản phẩm trong kho.</p>
        </div>
        <div className="flex gap-3">
          <button onClick={fetchAll} className="p-2.5 bg-white/5 border border-white/5 text-slate-400 rounded-xl hover:bg-white/10 transition-all">
            <RefreshCw size={16} className={loading ? 'animate-spin' : ''} />
          </button>
          <button
            onClick={() => { setEditProduct(null); setModalOpen(true); }}
            className="flex items-center gap-2 rounded-2xl bg-indigo-600 px-4 py-2.5 text-sm font-semibold text-white shadow-lg shadow-indigo-600/20 hover:bg-indigo-500 transition-all active:scale-95"
          >
            <Plus size={16} />Thêm sản phẩm
          </button>
        </div>
      </div>

      {/* Search */}
      <div className="relative max-w-sm">
        <Search size={14} className="absolute left-4 top-1/2 -translate-y-1/2 text-slate-500" />
        <input
          className="w-full bg-white/5 border border-white/5 text-white text-sm rounded-xl pl-10 pr-4 py-2.5 focus:outline-none focus:border-indigo-500 focus:ring-1 focus:ring-indigo-500/30 transition-all placeholder:text-slate-600"
          placeholder="Tìm sản phẩm..."
          value={search}
          onChange={e => setSearch(e.target.value)}
        />
      </div>

      {/* Error */}
      {error && (
        <div className="flex items-center gap-3 p-4 bg-rose-500/10 border border-rose-500/20 rounded-2xl text-rose-400">
          <AlertCircle size={18} />
          <span className="text-sm">{error}</span>
        </div>
      )}

      {/* Products Table */}
      {loading ? (
        <div className="space-y-3">
          {[1, 2, 3, 5].map(i => (
            <div key={i} className="backdrop-blur-md bg-white/5 border border-white/5 rounded-2xl p-4 animate-pulse">
              <div className="flex items-center gap-4">
                <div className="w-10 h-10 bg-white/10 rounded-xl" />
                <div className="flex-1 space-y-1.5">
                  <div className="h-3 bg-white/10 rounded w-48" />
                  <div className="h-2 bg-white/10 rounded w-32" />
                </div>
                <div className="h-4 bg-white/10 rounded w-20" />
              </div>
            </div>
          ))}
        </div>
      ) : filteredProducts.length === 0 ? (
        <div className="backdrop-blur-md bg-white/5 border border-white/5 p-12 rounded-3xl text-center space-y-4">
          <div className="h-16 w-16 bg-indigo-500/10 border border-indigo-500/10 text-indigo-400 rounded-3xl flex items-center justify-center mx-auto">
            <Package size={28} />
          </div>
          <h3 className="text-lg font-bold text-white">{search ? 'Không tìm thấy sản phẩm' : 'Chưa có sản phẩm nào'}</h3>
          <p className="text-slate-400 text-sm">{search ? 'Thử tìm với từ khóa khác.' : 'Bấm "Thêm sản phẩm" để bắt đầu.'}</p>
        </div>
      ) : (
        <div className="backdrop-blur-md bg-white/5 border border-white/5 rounded-2xl overflow-hidden">
          {/* Table Header */}
          <div className="grid grid-cols-12 gap-4 px-5 py-3 border-b border-white/5 text-[10px] font-bold uppercase tracking-widest text-slate-500">
            <div className="col-span-5">Sản phẩm</div>
            <div className="col-span-2 text-right">Giá</div>
            <div className="col-span-2">Danh mục</div>
            <div className="col-span-2">Kho</div>
            <div className="col-span-1 text-center">Thao tác</div>
          </div>

          <div className="divide-y divide-white/5">
            {filteredProducts.map(product => {
              const stockStatus = getStockStatus(product);
              return (
                <div key={product.id} className="grid grid-cols-12 gap-4 items-center px-5 py-4 hover:bg-white/5 transition-colors">
                  <div className="col-span-5 flex items-center gap-3 min-w-0">
                    <div className="w-10 h-10 bg-white/5 rounded-xl flex items-center justify-center flex-shrink-0">
                      <Package size={16} className="text-slate-400" />
                    </div>
                    <div className="min-w-0">
                      <p className="text-sm font-semibold text-white truncate">{product.name}</p>
                      <p className="text-[10px] text-slate-500 truncate">{product.description || 'Không có mô tả'}</p>
                    </div>
                  </div>
                  <div className="col-span-2 text-right">
                    <span className="text-sm font-bold text-indigo-400">{Number(product.price).toLocaleString()}đ</span>
                  </div>
                  <div className="col-span-2">
                    <span className="flex items-center gap-1 text-xs text-slate-400">
                      <Tag size={10} />{categoryNameById[product.categoryId] || '?'}
                    </span>
                  </div>
                  <div className="col-span-2">
                    <span className={`text-[10px] font-bold px-2 py-1 rounded-full border ${stockStatus.cls}`}>
                      {stockStatus.label}
                    </span>
                  </div>
                  <div className="col-span-1 flex items-center justify-center gap-2">
                    <button
                      onClick={() => { setEditProduct(product); setModalOpen(true); }}
                      className="p-1.5 text-slate-400 hover:text-indigo-400 transition-colors"
                      title="Sửa"
                    >
                      <Edit2 size={14} />
                    </button>
                    <button
                      onClick={() => handleDelete(product)}
                      className="p-1.5 text-slate-400 hover:text-rose-400 transition-colors"
                      title="Xoa"
                    >
                      <Trash2 size={14} />
                    </button>
                  </div>
                </div>
              );
            })}
          </div>
          <div className="px-5 py-3 border-t border-white/5 text-center">
            <span className="text-xs text-slate-500">Hiển thị {filteredProducts.length} / {products.length} sản phẩm</span>
          </div>
        </div>
      )}
    </div>
  );
}
