import React, { useState, useEffect, useCallback } from 'react';
import { getCart, clearCart, removeFromCart, updateQuantity } from '../api/cartStore';
import api from '../api/axios';
import { Link, useNavigate } from 'react-router-dom';
import {
  ShoppingBag, Trash2, CreditCard, ArrowLeft, X, CheckCircle2,
  Truck, Banknote, QrCode, Clock, ChevronRight, Package, AlertCircle
} from 'lucide-react';

// Helper: payment method config
const PAYMENT_METHODS = [
  {
    id: 'COD',
    label: 'Thanh toán khi nhận hàng',
    sublabel: 'COD — nhân viên thu tiền khi giao',
    icon: Banknote,
    color: 'text-emerald-600',
    bg: 'bg-emerald-500/10 border-emerald-500/30'
  },
  {
    id: 'MoMo',
    label: 'Ví MoMo',
    sublabel: 'Thanh toán qua QR MoMo',
    icon: QrCode,
    color: 'text-pink-600',
    bg: 'bg-pink-500/10 border-pink-500/30'
  },
  {
    id: 'VietQR',
    label: 'Chuyển khoản ngân hàng',
    sublabel: 'VietQR — quét mã ngân hàng nội địa',
    icon: CreditCard,
    color: 'text-primary',
    bg: 'bg-primary/10 border-primary/30'
  }
];

// Order status badge
function StatusBadge({ status }) {
  const config = {
    Pending: { label: 'Chờ xử lý', cls: 'bg-amber-500/10 text-amber-600 border-amber-500/20' },
    Confirmed: { label: 'Đã xác nhận', cls: 'bg-blue-500/10 text-blue-600 border-blue-500/20' },
    Shipping: { label: 'Đang giao', cls: 'bg-indigo-500/10 text-indigo-600 border-indigo-500/20' },
    Delivered: { label: 'Đã giao', cls: 'bg-emerald-500/10 text-emerald-600 border-emerald-500/20' },
    Cancelled: { label: 'Đã hủy', cls: 'bg-rose-500/10 text-rose-600 border-rose-500/20' },
  };
  const c = config[status] || config.Pending;
  return (
    <span className={`text-[10px] font-bold uppercase tracking-wider px-2.5 py-1 rounded-full border ${c.cls}`}>
      {c.label}
    </span>
  );
}

