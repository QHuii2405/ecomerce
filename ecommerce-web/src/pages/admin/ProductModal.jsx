import Swal from "sweetalert2";
import React, { useEffect, useState } from 'react';
import api from '../../api/axios';
import { X, CheckCircle2, Plus, Trash2, Tag, Box, Layers } from 'lucide-react';

const INIT_FORM = {
  name: '',
  description: '',
  price: '',
  categoryId: '',
  brand: '',
  imageUrls: [],
  initialStock: 10,
  attributes: {},
  variants: []
};

const INIT_VARIANT = {
  sku: '',
  name: '',
  price: '',
  stockQuantity: 10,
  attributes: {}
};

export default function ProductModal({ open, onClose, onSuccess, categories, editProduct }) {
  const [form, setForm] = useState(INIT_FORM);
  const [loading, setLoading] = useState(false);
  const [errors, setErrors] = useState({});
  
  // Tab state: 'basic' or 'variants'
  const [activeTab, setActiveTab] = useState('basic');

  useEffect(() => {
    if (editProduct) {
      setForm({
        name: editProduct.name || '',
        description: editProduct.description || '',
        price: editProduct.price || '',
        categoryId: editProduct.categoryId || '',
        brand: editProduct.brand || '',
        imageUrls: editProduct.imageUrls || [],
        initialStock: editProduct.inventory?.stockQuantity ?? editProduct.inventory?.availableQuantity ?? 0,
        attributes: editProduct.attributes || {},
        variants: editProduct.variants || []
      });
    } else {
      setForm(INIT_FORM);
    }
    setErrors({});
    setActiveTab('basic');
  }, [open, editProduct]);

  const validate = () => {
    const e = {};
    if (!form.name.trim()) e.name = 'Tên sản phẩm là bắt buộc';
    if (!form.price || isNaN(form.price) || Number(form.price) <= 0) e.price = 'Giá phải lớn hơn 0';
    if (!form.categoryId) e.categoryId = 'Vui lòng chọn danh mục';
    if (!form.brand.trim()) e.brand = 'Thương hiệu là bắt buộc';
    setErrors(e);
    return Object.keys(e).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validate()) {
      setActiveTab('basic');
      return;
    }
    setLoading(true);
    try {
      const payload = {
        name: form.name.trim(),
        description: form.description.trim(),
        price: Number(form.price),
        categoryId: form.categoryId,
        brand: form.brand.trim(),
        imageUrls: form.imageUrls,
        initialStock: Number(form.initialStock) || 0,
        stockQuantity: Number(form.initialStock) || 0,
        attributes: form.attributes
      };

      if (editProduct) {
        await api.put(`/products/${editProduct.id}`, payload);
      } else {
        payload.variants = form.variants;
        await api.post('/products', payload);
      }
      onSuccess();
      onClose();
    } catch (err) {
      Swal.fire({
        icon: "info",
        text: 'Lỗi: ' + (err.response?.data?.message || err.response?.data || err.message)
      });
    } finally {
      setLoading(false);
    }
  };

  // --- File Upload ---
  const handleImageUpload = async (e) => {
    const files = Array.from(e.target.files);
    if (!files.length) return;
    
    // Check size < 5MB per file
    if (files.some(f => f.size > 5 * 1024 * 1024)) {
      Swal.fire({ icon: 'error', text: 'Kích thước mỗi file không được quá 5MB' });
      return;
    }

    const formData = new FormData();
    files.forEach(f => formData.append('files', f));

    setLoading(true);
    try {
      const res = await api.post('/Upload/multiple', formData, {
        headers: { 'Content-Type': 'multipart/form-data' }
      });
      if (res.data.urls) {
        setForm(p => ({ ...p, imageUrls: [...p.imageUrls, ...res.data.urls] }));
        Swal.fire({ icon: 'success', text: 'Tải ảnh thành công', timer: 1500, showConfirmButton: false });
      }
    } catch (err) {
      Swal.fire({ icon: 'error', text: 'Lỗi tải ảnh: ' + (err.response?.data || err.message) });
    } finally {
      setLoading(false);
    }
  };

  const handleRemoveImage = (index) => {
    setForm(p => {
      const newUrls = [...p.imageUrls];
      newUrls.splice(index, 1);
      return { ...p, imageUrls: newUrls };
    });
  };

  // --- Variant Handlers ---
  const handleAddVariant = () => {
    setForm(p => ({
      ...p,
      variants: [...p.variants, { ...INIT_VARIANT, id: 'temp_' + Date.now() }]
    }));
  };

  const handleUpdateVariant = (index, field, value) => {
    const newVariants = [...form.variants];
    newVariants[index] = { ...newVariants[index], [field]: value };
    setForm(p => ({ ...p, variants: newVariants }));
  };

  const handleRemoveVariant = async (index, variant) => {
    if (variant.id && !variant.id.toString().startsWith('temp_') && editProduct) {
      if (!(await Swal.fire({
        title: "X�c nh?n",
        text: 'Bạn có chắc muốn xóa biến thể này khỏi hệ thống?',
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "�?ng �",
        cancelButtonText: "H?y"
      })).isConfirmed) return;
      try {
        await api.delete(`/products/${editProduct.id}/variants/${variant.id}`);
        // Remove from UI after successful delete
        const newVariants = [...form.variants];
        newVariants.splice(index, 1);
        setForm(p => ({ ...p, variants: newVariants }));
        return;
      } catch (err) {
        Swal.fire({
          icon: "info",
          text: 'Lỗi xóa biến thể: ' + err.message
        });
        return;
      }
    }
    
    // Just remove from local state
    const newVariants = [...form.variants];
    newVariants.splice(index, 1);
    setForm(p => ({ ...p, variants: newVariants }));
  };

  const handleSaveVariant = async (variant) => {
    if (!editProduct) return; // Only used when editing product
    try {
      if (variant.id && !variant.id.toString().startsWith('temp_')) {
        await api.put(`/products/${editProduct.id}/variants/${variant.id}`, {
          sku: variant.sku,
          name: variant.name,
          price: Number(variant.price),
          stockQuantity: Number(variant.stockQuantity),
          attributes: variant.attributes
        });
        Swal.fire({
          icon: "info",
          text: 'Cập nhật biến thể thành công!'
        });
      } else {
        await api.post(`/products/${editProduct.id}/variants`, {
          sku: variant.sku,
          name: variant.name,
          price: Number(variant.price),
          stockQuantity: Number(variant.stockQuantity),
          attributes: variant.attributes
        });
        Swal.fire({
          icon: "info",
          text: 'Thêm biến thể thành công! Vui lòng tải lại trang.'
        });
      }
    } catch (err) {
      Swal.fire({
        icon: "info",
        text: 'Lỗi lưu biến thể: ' + (err.response?.data?.message || err.message)
      });
    }
  };

  if (!open) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-black/50 backdrop-blur-sm">
      <div className="bg-surface border border-outline-variant/30 rounded-3xl w-full max-w-4xl shadow-2xl flex flex-col max-h-[90vh]">
        {/* Header */}
        <div className="flex items-center justify-between p-6 border-b border-outline-variant/30 flex-shrink-0">
          <h3 className="font-bold text-on-surface text-xl flex items-center gap-2">
            <Box className="text-primary" />
            {editProduct ? 'Sửa sản phẩm' : 'Thêm sản phẩm mới'}
          </h3>
          <button onClick={onClose} className="text-on-surface-variant hover:text-on-surface transition-colors">
            <X size={24} />
          </button>
        </div>

        {/* Tabs */}
        <div className="flex border-b border-outline-variant/30 px-6 pt-2 flex-shrink-0 gap-6">
          <button
            onClick={() => setActiveTab('basic')}
            className={`pb-3 font-semibold text-sm transition-colors border-b-2 ${
              activeTab === 'basic' ? 'border-primary text-primary' : 'border-transparent text-on-surface-variant hover:text-on-surface'
            }`}
          >
            Thông tin cơ bản
          </button>
          <button
            onClick={() => setActiveTab('variants')}
            className={`pb-3 font-semibold text-sm transition-colors border-b-2 flex items-center gap-2 ${
              activeTab === 'variants' ? 'border-primary text-primary' : 'border-transparent text-on-surface-variant hover:text-on-surface'
            }`}
          >
            Biến thể (Variants)
            <span className="bg-surface-container text-on-surface text-[10px] px-2 py-0.5 rounded-full">{form.variants.length}</span>
          </button>
          <button
            onClick={() => setActiveTab('specs')}
            className={`pb-3 font-semibold text-sm transition-colors border-b-2 flex items-center gap-2 ${
              activeTab === 'specs' ? 'border-primary text-primary' : 'border-transparent text-on-surface-variant hover:text-on-surface'
            }`}
          >
            Thông số kỹ thuật
            <span className="bg-surface-container text-on-surface text-[10px] px-2 py-0.5 rounded-full">{Object.keys(form.attributes).length}</span>
          </button>
        </div>

        {/* Body */}
        <div className="p-6 overflow-y-auto flex-1 custom-scrollbar">
          {activeTab === 'basic' && (
            <div className="space-y-5">
              <div className="grid grid-cols-2 gap-5">
                <div>
                  <label className="text-xs font-semibold text-on-surface-variant uppercase tracking-wider mb-1.5 block">Tên sản phẩm *</label>
                  <input
                    className={`w-full bg-surface-container-lowest border ${errors.name ? 'border-rose-500' : 'border-outline-variant/30'} text-on-surface rounded-xl px-4 py-3 text-sm focus:outline-none focus:border-primary focus:ring-1 focus:ring-primary/30 transition-all`}
                    placeholder="VD: Laptop Pro X1 2024"
                    value={form.name}
                    onChange={e => setForm(p => ({ ...p, name: e.target.value }))}
                  />
                  {errors.name && <p className="text-rose-500 text-xs mt-1">{errors.name}</p>}
                </div>
                <div>
                  <label className="text-xs font-semibold text-on-surface-variant uppercase tracking-wider mb-1.5 block">Thương hiệu *</label>
                  <input
                    className={`w-full bg-surface-container-lowest border ${errors.brand ? 'border-rose-500' : 'border-outline-variant/30'} text-on-surface rounded-xl px-4 py-3 text-sm focus:outline-none focus:border-primary transition-all`}
                    placeholder="VD: Apple, Samsung..."
                    value={form.brand}
                    onChange={e => setForm(p => ({ ...p, brand: e.target.value }))}
                  />
                  {errors.brand && <p className="text-rose-500 text-xs mt-1">{errors.brand}</p>}
                </div>
              </div>

              <div>
                <label className="text-xs font-semibold text-on-surface-variant uppercase tracking-wider mb-1.5 block">Hình ảnh sản phẩm</label>
                <div className="flex flex-wrap items-center gap-4">
                  {form.imageUrls.map((url, idx) => (
                    <div key={idx} className="relative w-24 h-24 rounded-xl border border-outline-variant/30 overflow-hidden group">
                      <img src={`${(import.meta.env.VITE_API_BASE_URL || '')}${url}`} alt={`Product ${idx}`} className="w-full h-full object-cover" />
                      <button 
                        type="button" 
                        className="absolute inset-0 bg-black/50 hidden group-hover:flex items-center justify-center text-white"
                        onClick={() => handleRemoveImage(idx)}
                      >
                        <Trash2 size={20} />
                      </button>
                    </div>
                  ))}
                  
                  <label className="w-24 h-24 rounded-xl border-2 border-dashed border-outline-variant/50 flex flex-col items-center justify-center cursor-pointer hover:border-primary hover:text-primary transition-colors text-on-surface-variant">
                    <Plus size={24} className="mb-1" />
                    <span className="text-[10px] font-medium">Thêm ảnh</span>
                    <input type="file" className="hidden" accept="image/*" multiple onChange={handleImageUpload} />
                  </label>
                </div>
              </div>

              <div>
                <label className="text-xs font-semibold text-on-surface-variant uppercase tracking-wider mb-1.5 block">Mô tả</label>
                <textarea
                  className="w-full bg-surface-container-lowest border border-outline-variant/30 text-on-surface rounded-xl px-4 py-3 text-sm focus:outline-none focus:border-primary transition-all resize-none"
                  rows={3}
                  placeholder="Mô tả ngắn về sản phẩm..."
                  value={form.description}
                  onChange={e => setForm(p => ({ ...p, description: e.target.value }))}
                />
              </div>

              <div className="grid grid-cols-3 gap-5">
                <div>
                  <label className="text-xs font-semibold text-on-surface-variant uppercase tracking-wider mb-1.5 block">Danh mục *</label>
                  <select
                    className={`w-full bg-surface-container-lowest border ${errors.categoryId ? 'border-rose-500' : 'border-outline-variant/30'} text-on-surface rounded-xl px-4 py-3 text-sm focus:outline-none focus:border-primary transition-all`}
                    value={form.categoryId}
                    onChange={e => setForm(p => ({ ...p, categoryId: e.target.value }))}
                  >
                    <option value="">-- Chọn danh mục --</option>
                    {categories.map(cat => (
                      <option key={cat.id} value={cat.id}>{cat.name}</option>
                    ))}
                  </select>
                  {errors.categoryId && <p className="text-rose-500 text-xs mt-1">{errors.categoryId}</p>}
                </div>
                <div>
                  <label className="text-xs font-semibold text-on-surface-variant uppercase tracking-wider mb-1.5 block">Giá chung (VNĐ) *</label>
                  <input
                    type="number"
                    className={`w-full bg-surface-container-lowest border ${errors.price ? 'border-rose-500' : 'border-outline-variant/30'} text-on-surface rounded-xl px-4 py-3 text-sm focus:outline-none focus:border-primary transition-all`}
                    placeholder="VD: 15000000"
                    value={form.price}
                    onChange={e => setForm(p => ({ ...p, price: e.target.value }))}
                  />
                  {errors.price && <p className="text-rose-500 text-xs mt-1">{errors.price}</p>}
                </div>
                <div>
                  <label className="text-xs font-semibold text-on-surface-variant uppercase tracking-wider mb-1.5 block">Tồn kho ban đầu</label>
                  <input
                    type="number"
                    min="0"
                    className="w-full bg-surface-container-lowest border border-outline-variant/30 text-on-surface rounded-xl px-4 py-3 text-sm focus:outline-none focus:border-primary transition-all"
                    value={form.initialStock}
                    onChange={e => setForm(p => ({ ...p, initialStock: e.target.value }))}
                  />
                </div>
              </div>
            </div>
          )}

          {activeTab === 'specs' && (
            <div className="space-y-4">
              <div className="flex justify-between items-center bg-surface-container-low p-4 rounded-2xl border border-outline-variant/30">
                <div>
                  <h4 className="font-bold text-sm text-on-surface">Thông số kỹ thuật</h4>
                  <p className="text-xs text-on-surface-variant mt-0.5">Các thông tin cấu hình, kích thước...</p>
                </div>
                <button
                  type="button"
                  onClick={() => {
                    const k = prompt('Nhập tên thông số (VD: Màn hình):');
                    if (k && k.trim()) {
                      if (form.attributes[k.trim()]) {
                        alert('Thông số này đã tồn tại!');
                        return;
                      }
                      setForm(p => ({ ...p, attributes: { ...p.attributes, [k.trim()]: '' } }));
                    }
                  }}
                  className="px-4 py-2 bg-primary/10 text-primary font-semibold text-sm rounded-xl hover:bg-primary/20 transition-colors flex items-center gap-1.5"
                >
                  <Plus size={16} /> Thêm thông số
                </button>
              </div>

              {Object.keys(form.attributes).length === 0 ? (
                <div className="text-center py-10 border border-dashed border-outline-variant/40 rounded-2xl text-on-surface-variant">
                  <Tag size={32} className="mx-auto mb-3 opacity-20" />
                  <p className="text-sm">Chưa có thông số nào</p>
                </div>
              ) : (
                <div className="grid grid-cols-2 gap-4">
                  {Object.entries(form.attributes).map(([key, val]) => (
                    <div key={key} className="bg-surface-container-lowest border border-outline-variant/30 p-3 rounded-xl flex items-center gap-3">
                      <div className="flex-1">
                        <label className="text-[10px] font-bold text-on-surface-variant uppercase tracking-wider mb-1 block">{key}</label>
                        <input
                          className="w-full bg-transparent border-b border-outline-variant/30 text-on-surface text-sm focus:outline-none focus:border-primary py-1"
                          placeholder="Nhập giá trị..."
                          value={val}
                          onChange={e => setForm(p => ({
                            ...p,
                            attributes: { ...p.attributes, [key]: e.target.value }
                          }))}
                        />
                      </div>
                      <button
                        type="button"
                        onClick={() => {
                          const newAttrs = { ...form.attributes };
                          delete newAttrs[key];
                          setForm(p => ({ ...p, attributes: newAttrs }));
                        }}
                        className="w-8 h-8 flex items-center justify-center rounded-full text-rose-500 hover:bg-rose-500/10 transition-colors"
                      >
                        <Trash2 size={14} />
                      </button>
                    </div>
                  ))}
                </div>
              )}
            </div>
          )}

          {activeTab === 'variants' && (
            <div className="space-y-6">
              <div className="flex justify-between items-center">
                <p className="text-sm text-on-surface-variant">
                  {editProduct 
                    ? 'Bạn có thể Thêm/Sửa/Xóa biến thể trực tiếp tại đây.' 
                    : 'Thêm các biến thể cho sản phẩm mới (Màu sắc, Dung lượng, ...)'}
                </p>
                <button
                  type="button"
                  onClick={handleAddVariant}
                  className="flex items-center gap-1.5 px-3 py-2 bg-primary/10 text-primary rounded-lg text-sm font-semibold hover:bg-primary/20 transition-all"
                >
                  <Plus size={16} /> Thêm biến thể
                </button>
              </div>

              {form.variants.length === 0 ? (
                <div className="border border-dashed border-outline-variant/30 rounded-2xl p-10 text-center flex flex-col items-center justify-center">
                  <Layers className="text-outline mb-3" size={32} />
                  <p className="text-on-surface-variant text-sm">Chưa có biến thể nào.</p>
                </div>
              ) : (
                <div className="space-y-4">
                  {form.variants.map((v, idx) => (
                    <div key={v.id || idx} className="bg-surface-container-low border border-outline-variant/30 rounded-2xl p-5 relative group">
                      <div className="grid grid-cols-2 md:grid-cols-4 gap-4 mb-4">
                        <div>
                          <label className="text-[10px] font-bold text-outline uppercase">Tên biến thể</label>
                          <input 
                            value={v.name} onChange={e => handleUpdateVariant(idx, 'name', e.target.value)}
                            className="w-full bg-transparent border-b border-outline-variant/30 text-on-surface px-1 py-2 text-sm focus:outline-none focus:border-primary transition-all"
                            placeholder="VD: 16GB - Đen"
                          />
                        </div>
                        <div>
                          <label className="text-[10px] font-bold text-outline uppercase">SKU</label>
                          <input 
                            value={v.sku} onChange={e => handleUpdateVariant(idx, 'sku', e.target.value)}
                            className="w-full bg-transparent border-b border-outline-variant/30 text-on-surface px-1 py-2 text-sm focus:outline-none focus:border-primary transition-all"
                            placeholder="VD: SP-16-BLK"
                          />
                        </div>
                        <div>
                          <label className="text-[10px] font-bold text-outline uppercase">Giá (VNĐ)</label>
                          <input 
                            type="number" value={v.price} onChange={e => handleUpdateVariant(idx, 'price', e.target.value)}
                            className="w-full bg-transparent border-b border-outline-variant/30 text-primary font-semibold px-1 py-2 text-sm focus:outline-none focus:border-primary transition-all"
                            placeholder="0"
                          />
                        </div>
                        <div>
                          <label className="text-[10px] font-bold text-outline uppercase">Kho</label>
                          <input 
                            type="number" value={v.stockQuantity} onChange={e => handleUpdateVariant(idx, 'stockQuantity', e.target.value)}
                            className="w-full bg-transparent border-b border-outline-variant/30 text-on-surface px-1 py-2 text-sm focus:outline-none focus:border-primary transition-all"
                            placeholder="10"
                          />
                        </div>
                      </div>

                      <div className="flex gap-2">
                        <input
                          value={v.attributes?.ram || ''}
                          onChange={e => handleUpdateVariant(idx, 'attributes', { ...v.attributes, ram: e.target.value })}
                          className="flex-1 bg-surface-container-lowest border border-outline-variant/30 rounded-lg px-3 py-2 text-xs text-on-surface"
                          placeholder="RAM (VD: 16GB)"
                        />
                        <input
                          value={v.attributes?.storage || ''}
                          onChange={e => handleUpdateVariant(idx, 'attributes', { ...v.attributes, storage: e.target.value })}
                          className="flex-1 bg-surface-container-lowest border border-outline-variant/30 rounded-lg px-3 py-2 text-xs text-on-surface"
                          placeholder="Ổ cứng (VD: 512GB)"
                        />
                        <input
                          value={v.attributes?.color || ''}
                          onChange={e => handleUpdateVariant(idx, 'attributes', { ...v.attributes, color: e.target.value })}
                          className="flex-1 bg-surface-container-lowest border border-outline-variant/30 rounded-lg px-3 py-2 text-xs text-on-surface"
                          placeholder="Màu (VD: Đen)"
                        />
                      </div>

                      {/* Action buttons for edit mode */}
                      <div className="absolute top-4 right-4 flex gap-2">
                        {editProduct && (
                          <button
                            type="button"
                            onClick={() => handleSaveVariant(v)}
                            className="text-xs bg-emerald-50 text-emerald-600 px-2 py-1 rounded border border-emerald-200 hover:bg-emerald-100 transition-all"
                          >
                            Lưu
                          </button>
                        )}
                        <button
                          type="button"
                          onClick={() => handleRemoveVariant(idx, v)}
                          className="text-on-surface-variant hover:text-rose-500 transition-all p-1"
                        >
                          <Trash2 size={16} />
                        </button>
                      </div>
                    </div>
                  ))}
                </div>
              )}
            </div>
          )}
        </div>

        {/* Footer */}
        <div className="p-6 border-t border-outline-variant/30 flex gap-3 flex-shrink-0 bg-surface rounded-b-3xl">
          <button
            type="button"
            onClick={onClose}
            className="flex-1 py-3 border border-outline-variant/30 text-on-surface-variant rounded-xl text-sm font-semibold hover:bg-surface-container-low transition-all"
          >
            Hủy
          </button>
          <button
            type="button"
            onClick={handleSubmit}
            disabled={loading}
            className="flex-1 py-3 bg-primary text-white rounded-xl text-sm font-semibold hover:bg-primary/90 transition-all active:scale-95 disabled:opacity-60 flex items-center justify-center gap-2"
          >
            {loading ? <><div className="animate-spin rounded-full h-4 w-4 border-b-2 border-white" /> Đang lưu...</> : <><CheckCircle2 size={16} /> Lưu Sản Phẩm</>}
          </button>
        </div>
      </div>
    </div>
  );
}
