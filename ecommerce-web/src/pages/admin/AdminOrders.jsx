import React, { useEffect, useState } from 'react';
import api from '../../api/axios';
import {
  ShoppingCart, RefreshCw, AlertCircle, Filter,
  ChevronDown, ChevronUp, Check, Truck, Package,
  XCircle, Clock, ArrowRight, User
} from 'lucide-react';

const STATUS_TABS = ['All', 'Pending', 'Confirmed', 'Shipping', 'Delivered', 'Cancelled'];

const STATUS_CONFIG = {
  Pending: { label: 'Chờ xử lý', cls: 'bg-amber-500/10 text-amber-500 border-amber-500/20', nextStatus: 'Confirmed', nextLabel: 'Xác nhận đơn', nextIcon: Check },
  Confirmed: { label: 'Đã xác nhận', cls: 'bg-blue-500/10 text-blue-400 border-blue-500/20', nextStatus: 'Shipping', nextLabel: 'Bắt đầu giao', nextIcon: Truck },
  Shipping: { label: 'Đang giao', cls: 'bg-indigo-500/10 text-indigo-400 border-indigo-500/20', nextStatus: 'Delivered', nextLabel: 'Xác nhận giao xong', nextIcon: Check },
  Delivered: { label: 'Đã giao', cls: 'bg-emerald-500/10 text-emerald-400 border-emerald-500/20', nextStatus: null },
  Cancelled: { label: 'Đã hủy', cls: 'bg-rose-500/10 text-rose-400 border-rose-500/20', nextStatus: null },
};

function StatusBadge({ status }) {
  const c = STATUS_CONFIG[status] || STATUS_CONFIG.Pending;
  return (
    <span className={`text-[10px] font-bold uppercase tracking-wider px-2.5 py-1 rounded-full border ${c.cls}`}>
      {c.label}
    </span>
  );
}

