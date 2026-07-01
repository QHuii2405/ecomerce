import React from 'react';
import { Link, Outlet, useNavigate, useLocation } from 'react-router-dom';
import { 
    LayoutDashboard, Package, ShoppingCart, FolderTree, Users,
    LogOut, Home, User as UserIcon, Menu, X, Archive, Tag
} from 'lucide-react';
import { useState } from 'react';

export default function AdminLayout() {
    const navigate = useNavigate();
    const location = useLocation();
    const [sidebarOpen, setSidebarOpen] = useState(false);

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
                <div className="p-6 border-t border-outline-variant/30 space-y-4">
                    {/* User info */}
                    <div className="flex items-center gap-3 px-2">
                        <div className="h-9 w-9 rounded-full bg-primary/10 border border-primary/20 flex items-center justify-center text-sm font-bold text-primary flex-shrink-0">
                            {userName[0]?.toUpperCase()}
                        </div>
                        <div className="overflow-hidden">
                            <h4 className="text-sm font-semibold text-on-surface truncate">{userName}</h4>
                            <span className="text-[10px] text-primary font-semibold uppercase tracking-wider">{userRole}</span>
                        </div>
                    </div>

                    <div className="space-y-1">
                        <Link
                            to="/"
                            className="flex items-center gap-3 px-4 py-2.5 rounded-xl text-sm font-medium text-on-surface-variant hover:text-primary hover:bg-surface-container-low transition-all"
                        >
                            <Home size={16} />
                            Về trang cửa hàng
                        </Link>
                        <button
                            onClick={handleLogout}
                            className="w-full flex items-center gap-3 px-4 py-2.5 rounded-xl text-sm font-medium text-rose-400 hover:text-rose-300 hover:bg-rose-500/5 transition-all"
                        >
                            <LogOut size={16} />
                            Đăng xuất
                        </button>
                    </div>
                </div>
            </aside>

            {/* Main Content */}
            <main className="flex-1 min-h-screen bg-background p-5 md:p-8 overflow-x-hidden relative">
                {/* Background glow accents */}
                <div className="fixed top-0 right-0 w-[600px] h-[600px] bg-primary/3 rounded-full blur-3xl pointer-events-none" />
                <div className="fixed bottom-0 left-64 w-[400px] h-[400px] bg-primary/3 rounded-full blur-3xl pointer-events-none" />
                <div className="relative z-10 max-w-[1400px] mx-auto">
                    <Outlet />
                </div>
            </main>
        </div>
    );
}
