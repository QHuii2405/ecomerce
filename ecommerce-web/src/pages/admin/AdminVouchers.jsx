import Swal from "sweetalert2";
import React, { useState, useEffect } from 'react';
import api from '../../api/axios';
import { Tag, Plus, CheckCircle2, XCircle, Search, RefreshCw, AlertCircle, Percent } from 'lucide-react';

export default function AdminVouchers() {
  const [vouchers, setVouchers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState('');
  const [showModal, setShowModal] = useState(false);
  const [form, setForm] = useState({
    code: '',
    discountType: 'Percentage',
    discountValue: 10,
    maxDiscountValue: 50000,
    minOrderValue: 100000,
    expiryDate: new Date(Date.now() + 7 * 24 * 60 * 60 * 1000).toISOString().slice(0, 16),
    usageLimit: 100
  });
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    fetchVouchers();
  }, []);

  const fetchVouchers = async () => {
    setLoading(true);
    try {
      const res = await api.get('/vouchers');
      setVouchers(res.data.data || []);
    } catch (err) {
      console.error(err);
      Swal.fire({
        icon: "info",
        text: 'Không thể tải danh sách khuyến mãi.'
      });
    } finally {
      setLoading(false);
    }
  };

  const handleToggle = async (id) => {
    try {
      await api.put(`/vouchers/${id}/toggle`);
      setVouchers(prev => prev.map(v => v.id === id ? { ...v, isActive: !v.isActive } : v));
    } catch (err) {
      Swal.fire({
        icon: "info",
        text: 'Không thể thay đổi trạng thái mã giảm giá.'
      });
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setSubmitting(true);
    try {
      const res = await api.post('/vouchers', {
        code: form.code,
        discountType: form.discountType,
        discountValue: Number(form.discountValue),
        maxDiscountValue: Number(form.maxDiscountValue),
        minOrderValue: Number(form.minOrderValue),
        expiryDate: new Date(form.expiryDate).toISOString(),
        usageLimit: Number(form.usageLimit)
      });
      setVouchers([...vouchers, res.data.data]);
      setShowModal(false);
      setForm({
        code: '',
        discountType: 'Percentage',
        discountValue: 10,
        maxDiscountValue: 50000,
        minOrderValue: 100000,
        expiryDate: new Date(Date.now() + 7 * 24 * 60 * 60 * 1000).toISOString().slice(0, 16),
        usageLimit: 100
      });
      Swal.fire({
        icon: "info",
        text: 'Tạo mã giảm giá thành công!'
      });
    } catch (err) {
      Swal.fire({
        icon: "info",
        text: err.response?.data?.message || 'Có lỗi xảy ra khi tạo.'
      });
    } finally {
      setSubmitting(false);
    }
  };

  const filtered = vouchers.filter(v => v.code.toLowerCase().includes(searchTerm.toLowerCase()));

  return (
    <div className="space-y-6 animate-in fade-in duration-300">
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <div>
          <h1 className="text-2xl font-bold text-on-surface">Mã Khuyến Mãi</h1>
          <p className="text-sm text-on-surface-variant">Quản lý các chương trình giảm giá</p>
        </div>
        <div className="flex items-center gap-2">
          <button onClick={fetchVouchers} className="p-2 border border-outline-variant/30 rounded-xl hover:bg-surface-container-low transition-colors">
            <RefreshCw size={18} className="text-on-surface-variant" />
          </button>
          <button onClick={() => setShowModal(true)} className="flex items-center gap-2 px-4 py-2 bg-primary text-white rounded-xl text-sm font-semibold hover:bg-primary-container shadow-md transition-colors">
            <Plus size={16} /> Thêm Mã
          </button>
        </div>
      </div>

      <div className="bg-surface border border-outline-variant/30 rounded-3xl p-6 shadow-sm space-y-6">
        <div className="relative w-full max-w-sm">
          <input
            type="text"
            placeholder="Tìm kiếm mã (VD: SUMMER2024)..."
            value={searchTerm}
            onChange={e => setSearchTerm(e.target.value)}
            className="w-full bg-surface-container-low border border-outline-variant/50 rounded-xl px-10 py-2.5 text-sm focus:border-primary outline-none"
          />
          <Search size={16} className="absolute left-4 top-3 text-on-surface-variant" />
        </div>

        {loading ? (
          <div className="space-y-4">
            {[1, 2, 3].map(i => <div key={i} className="h-16 bg-surface-container-low rounded-xl animate-pulse" />)}
          </div>
        ) : filtered.length === 0 ? (
          <div className="text-center py-12 border border-dashed border-outline-variant/30 rounded-2xl">
            <Tag size={40} className="mx-auto text-on-surface-variant opacity-30 mb-4" />
            <h3 className="font-bold text-on-surface">Không tìm thấy mã giảm giá</h3>
          </div>
        ) : (
          <div className="overflow-x-auto">
            <table className="w-full text-left text-sm whitespace-nowrap">
              <thead className="bg-surface-container-low/50">
                <tr>
                  <th className="px-4 py-3 font-semibold text-on-surface">Mã / Mô tả</th>
                  <th className="px-4 py-3 font-semibold text-on-surface">Mức Giảm</th>
                  <th className="px-4 py-3 font-semibold text-on-surface">Điều kiện (Tối thiểu)</th>
                  <th className="px-4 py-3 font-semibold text-on-surface">Thời gian áp dụng</th>
                  <th className="px-4 py-3 font-semibold text-on-surface text-center">Trạng thái</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-outline-variant/10">
                {filtered.map(v => {
                  const isActiveNow = v.isActive && new Date(v.expiryDate) >= new Date();
                  return (
                    <tr key={v.id} className="hover:bg-surface-container-lowest transition-colors">
                      <td className="px-4 py-4">
                        <p className="font-bold text-primary">{v.code}</p>
                        <p className="text-xs text-on-surface-variant max-w-[200px] truncate">{v.usageLimit - v.usedCount} lượt còn lại</p>
                      </td>
                      <td className="px-4 py-4">
                        <p className="font-bold flex items-center gap-1 text-emerald-600">
                          {v.discountType === 'Percentage' ? <Percent size={12}/> : null} 
                          {v.discountType === 'Percentage' ? `${v.discountValue}%` : `${v.discountValue.toLocaleString()}đ`}
                        </p>
                        {v.maxDiscountValue ? <p className="text-[10px] text-on-surface-variant">Tối đa {v.maxDiscountValue.toLocaleString()}đ</p> : null}
                      </td>
                      <td className="px-4 py-4 font-semibold text-on-surface-variant">
                        {v.minOrderValue.toLocaleString()}đ
                      </td>
                      <td className="px-4 py-4">
                        <p className="text-xs text-on-surface-variant">HSD: {new Date(v.expiryDate).toLocaleString('vi-VN')}</p>
                      </td>
                      <td className="px-4 py-4 text-center">
                        <button
                          onClick={() => handleToggle(v.id)}
                          className={`px-3 py-1 text-[10px] font-bold rounded-full uppercase tracking-wider transition-colors ${
                            isActiveNow ? 'bg-emerald-500/10 text-emerald-600 border border-emerald-500/20' : 
                            (v.isActive ? 'bg-amber-500/10 text-amber-600 border border-amber-500/20' : 'bg-rose-500/10 text-rose-600 border border-rose-500/20')
                          }`}
                        >
                          {isActiveNow ? 'Đang chạy' : (v.isActive ? 'Chờ chạy' : 'Đã khóa')}
                        </button>
                      </td>
                    </tr>
                  )
                })}
              </tbody>
            </table>
          </div>
        )}
      </div>

      {showModal && (
        <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-black/50 backdrop-blur-sm animate-in fade-in">
          <div className="bg-surface w-full max-w-lg rounded-3xl p-6 shadow-2xl relative">
            <div className="flex justify-between items-center mb-6">
              <h2 className="text-xl font-bold text-on-surface flex items-center gap-2"><Tag size={20} className="text-primary"/> Thêm Khuyến Mãi</h2>
              <button onClick={() => setShowModal(false)} className="p-2 text-on-surface-variant hover:bg-surface-container rounded-full">
                <XCircle size={20} />
              </button>
            </div>
            
            <form onSubmit={handleSubmit} className="space-y-4">
              <div className="grid grid-cols-2 gap-4">
                <div className="col-span-2">
                  <label className="block text-xs font-semibold text-on-surface-variant uppercase mb-1">Mã (Code)</label>
                  <input required value={form.code} onChange={e => setForm({...form, code: e.target.value.toUpperCase()})} placeholder="VD: TET2025" className="w-full bg-surface-container-lowest border border-outline-variant/30 rounded-xl px-4 py-2.5 text-sm uppercase focus:border-primary outline-none" />
                </div>
                <div>
                  <label className="block text-xs font-semibold text-on-surface-variant uppercase mb-1">Loại Giảm Giá</label>
                  <select value={form.discountType} onChange={e => setForm({...form, discountType: e.target.value})} className="w-full bg-surface-container-lowest border border-outline-variant/30 rounded-xl px-4 py-2.5 text-sm focus:border-primary outline-none">
                    <option value="Percentage">Theo %</option>
                    <option value="Fixed">Cố định (VNĐ)</option>
                  </select>
                </div>
                <div>
                  <label className="block text-xs font-semibold text-on-surface-variant uppercase mb-1">Mức giảm ({form.discountType === 'Percentage' ? '%' : 'VNĐ'})</label>
                  <input required type="number" min="1" value={form.discountValue} onChange={e => setForm({...form, discountValue: e.target.value})} className="w-full bg-surface-container-lowest border border-outline-variant/30 rounded-xl px-4 py-2.5 text-sm focus:border-primary outline-none" />
                </div>
                <div>
                  <label className="block text-xs font-semibold text-on-surface-variant uppercase mb-1">Giảm tối đa (VNĐ)</label>
                  <input required type="number" min="0" value={form.maxDiscountValue} onChange={e => setForm({...form, maxDiscountValue: e.target.value})} className="w-full bg-surface-container-lowest border border-outline-variant/30 rounded-xl px-4 py-2.5 text-sm focus:border-primary outline-none" />
                </div>
                <div>
                  <label className="block text-xs font-semibold text-on-surface-variant uppercase mb-1">Đơn hàng tối thiểu (VNĐ)</label>
                  <input required type="number" min="0" value={form.minOrderValue} onChange={e => setForm({...form, minOrderValue: e.target.value})} className="w-full bg-surface-container-lowest border border-outline-variant/30 rounded-xl px-4 py-2.5 text-sm focus:border-primary outline-none" />
                </div>
                <div>
                  <label className="block text-xs font-semibold text-on-surface-variant uppercase mb-1">Lượt sử dụng</label>
                  <input required type="number" min="1" value={form.usageLimit} onChange={e => setForm({...form, usageLimit: e.target.value})} className="w-full bg-surface-container-lowest border border-outline-variant/30 rounded-xl px-4 py-2.5 text-sm focus:border-primary outline-none" />
                </div>
                <div>
                  <label className="block text-xs font-semibold text-on-surface-variant uppercase mb-1">Ngày kết thúc</label>
                  <input required type="datetime-local" value={form.expiryDate} onChange={e => setForm({...form, expiryDate: e.target.value})} className="w-full bg-surface-container-lowest border border-outline-variant/30 rounded-xl px-4 py-2.5 text-sm focus:border-primary outline-none" />
                </div>
              </div>
              
              <div className="pt-4 flex justify-end gap-3">
                <button type="button" onClick={() => setShowModal(false)} className="px-5 py-2.5 text-sm font-semibold rounded-xl text-on-surface border border-outline-variant/30 hover:bg-surface-container">
                  Hủy
                </button>
                <button type="submit" disabled={submitting} className="px-5 py-2.5 text-sm font-semibold rounded-xl bg-primary text-white shadow-md hover:bg-primary-container disabled:opacity-50 flex items-center gap-2">
                  {submitting ? 'Đang tạo...' : <><Plus size={16}/> Tạo mã</>}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}