function OrderRow({ order, onStatusUpdate }) {
  const [expanded, setExpanded] = useState(false);
  const [updating, setUpdating] = useState(false);

  const config = STATUS_CONFIG[order.status];
  const formattedDate = new Date(order.createdAt).toLocaleDateString('vi-VN', {
    day: '2-digit', month: '2-digit', year: 'numeric', hour: '2-digit', minute: '2-digit'
  });

  const handleStatusUpdate = async (newStatus) => {
    setUpdating(true);
    try {
      await api.put(`/orders/${order.id}/status`, { newStatus });
      onStatusUpdate(order.id, newStatus);
    } catch (err) {
      alert('Lỗi cập nhật trạng thái: ' + (err.response?.data?.message || 'Thử lại'));
    } finally {
      setUpdating(false);
    }
  };

  const handleCancel = async () => {
    if (!window.confirm('Hủy đơn hàng này?')) return;
    setUpdating(true);
    try {
      await api.put(`/orders/${order.id}/status`, { newStatus: 'Cancelled' });
      onStatusUpdate(order.id, 'Cancelled');
    } catch (err) {
      alert('Lỗi hủy đơn: ' + (err.response?.data?.message || 'Thử lại'));
    } finally {
      setUpdating(false);
    }
  };

  return (
    <div className="backdrop-blur-md bg-surface border border-outline-variant/30 rounded-2xl overflow-hidden hover:border-primary/20 hover:shadow-sm transition-all shadow-sm">
      {/* Row Header */}
      <div className="p-4 flex items-center gap-4">
        <div className="flex-1 min-w-0 grid grid-cols-1 sm:grid-cols-4 gap-3 items-center">
          {/* Order ID */}
          <div>
            <p className="text-[10px] text-outline uppercase tracking-wider">Mã đơn</p>
            <p className="text-xs font-mono text-on-surface truncate">...{order.id?.slice(-8)}</p>
          </div>
          {/* Date */}
          <div>
            <p className="text-[10px] text-outline uppercase tracking-wider">Ngày đặt</p>
            <p className="text-xs text-on-surface">{formattedDate}</p>
          </div>
          {/* Amount */}
          <div>
            <p className="text-[10px] text-outline uppercase tracking-wider">Tổng tiền</p>
            <p className="text-sm font-bold text-primary">{order.totalAmount?.toLocaleString()}đ</p>
          </div>
          {/* Status */}
          <div>
            <StatusBadge status={order.status} />
          </div>
        </div>

        {/* Actions */}
        <div className="flex items-center gap-2 flex-shrink-0">
          {config?.nextStatus && (
            <button
              onClick={() => handleStatusUpdate(config.nextStatus)}
              disabled={updating}
              className="flex items-center gap-1.5 px-3 py-1.5 bg-primary text-white text-xs font-semibold rounded-xl hover:bg-primary/90 transition-all active:scale-95 disabled:opacity-50"
            >
              {updating ? (
                <div className="animate-spin rounded-full h-3 w-3 border-b-2 border-white" />
              ) : (
                <>
                  {React.createElement(config.nextIcon, { size: 12 })}
                  {config.nextLabel}
                </>
              )}
            </button>
          )}

          {(order.status === 'Pending' || order.status === 'Confirmed') && (
            <button
              onClick={handleCancel}
              disabled={updating}
              className="p-1.5 text-rose-500 hover:text-rose-600 transition-colors disabled:opacity-50"
              title="Hủy đơn"
            >
              <XCircle size={16} />
            </button>
          )}

          <button
            onClick={() => setExpanded(!expanded)}
            className="p-1.5 text-on-surface-variant hover:text-primary transition-colors"
          >
            {expanded ? <ChevronUp size={16} /> : <ChevronDown size={16} />}
          </button>
        </div>
      </div>

      {/* Expanded Details */}
      {expanded && (
        <div className="border-t border-outline-variant/30 p-4 space-y-4 bg-surface-container-lowest">
          {/* Order Items */}
          {order.orderItems && order.orderItems.length > 0 ? (
            <div className="space-y-2">
              <p className="text-[10px] font-bold uppercase tracking-widest text-outline">Chi tiết sản phẩm</p>
              {order.orderItems.map((item, i) => (
                <div key={i} className="flex items-center justify-between py-2 border-b border-outline-variant/10 last:border-0">
                  <div className="flex items-center gap-3">
                    <div className="w-8 h-8 bg-surface-container rounded-lg flex items-center justify-center">
                      <Package size={14} className="text-outline" />
                    </div>
                    <div>
                      <p className="text-xs font-semibold text-on-surface">{item.product?.name || `Sản phẩm #${i + 1}`}</p>
                      <p className="text-[10px] text-on-surface-variant">{item.unitPrice?.toLocaleString()}đ × {item.quantity}</p>
                    </div>
                  </div>
                  <span className="text-xs font-bold text-on-surface">{(item.unitPrice * item.quantity)?.toLocaleString()}đ</span>
                </div>
              ))}
            </div>
          ) : (
            <p className="text-xs text-outline italic">Không có thông tin sản phẩm</p>
          )}

          {/* Order meta */}
          {order.note && (
            <div className="bg-surface-container rounded-xl p-3">
              <p className="text-[10px] text-outline mb-1">Ghi chú</p>
              <p className="text-xs text-on-surface">{order.note}</p>
            </div>
          )}
        </div>
      )}
    </div>
  );
}

