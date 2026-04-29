import React, { useState, useEffect } from 'react';
import { getCart, clearCart } from '../api/cartStore';
import api from '../api/axios';
import { Trash2, CreditCard, ShoppingBag } from 'lucide-react';

function Cart() {
    const [cartItems, setCartItems] = useState([]);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        setCartItems(getCart());
    }, []);

    const totalAmount = cartItems.reduce((sum, item) => sum + (item.price * item.quantity), 0);

    const handleCheckout = async () => {
        if (cartItems.length === 0) return alert("Giỏ hàng trống!");

        setLoading(true);
        try {
            // Chuẩn bị dữ liệu gửi sang Backend (OrderRequest)
            const orderData = {
                items: cartItems.map(item => ({
                    productId: item.id,
                    quantity: item.quantity
                }))
            };

            const response = await api.post('/Orders', orderData);
            alert("Đặt hàng thành công! Số lượng kho đã được giữ chỗ.");
            clearCart();
            setCartItems([]);
            window.location.href = "/profile"; // Chuyển về trang cá nhân để xem đơn hàng
        } catch (error) {
            alert("Lỗi đặt hàng: " + (error.response?.data || "Vui lòng thử lại"));
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="min-h-screen bg-gray-50 p-6">
            <div className="max-w-4xl mx-auto">
                <h2 className="text-3xl font-black mb-8 flex items-center gap-3">
                    <ShoppingBag className="text-blue-600" /> Giỏ Hàng Của Bạn
                </h2>

                <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
                    {/* Danh sách sản phẩm */}
                    <div className="lg:col-span-2 space-y-4">
                        {cartItems.length === 0 ? (
                            <div className="bg-white p-10 rounded-2xl text-center shadow-sm">
                                <p className="text-gray-500">Chưa có sản phẩm nào trong giỏ.</p>
                            </div>
                        ) : (
                            cartItems.map(item => (
                                <div key={item.id} className="bg-white p-4 rounded-2xl shadow-sm flex items-center justify-between border border-gray-100">
                                    <div className="flex items-center gap-4">
                                        <div className="w-16 h-16 bg-blue-100 rounded-xl flex items-center justify-center font-bold text-blue-600">
                                            {item.name.charAt(0)}
                                        </div>
                                        <div>
                                            <h4 className="font-bold text-gray-800">{item.name}</h4>
                                            <p className="text-sm text-gray-500">{item.price.toLocaleString()}đ x {item.quantity}</p>
                                        </div>
                                    </div>
                                    <p className="font-black text-gray-900">{(item.price * item.quantity).toLocaleString()}đ</p>
                                </div>
                            ))
                        )}
                    </div>

                    {/* Tổng kết thanh toán */}
                    <div className="bg-white p-6 rounded-3xl shadow-xl h-fit border border-blue-50">
                        <h3 className="text-xl font-bold mb-6">Tổng đơn hàng</h3>
                        <div className="flex justify-between mb-4">
                            <span className="text-gray-500">Tạm tính:</span>
                            <span className="font-medium">{totalAmount.toLocaleString()}đ</span>
                        </div>
                        <div className="flex justify-between mb-6 pt-4 border-t border-dashed">
                            <span className="text-lg font-bold">Tổng cộng:</span>
                            <span className="text-2xl font-black text-blue-600">{totalAmount.toLocaleString()}đ</span>
                        </div>
                        <button
                            onClick={handleCheckout}
                            disabled={loading || cartItems.length === 0}
                            className="w-full bg-slate-900 text-white py-4 rounded-2xl font-bold flex items-center justify-center gap-2 hover:bg-blue-600 transition disabled:bg-gray-300"
                        >
                            <CreditCard size={20} />
                            {loading ? "Đang xử lý..." : "Đặt hàng ngay"}
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Cart;