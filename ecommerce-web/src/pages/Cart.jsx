import React, { useState, useEffect, useCallback } from 'react';
import { getCart, clearCart, removeFromCart, updateQuantity, updateCartItemVariant, getCartItemKey } from '../api/cartStore';
import api from '../api/axios';
import { Link, useNavigate } from 'react-router-dom';
import {
  ShoppingBag, Trash2, CreditCard, ArrowLeft, X, CheckCircle2,
  Truck, Banknote, QrCode, Clock, ChevronRight, Package, AlertCircle, SlidersHorizontal, Tag, MapPin
} from 'lucide-react';
import Swal from 'sweetalert2';

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
    id: 'PayOS',
    label: 'Chuyển khoản VietQR',
    sublabel: 'Thanh toán an toàn qua PayOS',
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


export default function Cart() {
  const navigate = useNavigate();
  const [cartItems, setCartItems] = useState([]);
  const [paymentMethod, setPaymentMethod] = useState('COD');
  const [loading, setLoading] = useState(false);
  const [successOrderId, setSuccessOrderId] = useState(null);

  const [recipientName, setRecipientName] = useState('');
  const [recipientPhone, setRecipientPhone] = useState('');
  const [shippingAddress, setShippingAddress] = useState('');
  const [savedAddressesList, setSavedAddressesList] = useState([]);
  const [selectedAddressType, setSelectedAddressType] = useState('EXISTING'); // 'EXISTING' or 'NEW'

  const [voucherCode, setVoucherCode] = useState('');
  const [appliedVoucher, setAppliedVoucher] = useState(null);
  const [voucherError, setVoucherError] = useState('');

  useEffect(() => {
    setCartItems(getCart());
    const fetchUser = async () => {
      try {
        const token = localStorage.getItem('token');
        if (!token) return;
        const res = await api.get('/auth/me');
        if (res.data) {
          setRecipientName(res.data.fullName || '');
          setRecipientPhone(res.data.phoneNumber || '');
          
          let firstAddr = res.data.address || '';
          let list = [];
          if (res.data.savedAddresses) {
            try {
               const parsed = JSON.parse(res.data.savedAddresses);
               if (parsed && parsed.length > 0) {
                   list = parsed;
                   firstAddr = parsed[0];
               }
            } catch(e){}
          }
          setSavedAddressesList(list);
          if (list.length > 0) {
              setShippingAddress(firstAddr);
              setSelectedAddressType(firstAddr);
          } else {
              setShippingAddress('');
              setSelectedAddressType('NEW');
          }
        }
      } catch (err) {}
    }
    fetchUser();
  }, []);

  const totalAmount = cartItems.reduce((sum, item) => sum + (item.price * item.quantity), 0);

  const handleRemove = (cartItemKey) => {
    removeFromCart(cartItemKey);
    setCartItems(getCart());
  };

  const handleQtyChange = (cartItemKey, delta) => {
    const item = cartItems.find(i => getCartItemKey(i) === cartItemKey);
    if (!item) return;
    const newQty = item.quantity + delta;
    if (newQty < 1) return;
    updateQuantity(cartItemKey, newQty);
    setCartItems(getCart());
  };

  const handleVariantChange = (cartItemKey, key, value) => {
    const item = cartItems.find(i => getCartItemKey(i) === cartItemKey);
    if (!item || !item.variants) return;
    
    const currentAttr = item.variantAttributes || {};
    const nextAttr = { ...currentAttr, [key]: value };
    
    let newVariant = item.variants.find(v => v.attributes && Object.entries(nextAttr).every(([k, v2]) => v.attributes[k] === v2));
    
    if (!newVariant) {
        newVariant = item.variants.find(v => v.attributes && v.attributes[key] === value);
    }
    
    if (newVariant) {
        updateCartItemVariant(cartItemKey, newVariant);
        setCartItems(getCart());
    }
  };

  const handleApplyVoucher = async () => {
    if (!voucherCode.trim()) return;
    setVoucherError('');
    try {
      const res = await api.post('/Vouchers/apply', { code: voucherCode, orderTotal: totalAmount });
      if (res.data && res.data.data) {
        setAppliedVoucher(res.data.data);
      }
    } catch (error) {
      setVoucherError(error.response?.data?.message || 'Mã giảm giá không hợp lệ');
      setAppliedVoucher(null);
    }
  };

  const removeVoucher = () => {
    setAppliedVoucher(null);
    setVoucherCode('');
    setVoucherError('');
  };

  const handleCheckout = async () => {
    if (cartItems.length === 0) return;
    const token = localStorage.getItem('token');
    if (!token) { navigate('/login'); return; }

    if (!recipientName.trim() || !recipientPhone.trim() || !shippingAddress.trim()) {
      Swal.fire({ icon: 'warning', title: 'Thiếu thông tin', text: 'Vui lòng điền đầy đủ thông tin giao hàng!' });
      return;
    }

    setLoading(true);
    try {
      const orderData = {
        items: cartItems.map(item => ({ productId: item.id, productVariantId: item.variantId || null, quantity: item.quantity })),
        note: paymentMethod === 'COD' ? 'COD' : paymentMethod,
        recipientName,
        recipientPhone,
        shippingAddress,
        paymentMethod,
        voucherId: appliedVoucher ? appliedVoucher.voucherId : null
      };

      const res = await api.post('/Orders', orderData);
      const orderId = res.data.orderId;

      clearCart();
      setCartItems([]);

      if (paymentMethod === 'COD') {
        setSuccessOrderId(orderId);
      } else {
        const paymentRes = await api.post('/payments/create', {
          orderId,
          provider: paymentMethod,
          returnUrl: window.location.origin + '/payment-result'
        });
        if (paymentRes.data?.paymentUrl) {
          window.location.href = paymentRes.data.paymentUrl;
        }
      }
    } catch (error) {
      Swal.fire({ icon: 'error', title: 'Lỗi đặt hàng', text: error.response?.data?.message || error.response?.data || 'Vui lòng thử lại' });
    } finally {
      setLoading(false);
    }
  };

  const getProductImage = (item) => {
    if (item?.imageUrls && item.imageUrls.length > 0) {
      const url = item.imageUrls[0];
      return url.startsWith('http') ? url : `http://localhost:5092${url}`;
    }
    const name = item?.name || '';
    const n = name.toLowerCase();
    if (n.includes('laptop') || n.includes('macbook') || n.includes('pro')) return 'https://lh3.googleusercontent.com/aida-public/AB6AXuDzzkgw3qdK2qx_eejr9Oee4qzRfJoNIb-smYiUqNBNBZ4_KNAbm4HxoqNIyfUqk9pV0qWkdyFf7t7SYsjbbwCkkEN06FQNQBCseQPzXacixmLlO0YB5GVfxd7AR42kwnUufnptDgHCXnHlGXhg3x4QzV7sXZkMAYLQHcoPSLjWbTIWG3W_hrzh4eWEuFkuhEuk_62jmY9dMbWUMr11EIDivHK_RMuJDGpKxGyfr8JRcfcL8i0qv6Ve';
    if (n.includes('keyboard') || n.includes('mouse') || n.includes('gaming')) return 'https://lh3.googleusercontent.com/aida-public/AB6AXuCtePE6a8azsOINxsGhPHJGa7pybuOEtEXGQUTmgTTPhKMBwOS-7FOpWVDKTgo17TVnuDYz0tGqUltaJ9vPZxqJCWCdknYb9x1VpLfsq9eC8Qlc1fG2cjNg5i2klZdqsM6d2QHuBDP_mHVRg-Ley5Dw6z3yNJURrfQ5bnPx_TyxzBc7EuP7b5boquCcOezT3SWcmEEoMElGb7VQSAkNtos5xtMcNxJFn8D0-Oq4x3zH0vgAz5bRpJdx';
    if (n.includes('bud') || n.includes('headphone') || n.includes('tai nghe')) return 'https://lh3.googleusercontent.com/aida-public/AB6AXujCnwa9YYlaySQHciXXGs5ENqNsPlYyM48pBFebzeWQc7dPKrvdRI9-hr5S5mxpSSR3ZK689MtFW5CFO2yl2XF3_1RCn3iwdnmtwXFQeVUo_FXR0vOQ7FYE_qzsTZb4Q8_zaMowCCLDFa84jAXRV-XqKV2AZPbY2fWUotHuBFbc9Jv95ESTZuet5JJdQVxXrmhm4ItvrDA3BDDkW6wfXdjWtO5ynIppnxJllrxffafcwXaW4XKodrR';
    return 'https://lh3.googleusercontent.com/aida-public/AB6AXuDzzkgw3qdK2qx_eejr9Oee4qzRfJoNIb-smYiUqNBNBZ4_KNAbm4HxoqNIyfUqk9pV0qWkdyFf7t7SYsjbbwCkkEN06FQNQBCseQPzXacixmLlO0YB5GVfxd7AR42kwnUufnptDgHCXnHlGXhg3x4QzV7sXZkMAYLQHcoPSLjWbTIWG3W_hrzh4eWEuFkuhEuk_62jmY9dMbWUMr11EIDivHK_RMuJDGpKxGyfr8JRcfcL8i0qv6Ve';
  };

  const getVariantOptions = (item) => {
    if (!item.variants) return {};
    const groups = {};
    item.variants.forEach(v => {
      if (v.attributes) {
        Object.entries(v.attributes).forEach(([key, value]) => {
          if (!groups[key]) groups[key] = new Set();
          groups[key].add(value);
        });
      }
    });
    const result = {};
    Object.keys(groups).forEach(key => {
      result[key] = Array.from(groups[key]);
    });
    return result;
  };

  const getVariantLabel = (key) => ({
    color: 'Màu',
    ram: 'RAM',
    ssd: 'Ổ cứng',
    storage: 'Bộ nhớ',
    switch: 'Switch',
    edition: 'Phiên bản',
    config: 'Cấu hình',
    weight: 'Trọng lượng'
  }[key] || key);

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
              {cartItems.map(item => {
                const cartItemKey = getCartItemKey(item);
                const variantOptions = getVariantOptions(item);
                return (
                  <div key={cartItemKey} className="bg-surface-container-lowest border border-outline-variant/30 rounded-2xl p-4 flex flex-col sm:flex-row gap-4 hover:border-primary/20 transition-all group">
                    <div className="w-16 h-16 rounded-xl overflow-hidden bg-surface-container-low flex-shrink-0 flex items-center justify-center">
                      <img src={getProductImage(item)} alt={item.name} className="w-full h-full object-contain p-1" />
                    </div>
                    <div className="flex-1 min-w-0 space-y-3">
                      <div>
                        <h4 className="font-semibold text-on-surface text-sm line-clamp-1">{item.name}</h4>
                        <p className="text-primary font-bold text-sm mt-0.5">{item.price.toLocaleString()}đ</p>
                      </div>
                      {Object.keys(variantOptions).length > 0 && (
                        <div className="rounded-2xl bg-surface-container-low border border-outline-variant/20 p-3 space-y-2">
                          <div className="flex items-center gap-1.5 text-[10px] font-bold uppercase tracking-wider text-on-surface-variant">
                            <SlidersHorizontal size={12} className="text-primary" /> Tùy chọn sản phẩm
                          </div>
                          <div className="grid grid-cols-1 sm:grid-cols-3 gap-2">
                            {Object.entries(variantOptions).map(([key, options]) => (
                              <label key={key} className="space-y-1">
                                <span className="text-[10px] font-semibold text-on-surface-variant">{getVariantLabel(key)}</span>
                                <select
                                  value={item.variantAttributes?.[key] || options[0]}
                                  onChange={(e) => handleVariantChange(cartItemKey, key, e.target.value)}
                                  className="w-full bg-surface-container-lowest border border-outline-variant/30 rounded-xl px-2.5 py-2 text-xs text-on-surface focus:border-primary focus:ring-1 focus:ring-primary outline-none"
                                >
                                  {options.map(option => <option key={option} value={option}>{option}</option>)}
                                </select>
                              </label>
                            ))}
                          </div>
                        </div>
                      )}
                    </div>
                    <div className="flex items-center gap-2 flex-shrink-0 sm:self-start">
                      <div className="flex items-center gap-1 bg-surface-container rounded-full">
                        <button
                          onClick={() => handleQtyChange(cartItemKey, -1)}
                          className="w-7 h-7 rounded-full flex items-center justify-center text-on-surface-variant hover:bg-surface-container-high hover:text-primary transition-all font-bold text-lg"
                        >−</button>
                        <span className="text-sm font-bold text-on-surface w-6 text-center">{item.quantity}</span>
                        <button
                          onClick={() => handleQtyChange(cartItemKey, 1)}
                          className="w-7 h-7 rounded-full flex items-center justify-center text-on-surface-variant hover:bg-surface-container-high hover:text-primary transition-all font-bold text-lg"
                        >+</button>
                      </div>
                      <span className="font-bold text-on-surface text-sm w-20 text-right">{(item.price * item.quantity).toLocaleString()}đ</span>
                      <button onClick={() => handleRemove(cartItemKey)} className="text-rose-400 hover:text-rose-600 transition-colors p-1">
                        <Trash2 size={15} />
                      </button>
                    </div>
                  </div>
                );
              })}
            </div>

            {/* Checkout Panel */}
            <div className="space-y-5">
              {/* Shipping Info */}
              <div className="bg-surface-container-lowest border border-outline-variant/30 rounded-2xl p-5 space-y-4">
                <h3 className="font-bold text-on-surface text-sm">Thông tin giao hàng</h3>
                <div className="space-y-3">
                  <div>
                    <label className="block text-[10px] font-bold text-on-surface-variant uppercase tracking-wider mb-1">Người nhận</label>
                    <input
                      type="text"
                      value={recipientName}
                      onChange={e => setRecipientName(e.target.value)}
                      placeholder="Họ và tên người nhận"
                      className="w-full bg-surface-container-low border border-outline-variant/30 text-on-surface rounded-xl px-3 py-2.5 text-sm focus:border-primary focus:ring-1 focus:ring-primary outline-none transition-all"
                    />
                  </div>
                  <div>
                    <label className="block text-[10px] font-bold text-on-surface-variant uppercase tracking-wider mb-1">Số điện thoại</label>
                    <input
                      type="text"
                      value={recipientPhone}
                      onChange={e => setRecipientPhone(e.target.value)}
                      placeholder="Số điện thoại liên hệ"
                      className="w-full bg-surface-container-low border border-outline-variant/30 text-on-surface rounded-xl px-3 py-2.5 text-sm focus:border-primary focus:ring-1 focus:ring-primary outline-none transition-all"
                    />
                  </div>
                  <div>
                    <label className="block text-[10px] font-bold text-on-surface-variant uppercase tracking-wider mb-2">Địa chỉ giao hàng</label>
                    
                    {savedAddressesList.length > 0 && (
                      <div className="space-y-2 mb-3">
                        {savedAddressesList.map((addr, idx) => (
                          <label key={idx} className={`flex items-start gap-3 p-3 rounded-xl border cursor-pointer transition-all ${selectedAddressType === addr ? 'bg-primary/5 border-primary/40' : 'border-outline-variant/30 hover:bg-surface-container-low'}`}>
                            <input 
                              type="radio" 
                              name="addressSelection" 
                              className="mt-1 accent-primary" 
                              checked={selectedAddressType === addr}
                              onChange={() => {
                                setSelectedAddressType(addr);
                                setShippingAddress(addr);
                              }}
                            />
                            <span className="text-sm text-on-surface flex-1 leading-relaxed">{addr}</span>
                          </label>
                        ))}
                        
                        <label className={`flex items-center gap-3 p-3 rounded-xl border cursor-pointer transition-all ${selectedAddressType === 'NEW' ? 'bg-primary/5 border-primary/40' : 'border-outline-variant/30 hover:bg-surface-container-low'}`}>
                          <input 
                            type="radio" 
                            name="addressSelection" 
                            className="accent-primary" 
                            checked={selectedAddressType === 'NEW'}
                            onChange={() => {
                              setSelectedAddressType('NEW');
                              setShippingAddress('');
                            }}
                          />
                          <span className="text-sm text-on-surface font-medium flex items-center gap-1.5"><MapPin size={16}/> Nhập địa chỉ khác...</span>
                        </label>
                      </div>
                    )}

                    {(selectedAddressType === 'NEW' || savedAddressesList.length === 0) && (
                      <textarea
                        value={shippingAddress}
                        onChange={e => setShippingAddress(e.target.value)}
                        placeholder="Nhập chi tiết: Số nhà, tên đường, phường/xã, quận/huyện, tỉnh/thành phố..."
                        rows={2}
                        className="w-full bg-surface-container-lowest border border-outline-variant/40 text-on-surface rounded-xl px-4 py-3 text-sm focus:border-primary focus:ring-2 focus:ring-primary/50 outline-none transition-all resize-none placeholder:text-on-surface-variant/50 shadow-sm"
                      />
                    )}
                  </div>
                </div>
              </div>

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

              {/* Voucher Input */}
              <div className="bg-surface-container-lowest border border-outline-variant/30 rounded-2xl p-5 space-y-4">
                <h3 className="font-bold text-on-surface text-sm flex items-center gap-2"><Tag size={16} className="text-primary"/> Mã giảm giá</h3>
                {!appliedVoucher ? (
                  <div className="space-y-2">
                    <div className="flex gap-2">
                      <input
                        type="text"
                        value={voucherCode}
                        onChange={e => { setVoucherCode(e.target.value.toUpperCase()); setVoucherError(''); }}
                        placeholder="Nhập mã giảm giá"
                        className="flex-1 bg-surface-container-low border border-outline-variant/30 text-on-surface rounded-xl px-3 py-2 text-sm focus:border-primary focus:ring-1 focus:ring-primary outline-none transition-all uppercase"
                      />
                      <button onClick={handleApplyVoucher} className="px-4 py-2 bg-primary text-white rounded-xl text-sm font-semibold hover:bg-primary-container transition-colors">
                        Áp dụng
                      </button>
                    </div>
                    {voucherError && <p className="text-xs text-rose-500 font-medium">{voucherError}</p>}
                  </div>
                ) : (
                  <div className="flex items-center justify-between bg-emerald-500/10 border border-emerald-500/20 rounded-xl p-3">
                    <div className="flex items-center gap-2">
                      <div className="w-8 h-8 rounded-full bg-emerald-500/20 flex items-center justify-center">
                        <CheckCircle2 size={16} className="text-emerald-600" />
                      </div>
                      <div>
                        <p className="text-sm font-bold text-emerald-700">{appliedVoucher.code}</p>
                        <p className="text-[10px] text-emerald-600">Đã giảm {appliedVoucher.discountAmount.toLocaleString()}đ</p>
                      </div>
                    </div>
                    <button onClick={removeVoucher} className="text-on-surface-variant hover:text-rose-500 p-1 transition-colors">
                      <X size={16} />
                    </button>
                  </div>
                )}
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
                  {appliedVoucher && (
                    <div className="flex justify-between text-emerald-600">
                      <span>Giảm giá Voucher</span>
                      <span className="font-semibold">-{appliedVoucher.discountAmount.toLocaleString()}đ</span>
                    </div>
                  )}
                  <div className="border-t border-outline-variant/30 pt-3 flex justify-between items-baseline">
                    <span className="font-bold text-on-surface">Tổng cộng</span>
                    <span className="text-xl font-extrabold text-primary">{(appliedVoucher ? appliedVoucher.newTotal : totalAmount).toLocaleString()}đ</span>
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
                  <AlertCircle size={10} /> Bằng cách đặt hàng, bạn đồng ý với điều khoản dịch vụ của iLuminaty Shop
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
