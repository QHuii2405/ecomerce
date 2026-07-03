import Swal from "sweetalert2";
import React, { useState, useEffect } from 'react';
import { Package, Plus, Search, Filter, AlertCircle, Edit, Trash2, CheckCircle, XCircle, Box, Receipt } from 'lucide-react';
import api from '../../api/axios';
import GoodsReceiptModal from './GoodsReceiptModal';

export default function AdminInventory() {
    const [activeTab, setActiveTab] = useState('stock'); // 'stock' or 'receipts'
    const [receipts, setReceipts] = useState([]);
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const userRole = (localStorage.getItem('userRole') || 'Staff').toLowerCase();

    useEffect(() => {
        if (activeTab === 'receipts') {
            fetchReceipts();
        } else {
            fetchProducts();
        }
    }, [activeTab]);

    const fetchProducts = async () => {
        try {
            setLoading(true);
            const response = await api.get('/Products');
            setProducts(response.data.data);
        } catch (err) {
            console.error('Failed to fetch products:', err);
            setError('Không thể tải dữ liệu tồn kho.');
        } finally {
            setLoading(false);
        }
    };

    const fetchReceipts = async () => {
        try {
            setLoading(true);
            const response = await api.get('/Inventory/receipts');
            setReceipts(response.data); // Assuming returning array directly based on IInventoryService refactor
        } catch (err) {
            console.error('Failed to fetch receipts:', err);
            setError('Không thể tải dữ liệu kho hàng.');
        } finally {
            setLoading(false);
        }
    };

    const handleApprove = async (id) => {
        if (!(await Swal.fire({
            title: "X�c nh?n",
            text: 'Xác nhận duyệt phiếu nhập hàng này? Số lượng kho sẽ được cập nhật.',
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "�?ng �",
            cancelButtonText: "H?y"
        })).isConfirmed) return;
        try {
            await api.post(`/Inventory/receipts/${id}/approve`);
            fetchReceipts();
        } catch (err) {
            console.error(err);
            Swal.fire({
                icon: "info",
                text: 'Lỗi khi duyệt phiếu'
            });
        }
    };

    const handleReject = async (id) => {
        if (!(await Swal.fire({
            title: "X�c nh?n",
            text: 'Xác nhận từ chối phiếu nhập hàng này?',
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "�?ng �",
            cancelButtonText: "H?y"
        })).isConfirmed) return;
        try {
            await api.post(`/Inventory/receipts/${id}/reject`);
            fetchReceipts();
        } catch (err) {
            console.error(err);
            Swal.fire({
                icon: "info",
                text: 'Lỗi khi từ chối phiếu'
            });
        }
    };

    return (
        <div className="space-y-6">
            <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4">
                <div>
                    <h1 className="text-2xl font-bold text-on-surface flex items-center gap-2">
                        <Package className="text-primary" />
                        Quản Lý Kho Hàng
                    </h1>
                    <p className="text-sm text-on-surface-variant">Xem tồn kho và quản lý phiếu nhập hàng</p>
                </div>
                <button
                    onClick={() => setIsModalOpen(true)}
                    className="bg-primary hover:bg-primary/90 text-white px-4 py-2 rounded-xl text-sm font-semibold flex items-center gap-2 transition-all shadow-lg shadow-primary/20"
                >
                    <Plus size={16} /> Tạo Phiếu Nhập
                </button>
            </div>

            {/* Tabs */}
            <div className="flex gap-4 border-b border-primary/20">
                <button
                    onClick={() => setActiveTab('stock')}
                    className={`pb-4 flex items-center gap-2 font-semibold text-sm transition-colors border-b-2 ${
                        activeTab === 'stock' ? 'border-primary text-primary' : 'border-transparent text-on-surface-variant hover:text-on-surface'
                    }`}
                >
                    <Box size={16} /> Tồn Kho Hiện Tại
                </button>
                <button
                    onClick={() => setActiveTab('receipts')}
                    className={`pb-4 flex items-center gap-2 font-semibold text-sm transition-colors border-b-2 ${
                        activeTab === 'receipts' ? 'border-primary text-primary' : 'border-transparent text-on-surface-variant hover:text-on-surface'
                    }`}
                >
                    <Receipt size={16} /> Lịch Sử Nhập Hàng
                </button>
            </div>

            <div className="bg-surface border border-outline-variant/30 rounded-2xl overflow-hidden shadow-sm">
                {loading ? (
                    <div className="p-12 text-center text-on-surface-variant">Đang tải...</div>
                ) : error ? (
                    <div className="p-12 text-center text-rose-500 flex flex-col items-center gap-2">
                        <AlertCircle size={24} />
                        {error}
                    </div>
                ) : activeTab === 'receipts' ? (
                    <div className="overflow-x-auto">
                        <table className="w-full text-left text-sm whitespace-nowrap">
                            <thead className="bg-surface-container-low text-primary">
                                <tr>
                                    <th className="px-6 py-4 font-semibold">Mã Phiếu</th>
                                    <th className="px-6 py-4 font-semibold">Người Tạo</th>
                                    <th className="px-6 py-4 font-semibold">Tổng Tiền</th>
                                    <th className="px-6 py-4 font-semibold">Trạng Thái</th>
                                    <th className="px-6 py-4 font-semibold">Ngày Tạo</th>
                                    <th className="px-6 py-4 font-semibold text-right">Thao Tác</th>
                                </tr>
                            </thead>
                            <tbody className="divide-y divide-outline-variant/30">
                                {receipts.map(receipt => (
                                    <tr key={receipt.id} className="hover:bg-surface-container-lowest transition-colors">
                                        <td className="px-6 py-4 text-on-surface font-medium">{receipt.receiptNumber}</td>
                                        <td className="px-6 py-4 text-on-surface-variant">{receipt.createdByName}</td>
                                        <td className="px-6 py-4 text-on-surface">
                                            {receipt.totalAmount.toLocaleString('vi-VN')} đ
                                        </td>
                                        <td className="px-6 py-4">
                                            <span className={`px-2 py-1 rounded-md text-xs font-semibold ${
                                                receipt.status === 'Approved' ? 'bg-emerald-50 text-emerald-600 border border-emerald-200' :
                                                receipt.status === 'Rejected' ? 'bg-rose-50 text-rose-600 border border-rose-200' :
                                                'bg-amber-50 text-amber-600 border border-amber-200'
                                            }`}>
                                                {receipt.status === 'Approved' ? 'Đã duyệt' : receipt.status === 'Rejected' ? 'Từ chối' : 'Chờ duyệt'}
                                            </span>
                                        </td>
                                        <td className="px-6 py-4 text-on-surface-variant">
                                            {new Date(receipt.createdAt).toLocaleDateString('vi-VN')}
                                        </td>
                                        <td className="px-6 py-4 text-right">
                                            <div className="flex justify-end gap-2">
                                                {receipt.status === 'Pending' && userRole === 'admin' && (
                                                    <>
                                                        <button onClick={() => handleApprove(receipt.id)} className="p-1.5 bg-emerald-50 hover:bg-emerald-100 text-emerald-600 rounded-lg transition-colors" title="Duyệt">
                                                            <CheckCircle size={16} />
                                                        </button>
                                                        <button onClick={() => handleReject(receipt.id)} className="p-1.5 bg-rose-50 hover:bg-rose-100 text-rose-600 rounded-lg transition-colors" title="Từ chối">
                                                            <XCircle size={16} />
                                                        </button>
                                                    </>
                                                )}
                                            </div>
                                        </td>
                                    </tr>
                                ))}
                                {receipts.length === 0 && (
                                    <tr>
                                        <td colSpan="6" className="px-6 py-12 text-center text-outline">
                                            Chưa có phiếu nhập hàng nào.
                                        </td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>
                ) : (
                    <div className="overflow-x-auto">
                        <table className="w-full text-left text-sm whitespace-nowrap">
                            <thead className="bg-surface-container-low text-primary">
                                <tr>
                                    <th className="px-6 py-4 font-semibold">Tên Sản Phẩm</th>
                                    <th className="px-6 py-4 font-semibold">Biến Thể (SKU)</th>
                                    <th className="px-6 py-4 font-semibold">Thương Hiệu</th>
                                    <th className="px-6 py-4 font-semibold text-right">Tồn Kho</th>
                                </tr>
                            </thead>
                            <tbody className="divide-y divide-outline-variant/30">
                                {products.map(product => {
                                    if (product.variants && product.variants.length > 0) {
                                        return product.variants.map(variant => (
                                            <tr key={variant.id} className="hover:bg-surface-container-lowest transition-colors">
                                                <td className="px-6 py-4 text-on-surface font-medium">{product.name}</td>
                                                <td className="px-6 py-4 text-on-surface-variant">{variant.name} <span className="text-xs text-outline">({variant.sku})</span></td>
                                                <td className="px-6 py-4 text-on-surface-variant">{product.brand || '-'}</td>
                                                <td className="px-6 py-4 text-right font-bold text-emerald-600">
                                                    {variant.stockQuantity}
                                                </td>
                                            </tr>
                                        ));
                                    } else {
                                        return (
                                            <tr key={product.id} className="hover:bg-surface-container-lowest transition-colors">
                                                <td className="px-6 py-4 text-on-surface font-medium">{product.name}</td>
                                                <td className="px-6 py-4 text-on-surface-variant">-</td>
                                                <td className="px-6 py-4 text-on-surface-variant">{product.brand || '-'}</td>
                                                <td className="px-6 py-4 text-right font-bold text-emerald-600">
                                                    {product.inventory?.stockQuantity || 0}
                                                </td>
                                            </tr>
                                        );
                                    }
                                })}
                                {products.length === 0 && (
                                    <tr>
                                        <td colSpan="4" className="px-6 py-12 text-center text-outline">
                                            Chưa có sản phẩm nào.
                                        </td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>
                )}
            </div>

            <GoodsReceiptModal 
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                onSuccess={() => {
                    if (activeTab === 'receipts') {
                        fetchReceipts();
                    } else {
                        setActiveTab('receipts');
                    }
                }}
            />
        </div>
    );
}
