import React, { useState, useEffect } from 'react';
import { X, Plus, Trash2, PackageSearch } from 'lucide-react';
import api from '../../api/axios';

export default function GoodsReceiptModal({ isOpen, onClose, onSuccess }) {
    const [note, setNote] = useState('');
    const [details, setDetails] = useState([]);
    
    // For picking product
    const [products, setProducts] = useState([]);
    const [selectedProductId, setSelectedProductId] = useState('');
    const [selectedVariantId, setSelectedVariantId] = useState('');
    const [quantity, setQuantity] = useState(1);
    const [importPrice, setImportPrice] = useState(0);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        if (isOpen) {
            fetchProducts();
            setNote('');
            setDetails([]);
            resetForm();
        }
    }, [isOpen]);

    const fetchProducts = async () => {
        try {
            const res = await api.get('/Products');
            setProducts(res.data.data);
        } catch (error) {
            console.error('Lỗi khi tải sản phẩm:', error);
        }
    };

    const resetForm = () => {
        setSelectedProductId('');
        setSelectedVariantId('');
        setQuantity(1);
        setImportPrice(0);
    };

    const handleAddDetail = () => {
        if (!selectedProductId) {
            alert('Vui lòng chọn sản phẩm');
            return;
        }
        if (quantity <= 0 || importPrice < 0) {
            alert('Số lượng và giá nhập phải hợp lệ');
            return;
        }

        const product = products.find(p => p.id === selectedProductId);
        const variant = product?.variants?.find(v => v.id === selectedVariantId);

        // Check duplicates
        const exists = details.find(d => d.productId === selectedProductId && d.productVariantId === (selectedVariantId || null));
        if (exists) {
            alert('Sản phẩm này đã có trong danh sách nhập. Vui lòng xóa và thêm lại nếu muốn đổi số lượng.');
            return;
        }

        setDetails([...details, {
            productId: selectedProductId,
            productName: product.name,
            productVariantId: selectedVariantId || null,
            variantName: variant ? variant.name : null,
            quantity: Number(quantity),
            importPrice: Number(importPrice)
        }]);

        resetForm();
    };

    const handleRemoveDetail = (index) => {
        const newDetails = [...details];
        newDetails.splice(index, 1);
        setDetails(newDetails);
    };

    const handleSubmit = async () => {
        if (details.length === 0) {
            alert('Vui lòng thêm ít nhất một sản phẩm vào phiếu nhập');
            return;
        }

        try {
            setLoading(true);
            const request = {
                note: note,
                details: details.map(d => ({
                    productId: d.productId,
                    productVariantId: d.productVariantId,
                    quantity: d.quantity,
                    importPrice: d.importPrice
                }))
            };

            await api.post('/Inventory/receipts', request);
            onSuccess();
            onClose();
        } catch (error) {
            console.error('Lỗi tạo phiếu nhập:', error);
            alert('Đã xảy ra lỗi khi tạo phiếu nhập');
        } finally {
            setLoading(false);
        }
    };

    if (!isOpen) return null;

    const selectedProduct = products.find(p => p.id === selectedProductId);
    const hasVariants = selectedProduct && selectedProduct.variants && selectedProduct.variants.length > 0;

    return (
        <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-black/50 backdrop-blur-sm">
            <div className="bg-surface w-full max-w-4xl rounded-3xl border border-outline-variant/30 shadow-2xl flex flex-col max-h-[90vh]">
                <div className="flex justify-between items-center p-6 border-b border-outline-variant/30">
                    <h2 className="text-xl font-bold text-on-surface flex items-center gap-2">
                        <Plus className="text-primary" />
                        Tạo Phiếu Nhập Hàng
                    </h2>
                    <button onClick={onClose} className="p-2 rounded-xl hover:bg-surface-container-low text-on-surface-variant hover:text-on-surface transition-colors">
                        <X size={20} />
                    </button>
                </div>

                <div className="p-6 overflow-y-auto flex-1 space-y-8">
                    {/* Header Info */}
                    <div className="space-y-4">
                        <h3 className="text-sm font-semibold text-primary uppercase tracking-wider">Thông tin chung</h3>
                        <div>
                            <label className="block text-sm font-medium text-on-surface-variant mb-1">Ghi chú phiếu nhập</label>
                            <textarea 
                                value={note}
                                onChange={(e) => setNote(e.target.value)}
                                className="w-full bg-surface-container-lowest border border-outline-variant/30 rounded-xl px-4 py-2 text-on-surface focus:outline-none focus:border-primary transition-colors"
                                rows="2"
                                placeholder="Ví dụ: Nhập hàng đợt 1 tháng 7/2026..."
                            />
                        </div>
                    </div>

                    {/* Add Item Form */}
                    <div className="space-y-4 bg-surface-container-lowest p-6 rounded-2xl border border-outline-variant/30">
                        <h3 className="text-sm font-semibold text-primary uppercase tracking-wider flex items-center gap-2">
                            <PackageSearch size={16} /> Thêm sản phẩm
                        </h3>
                        
                        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                            <div>
                                <label className="block text-sm font-medium text-on-surface-variant mb-1">Sản Phẩm</label>
                                <select 
                                    value={selectedProductId}
                                    onChange={(e) => {
                                        setSelectedProductId(e.target.value);
                                        setSelectedVariantId('');
                                    }}
                                    className="w-full bg-surface border border-outline-variant/30 rounded-xl px-4 py-2.5 text-on-surface focus:outline-none focus:border-primary transition-colors appearance-none"
                                >
                                    <option value="">-- Chọn Sản Phẩm --</option>
                                    {products.map(p => (
                                        <option key={p.id} value={p.id}>{p.name}</option>
                                    ))}
                                </select>
                            </div>

                            <div>
                                <label className="block text-sm font-medium text-on-surface-variant mb-1">Biến Thể (nếu có)</label>
                                <select 
                                    value={selectedVariantId}
                                    onChange={(e) => setSelectedVariantId(e.target.value)}
                                    disabled={!hasVariants}
                                    className="w-full bg-surface border border-outline-variant/30 rounded-xl px-4 py-2.5 text-on-surface focus:outline-none focus:border-primary transition-colors appearance-none disabled:opacity-50 disabled:cursor-not-allowed"
                                >
                                    <option value="">{hasVariants ? '-- Chọn Biến Thể --' : 'Không có biến thể'}</option>
                                    {hasVariants && selectedProduct.variants.map(v => (
                                        <option key={v.id} value={v.id}>{v.name} (SKU: {v.sku})</option>
                                    ))}
                                </select>
                            </div>

                            <div>
                                <label className="block text-sm font-medium text-on-surface-variant mb-1">Số Lượng Nhập</label>
                                <input 
                                    type="number"
                                    min="1"
                                    value={quantity}
                                    onChange={(e) => setQuantity(e.target.value)}
                                    className="w-full bg-surface border border-outline-variant/30 rounded-xl px-4 py-2.5 text-on-surface focus:outline-none focus:border-primary transition-colors"
                                />
                            </div>

                            <div>
                                <label className="block text-sm font-medium text-on-surface-variant mb-1">Giá Nhập (đ/sp)</label>
                                <input 
                                    type="number"
                                    min="0"
                                    step="1000"
                                    value={importPrice}
                                    onChange={(e) => setImportPrice(e.target.value)}
                                    className="w-full bg-surface border border-outline-variant/30 rounded-xl px-4 py-2.5 text-on-surface focus:outline-none focus:border-primary transition-colors"
                                />
                            </div>
                        </div>

                        <div className="flex justify-end pt-2">
                            <button 
                                onClick={handleAddDetail}
                                className="bg-primary/10 hover:bg-primary/20 border border-primary/30 text-primary px-4 py-2 rounded-xl text-sm font-semibold transition-colors flex items-center gap-2"
                            >
                                <Plus size={16} /> Thêm Vào Phiếu
                            </button>
                        </div>
                    </div>

                    {/* Receipt Details Table */}
                    <div className="space-y-4">
                        <h3 className="text-sm font-semibold text-primary uppercase tracking-wider">Danh Sách Nhập ({details.length})</h3>
                        <div className="border border-outline-variant/30 rounded-2xl overflow-hidden bg-surface-container-lowest">
                            <table className="w-full text-left text-sm">
                                <thead className="bg-surface-container-low text-primary border-b border-outline-variant/30">
                                    <tr>
                                        <th className="px-4 py-3 font-semibold">Sản phẩm</th>
                                        <th className="px-4 py-3 font-semibold">Phân loại</th>
                                        <th className="px-4 py-3 font-semibold text-right">SL</th>
                                        <th className="px-4 py-3 font-semibold text-right">Đơn giá</th>
                                        <th className="px-4 py-3 font-semibold text-right">Thành tiền</th>
                                        <th className="px-4 py-3 font-semibold text-center"></th>
                                    </tr>
                                </thead>
                                <tbody className="divide-y divide-outline-variant/30">
                                    {details.map((d, index) => (
                                        <tr key={index} className="hover:bg-surface-container-low">
                                            <td className="px-4 py-3 text-on-surface font-medium">{d.productName}</td>
                                            <td className="px-4 py-3 text-on-surface-variant">{d.variantName || '-'}</td>
                                            <td className="px-4 py-3 text-right text-on-surface-variant">{d.quantity}</td>
                                            <td className="px-4 py-3 text-right text-on-surface-variant">{d.importPrice.toLocaleString()} đ</td>
                                            <td className="px-4 py-3 text-right text-emerald-600 font-medium">{(d.quantity * d.importPrice).toLocaleString()} đ</td>
                                            <td className="px-4 py-3 text-center">
                                                <button onClick={() => handleRemoveDetail(index)} className="text-rose-500 hover:text-rose-600 p-1 bg-rose-50 rounded-lg">
                                                    <Trash2 size={14} />
                                                </button>
                                            </td>
                                        </tr>
                                    ))}
                                    {details.length === 0 && (
                                        <tr>
                                            <td colSpan="6" className="px-4 py-8 text-center text-outline">
                                                Chưa có sản phẩm nào
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>
                            {details.length > 0 && (
                                <div className="p-4 bg-surface-container-low border-t border-outline-variant/30 flex justify-end items-center gap-4">
                                    <span className="text-on-surface-variant">Tổng thanh toán:</span>
                                    <span className="text-xl font-bold text-on-surface">
                                        {details.reduce((sum, d) => sum + (d.quantity * d.importPrice), 0).toLocaleString()} đ
                                    </span>
                                </div>
                            )}
                        </div>
                    </div>
                </div>

                <div className="p-6 border-t border-outline-variant/30 flex justify-end gap-3 bg-surface-container-lowest rounded-b-3xl">
                    <button 
                        onClick={onClose}
                        className="px-6 py-2.5 rounded-xl text-sm font-semibold text-on-surface-variant hover:bg-surface-container-low transition-colors"
                    >
                        Hủy Bỏ
                    </button>
                    <button 
                        onClick={handleSubmit}
                        disabled={loading || details.length === 0}
                        className="bg-primary hover:bg-primary/90 text-white px-8 py-2.5 rounded-xl text-sm font-semibold transition-all shadow-lg shadow-primary/20 disabled:opacity-50 disabled:cursor-not-allowed"
                    >
                        {loading ? 'Đang Xử Lý...' : 'Hoàn Tất & Tạo Phiếu'}
                    </button>
                </div>
            </div>
        </div>
    );
}
