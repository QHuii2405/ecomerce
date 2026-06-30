import React, { useEffect, useState } from 'react';
import api from '../api/axios';
import { Link } from 'react-router-dom';
import {
  User, Package, MapPin, Phone, Mail, ArrowLeft,
  ChevronDown, ChevronUp, Truck, CheckCircle2, Clock,
  XCircle, ShoppingBag, AlertCircle, RefreshCw
} from 'lucide-react';

// Delivery tracking timeline config
const ORDER_STEPS = [
  { key: 'Pending',   label: 'Đã đặt hàng',     icon: ShoppingBag,  desc: 'Đơn hàng đang chờ xác nhận' },
  { key: 'Confirmed', label: 'Đã xác nhận',      icon: CheckCircle2, desc: 'Đơn hàng đã được xác nhận' },
  { key: 'Shipping',  label: 'Đang giao hàng',   icon: Truck,        desc: 'Đơn hàng đang trên đường giao' },
  { key: 'Delivered', label: 'Đã nhận hàng',     icon: CheckCircle2, desc: 'Giao hàng thành công' },
];

const STATUS_ORDER = ['Pending', 'Confirmed', 'Shipping', 'Delivered'];

function getStatusIndex(status) {
  return STATUS_ORDER.indexOf(status);
}

const STATUS_CONFIG = {
  Pending:   { label: 'Chờ xử lý',     cls: 'bg-amber-500/10 text-amber-600 border-amber-500/20' },
  Confirmed: { label: 'Đã xác nhận',   cls: 'bg-blue-500/10 text-blue-600 border-blue-500/20' },
  Shipping:  { label: 'Đang giao',     cls: 'bg-indigo-500/10 text-indigo-600 border-indigo-500/20' },
  Delivered: { label: 'Đã giao',       cls: 'bg-emerald-500/10 text-emerald-600 border-emerald-500/20' },
  Cancelled: { label: 'Đã hủy',        cls: 'bg-rose-500/10 text-rose-600 border-rose-500/20' },
};

function StatusBadge({ status }) {
  const c = STATUS_CONFIG[status] || STATUS_CONFIG.Pending;
  return (
    <span className={`text-[10px] font-bold uppercase tracking-wider px-2.5 py-1 rounded-full border ${c.cls}`}>
      {c.label}
    </span>
  );
}

function DeliveryTimeline({ status }) {
  if (status === 'Cancelled') {
    return (
      <div className="flex items-center gap-2 mt-4 p-3 bg-rose-500/5 border border-rose-500/20 rounded-xl">
        <XCircle size={16} className="text-rose-500 flex-shrink-0" />
        <span className="text-xs text-rose-600 font-medium">Đơn hàng đã bị hủy</span>
      </div>
    );
  }

  const currentIdx = getStatusIndex(status);

  return (
    <div className="mt-4 pt-4 border-t border-outline-variant/20">
      <p className="text-[10px] font-bold uppercase tracking-widest text-on-surface-variant mb-4">Trạng thái giao hàng</p>
      <div className="relative">
        {/* Progress Line */}
        <div className="absolute left-4 top-4 bottom-4 w-0.5 bg-outline-variant/30" />
        <div
          className="absolute left-4 top-4 w-0.5 bg-primary transition-all duration-700"
          style={{ height: `${Math.max(0, (currentIdx / (ORDER_STEPS.length - 1)) * 100)}%` }}
        />

        <div className="space-y-4">
          {ORDER_STEPS.map((step, idx) => {
            const Icon = step.icon;
            const isDone = idx <= currentIdx;
            const isCurrent = idx === currentIdx;

            return (
              <div key={step.key} className="flex items-start gap-4 relative">
                <div className={`relative z-10 w-8 h-8 rounded-full flex items-center justify-center flex-shrink-0 transition-all duration-500 ${
                  isDone
                    ? isCurrent
                      ? 'bg-primary text-white shadow-md shadow-primary/30 ring-2 ring-primary/20'
                      : 'bg-primary/10 text-primary'
                    : 'bg-surface-container border border-outline-variant/30 text-on-surface-variant'
                }`}>
                  <Icon size={14} />
                </div>
                <div className={`flex-1 pb-1 ${isDone ? '' : 'opacity-40'}`}>
                  <p className={`text-xs font-bold ${isCurrent ? 'text-primary' : isDone ? 'text-on-surface' : 'text-on-surface-variant'}`}>
                    {step.label}
                  </p>
                  <p className="text-[10px] text-on-surface-variant">{step.desc}</p>
                </div>
                {isCurrent && (
                  <span className="flex-shrink-0 flex items-center gap-1 text-[10px] text-primary bg-primary/10 px-2 py-0.5 rounded-full">
                    <div className="w-1.5 h-1.5 bg-primary rounded-full animate-pulse" />
                    Hiện tại
                  </span>
                )}
              </div>
            );
          })}
        </div>
      </div>
    </div>
  );
}

