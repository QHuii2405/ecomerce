import React, { useEffect, useState } from 'react';
import api from '../api/axios';
import { Link, useNavigate } from 'react-router-dom';
import {
  User, Package, MapPin, Phone, Mail, ArrowLeft,
  ChevronDown, ChevronUp, Truck, CheckCircle2, Clock,
  XCircle, ShoppingBag, AlertCircle, RefreshCw, Save, Plus, Trash2, Edit, Camera, Heart, ShoppingCart
} from 'lucide-react';

// Delivery tracking timeline config
const ORDER_STEPS = [
  { key: 'Pending', label: 'Đã đặt hàng', icon: ShoppingBag, desc: 'Đơn hàng đang chờ xác nhận' },
  { key: 'Confirmed', label: 'Đã xác nhận', icon: CheckCircle2, desc: 'Đơn hàng đã được xác nhận' },
  { key: 'Shipping', label: 'Đang giao hàng', icon: Truck, desc: 'Đơn hàng đang trên đường giao' },
  { key: 'Delivered', label: 'Đã nhận hàng', icon: CheckCircle2, desc: 'Giao hàng thành công' },
];

const STATUS_ORDER = ['Pending', 'Confirmed', 'Shipping', 'Delivered'];

function getStatusIndex(status) {
  return STATUS_ORDER.indexOf(status);
}

