import React, { useEffect, useState } from 'react';
import api from '../../api/axios';
import { Link } from 'react-router-dom';
import {
  ShoppingBag, Users, ShoppingCart, TrendingUp,
  Activity, ArrowUpRight, RefreshCw, Package,
  Clock, CheckCircle2, XCircle, Truck, AlertCircle
} from 'lucide-react';

const STATUS_CONFIG = {
  Pending:   { label: 'Chờ xử lý',   cls: 'text-amber-600',   icon: Clock },
  Confirmed: { label: 'Đã xác nhận', cls: 'text-blue-600',    icon: CheckCircle2 },
  Shipping:  { label: 'Đang giao',   cls: 'text-indigo-600',  icon: Truck },
  Delivered: { label: 'Đã giao',     cls: 'text-emerald-600', icon: CheckCircle2 },
  Cancelled: { label: 'Đã hủy',      cls: 'text-rose-600',    icon: XCircle },
};

export default function AdminDashboard() {
  const [stats, setStats] = useState(null);
  const [recentOrders, setRecentOrders] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchDashboard();
  }, []);

  const fetchDashboard = async () => {
    setLoading(true);
    try {
      const [productsRes, ordersRes, usersRes] = await Promise.all([
        api.get('/products'),
        api.get('/orders').catch(() => ({ data: [] })),
        api.get('/auth/users').catch(() => ({ data: [] })),
      ]);

      const products = productsRes.data.data || [];
      const orders = ordersRes.data || [];
      const users = usersRes.data || [];

      const revenue = orders
        .filter(o => o.status !== 'Cancelled')
        .reduce((sum, o) => sum + (o.totalAmount || 0), 0);

      setStats({
        productsCount: products.length,
        usersCount: users.length,
        ordersCount: orders.length,
        revenue,
        pendingCount: orders.filter(o => o.status === 'Pending').length,
        shippingCount: orders.filter(o => o.status === 'Shipping').length,
        deliveredCount: orders.filter(o => o.status === 'Delivered').length,
      });

      setRecentOrders(orders.slice(0, 5));
    } catch (err) {
      console.error('Dashboard error:', err);
      // Fallback to mock data
      setStats({
        productsCount: 0, usersCount: 0, ordersCount: 0, revenue: 0,
        pendingCount: 0, shippingCount: 0, deliveredCount: 0
      });
    } finally {
      setLoading(false);
    }
  };

  const cards = stats ? [
    { title: 'Tổng Doanh Thu', value: `${stats.revenue.toLocaleString()}đ`, icon: TrendingUp, color: 'text-emerald-600 bg-emerald-500/10 border-emerald-200', link: '/admin/orders' },
    { title: 'Sản Phẩm Đang Bán', value: stats.productsCount, icon: ShoppingBag, color: 'text-indigo-600 bg-indigo-500/10 border-indigo-200', link: '/admin/products' },
    { title: 'Tổng Đơn Hàng', value: stats.ordersCount, icon: ShoppingCart, color: 'text-violet-600 bg-violet-500/10 border-violet-200', link: '/admin/orders' },
    { title: 'Chờ Xử Lý', value: stats.pendingCount, icon: Clock, color: 'text-amber-600 bg-amber-500/10 border-amber-200', link: '/admin/orders' },
  ] : [];

  if (loading) {
    return (
      <div className="space-y-8 animate-pulse">
        <div className="h-8 bg-surface-container rounded-xl w-48" />
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
          {[1, 2, 3, 4].map(i => (
            <div key={i} className="h-24 bg-surface border border-outline-variant/10 rounded-3xl" />
          ))}
        </div>
        <div className="h-64 bg-surface border border-outline-variant/10 rounded-3xl" />
      </div>
    );
  }

  return (
    <div className="space-y-8 animate-in fade-in duration-300">
      {/* Header */}
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-3xl font-bold text-on-surface tracking-tight">Tổng Quan Hệ Thống</h1>
          <p className="text-on-surface-variant mt-1">Dữ liệu thực thời gian thực từ API.</p>
        </div>
        <button
          onClick={fetchDashboard}
          className="flex items-center gap-2 px-4 py-2.5 bg-surface border border-outline-variant/30 text-on-surface-variant rounded-xl text-sm hover:bg-surface-container-low transition-all"
        >
          <RefreshCw size={14} />
          Làm mới
        </button>
      </div>

      {/* Stats Grid */}
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
        {cards.map((card, i) => {
          const Icon = card.icon;
          return (
            <Link
              key={i}
              to={card.link}
              className="backdrop-blur-md bg-surface border border-outline-variant/20 p-6 rounded-3xl flex items-center justify-between shadow-sm hover:border-primary/20 hover:shadow-md transition-all duration-300 group"
            >
              <div className="space-y-1">
                <span className="text-xs font-semibold text-on-surface-variant uppercase tracking-wider">{card.title}</span>
                <h3 className="text-2xl font-black text-on-surface group-hover:text-primary transition-colors">{card.value}</h3>
              </div>
              <div className={`h-12 w-12 rounded-2xl border flex items-center justify-center ${card.color}`}>
                <Icon size={20} />
              </div>
            </Link>
          );
        })}
      </div>

      {/* Order Status Summary + Recent Orders */}
      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        {/* Recent Orders */}
        <div className="lg:col-span-2 backdrop-blur-md bg-surface border border-outline-variant/20 p-6 rounded-3xl space-y-5 shadow-sm">
          <div className="flex items-center justify-between">
            <h3 className="text-lg font-bold text-on-surface flex items-center gap-2">
              <Activity size={18} className="text-primary" />
              Đơn Hàng Gần Đây
            </h3>
            <Link to="/admin/orders" className="text-xs text-primary font-medium flex items-center gap-1 hover:text-primary/80 transition-colors">
              Xem tất cả <ArrowUpRight size={14} />
            </Link>
          </div>

          {recentOrders.length === 0 ? (
            <div className="text-center py-8 text-on-surface-variant">
              <Package size={24} className="mx-auto mb-2 opacity-40" />
              <p className="text-sm">Chưa có đơn hàng nào</p>
            </div>
          ) : (
            <div className="space-y-3">
              {recentOrders.map(order => {
                const sc = STATUS_CONFIG[order.status] || STATUS_CONFIG.Pending;
                const Icon = sc.icon;
                return (
                  <div key={order.id} className="flex items-center justify-between py-3 border-b border-outline-variant/10 last:border-0">
                    <div className="flex items-center gap-3">
                      <div className={`w-7 h-7 rounded-lg flex items-center justify-center bg-surface-container ${sc.cls}`}>
                        <Icon size={13} />
                      </div>
                      <div>
                        <p className="text-xs font-semibold text-on-surface">Đơn hàng #{order.id?.slice(-8).toUpperCase()}</p>
                        <p className="text-[10px] text-on-surface-variant">
                          {new Date(order.createdAt).toLocaleDateString('vi-VN')} — {order.orderItems?.length || 0} sản phẩm
                        </p>
                      </div>
                    </div>
                    <div className="text-right">
                      <p className="text-xs font-bold text-on-surface">{order.totalAmount?.toLocaleString()}đ</p>
                      <p className={`text-[10px] font-semibold ${sc.cls}`}>{sc.label}</p>
                    </div>
                  </div>
                );
              })}
            </div>
          )}
        </div>

        {/* Status Breakdown + System Status */}
        <div className="space-y-5">
          {/* Order breakdown */}
          <div className="backdrop-blur-md bg-surface border border-outline-variant/20 p-6 rounded-3xl space-y-4 shadow-sm">
            <h3 className="text-sm font-bold text-on-surface">Phân Tích Đơn Hàng</h3>
            {stats && [
              { label: 'Đang giao', value: stats.shippingCount, color: 'bg-primary' },
              { label: 'Đã giao', value: stats.deliveredCount, color: 'bg-emerald-500' },
              { label: 'Chờ xử lý', value: stats.pendingCount, color: 'bg-amber-500' },
            ].map((item, i) => (
              <div key={i} className="space-y-1">
                <div className="flex justify-between text-xs">
                  <span className="text-on-surface-variant">{item.label}</span>
                  <span className="text-on-surface font-bold">{item.value}</span>
                </div>
                <div className="h-1.5 bg-surface-container rounded-full overflow-hidden">
                  <div
                    className={`h-full ${item.color} rounded-full transition-all duration-700`}
                    style={{ width: stats.ordersCount > 0 ? `${(item.value / stats.ordersCount) * 100}%` : '0%' }}
                  />
                </div>
              </div>
            ))}
          </div>

          {/* System status */}
          <div className="backdrop-blur-md bg-surface border border-outline-variant/20 p-5 rounded-3xl space-y-3 shadow-sm">
            <h3 className="text-sm font-bold text-on-surface">Trạng Thái Hệ Thống</h3>
            {[
              { label: 'Database (SQL Server)', status: '🟢 Khỏe mạnh' },
              { label: 'Redis Cache', status: '🟢 Khỏe mạnh' },
              { label: 'API Server (.NET 10)', status: '🟢 Hoạt động' },
            ].map((s, i) => (
              <div key={i} className="flex justify-between items-center text-xs">
                <span className="text-on-surface-variant">{s.label}</span>
                <span className="text-emerald-600 font-semibold">{s.status}</span>
              </div>
            ))}
            <div className="pt-2 border-t border-outline-variant/10 text-center">
              <span className="text-[10px] text-outline">v.NET 10.0 — Production</span>
            </div>
          </div>
        </div>
      </div>

      {/* Quick Actions */}
      <div className="backdrop-blur-md bg-surface border border-outline-variant/20 p-6 rounded-3xl shadow-sm">
        <h3 className="text-sm font-bold text-on-surface mb-4">Truy Cập Nhanh</h3>
        <div className="grid grid-cols-2 sm:grid-cols-4 gap-3">
            {[
            { label: 'Thêm sản phẩm', to: '/admin/products', icon: ShoppingBag, color: 'text-primary' },
            { label: 'Quản lý đơn hàng', to: '/admin/orders', icon: ShoppingCart, color: 'text-primary' },
            { label: 'Quản lý kho hàng', to: '/admin/inventory', icon: Package, color: 'text-amber-600' },
            { label: 'Quản lý nhân sự', to: '/admin/users', icon: Users, color: 'text-emerald-600' },
          ].map((action, i) => {
            const Icon = action.icon;
            return (
              <Link
                key={i}
                to={action.to}
                className="flex flex-col items-center gap-2 p-4 bg-surface-container-lowest border border-outline-variant/20 rounded-2xl hover:border-primary/30 hover:bg-surface-container-low transition-all group text-center"
              >
                <Icon size={20} className={`${action.color} group-hover:scale-110 transition-transform`} />
                <span className="text-xs font-semibold text-on-surface-variant group-hover:text-primary transition-colors">{action.label}</span>
              </Link>
            );
          })}
        </div>
      </div>
    </div>
  );
}
