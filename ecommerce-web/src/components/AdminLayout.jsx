import React from 'react';
import { Link, Outlet, useNavigate, useLocation } from 'react-router-dom';
import { 
    LayoutDashboard, Package, ShoppingCart, FolderTree, Users,
    LogOut, Home, User as UserIcon, Menu, X, Zap
} from 'lucide-react';
import { useState } from 'react';

export default function AdminLayout() {
    const navigate = useNavigate();
    const location = useLocation();
    const [sidebarOpen, setSidebarOpen] = useState(false);

    const userName = localStorage.getItem('userName') || 'Administrator';
    const userRole = localStorage.getItem('userRole') || 'Staff';

    const handleLogout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('refreshToken');
        localStorage.removeItem('userRole');
        localStorage.removeItem('userName');
        navigate('/login');
    };

    const menuItems = [
        { path: '/admin',            label: 'Tổng Quan',       icon: LayoutDashboard, roles: ['Admin', 'Staff'] },
        { path: '/admin/products',   label: 'Sản Phẩm',        icon: Package,         roles: ['Admin', 'Staff'] },
        { path: '/admin/orders',     label: 'Đơn Hàng',        icon: ShoppingCart,    roles: ['Admin', 'Staff'] },
        { path: '/admin/categories', label: 'Danh Mục',        icon: FolderTree,      roles: ['Admin', 'Staff'] },
        { path: '/admin/users',      label: 'Nhân Sự',          icon: Users,           roles: ['Admin'] },
    ];

    const filteredMenu = menuItems.filter(item => item.roles.includes(userRole));

    return (
        <div className="min-h-screen bg-[#0a0a14] text-slate-100 flex flex-col md:flex-row font-sans">
            {/* Mobile Header */}
            <div className="md:hidden bg-[#0d0d1a] border-b border-indigo-500/10 p-4 flex justify-between items-center z-20">
                <div className="flex items-center gap-2">
                    <div className="h-7 w-7 rounded-lg bg-gradient-to-br from-indigo-500 to-violet-600 flex items-center justify-center shadow-lg shadow-indigo-500/30">
                        <Zap size={14} className="text-white" />
                    </div>
                    <span className="text-base font-black tracking-tight text-white">iLuminaty <span className="text-indigo-400">Admin</span></span>
                </div>
                <button 
                    onClick={() => setSidebarOpen(!sidebarOpen)}
                    className="p-1.5 rounded-xl border border-white/10 hover:bg-white/5 text-slate-400 hover:text-white transition-all"
                >
                    {sidebarOpen ? <X size={20} /> : <Menu size={20} />}
                </button>
            </div>

            {/* Sidebar */}
            <aside className={`
                fixed inset-y-0 left-0 w-64 bg-[#0d0d1a] border-r border-indigo-500/10 flex flex-col justify-between z-30 transform transition-transform duration-300 ease-in-out md:relative md:transform-none
                ${sidebarOpen ? 'translate-x-0' : '-translate-x-full md:translate-x-0'}
            `}>
                {/* Top: Logo + Nav */}
                <div className="flex flex-col gap-8 p-6">
                    {/* Logo */}
                    <Link to="/" className="flex items-center gap-3 group">
                        <div className="h-10 w-10 rounded-2xl bg-gradient-to-br from-indigo-500 to-violet-600 flex items-center justify-center shadow-lg shadow-indigo-500/30 group-hover:shadow-indigo-500/50 transition-all">
                            <Zap size={18} className="text-white" />
                        </div>
                        <div>
                            <p className="text-sm font-black tracking-tight text-white leading-none">iLuminaty</p>
                            <p className="text-[10px] text-indigo-400 font-semibold uppercase tracking-widest">Control Panel</p>
                        </div>
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
                                            ? 'bg-gradient-to-r from-indigo-600 to-violet-600 text-white shadow-lg shadow-indigo-600/20' 
                                            : 'text-slate-400 hover:text-white hover:bg-white/5'
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
                <div className="p-6 border-t border-indigo-500/10 space-y-4">
                    {/* User info */}
                    <div className="flex items-center gap-3 px-2">
                        <div className="h-9 w-9 rounded-full bg-gradient-to-br from-indigo-500/20 to-violet-500/20 border border-indigo-500/20 flex items-center justify-center text-sm font-bold text-indigo-400 flex-shrink-0">
                            {userName[0]?.toUpperCase()}
                        </div>
                        <div className="overflow-hidden">
                            <h4 className="text-sm font-semibold text-white truncate">{userName}</h4>
                            <span className="text-[10px] text-indigo-400 font-semibold uppercase tracking-wider">{userRole}</span>
                        </div>
                    </div>

                    <div className="space-y-1">
                        <Link
                            to="/"
                            className="flex items-center gap-3 px-4 py-2.5 rounded-xl text-sm font-medium text-slate-400 hover:text-white hover:bg-white/5 transition-all"
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
            <main className="flex-1 min-h-screen bg-[#0a0a14] p-5 md:p-8 overflow-x-hidden relative">
                {/* Background glow accents */}
                <div className="fixed top-0 right-0 w-[600px] h-[600px] bg-indigo-600/3 rounded-full blur-3xl pointer-events-none" />
                <div className="fixed bottom-0 left-64 w-[400px] h-[400px] bg-violet-600/3 rounded-full blur-3xl pointer-events-none" />
                <div className="relative z-10 max-w-[1400px] mx-auto">
                    <Outlet />
                </div>
            </main>
        </div>
    );
}
