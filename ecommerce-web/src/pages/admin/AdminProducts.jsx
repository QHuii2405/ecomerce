import Swal from "sweetalert2";
import React, { useEffect, useState } from 'react';
import api from '../../api/axios';
import {
  Package, Plus, X, AlertCircle, RefreshCw,
  Search, Edit2, Trash2, CheckCircle2, Tag
} from 'lucide-react';

import ProductModal from './ProductModal';

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
    if (!(await Swal.fire({
      title: "X�c nh?n",
      text: `Xoa san pham "${product.name}"?`,
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "�?ng �",
      cancelButtonText: "H?y"
    })).isConfirmed) return;
    try {
      await api.delete(`/products/${product.id}`);
      await fetchAll();
    } catch (err) {
      Swal.fire({
        icon: "info",
        text: 'Loi xoa san pham: ' + (err.response?.data?.message || err.message)
      });
    }
  };

  const filteredProducts = products.filter(p =>
    p.name?.toLowerCase().includes(search.toLowerCase()) ||
    categoryNameById[p.categoryId]?.toLowerCase().includes(search.toLowerCase())
  );

  const getStockStatus = (product) => {
    const available = product.inventory?.availableQuantity ?? product.inventory?.stockQuantity ?? 0;
    if (available <= 0) return { label: 'Hết hàng', cls: 'text-rose-600 bg-rose-50 border-rose-200' };
    if (available < 5) return { label: `Sắp hết (${available})`, cls: 'text-amber-600 bg-amber-50 border-amber-200' };
    return { label: `Còn ${available}`, cls: 'text-emerald-600 bg-emerald-50 border-emerald-200' };
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
          <h1 className="text-3xl font-bold text-on-surface tracking-tight">Quản Lý Sản Phẩm</h1>
          <p className="text-on-surface-variant mt-1">Thêm, chỉnh sửa và quản lý sản phẩm trong kho.</p>
        </div>
        <div className="flex gap-3">
          <button onClick={fetchAll} className="p-2.5 bg-surface border border-outline-variant/30 text-on-surface-variant rounded-xl hover:bg-surface-container-low transition-all">
            <RefreshCw size={16} className={loading ? 'animate-spin' : ''} />
          </button>
          <button
            onClick={() => { setEditProduct(null); setModalOpen(true); }}
            className="flex items-center gap-2 rounded-2xl bg-primary px-4 py-2.5 text-sm font-semibold text-white shadow-lg shadow-primary/20 hover:bg-primary/90 transition-all active:scale-95"
          >
            <Plus size={16} />Thêm sản phẩm
          </button>
        </div>
      </div>

      {/* Search */}
      <div className="relative max-w-sm">
        <Search size={14} className="absolute left-4 top-1/2 -translate-y-1/2 text-outline" />
        <input
          className="w-full bg-surface border border-outline-variant/30 text-on-surface text-sm rounded-xl pl-10 pr-4 py-2.5 focus:outline-none focus:border-primary focus:ring-1 focus:ring-primary/30 transition-all placeholder:text-outline"
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
            <div key={i} className="backdrop-blur-md bg-surface border border-outline-variant/30 rounded-2xl p-4 animate-pulse">
              <div className="flex items-center gap-4">
                <div className="w-10 h-10 bg-surface-container rounded-xl" />
                <div className="flex-1 space-y-1.5">
                  <div className="h-3 bg-surface-container rounded w-48" />
                  <div className="h-2 bg-surface-container rounded w-32" />
                </div>
                <div className="h-4 bg-surface-container rounded w-20" />
              </div>
            </div>
          ))}
        </div>
      ) : filteredProducts.length === 0 ? (
        <div className="backdrop-blur-md bg-surface border border-outline-variant/30 p-12 rounded-3xl text-center space-y-4 shadow-sm">
          <div className="h-16 w-16 bg-primary/10 border border-primary/20 text-primary rounded-3xl flex items-center justify-center mx-auto">
            <Package size={28} />
          </div>
          <h3 className="text-lg font-bold text-on-surface">{search ? 'Không tìm thấy sản phẩm' : 'Chưa có sản phẩm nào'}</h3>
          <p className="text-on-surface-variant text-sm">{search ? 'Thử tìm với từ khóa khác.' : 'Bấm "Thêm sản phẩm" để bắt đầu.'}</p>
        </div>
      ) : (
        <div className="backdrop-blur-md bg-surface border border-outline-variant/30 rounded-2xl overflow-hidden shadow-sm">
          {/* Table Header */}
          <div className="grid grid-cols-12 gap-4 px-5 py-3 border-b border-outline-variant/30 text-[10px] font-bold uppercase tracking-widest text-outline bg-surface-container-lowest">
            <div className="col-span-5">Sản phẩm</div>
            <div className="col-span-2 text-right">Giá</div>
            <div className="col-span-2">Danh mục</div>
            <div className="col-span-2">Kho</div>
            <div className="col-span-1 text-center">Thao tác</div>
          </div>

          <div className="divide-y divide-outline-variant/30">
            {filteredProducts.map(product => {
              const stockStatus = getStockStatus(product);
              return (
                <div key={product.id} className="grid grid-cols-12 gap-4 items-center px-5 py-4 hover:bg-surface-container-low transition-colors">
                  <div className="col-span-5 flex items-center gap-3 min-w-0">
                    <div className="w-10 h-10 bg-surface-container-lowest rounded-xl flex items-center justify-center flex-shrink-0 border border-outline-variant/20 overflow-hidden">
                      {product.imageUrls && product.imageUrls.length > 0 ? (
                        <img src={`${(import.meta.env.VITE_API_BASE_URL || '')}${product.imageUrls[0]}`} className="w-full h-full object-cover" alt="" />
                      ) : (
                        <Package size={16} className="text-outline" />
                      )}
                    </div>
                    <div className="min-w-0">
                      <p className="text-sm font-semibold text-on-surface truncate">{product.name}</p>
                      <p className="text-[10px] text-on-surface-variant truncate">{product.description || 'Không có mô tả'}</p>
                    </div>
                  </div>
                  <div className="col-span-2 text-right">
                    <span className="text-sm font-bold text-primary">{Number(product.price).toLocaleString()}đ</span>
                  </div>
                  <div className="col-span-2">
                    <span className="flex items-center gap-1 text-xs text-on-surface-variant">
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
                      className="p-1.5 text-on-surface-variant hover:text-primary transition-colors"
                      title="Sửa"
                    >
                      <Edit2 size={14} />
                    </button>
                    <button
                      onClick={() => handleDelete(product)}
                      className="p-1.5 text-on-surface-variant hover:text-rose-500 transition-colors"
                      title="Xoa"
                    >
                      <Trash2 size={14} />
                    </button>
                  </div>
                </div>
              );
            })}
          </div>
          <div className="px-5 py-3 border-t border-outline-variant/30 text-center bg-surface-container-lowest">
            <span className="text-xs text-outline">Hiển thị {filteredProducts.length} / {products.length} sản phẩm</span>
          </div>
        </div>
      )}
    </div>
  );
}