function OrderCard({ order, onCancel }) {
  const [expanded, setExpanded] = useState(false);
  const [cancelling, setCancelling] = useState(false);

  const canCancel = order.status === 'Pending' || order.status === 'Confirmed';
  const formattedDate = new Date(order.createdAt).toLocaleDateString('vi-VN', {
    day: '2-digit', month: '2-digit', year: 'numeric', hour: '2-digit', minute: '2-digit'
  });

  const handleCancel = async () => {
    if (!window.confirm('Bạn có chắc muốn hủy đơn hàng này?')) return;
    setCancelling(true);
    try {
      await api.post(`/orders/${order.id}/cancel`);
      onCancel(order.id);
    } catch (err) {
      alert('Không thể hủy đơn: ' + (err.response?.data?.message || 'Thử lại sau'));
    } finally {
      setCancelling(false);
    }
  };

  return (
    <div className="bg-surface-container-lowest border border-outline-variant/30 rounded-2xl overflow-hidden hover:border-primary/20 transition-all">
      {/* Order Header */}
      <div className="p-4 flex items-start justify-between gap-3">
        <div className="space-y-1 flex-1 min-w-0">
          <div className="flex items-center gap-2 flex-wrap">
            <StatusBadge status={order.status} />
            <span className="text-[10px] text-on-surface-variant">{formattedDate}</span>
          </div>
          <p className="text-xs font-mono text-on-surface-variant truncate">#{order.id}</p>
        </div>
        <div className="text-right flex-shrink-0">
          <p className="font-extrabold text-primary text-base">{order.totalAmount?.toLocaleString()}đ</p>
          <p className="text-[10px] text-on-surface-variant">{order.orderItems?.length || 0} sản phẩm</p>
        </div>
      </div>

      {/* Expand toggle */}
      <button
        onClick={() => setExpanded(!expanded)}
        className="w-full px-4 py-2.5 bg-surface-container-low border-t border-outline-variant/20 text-xs text-on-surface-variant flex items-center justify-between hover:bg-surface-container transition-colors"
      >
        <span className="font-medium">{expanded ? 'Thu gọn' : 'Xem chi tiết đơn hàng'}</span>
        {expanded ? <ChevronUp size={14} /> : <ChevronDown size={14} />}
      </button>

      {/* Expanded Content */}
      {expanded && (
        <div className="p-4 border-t border-outline-variant/20 space-y-4">
          {/* Order items */}
          {order.orderItems && order.orderItems.length > 0 && (
            <div className="space-y-2">
              <p className="text-[10px] font-bold uppercase tracking-widest text-on-surface-variant">Chi tiết sản phẩm</p>
              {order.orderItems.map((item, i) => (
                <div key={i} className="flex items-center justify-between py-2 border-b border-outline-variant/10 last:border-0">
                  <div className="flex-1 min-w-0">
                    <p className="text-xs font-semibold text-on-surface truncate">
                      {item.product?.name || `Sản phẩm #${i + 1}`}
                    </p>
                    <p className="text-[10px] text-on-surface-variant">
                      {item.unitPrice?.toLocaleString()}đ × {item.quantity}
                    </p>
                  </div>
                  <span className="text-xs font-bold text-on-surface flex-shrink-0 ml-3">
                    {(item.unitPrice * item.quantity).toLocaleString()}đ
                  </span>
                </div>
              ))}
            </div>
          )}

          {/* Delivery timeline */}
          <DeliveryTimeline status={order.status} />

          {/* Cancel button */}
          {canCancel && (
            <button
              onClick={handleCancel}
              disabled={cancelling}
              className="w-full py-2.5 border border-rose-400/50 text-rose-500 rounded-xl text-xs font-semibold hover:bg-rose-500/5 transition-all active:scale-95 disabled:opacity-50 flex items-center justify-center gap-2"
            >
              {cancelling ? <><div className="animate-spin rounded-full h-3 w-3 border-b-2 border-rose-500" /> Đang hủy...</> : <><XCircle size={14} /> Hủy đơn hàng</>}
            </button>
          )}
        </div>
      )}
    </div>
  );
}