const STATUS_CONFIG = {
  Pending: { label: 'Chờ xử lý', cls: 'bg-amber-500/10 text-amber-600 border-amber-500/20' },
  Confirmed: { label: 'Đã xác nhận', cls: 'bg-blue-500/10 text-blue-600 border-blue-500/20' },
  Shipping: { label: 'Đang giao', cls: 'bg-indigo-500/10 text-indigo-600 border-indigo-500/20' },
  Delivered: { label: 'Đã giao', cls: 'bg-emerald-500/10 text-emerald-600 border-emerald-500/20' },
  Cancelled: { label: 'Đã hủy', cls: 'bg-rose-500/10 text-rose-600 border-rose-500/20' },
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
                <div className={`relative z-10 w-8 h-8 rounded-full flex items-center justify-center flex-shrink-0 transition-all duration-500 ${isDone
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
  const navigate = useNavigate();
  const [user, setUser] = useState(null);
  const [orders, setOrders] = useState([]);
  const [loadingUser, setLoadingUser] = useState(true);
  const [loadingOrders, setLoadingOrders] = useState(true);
  const [activeTab, setActiveTab] = useState('info'); // 'info', 'addresses', 'orders', 'wishlist'
  
  const [wishlistItems, setWishlistItems] = useState([]);
  const [loadingWishlist, setLoadingWishlist] = useState(true);

  // Edit Profile States
  const [isEditing, setIsEditing] = useState(false);
  const [editForm, setEditForm] = useState({ fullName: '', phoneNumber: '', avatarUrl: '' });
  const [updating, setUpdating] = useState(false);

  // Address States
  const [addresses, setAddresses] = useState([]);
  const [addressParts, setAddressParts] = useState({ street: '', ward: '', district: '', city: '' });
  const [showAddAddress, setShowAddAddress] = useState(false);

  useEffect(() => {
    fetchUser();
    fetchOrders();
    fetchWishlist();
  }, []);

  const fetchUser = async () => {
    try {
      const res = await api.get('/auth/me');
      setUser(res.data);
      setEditForm({
        fullName: res.data.fullName || '',
        phoneNumber: res.data.phoneNumber || '',
        avatarUrl: res.data.avatarUrl || ''
      });
      if (res.data.savedAddresses) {
        try {
          setAddresses(JSON.parse(res.data.savedAddresses));
        } catch(e) {}
      }
    } catch {
      navigate('/login');
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

  const fetchWishlist = async () => {
    try {
      const res = await api.get('/Wishlist');
      setWishlistItems(res.data.data || []);
    } catch {
      setWishlistItems([]);
    } finally {
      setLoadingWishlist(false);
    }
  };

  const handleRemoveWishlist = async (productId) => {
    try {
      await api.post(`/Wishlist/${productId}/toggle`);
      setWishlistItems(prev => prev.filter(item => item.id !== productId));
    } catch (err) {}
  };

  const handleCancelOrder = (cancelledId) => {
    setOrders(prev => prev.map(o => o.id === cancelledId ? { ...o, status: 'Cancelled' } : o));
  };

  const handleUpdateProfile = async (e) => {
    e.preventDefault();
    setUpdating(true);
    try {
      await api.put('/auth/profile', editForm);
      setUser({ ...user, ...editForm });
      setIsEditing(false);
      alert('Cập nhật thông tin thành công!');
    } catch (err) {
      alert('Lỗi: ' + (err.response?.data?.message || 'Không thể cập nhật'));
    } finally {
      setUpdating(false);
    }
  };

  const handleAvatarChange = (e) => {
    const file = e.target.files[0];
    if (file) {
        if (file.size > 2 * 1024 * 1024) {
            alert('Vui lòng chọn ảnh nhỏ hơn 2MB');
            return;
        }
        const reader = new FileReader();
        reader.onloadend = () => {
            setEditForm({ ...editForm, avatarUrl: reader.result });
        };
        reader.readAsDataURL(file);
    }
  };

  const handleAddAddress = async () => {
    const { street, ward, district, city } = addressParts;
    if (!street.trim() || !ward.trim() || !district.trim() || !city.trim()) {
        alert('Vui lòng điền đầy đủ các phần của địa chỉ!');
        return;
    }
    const fullAddress = `${street.trim()}, ${ward.trim()}, ${district.trim()}, ${city.trim()}`;
    const updatedAddresses = [...addresses, fullAddress];
    try {
      await api.put('/auth/profile', { savedAddresses: JSON.stringify(updatedAddresses) });
      setAddresses(updatedAddresses);
      setAddressParts({ street: '', ward: '', district: '', city: '' });
      setShowAddAddress(false);
    } catch (err) {
      alert('Không thể thêm địa chỉ');
    }
  };

  const handleDeleteAddress = async (index) => {
    const updatedAddresses = addresses.filter((_, i) => i !== index);
    try {
      await api.put('/auth/profile', { savedAddresses: JSON.stringify(updatedAddresses) });
      setAddresses(updatedAddresses);
    } catch (err) {
      alert('Không thể xóa địa chỉ');
    }
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
          <div className="w-28 text-right">
             <button onClick={() => { localStorage.removeItem('token'); navigate('/login'); }} className="text-rose-500 text-sm font-semibold hover:underline">Đăng xuất</button>
          </div>
        </div>
      </nav>

      <main className="max-w-[1440px] mx-auto px-4 md:px-12 py-10">
        <div className="grid grid-cols-1 lg:grid-cols-4 gap-8">

          {/* Sidebar */}
          <div className="lg:col-span-1 space-y-5">
            {/* User Summary */}
            <div className="bg-surface-container-lowest border border-outline-variant/30 rounded-2xl p-6 text-center space-y-3">
              <div className="relative inline-block">
                {user?.avatarUrl ? (
                  <img src={user.avatarUrl} alt="Avatar" className="w-20 h-20 rounded-full object-cover border-2 border-primary/20 mx-auto" />
                ) : (
                  <div className="w-20 h-20 bg-gradient-to-br from-primary to-primary-container rounded-full flex items-center justify-center mx-auto text-white font-bold text-3xl shadow-lg shadow-primary/20">
                    {loadingUser ? '?' : (user?.fullName?.[0] || 'U')}
                  </div>
                )}
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
                  <span className={`inline-block text-[10px] font-bold uppercase tracking-wider px-3 py-1 rounded-full border ${user?.role === 'Admin' ? 'bg-purple-500/10 text-purple-600 border-purple-500/20' :
                      user?.role === 'Staff' ? 'bg-blue-500/10 text-blue-600 border-blue-500/20' :
                        'bg-primary/10 text-primary border-primary/20'
                    }`}>
                    {user?.role}
                  </span>
                </>
              )}
            </div>

            {/* Navigation Tabs */}
            <div className="bg-surface-container-lowest border border-outline-variant/30 rounded-2xl overflow-hidden">
              <button 
                onClick={() => setActiveTab('info')}
                className={`w-full flex items-center gap-3 px-5 py-4 text-sm font-semibold transition-colors ${activeTab === 'info' ? 'bg-primary/10 text-primary border-l-2 border-primary' : 'text-on-surface hover:bg-surface-container-low'}`}
              >
                <User size={18} /> Thông tin cá nhân
              </button>
              <button 
                onClick={() => setActiveTab('addresses')}
                className={`w-full flex items-center gap-3 px-5 py-4 text-sm font-semibold transition-colors ${activeTab === 'addresses' ? 'bg-primary/10 text-primary border-l-2 border-primary' : 'text-on-surface hover:bg-surface-container-low'}`}
              >
                <MapPin size={18} /> Địa chỉ giao hàng
              </button>
              <button 
                onClick={() => setActiveTab('orders')}
                className={`w-full flex items-center gap-3 px-5 py-4 text-sm font-semibold transition-colors ${activeTab === 'orders' ? 'bg-primary/10 text-primary border-l-2 border-primary' : 'text-on-surface hover:bg-surface-container-low'}`}
              >
                <ShoppingBag size={18} /> Lịch sử đơn hàng
              </button>
              <button 
                onClick={() => setActiveTab('wishlist')}
                className={`w-full flex items-center gap-3 px-5 py-4 text-sm font-semibold transition-colors ${activeTab === 'wishlist' ? 'bg-rose-500/10 text-rose-500 border-l-2 border-rose-500' : 'text-on-surface hover:bg-surface-container-low'}`}
              >
                <Heart size={18} /> Sản phẩm yêu thích
              </button>
            </div>
          </div>

          {/* Main Content Area */}
          <div className="lg:col-span-3">
            
            {/* TAB: INFO */}
            {activeTab === 'info' && (
              <div className="bg-surface-container-lowest border border-outline-variant/30 rounded-3xl p-8 space-y-6 animate-in fade-in duration-300">
                <div className="flex items-center justify-between">
                  <div>
                    <h2 className="text-xl font-bold text-on-surface">Thông tin cá nhân</h2>
                    <p className="text-sm text-on-surface-variant">Quản lý thông tin hồ sơ của bạn</p>
                  </div>
                  {!isEditing && (
                    <button onClick={() => setIsEditing(true)} className="flex items-center gap-2 px-4 py-2 bg-surface-container-low border border-outline-variant/30 rounded-xl text-sm font-semibold hover:bg-surface-container transition-colors">
                      <Edit size={16} /> Chỉnh sửa
                    </button>
                  )}
                </div>

                {!isEditing ? (
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-6 pt-4">
                    <div>
                      <p className="text-[10px] font-bold text-on-surface-variant uppercase tracking-wider mb-1">Họ và tên</p>
                      <p className="font-semibold text-on-surface">{user?.fullName}</p>
                    </div>
                    <div>
                      <p className="text-[10px] font-bold text-on-surface-variant uppercase tracking-wider mb-1">Email</p>
                      <p className="font-semibold text-on-surface">{user?.email}</p>
                    </div>
                    <div>
                      <p className="text-[10px] font-bold text-on-surface-variant uppercase tracking-wider mb-1">Số điện thoại</p>
                      <p className="font-semibold text-on-surface">{user?.phoneNumber || 'Chưa cập nhật'}</p>
                    </div>
                  </div>
                ) : (
                  <form onSubmit={handleUpdateProfile} className="space-y-5 pt-4">
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-5">
                      <div>
                        <label className="block text-[10px] font-bold text-on-surface-variant uppercase tracking-wider mb-1">Họ và tên</label>
                        <input type="text" value={editForm.fullName} onChange={e => setEditForm({...editForm, fullName: e.target.value})} className="w-full bg-surface-container-low border border-outline-variant/30 rounded-xl px-4 py-2.5 text-sm focus:border-primary outline-none" required />
                      </div>
                      <div>
                        <label className="block text-[10px] font-bold text-on-surface-variant uppercase tracking-wider mb-1">Số điện thoại</label>
                        <input type="text" value={editForm.phoneNumber} onChange={e => setEditForm({...editForm, phoneNumber: e.target.value})} className="w-full bg-surface-container-low border border-outline-variant/30 rounded-xl px-4 py-2.5 text-sm focus:border-primary outline-none" />
                      </div>
                      <div className="md:col-span-2">
                        <label className="block text-[10px] font-bold text-on-surface-variant uppercase tracking-wider mb-1">Ảnh đại diện (Nhỏ hơn 2MB)</label>
                        <div className="flex gap-3 items-center">
                          {editForm.avatarUrl && (
                              <img src={editForm.avatarUrl} alt="Preview" className="w-12 h-12 rounded-full object-cover border border-primary/20" />
                          )}
                          <input type="file" accept="image/*" onChange={handleAvatarChange} className="flex-1 bg-surface-container-low border border-outline-variant/30 rounded-xl px-4 py-2 text-sm file:mr-4 file:py-2 file:px-4 file:rounded-full file:border-0 file:text-xs file:font-semibold file:bg-primary/10 file:text-primary hover:file:bg-primary/20" />
                        </div>
                      </div>
                    </div>
                    <div className="flex gap-3 pt-2">
                      <button type="submit" disabled={updating} className="px-6 py-2.5 bg-primary text-white rounded-xl text-sm font-semibold hover:bg-primary-container transition-all flex items-center gap-2 shadow-lg shadow-primary/20">
                        {updating ? 'Đang lưu...' : <><Save size={16} /> Lưu thay đổi</>}
                      </button>
                      <button type="button" onClick={() => setIsEditing(false)} className="px-6 py-2.5 border border-outline-variant/30 text-on-surface rounded-xl text-sm font-semibold hover:bg-surface-container transition-all">
                        Hủy
                      </button>
                    </div>
                  </form>
                )}
              </div>
            )}

            {/* TAB: ADDRESSES */}
            {activeTab === 'addresses' && (
              <div className="bg-surface-container-lowest border border-outline-variant/30 rounded-3xl p-8 space-y-6 animate-in fade-in duration-300">
                <div className="flex items-center justify-between">
                  <div>
                    <h2 className="text-xl font-bold text-on-surface">Địa chỉ giao hàng</h2>
                    <p className="text-sm text-on-surface-variant">Quản lý danh sách địa chỉ nhận hàng của bạn</p>
                  </div>
                  {!showAddAddress && (
                    <button onClick={() => setShowAddAddress(true)} className="flex items-center gap-2 px-4 py-2 bg-primary text-white rounded-xl text-sm font-semibold hover:bg-primary-container transition-all shadow-md shadow-primary/20">
                      <Plus size={16} /> Thêm địa chỉ
                    </button>
                  )}
                </div>

                {showAddAddress && (
                  <div className="bg-surface-container-low border border-primary/30 p-5 rounded-2xl space-y-4">
                    <div className="grid grid-cols-2 gap-4">
                      <div className="col-span-2 md:col-span-1">
                        <label className="block text-[10px] font-bold text-on-surface-variant uppercase tracking-wider mb-2">Tỉnh / Thành phố</label>
                        <input
                          value={addressParts.city}
                          onChange={e => setAddressParts({...addressParts, city: e.target.value})}
                          placeholder="VD: TP.HCM, Hà Nội..."
                          className="w-full bg-surface-container border border-outline-variant/30 rounded-xl px-4 py-2.5 text-sm focus:border-primary outline-none"
                        />
                      </div>
                      <div className="col-span-2 md:col-span-1">
                        <label className="block text-[10px] font-bold text-on-surface-variant uppercase tracking-wider mb-2">Quận / Huyện</label>
                        <input
                          value={addressParts.district}
                          onChange={e => setAddressParts({...addressParts, district: e.target.value})}
                          placeholder="VD: Quận 1, Quận Bình Thạnh..."
                          className="w-full bg-surface-container border border-outline-variant/30 rounded-xl px-4 py-2.5 text-sm focus:border-primary outline-none"
                        />
                      </div>
                      <div className="col-span-2 md:col-span-1">
                        <label className="block text-[10px] font-bold text-on-surface-variant uppercase tracking-wider mb-2">Phường / Xã</label>
                        <input
                          value={addressParts.ward}
                          onChange={e => setAddressParts({...addressParts, ward: e.target.value})}
                          placeholder="VD: Phường Bến Nghé..."
                          className="w-full bg-surface-container border border-outline-variant/30 rounded-xl px-4 py-2.5 text-sm focus:border-primary outline-none"
                        />
                      </div>
                      <div className="col-span-2 md:col-span-1">
                        <label className="block text-[10px] font-bold text-on-surface-variant uppercase tracking-wider mb-2">Số nhà / Đường</label>
                        <input
                          value={addressParts.street}
                          onChange={e => setAddressParts({...addressParts, street: e.target.value})}
                          placeholder="VD: 123 Nguyễn Huệ..."
                          className="w-full bg-surface-container border border-outline-variant/30 rounded-xl px-4 py-2.5 text-sm focus:border-primary outline-none"
                        />
                      </div>
                    </div>
                    <div className="flex gap-3">
                      <button onClick={handleAddAddress} className="px-5 py-2 bg-primary text-white rounded-xl text-sm font-semibold">Lưu địa chỉ</button>
                      <button onClick={() => setShowAddAddress(false)} className="px-5 py-2 border border-outline-variant/30 text-on-surface rounded-xl text-sm font-semibold hover:bg-surface-container">Hủy</button>
                    </div>
                  </div>
                )}

                <div className="space-y-3 pt-2">
                  {addresses.length === 0 ? (
                    <div className="text-center py-8 text-on-surface-variant border border-dashed border-outline-variant/30 rounded-2xl">
                      <MapPin size={24} className="mx-auto mb-2 opacity-50" />
                      <p className="text-sm">Bạn chưa lưu địa chỉ nào.</p>
                    </div>
                  ) : (
                    addresses.map((addr, idx) => (
                      <div key={idx} className="flex items-start justify-between p-4 bg-surface-container-lowest border border-outline-variant/30 rounded-2xl hover:border-primary/20 transition-colors">
                        <div className="flex gap-3 items-start">
                          <MapPin size={18} className="text-primary mt-0.5" />
                          <div>
                            <p className="text-sm font-medium text-on-surface leading-relaxed">{addr}</p>
                            {idx === 0 && <span className="inline-block mt-2 text-[10px] font-bold text-primary bg-primary/10 px-2 py-0.5 rounded-full">Mặc định</span>}
                          </div>
                        </div>
                        <button onClick={() => handleDeleteAddress(idx)} className="p-2 text-rose-400 hover:text-rose-600 hover:bg-rose-500/10 rounded-lg transition-colors">
                          <Trash2 size={16} />
                        </button>
                      </div>
                    ))
                  )}
                </div>
              </div>
            )}

            {/* TAB: ORDERS */}
            {activeTab === 'orders' && (
              <div className="bg-surface-container-lowest border border-outline-variant/30 rounded-3xl p-8 space-y-6 animate-in fade-in duration-300">
                <div className="flex items-center justify-between">
                  <div>
                    <h2 className="text-xl font-bold text-on-surface">Lịch sử đơn hàng</h2>
                    <p className="text-sm text-on-surface-variant">Theo dõi và quản lý các đơn hàng của bạn</p>
                  </div>
                  <button onClick={fetchOrders} className="p-2 text-primary hover:bg-primary/10 rounded-xl transition-colors">
                    <RefreshCw size={18} />
                  </button>
                </div>

                <div className="grid grid-cols-2 gap-4 mb-6">
                  <div className="bg-surface-container-low border border-outline-variant/20 rounded-2xl p-4 flex items-center gap-4">
                    <div className="w-12 h-12 bg-primary/10 rounded-full flex items-center justify-center text-primary"><ShoppingBag size={20} /></div>
                    <div>
                      <p className="text-[10px] font-bold text-on-surface-variant uppercase tracking-wider">Tổng đơn</p>
                      <p className="text-xl font-extrabold text-on-surface">{orders.length}</p>
                    </div>
                  </div>
                  <div className="bg-surface-container-low border border-outline-variant/20 rounded-2xl p-4 flex items-center gap-4">
                    <div className="w-12 h-12 bg-amber-500/10 rounded-full flex items-center justify-center text-amber-500"><Clock size={20} /></div>
                    <div>
                      <p className="text-[10px] font-bold text-on-surface-variant uppercase tracking-wider">Đang xử lý</p>
                      <p className="text-xl font-extrabold text-on-surface">{pendingCount}</p>
                    </div>
                  </div>
                </div>

                {loadingOrders ? (
                  <div className="space-y-4">
                    {[1, 2, 3].map(i => (
                      <div key={i} className="h-24 bg-surface-container-low rounded-2xl animate-pulse" />
                    ))}
                  </div>
                ) : orders.length === 0 ? (
                  <div className="text-center py-12 border border-dashed border-outline-variant/30 rounded-2xl space-y-4">
                    <Package size={36} className="mx-auto text-on-surface-variant opacity-30" />
                    <div>
                      <h3 className="font-bold text-on-surface">Chưa có đơn hàng nào</h3>
                      <p className="text-sm text-on-surface-variant">Hãy khám phá sản phẩm và đặt đơn hàng đầu tiên!</p>
                    </div>
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
            )}

            {/* TAB: WISHLIST */}
            {activeTab === 'wishlist' && (
              <div className="bg-surface-container-lowest border border-outline-variant/30 rounded-3xl p-8 space-y-6 animate-in fade-in duration-300">
                <div className="flex items-center justify-between">
                  <div>
                    <h2 className="text-xl font-bold text-on-surface text-rose-500">Sản phẩm yêu thích</h2>
                    <p className="text-sm text-on-surface-variant">Những sản phẩm bạn đã lưu</p>
                  </div>
                </div>

                {loadingWishlist ? (
                  <div className="space-y-4">
                    {[1, 2, 3].map(i => (
                      <div key={i} className="h-20 bg-surface-container-low rounded-2xl animate-pulse" />
                    ))}
                  </div>
                ) : wishlistItems.length === 0 ? (
                  <div className="text-center py-12 border border-dashed border-outline-variant/30 rounded-2xl space-y-4">
                    <Heart size={36} className="mx-auto text-on-surface-variant opacity-30" />
                    <div>
                      <h3 className="font-bold text-on-surface">Danh sách trống</h3>
                      <p className="text-sm text-on-surface-variant">Bạn chưa có sản phẩm yêu thích nào.</p>
                    </div>
                    <Link to="/" className="inline-flex items-center gap-2 bg-primary text-white px-5 py-2.5 rounded-xl text-sm font-semibold hover:bg-primary-container transition-all shadow-lg shadow-primary/20">
                      Khám phá ngay
                    </Link>
                  </div>
                ) : (
                  <div className="space-y-4">
                    {wishlistItems.map(product => (
                      <div key={product.id} className="flex items-center gap-4 bg-surface-container-lowest border border-outline-variant/30 rounded-2xl p-4 hover:border-rose-500/30 transition-all">
                        <div className="w-16 h-16 bg-surface-container rounded-xl overflow-hidden p-2">
                           {/* Using a placeholder since getProductImage is in Products.jsx. 
                               Ideally this should be a global helper, but we use a default image for now. */}
                           <img src="https://lh3.googleusercontent.com/aida-public/AB6AXuDzzkgw3qdK2qx_eejr9Oee4qzRfJoNIb-smYiUqNBNBZ4_KNAbm4HxoqNIyfUqk9pV0qWkdyFf7t7SYsjbbwCkkEN06FQNQBCseQPzXacixmLlO0YB5GVfxd7AR42kwnUufnptDgHCXnHlGXhg3x4QzV7sXZkMAYLQHcoPSLjWbTIWG3W_hrzh4eWEuFkuhEuk_62jmY9dMbWUMr11EIDivHK_RMuJDGpKxGyfr8JRcfcL8i0qv6Ve" alt={product.name} className="w-full h-full object-contain" />
                        </div>
                        <div className="flex-1 min-w-0">
                           <Link to={`/product/${product.id}`} className="text-sm font-bold text-on-surface hover:text-primary transition-colors line-clamp-1">{product.name}</Link>
                           <p className="text-sm font-extrabold text-primary">{product.price.toLocaleString()}đ</p>
                        </div>
                        <div className="flex items-center gap-2">
                           <Link to={`/product/${product.id}`} className="p-2 text-primary bg-primary/10 hover:bg-primary hover:text-white rounded-xl transition-colors">
                              <ShoppingCart size={16} />
                           </Link>
                           <button onClick={() => handleRemoveWishlist(product.id)} className="p-2 text-rose-500 bg-rose-500/10 hover:bg-rose-500 hover:text-white rounded-xl transition-colors">
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
        </div>
      </main>
    </div>
  );
}