export default function AdminOrders() {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [activeTab, setActiveTab] = useState('All');
  const [search, setSearch] = useState('');

  useEffect(() => {
    fetchOrders();
  }, []);

  const fetchOrders = async () => {
    setLoading(true);
    setError(null);
    try {
      const res = await api.get('/orders');
      setOrders(res.data || []);
    } catch (err) {
      setError('Không thể tải danh sách đơn hàng. ' + (err.response?.data?.message || ''));
    } finally {
      setLoading(false);
    }
  };

  const handleStatusUpdate = (orderId, newStatus) => {
    setOrders(prev => prev.map(o => o.id === orderId ? { ...o, status: newStatus } : o));
  };

  const filteredOrders = orders.filter(o => {
    const matchTab = activeTab === 'All' || o.status === activeTab;
    const matchSearch = !search || o.id?.toLowerCase().includes(search.toLowerCase());
    return matchTab && matchSearch;
  });

  const countByStatus = (status) => orders.filter(o => status === 'All' ? true : o.status === status).length;

  return (
    <div className="space-y-6">
      {/* Header */}
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <div>
          <h1 className="text-3xl font-bold text-on-surface tracking-tight">Quản Lý Đơn Hàng</h1>
          <p className="text-on-surface-variant mt-1">Xử lý, duyệt và theo dõi đơn hàng từ khách hàng.</p>
        </div>
        <button
          onClick={fetchOrders}
          className="flex items-center gap-2 px-4 py-2.5 bg-surface border border-outline-variant/30 text-on-surface-variant rounded-xl text-sm hover:bg-surface-container-low transition-all"
        >
          <RefreshCw size={14} className={loading ? 'animate-spin' : ''} />
          Làm mới
        </button>
      </div>

      {/* Status Tabs */}
      <div className="flex gap-2 flex-wrap">
        {STATUS_TABS.map(tab => (
          <button
            key={tab}
            onClick={() => setActiveTab(tab)}
            className={`flex items-center gap-2 px-4 py-2 rounded-xl text-xs font-semibold transition-all ${activeTab === tab
                ? 'bg-primary text-white shadow-lg shadow-primary/20'
                : 'bg-surface border border-outline-variant/30 text-on-surface-variant hover:border-primary/20 hover:text-on-surface'
              }`}
          >
            {tab === 'All' ? 'Tất Cả' :
              tab === 'Pending' ? 'Chờ Xử Lý' :
                tab === 'Confirmed' ? 'Đã Xác Nhận' :
                  tab === 'Shipping' ? 'Đang Giao' :
                    tab === 'Delivered' ? 'Đã Giao' : 'Đã Hủy'}
            <span className={`px-1.5 py-0.5 rounded-full text-[10px] font-bold ${activeTab === tab ? 'bg-white/20' : 'bg-surface-container-high'
              }`}>
              {countByStatus(tab)}
            </span>
          </button>
        ))}
      </div>

      {/* Error */}
      {error && (
        <div className="flex items-center gap-3 p-4 bg-rose-500/10 border border-rose-500/20 rounded-2xl text-rose-400">
          <AlertCircle size={18} />
          <span className="text-sm">{error}</span>
          <button onClick={fetchOrders} className="ml-auto text-xs underline">Thử lại</button>
        </div>
      )}

      {/* Orders List */}
      {loading ? (
        <div className="space-y-3">
          {[1, 2, 3, 4].map(i => (
            <div key={i} className="backdrop-blur-md bg-surface border border-outline-variant/30 rounded-2xl p-4 animate-pulse">
              <div className="grid grid-cols-4 gap-4">
                {[1, 2, 3, 4].map(j => (
                  <div key={j} className="space-y-1">
                    <div className="h-2 bg-surface-container rounded w-16" />
                    <div className="h-3 bg-surface-container rounded w-24" />
                  </div>
                ))}
              </div>
            </div>
          ))}
        </div>
      ) : filteredOrders.length === 0 ? (
        <div className="backdrop-blur-md bg-surface border border-outline-variant/30 p-12 rounded-3xl text-center space-y-4 shadow-sm">
          <div className="h-16 w-16 bg-surface-container border border-outline-variant/20 text-outline rounded-3xl flex items-center justify-center mx-auto">
            <ShoppingCart size={28} />
          </div>
          <h3 className="text-lg font-bold text-on-surface">Không có đơn hàng</h3>
          <p className="text-on-surface-variant text-sm">
            {activeTab === 'All' ? 'Chưa có đơn hàng nào trong hệ thống.' : `Không có đơn ở trạng thái "${activeTab}".`}
          </p>
        </div>
      ) : (
        <div className="space-y-3">
          {filteredOrders.map(order => (
            <OrderRow key={order.id} order={order} onStatusUpdate={handleStatusUpdate} />
          ))}
          <p className="text-center text-xs text-outline pt-2">
            Hiển thị {filteredOrders.length} / {orders.length} đơn hàng
          </p>
        </div>
      )}
    </div>
  );
}