// MoMo payment modal
function MomoModal({ orderId, amount, onSuccess, onClose }) {
  const [qrData, setQrData] = useState(null);
  const [timeLeft, setTimeLeft] = useState(300);
  const [loading, setLoading] = useState(true);
  const [confirming, setConfirming] = useState(false);

  useEffect(() => {
    const fetchQr = async () => {
      try {
        const res = await api.post('/payments/momo/create', { orderId, amount });
        setQrData(res.data);
      } catch {
        setQrData({ qrImageUrl: `https://api.qrserver.com/v1/create-qr-code/?size=250x250&data=MOMO-DEMO-${orderId}` });
      } finally {
        setLoading(false);
      }
    };
    fetchQr();
  }, [orderId, amount]);

  useEffect(() => {
    if (timeLeft <= 0) return;
    const t = setInterval(() => setTimeLeft(p => p - 1), 1000);
    return () => clearInterval(t);
  }, [timeLeft]);

  const mins = String(Math.floor(timeLeft / 60)).padStart(2, '0');
  const secs = String(timeLeft % 60).padStart(2, '0');

  const handleConfirm = async () => {
    setConfirming(true);
    try {
      await api.post('/payments/momo/confirm', { orderId, simulateSuccess: true });
      onSuccess();
    } catch {
      alert('Thanh toán thất bại!');
    } finally {
      setConfirming(false);
    }
  };

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-black/60 backdrop-blur-sm animate-in fade-in duration-200">
      <div className="bg-surface-container-lowest rounded-3xl shadow-2xl w-full max-w-sm border border-outline-variant/30 overflow-hidden">
        {/* Header */}
        <div className="bg-gradient-to-r from-pink-500 to-pink-600 p-6 text-center relative">
          <button onClick={onClose} className="absolute top-4 right-4 text-white/70 hover:text-white">
            <X size={20} />
          </button>
          <div className="w-12 h-12 bg-white/20 rounded-2xl flex items-center justify-center mx-auto mb-2">
            <QrCode size={24} className="text-white" />
          </div>
          <h3 className="text-white font-bold text-lg">Thanh toán MoMo</h3>
          <p className="text-white/80 text-xs mt-1">{amount.toLocaleString()}đ</p>
        </div>

        <div className="p-6 space-y-4">
          {/* QR Code */}
          <div className="aspect-square max-w-[200px] mx-auto bg-surface-container-low rounded-2xl overflow-hidden flex items-center justify-center border border-outline-variant/30">
            {loading ? (
              <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-pink-500" />
            ) : (
              <img src={qrData?.qrImageUrl} alt="QR MoMo" className="w-full h-full object-contain p-2" />
            )}
          </div>

          {/* Countdown */}
          <div className="flex items-center justify-center gap-2 text-sm">
            <Clock size={14} className={timeLeft < 60 ? 'text-rose-500' : 'text-on-surface-variant'} />
            <span className={`font-mono font-bold ${timeLeft < 60 ? 'text-rose-500' : 'text-on-surface'}`}>
              {mins}:{secs}
            </span>
            <span className="text-on-surface-variant text-xs">còn lại</span>
          </div>

          <p className="text-center text-xs text-on-surface-variant">
            Mở ứng dụng <strong className="text-pink-600">MoMo</strong> và quét mã QR hoặc nhấn nút dưới để giả lập thanh toán thành công.
          </p>

          <button
            onClick={handleConfirm}
            disabled={confirming || timeLeft <= 0}
            className="w-full bg-gradient-to-r from-pink-500 to-pink-600 text-white py-3.5 rounded-2xl font-bold text-sm hover:shadow-lg hover:shadow-pink-500/20 transition-all active:scale-95 disabled:opacity-50 flex items-center justify-center gap-2"
          >
            {confirming ? (
              <><div className="animate-spin rounded-full h-4 w-4 border-b-2 border-white" /> Đang xử lý...</>
            ) : (
              <><CheckCircle2 size={16} /> Xác nhận đã thanh toán</>
            )}
          </button>
        </div>
      </div>
    </div>
  );
}

// VietQR modal
function VietQRModal({ orderId, amount, onClose }) {
  const [qrData, setQrData] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchQr = async () => {
      try {
        const res = await api.post('/payments/vietqr/create', { orderId, amount });
        setQrData(res.data);
      } catch {
        setQrData({
          qrImageUrl: `https://img.vietqr.io/image/MB-0123456789-compact.png?amount=${amount}&addInfo=LUMINA${orderId.replace(/-/g, '').slice(0, 8).toUpperCase()}&accountName=LUMINA%20TECH%20STORE`,
          bankId: 'MB',
          accountNo: '0123456789',
          accountName: 'LUMINA TECH STORE',
          transferContent: `LUMINA${orderId.replace(/-/g, '').slice(0, 8).toUpperCase()}`
        });
      } finally {
        setLoading(false);
      }
    };
    fetchQr();
  }, [orderId, amount]);

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-black/60 backdrop-blur-sm animate-in fade-in duration-200">
      <div className="bg-surface-container-lowest rounded-3xl shadow-2xl w-full max-w-sm border border-outline-variant/30 overflow-hidden">
        <div className="bg-gradient-to-r from-primary to-primary-container p-6 text-center relative">
          <button onClick={onClose} className="absolute top-4 right-4 text-white/70 hover:text-white">
            <X size={20} />
          </button>
          <div className="w-12 h-12 bg-white/20 rounded-2xl flex items-center justify-center mx-auto mb-2">
            <CreditCard size={24} className="text-white" />
          </div>
          <h3 className="text-white font-bold text-lg">VietQR — Chuyển khoản</h3>
          <p className="text-white/80 text-xs mt-1">{amount.toLocaleString()}đ</p>
        </div>

        <div className="p-6 space-y-4">
          <div className="aspect-square max-w-[200px] mx-auto bg-surface-container-low rounded-2xl overflow-hidden flex items-center justify-center border border-outline-variant/30">
            {loading ? (
              <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-primary" />
            ) : (
              <img src={qrData?.qrImageUrl} alt="VietQR" className="w-full h-full object-contain p-2"
                onError={(e) => { e.target.src = `https://api.qrserver.com/v1/create-qr-code/?size=250x250&data=VIETQR-DEMO`; }} />
            )}
          </div>

          {qrData && (
            <div className="bg-surface-container-low rounded-2xl p-4 space-y-2 text-xs">
              <div className="flex justify-between"><span className="text-on-surface-variant">Ngân hàng</span><span className="font-bold">{qrData.bankId} Bank</span></div>
              <div className="flex justify-between"><span className="text-on-surface-variant">Số tài khoản</span><span className="font-bold">{qrData.accountNo}</span></div>
              <div className="flex justify-between"><span className="text-on-surface-variant">Chủ TK</span><span className="font-bold">{qrData.accountName}</span></div>
              <div className="flex justify-between"><span className="text-on-surface-variant">Nội dung CK</span><span className="font-bold text-primary">{qrData.transferContent}</span></div>
              <div className="flex justify-between"><span className="text-on-surface-variant">Số tiền</span><span className="font-bold text-primary">{amount.toLocaleString()}đ</span></div>
            </div>
          )}

          <p className="text-center text-xs text-on-surface-variant">
            Sau khi chuyển khoản, nhân viên sẽ xác nhận đơn hàng trong vòng <strong>15 phút</strong>.
          </p>

          <button onClick={onClose} className="w-full bg-surface-container border border-outline-variant text-on-surface py-3 rounded-2xl font-semibold text-sm hover:bg-surface-container-high transition-all">
            Đã chuyển khoản
          </button>
        </div>
      </div>
    </div>
  );
}

