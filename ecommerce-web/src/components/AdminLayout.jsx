import React from 'react';
import { Link, Outlet, useNavigate, useLocation } from 'react-router-dom';
import { 
    LayoutDashboard, Package, ShoppingCart, FolderTree, Users,
    LogOut, Home, User as UserIcon, Menu, X, Archive, Tag, Bell
} from 'lucide-react';
import { useState } from 'react';
import { useNotification } from '../contexts/NotificationContext';

export default function AdminLayout() {
    const navigate = useNavigate();
    const location = useLocation();
    const [sidebarOpen, setSidebarOpen] = useState(false);
    const [notificationOpen, setNotificationOpen] = useState(false);

    const { notifications, unreadCount, markAsRead } = useNotification();

    const userName = localStorage.getItem('userName') || 'Administrator';
    const userRole = (localStorage.getItem('userRole') || 'Staff').toLowerCase();

    const handleLogout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('refreshToken');
        localStorage.removeItem('userRole');
        localStorage.removeItem('userName');
        navigate('/login');
    };

    const menuItems = [
        { path: '/admin',            label: 'Tổng Quan',       icon: LayoutDashboard, roles: ['admin', 'staff'] },
        { path: '/admin/products',   label: 'Sản Phẩm',        icon: Package,         roles: ['admin', 'staff'] },
        { path: '/admin/orders',     label: 'Đơn Hàng',        icon: ShoppingCart,    roles: ['admin', 'staff'] },
        { path: '/admin/categories', label: 'Danh Mục',        icon: FolderTree,      roles: ['admin', 'staff'] },
        { path: '/admin/vouchers',   label: 'Khuyến Mãi',      icon: Tag,             roles: ['admin', 'staff'] },
        { path: '/admin/inventory',  label: 'Kho Hàng',        icon: Archive,         roles: ['admin', 'staff'] },
        { path: '/admin/reviews',    label: 'Đánh Giá',        icon: Bell,            roles: ['admin', 'staff'] },
        { path: '/admin/returns',    label: 'Hoàn Trả',        icon: Package,         roles: ['admin', 'staff'] },
        { path: '/admin/cms',        label: 'Nội Dung',        icon: FolderTree,      roles: ['admin'] },
        { path: '/admin/users',      label: 'Nhân Sự',          icon: Users,           roles: ['admin'] },
    ];

    const filteredMenu = menuItems.filter(item => item.roles.includes(userRole));

    return (
        <div className="min-h-screen bg-background text-on-surface flex flex-col md:flex-row font-sans">
            {/* Mobile Header */}
            <div className="md:hidden bg-surface border-b border-outline-variant/30 p-4 flex justify-between items-center z-20">
                <div className="flex items-center gap-2">
                    <img 
                        alt="iLuminaty Shop Logo" 
                        className="h-8 w-auto object-contain" 
                        src="/Favicon.png"
                    />
                </div>
                <button 
                    onClick={() => setSidebarOpen(!sidebarOpen)}
                    className="p-1.5 rounded-xl border border-outline-variant/30 hover:bg-surface-container-low text-on-surface-variant hover:text-primary transition-all"
                >
                    {sidebarOpen ? <X size={20} /> : <Menu size={20} />}
                </button>
            </div>

            {/* Sidebar */}
            <aside className={`
                fixed inset-y-0 left-0 w-64 bg-surface border-r border-outline-variant/30 flex flex-col justify-between z-30 transform transition-transform duration-300 ease-in-out md:relative md:transform-none
                ${sidebarOpen ? 'translate-x-0' : '-translate-x-full md:translate-x-0'}
            `}>
                {/* Top: Logo + Nav */}
                <div className="flex flex-col gap-8 p-6">
                    {/* Logo */}
                    <Link to="/" className="flex items-center gap-3 group">
                        <img 
                            alt="iLuminaty Shop Logo" 
                            className="h-10 w-auto object-contain" 
                            src="/Favicon.png"
                        />
                    </Link>

                    {/* Nav */}
                    <nav className="space-y-1">
                        {filteredMenu.map((item) => {
                            const isActive = location.pathname === item.path;
                            const Icon = item.icon;
                            return (
                                <Link
                                    key={item.path}
                                    to={item.path}
                                    onClick={() => setSidebarOpen(false)}
                                    className={`
                                        flex items-center gap-3 px-4 py-3 rounded-2xl text-sm font-medium transition-all duration-200
                                        ${isActive 
                                            ? 'bg-primary text-white shadow-lg shadow-primary/20' 
                                            : 'text-on-surface-variant hover:text-primary hover:bg-surface-container-low'
                                        }
                                    `}
                                >
                                    <Icon size={17} className={isActive ? 'text-white' : ''} />
                                    {item.label}
                                    {isActive && <div className="ml-auto w-1.5 h-1.5 bg-white rounded-full animate-pulse" />}
                                </Link>
                            );
                        })}
                    </nav>
                </div>

                {/* Bottom: User + Links */}
                <div className="p-6 border-t border-outline-variant/30 space-y-4 relative">
                    <div className="flex items-center gap-3 bg-surface-container-low p-3 rounded-2xl border border-outline-variant/30">
                        <div className="w-10 h-10 rounded-full bg-primary/10 flex items-center justify-center border border-primary/20">
                            <UserIcon size={20} className="text-primary" />
                        </div>
                        <div className="flex-1 overflow-hidden">
                            <p className="text-sm font-bold text-on-surface truncate">{userName}</p>
                            <p className="text-[10px] font-semibold text-primary uppercase tracking-wider">{userRole}</p>
                        </div>
                    </div>

                    <div className="grid grid-cols-3 gap-2">
                        <Link 
                            to="/"
                            className="flex flex-col items-center justify-center gap-1 p-2 rounded-xl text-on-surface-variant hover:text-primary hover:bg-surface-container-low transition-colors border border-transparent hover:border-outline-variant/30"
                            title="Quay lại Cửa hàng"
                        >
                            <Home size={18} />
                            <span className="text-[10px] font-semibold">Store</span>
                        </Link>
                        
                        <div className="relative">
                            <button 
                                onClick={() => {
                                    setNotificationOpen(!notificationOpen);
                                    if (!notificationOpen) markAsRead();
                                }}
                                className="w-full flex flex-col items-center justify-center gap-1 p-2 rounded-xl text-on-surface-variant hover:text-primary hover:bg-surface-container-low transition-colors border border-transparent hover:border-outline-variant/30"
                                title="Thông báo"
                            >
                                <Bell size={18} />
                                <span className="text-[10px] font-semibold">Thông báo</span>
                                {unreadCount > 0 && (
                                    <span className="absolute top-1 right-2 w-2.5 h-2.5 bg-error rounded-full border-2 border-surface"></span>
                                )}
                            </button>
                            {/* Notification Dropdown */}
                            {notificationOpen && (
                                <div className="absolute bottom-full left-0 mb-2 w-72 bg-surface border border-outline-variant/30 rounded-2xl shadow-xl z-50 animate-in fade-in slide-in-from-bottom-2 overflow-hidden">
                                    <div className="p-3 border-b border-outline-variant/20 bg-surface-container-lowest">
                                        <h4 className="font-bold text-xs text-on-surface">Thông báo hệ thống</h4>
                                    </div>
                                    <div className="max-h-60 overflow-y-auto">
                                        {notifications.length === 0 ? (
                                            <div className="p-4 text-center text-on-surface-variant text-xs">
                                                Không có thông báo.
                                            </div>
                                        ) : (
                                            notifications.map((notif, idx) => (
                                                <div key={idx} className="p-3 border-b border-outline-variant/10 hover:bg-surface-container-lowest">
                                                    <h5 className="text-[11px] font-bold text-on-surface">{notif.title}</h5>
                                                    <p className="text-[10px] text-on-surface-variant mt-0.5">{notif.message}</p>
                                                    <span className="text-[9px] text-outline mt-1 block">{new Date(notif.date).toLocaleString('vi-VN')}</span>
                                                </div>
                                            ))
                                        )}
                                    </div>
                                </div>
                            )}
                        </div>

                        <button 
                            onClick={handleLogout}
                            className="flex flex-col items-center justify-center gap-1 p-2 rounded-xl text-rose-500 hover:bg-rose-500/10 transition-colors border border-transparent hover:border-rose-500/20"
                            title="Đăng xuất"
                        >
                            <LogOut size={18} />
                            <span className="text-[10px] font-semibold">Thoát</span>
                        </button>
                    </div>
                </div>
            </aside>

            {/* Main Content */}
            <main className="flex-1 overflow-y-auto h-screen relative bg-surface-container-lowest/30">
                <div className="absolute inset-0 bg-[url('https://www.transparenttextures.com/patterns/cubes.png')] opacity-[0.02] z-0 pointer-events-none"></div>
                <div className="p-4 md:p-8 relative z-10">
                    <Outlet />
                </div>
            </main>
        </div>
    );
}