export default function Profile() {
  const [user, setUser] = useState(null);
  const [orders, setOrders] = useState([]);
  const [loadingUser, setLoadingUser] = useState(true);
  const [loadingOrders, setLoadingOrders] = useState(true);
  const [activeTab, setActiveTab] = useState('orders');

  useEffect(() => {
    fetchUser();
    fetchOrders();
  }, []);

  const fetchUser = async () => {
    try {
      const res = await api.get('/auth/me');
      setUser(res.data);
    } catch {
      /* ignore */
    } finally {
      setLoadingUser(false);
    }
  };

  const fetchOrders = async () => {
    try {
      const res = await api.get('/orders/my-orders');
      setOrders(res.data || []);
    } catch {
      setOrders([]);
    } finally {
      setLoadingOrders(false);
    }
  };

  const handleCancelOrder = (cancelledId) => {
    setOrders(prev => prev.map(o => o.id === cancelledId ? { ...o, status: 'Cancelled' } : o));
  };

  const pendingCount = orders.filter(o => o.status === 'Pending' || o.status === 'Confirmed' || o.status === 'Shipping').length;

  return (
    <div className="min-h-screen bg-background text-on-surface font-sans">
      {/* Nav */}
      <nav className="sticky top-0 z-40 bg-surface/80 backdrop-blur-md border-b border-outline-variant/30 py-4 px-4 md:px-12">
        <div className="max-w-[1440px] mx-auto flex items-center justify-between">
          <Link to="/" className="flex items-center gap-2 text-on-surface-variant hover:text-primary transition-colors">
            <ArrowLeft size={18} />
            <span className="text-sm font-medium">Trang chủ</span>
          </Link>
          <span className="font-bold text-on-surface">Tài khoản của tôi</span>
          <div className="w-28" />
        </div>
      </nav>

      <main className="max-w-[1440px] mx-auto px-4 md:px-12 py-10">
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">

          {/* Left Panel — User Info */}
          <div className="space-y-5">
            {/* Avatar + Name */}
            <div className="bg-surface-container-lowest border border-outline-variant/30 rounded-2xl p-6 text-center space-y-3">
              <div className="w-16 h-16 bg-gradient-to-br from-primary to-primary-container rounded-full flex items-center justify-center mx-auto text-white font-bold text-2xl shadow-lg shadow-primary/20">
                {loadingUser ? '?' : (user?.fullName?.[0] || 'U')}
              </div>
              {loadingUser ? (
                <div className="space-y-2">
                  <div className="h-4 bg-surface-container-low rounded animate-pulse" />
                  <div className="h-3 bg-surface-container-low rounded w-3/4 mx-auto animate-pulse" />
                </div>
              ) : (
                <>
                  <div>
                    <h2 className="font-bold text-on-surface text-lg">{user?.fullName}</h2>
                    <p className="text-xs text-on-surface-variant">{user?.email}</p>
                  </div>
                  <span className={`inline-block text-[10px] font-bold uppercase tracking-wider px-3 py-1 rounded-full border ${
                    user?.role === 'Admin' ? 'bg-purple-500/10 text-purple-600 border-purple-500/20' :
                    user?.role === 'Staff' ? 'bg-blue-500/10 text-blue-600 border-blue-500/20' :
                    'bg-primary/10 text-primary border-primary/20'
                  }`}>
                    {user?.role}
                  </span>
                </>
              )}
            </div>

            {/* Info Card */}
            {!loadingUser && user && (
              <div className="bg-surface-container-lowest border border-outline-variant/30 rounded-2xl p-5 space-y-4">
                <h3 className="font-bold text-on-surface text-sm">Thông tin cá nhân</h3>
                <div className="space-y-3 text-sm">
                  <div className="flex items-start gap-3">
                    <Mail size={14} className="text-on-surface-variant mt-0.5 flex-shrink-0" />
                    <div>
                      <p className="text-[10px] text-on-surface-variant">Email</p>
                      <p className="font-medium text-on-surface">{user.email}</p>
                    </div>
                  </div>
                  {user.phoneNumber && (
                    <div className="flex items-start gap-3">
                      <Phone size={14} className="text-on-surface-variant mt-0.5 flex-shrink-0" />
                      <div>
                        <p className="text-[10px] text-on-surface-variant">Điện thoại</p>
                        <p className="font-medium text-on-surface">{user.phoneNumber}</p>
                      </div>
                    </div>
                  )}
                  {user.address && (
                    <div className="flex items-start gap-3">
                      <MapPin size={14} className="text-on-surface-variant mt-0.5 flex-shrink-0" />
                      <div>
                        <p className="text-[10px] text-on-surface-variant">Địa chỉ</p>
                        <p className="font-medium text-on-surface">{user.address}</p>
                      </div>
                    </div>
                  )}
                </div>
              </div>
            )}

            {/* Order stats */}
            <div className="grid grid-cols-2 gap-3">
              <div className="bg-surface-container-lowest border border-outline-variant/30 rounded-xl p-4 text-center">
                <p className="text-2xl font-extrabold text-primary">{orders.length}</p>
                <p className="text-[10px] text-on-surface-variant font-medium">Tổng đơn hàng</p>
              </div>
              <div className="bg-surface-container-lowest border border-outline-variant/30 rounded-xl p-4 text-center">
                <p className="text-2xl font-extrabold text-amber-500">{pendingCount}</p>
                <p className="text-[10px] text-on-surface-variant font-medium">Đang xử lý</p>
              </div>
            </div>
          </div>

          {/* Right Panel — Orders */}
          <div className="lg:col-span-2 space-y-5">
            <div className="flex items-center justify-between">
              <h2 className="font-bold text-on-surface text-xl">Lịch sử đơn hàng</h2>
              <button onClick={fetchOrders} className="text-primary hover:text-primary-container transition-colors">
                <RefreshCw size={16} />
              </button>
            </div>

            {loadingOrders ? (
              <div className="space-y-3">
                {[1, 2, 3].map(i => (
                  <div key={i} className="bg-surface-container-lowest border border-outline-variant/30 rounded-2xl p-4 animate-pulse">
                    <div className="flex justify-between">
                      <div className="space-y-2 flex-1">
                        <div className="h-3 bg-surface-container-low rounded w-24" />
                        <div className="h-2 bg-surface-container-low rounded w-48" />
                      </div>
                      <div className="h-5 bg-surface-container-low rounded w-20" />
                    </div>
                  </div>
                ))}
              </div>
            ) : orders.length === 0 ? (
              <div className="bg-surface-container-lowest border border-outline-variant/30 rounded-2xl p-12 text-center space-y-4">
                <Package size={36} className="mx-auto text-on-surface-variant opacity-30" />
                <h3 className="font-bold text-on-surface">Chưa có đơn hàng nào</h3>
                <p className="text-sm text-on-surface-variant">Hãy khám phá sản phẩm và đặt đơn hàng đầu tiên!</p>
                <Link to="/" className="inline-flex items-center gap-2 bg-primary text-white px-5 py-2.5 rounded-xl text-sm font-semibold hover:bg-primary-container transition-all shadow-lg shadow-primary/20">
                  Mua sắm ngay
                </Link>
              </div>
            ) : (
              <div className="space-y-4">
                {orders.map(order => (
                  <OrderCard key={order.id} order={order} onCancel={handleCancelOrder} />
                ))}
              </div>
            )}
          </div>
        </div>
      </main>
    </div>
  );
}