export default function Cart() {
  const navigate = useNavigate();
  const [cartItems, setCartItems] = useState([]);
  const [paymentMethod, setPaymentMethod] = useState('COD');
  const [loading, setLoading] = useState(false);
  const [momoModal, setMomoModal] = useState(null); // { orderId, amount }
  const [vietqrModal, setVietqrModal] = useState(null);
  const [successOrderId, setSuccessOrderId] = useState(null);

  useEffect(() => {
    setCartItems(getCart());
  }, []);

  const totalAmount = cartItems.reduce((sum, item) => sum + (item.price * item.quantity), 0);

  const handleRemove = (id) => {
    removeFromCart(id);
    setCartItems(getCart());
  };

  const handleQtyChange = (id, delta) => {
    const item = cartItems.find(i => i.id === id);
    if (!item) return;
    const newQty = item.quantity + delta;
    if (newQty < 1) return;
    updateQuantity(id, newQty);
    setCartItems(getCart());
  };

  const handleCheckout = async () => {
    if (cartItems.length === 0) return;
    const token = localStorage.getItem('token');
    if (!token) { navigate('/login'); return; }

    setLoading(true);
    try {
      const orderData = {
        items: cartItems.map(item => ({ productId: item.id, quantity: item.quantity })),
        note: paymentMethod === 'COD' ? 'COD' : paymentMethod
      };

      const res = await api.post('/Orders', orderData);
      const orderId = res.data.orderId;

      clearCart();
      setCartItems([]);

      if (paymentMethod === 'COD') {
        setSuccessOrderId(orderId);
      } else if (paymentMethod === 'MoMo') {
        setMomoModal({ orderId, amount: totalAmount });
      } else if (paymentMethod === 'VietQR') {
        setVietqrModal({ orderId, amount: totalAmount });
      }
    } catch (error) {
      alert('Lỗi đặt hàng: ' + (error.response?.data?.message || error.response?.data || 'Vui lòng thử lại'));
    } finally {
      setLoading(false);
    }
  };

  const getProductImage = (name = '') => {
    const n = name.toLowerCase();
    if (n.includes('laptop') || n.includes('macbook') || n.includes('pro')) return 'https://lh3.googleusercontent.com/aida-public/AB6AXuDzzkgw3qdK2qx_eejr9Oee4qzRfJoNIb-smYiUqNBNBZ4_KNAbm4HxoqNIyfUqk9pV0qWkdyFf7t7SYsjbbwCkkEN06FQNQBCseQPzXacixmLlO0YB5GVfxd7AR42kwnUufnptDgHCXnHlGXhg3x4QzV7sXZkMAYLQHcoPSLjWbTIWG3W_hrzh4eWEuFkuhEuk_62jmY9dMbWUMr11EIDivHK_RMuJDGpKxGyfr8JRcfcL8i0qv6Ve';
    if (n.includes('keyboard') || n.includes('mouse') || n.includes('gaming')) return 'https://lh3.googleusercontent.com/aida-public/AB6AXuCtePE6a8azsOINxsGhPHJGa7pybuOEtEXGQUTmgTTPhKMBwOS-7FOpWVDKTgo17TVnuDYz0tGqUltaJ9vPZxqJCWCdknYb9x1VpLfsq9eC8Qlc1fG2cjNg5i2klZdqsM6d2QHuBDP_mHVRg-Ley5Dw6z3yNJURrfQ5bnPx_TyxzBc7EuP7b5boquCcOezT3SWcmEEoMElGb7VQSAkNtos5xtMcNxJFn8D0-Oq4x3zH0vgAz5bRpJdx';
    if (n.includes('bud') || n.includes('headphone') || n.includes('tai nghe')) return 'https://lh3.googleusercontent.com/aida-public/AB6AXujCnwa9YYlaySQHciXXGs5ENqNsPlYyM48pBFebzeWQc7dPKrvdRI9-hr5S5mxpSSR3ZK689MtFW5CFO2yl2XF3_1RCn3iwdnmtwXFQeVUo_FXR0vOQ7FYE_qzsTZb4Q8_zaMowCCLDFa84jAXRV-XqKV2AZPbY2fWUotHuBFbc9Jv95ESTZuet5JJdQVxXrmhm4ItvrDA3BDDkW6wfXdjWtO5ynIppnxJllrxffafcwXaW4XKodrR';
    return 'https://lh3.googleusercontent.com/aida-public/AB6AXuDzzkgw3qdK2qx_eejr9Oee4qzRfJoNIb-smYiUqNBNBZ4_KNAbm4HxoqNIyfUqk9pV0qWkdyFf7t7SYsjbbwCkkEN06FQNQBCseQPzXacixmLlO0YB5GVfxd7AR42kwnUufnptDgHCXnHlGXhg3x4QzV7sXZkMAYLQHcoPSLjWbTIWG3W_hrzh4eWEuFkuhEuk_62jmY9dMbWUMr11EIDivHK_RMuJDGpKxGyfr8JRcfcL8i0qv6Ve';
  };

  // Success screen
  if (successOrderId) {
    return (
      <div className="min-h-screen bg-background flex items-center justify-center p-4">
        <div className="text-center max-w-md space-y-6">
          <div className="w-20 h-20 bg-emerald-500/10 rounded-full flex items-center justify-center mx-auto border border-emerald-500/20">
            <CheckCircle2 size={40} className="text-emerald-500" />
          </div>
          <div className="space-y-2">
            <h2 className="text-2xl font-bold text-on-surface">Đặt hàng thành công!</h2>
            <p className="text-on-surface-variant text-sm">Đơn hàng COD của bạn đã được ghi nhận. Nhân viên sẽ liên hệ xác nhận sớm nhất.</p>
            <p className="text-xs text-on-surface-variant font-mono bg-surface-container px-3 py-1.5 rounded-full inline-block">#{successOrderId}</p>
          </div>
          <div className="flex gap-3 justify-center">
            <Link to="/" className="px-5 py-2.5 border border-outline-variant text-on-surface rounded-xl text-sm font-semibold hover:bg-surface-container transition-all">
              Tiếp tục mua sắm
            </Link>
            <Link to="/profile" className="px-5 py-2.5 bg-primary text-white rounded-xl text-sm font-semibold hover:bg-primary-container transition-all shadow-lg shadow-primary/20">
              Theo dõi đơn hàng
            </Link>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-background text-on-surface font-sans">
      {/* Modals */}
      {momoModal && (
        <MomoModal
          orderId={momoModal.orderId}
          amount={momoModal.amount}
          onSuccess={() => { setMomoModal(null); setSuccessOrderId(momoModal.orderId); }}
          onClose={() => setMomoModal(null)}
        />
      )}
      {vietqrModal && (
        <VietQRModal
          orderId={vietqrModal.orderId}
          amount={vietqrModal.amount}
          onClose={() => { setVietqrModal(null); navigate('/profile'); }}
        />
      )}

      {/* Nav */}
      <nav className="sticky top-0 z-40 bg-surface/80 backdrop-blur-md border-b border-outline-variant/30 py-4 px-4 md:px-12">
        <div className="max-w-[1440px] mx-auto flex items-center justify-between">
          <Link to="/" className="flex items-center gap-2 text-on-surface-variant hover:text-primary transition-colors">
            <ArrowLeft size={18} />
            <span className="text-sm font-medium">Tiếp tục mua sắm</span>
          </Link>
          <div className="flex items-center gap-3">
            <ShoppingBag size={20} className="text-primary" />
            <span className="font-bold text-on-surface">Giỏ hàng</span>
            {cartItems.length > 0 && (
              <span className="bg-primary text-white text-[10px] font-bold px-2 py-0.5 rounded-full">{cartItems.length}</span>
            )}
          </div>
          <div className="w-28" />
        </div>
      </nav>

      <main className="max-w-[1440px] mx-auto px-4 md:px-12 py-10">
        {cartItems.length === 0 ? (
          <div className="text-center py-24 space-y-5">
            <div className="w-16 h-16 bg-surface-container-low rounded-full flex items-center justify-center mx-auto border border-outline-variant/30">
              <ShoppingBag size={28} className="text-on-surface-variant opacity-50" />
            </div>
            <h3 className="text-xl font-bold text-on-surface">Giỏ hàng trống</h3>
            <p className="text-sm text-on-surface-variant">Hãy thêm sản phẩm để tiếp tục mua sắm.</p>
            <Link to="/" className="inline-flex items-center gap-2 bg-primary text-white px-6 py-3 rounded-xl font-semibold text-sm hover:bg-primary-container transition-all shadow-lg shadow-primary/20">
              Khám phá sản phẩm <ChevronRight size={16} />
            </Link>
          </div>
        ) : (
          <div className="grid grid-cols-1 lg:grid-cols-3 gap-8 xl:gap-12">
            {/* Cart Items */}
            <div className="lg:col-span-2 space-y-3">
              <h2 className="text-xl font-bold text-on-surface mb-5">Sản phẩm đã chọn</h2>
              {cartItems.map(item => (
                <div key={item.id} className="bg-surface-container-lowest border border-outline-variant/30 rounded-2xl p-4 flex gap-4 hover:border-primary/20 transition-all group">
                  <div className="w-16 h-16 rounded-xl overflow-hidden bg-surface-container-low flex-shrink-0 flex items-center justify-center">
                    <img src={getProductImage(item.name)} alt={item.name} className="w-full h-full object-contain p-1" />
                  </div>
                  <div className="flex-1 min-w-0">
                    <h4 className="font-semibold text-on-surface text-sm line-clamp-1">{item.name}</h4>
                    <p className="text-primary font-bold text-sm mt-0.5">{item.price.toLocaleString()}đ</p>
                  </div>
                  <div className="flex items-center gap-2 flex-shrink-0">
                    <div className="flex items-center gap-1 bg-surface-container rounded-full">
                      <button
                        onClick={() => handleQtyChange(item.id, -1)}
                        className="w-7 h-7 rounded-full flex items-center justify-center text-on-surface-variant hover:bg-surface-container-high hover:text-primary transition-all font-bold text-lg"
                      >−</button>
                      <span className="text-sm font-bold text-on-surface w-6 text-center">{item.quantity}</span>
                      <button
                        onClick={() => handleQtyChange(item.id, 1)}
                        className="w-7 h-7 rounded-full flex items-center justify-center text-on-surface-variant hover:bg-surface-container-high hover:text-primary transition-all font-bold text-lg"
                      >+</button>
                    </div>
                    <span className="font-bold text-on-surface text-sm w-20 text-right">{(item.price * item.quantity).toLocaleString()}đ</span>
                    <button onClick={() => handleRemove(item.id)} className="text-rose-400 hover:text-rose-600 transition-colors p-1">
                      <Trash2 size={15} />
                    </button>
                  </div>
                </div>
              ))}
            </div>

            {/* Checkout Panel */}
            <div className="space-y-5">
              {/* Payment Method */}
              <div className="bg-surface-container-lowest border border-outline-variant/30 rounded-2xl p-5 space-y-4">
                <h3 className="font-bold text-on-surface text-sm">Phương thức thanh toán</h3>
                <div className="space-y-2">
                  {PAYMENT_METHODS.map(method => {
                    const Icon = method.icon;
                    const isSelected = paymentMethod === method.id;
                    return (
                      <button
                        key={method.id}
                        onClick={() => setPaymentMethod(method.id)}
                        className={`w-full flex items-center gap-3 p-3 rounded-xl border transition-all text-left ${isSelected ? `${method.bg} border-2` : 'border border-outline-variant/30 hover:bg-surface-container-low'}`}
                      >
                        <div className={`w-8 h-8 rounded-lg flex items-center justify-center ${isSelected ? 'bg-white/50' : 'bg-surface-container-low'}`}>
                          <Icon size={16} className={isSelected ? method.color : 'text-on-surface-variant'} />
                        </div>
                        <div className="flex-1">
                          <p className={`text-sm font-semibold ${isSelected ? method.color : 'text-on-surface'}`}>{method.label}</p>
                          <p className="text-[10px] text-on-surface-variant">{method.sublabel}</p>
                        </div>
                        <div className={`w-4 h-4 rounded-full border-2 flex items-center justify-center ${isSelected ? 'border-primary bg-primary' : 'border-outline'}`}>
                          {isSelected && <div className="w-1.5 h-1.5 bg-white rounded-full" />}
                        </div>
                      </button>
                    );
                  })}
                </div>
              </div>

              {/* Order Summary */}
              <div className="bg-surface-container-lowest border border-outline-variant/30 rounded-2xl p-5 space-y-4">
                <h3 className="font-bold text-on-surface text-sm">Tóm tắt đơn hàng</h3>
                <div className="space-y-2 text-sm">
                  <div className="flex justify-between text-on-surface-variant">
                    <span>Tạm tính ({cartItems.reduce((s, i) => s + i.quantity, 0)} sản phẩm)</span>
                    <span>{totalAmount.toLocaleString()}đ</span>
                  </div>
                  <div className="flex justify-between text-on-surface-variant">
                    <span>Phí vận chuyển</span>
                    <span className="text-emerald-600 font-semibold">Miễn phí</span>
                  </div>
                  <div className="border-t border-outline-variant/30 pt-3 flex justify-between items-baseline">
                    <span className="font-bold text-on-surface">Tổng cộng</span>
                    <span className="text-xl font-extrabold text-primary">{totalAmount.toLocaleString()}đ</span>
                  </div>
                </div>

                <button
                  onClick={handleCheckout}
                  disabled={loading}
                  className="w-full bg-primary text-white py-4 rounded-2xl font-bold text-sm hover:bg-primary-container transition-all shadow-lg shadow-primary/20 hover:shadow-xl hover:shadow-primary/30 active:scale-95 disabled:opacity-60 flex items-center justify-center gap-2"
                >
                  {loading ? (
                    <><div className="animate-spin rounded-full h-4 w-4 border-b-2 border-white" /> Đang xử lý...</>
                  ) : paymentMethod === 'COD' ? (
                    <><Truck size={16} /> Đặt hàng (COD)</>
                  ) : paymentMethod === 'MoMo' ? (
                    <><QrCode size={16} /> Thanh toán qua MoMo</>
                  ) : (
                    <><CreditCard size={16} /> Tạo mã QR chuyển khoản</>
                  )}
                </button>
                <p className="text-center text-[10px] text-on-surface-variant flex items-center justify-center gap-1">
                  <AlertCircle size={10} /> Bằng cách đặt hàng, bạn đồng ý với điều khoản dịch vụ của Lumina Tech
                </p>
              </div>

              {/* Trust badges */}
              <div className="grid grid-cols-3 gap-2">
                {[
                  { icon: Truck, label: 'Giao hàng nhanh', sub: '2-3 ngày' },
                  { icon: Package, label: 'Đóng gói chuẩn', sub: '100% an toàn' },
                  { icon: CheckCircle2, label: 'Đổi trả', sub: '30 ngày' },
                ].map((b, i) => {
                  const Icon = b.icon;
                  return (
                    <div key={i} className="bg-surface-container-low rounded-xl p-3 text-center space-y-1">
                      <Icon size={16} className="mx-auto text-primary" />
                      <p className="text-[10px] font-semibold text-on-surface">{b.label}</p>
                      <p className="text-[9px] text-on-surface-variant">{b.sub}</p>
                    </div>
                  );
                })}
              </div>
            </div>
          </div>
        )}
      </main>
    </div>
  );
}