import Swal from "sweetalert2";
import React, { useEffect, useState, useCallback } from 'react';
import api from './api/axios';
import { 
    ShoppingCart, Package, Tag, Database, Zap, ArrowRight, Star, 
    AlertCircle, ShoppingBag, Search, User, LogOut, Heart, 
    Phone, Mail, Share2, Globe, AtSign, Award, Truck, ShieldCheck, 
    CheckCircle2, Menu, X, ArrowUpRight, ChevronRight, HelpCircle, Sparkles, Bell 
} from 'lucide-react';
import { Link, useNavigate } from 'react-router-dom';
import { addToCart, getCartCount } from './api/cartStore';
import { useNotification } from './contexts/NotificationContext';

export default function App() {
    const navigate = useNavigate();
    const [products, setProducts] = useState([]);
    const [source, setSource] = useState(''); // Redis Cache or SQL Server Database
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    // Banners state
    const [banners, setBanners] = useState([]);
    const [currentBannerIndex, setCurrentBannerIndex] = useState(0);

    // Search and filter states
    const [searchTerm, setSearchTerm] = useState('');
    const [selectedCategory, setSelectedCategory] = useState('All');
    
    // UI Interaction states
    const [isScrolled, setIsScrolled] = useState(false);
    const [activeSection, setActiveSection] = useState('shop');
    const [mobileMenuOpen, setMobileMenuOpen] = useState(false);
    const [contactForm, setContactForm] = useState({ name: '', email: '', message: '' });
    const [contactSubmitted, setContactSubmitted] = useState(false);

    const token = localStorage.getItem('token');
    const userName = localStorage.getItem('userName');
    const userRole = localStorage.getItem('userRole');
    const [cartCount, setCartCount] = useState(getCartCount);
    const [toast, setToast] = useState(null); // { message: string }
    const { notifications, unreadCount, markAsRead } = useNotification() || { notifications: [], unreadCount: 0, markAsRead: () => {} };
    const [notificationOpen, setNotificationOpen] = useState(false);

    const showToast = (message) => {
        setToast(message);
        setTimeout(() => setToast(null), 2500);
    };

    // Lắng nghe sự kiện cart-updated để cập nhật badge real-time
    useEffect(() => {
        const updateCount = () => setCartCount(getCartCount());
        window.addEventListener('cart-updated', updateCount);
        return () => window.removeEventListener('cart-updated', updateCount);
    }, []);

    useEffect(() => {
        fetchProducts();
        fetchBanners();
        
        const handleScroll = () => {
            if (window.scrollY > 50) {
                setIsScrolled(true);
            } else {
                setIsScrolled(false);
            }
        };
        window.addEventListener('scroll', handleScroll);

        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    setActiveSection(entry.target.id);
                }
            });
        }, { threshold: 0.3 });
        
        setTimeout(() => {
            document.querySelectorAll('section[id]').forEach((section) => {
                observer.observe(section);
            });
        }, 500);

        return () => {
            window.removeEventListener('scroll', handleScroll);
            observer.disconnect();
        };
    }, []);

    useEffect(() => {
        if (banners.length > 1) {
            const interval = setInterval(() => {
                setCurrentBannerIndex((prev) => (prev + 1) % banners.length);
            }, 5000);
            return () => clearInterval(interval);
        }
    }, [banners.length]);

    const fetchBanners = async () => {
        try {
            const response = await api.get('/Cms/banners?onlyActive=true');
            setBanners(response.data.data || []);
        } catch (err) {
            console.error("Lỗi lấy banners:", err);
        }
    };

    const fetchProducts = async () => {
        try {
            setLoading(true);
            setError(null);
            const response = await api.get('/Products');
            setProducts(response.data.data);
            setSource(response.data.source);
        } catch (err) {
            console.error("Lỗi lấy sản phẩm:", err);
            setError("Không thể tải danh sách sản phẩm. Vui lòng kiểm tra kết nối.");
        } finally {
            setLoading(false);
        }
    };

    const handleAddToCart = (e, product) => {
        e.preventDefault();
        e.stopPropagation();
        if (!token) {
            Swal.fire({
                icon: "info",
                text: "Vui lòng đăng nhập để thêm sản phẩm vào giỏ hàng!"
            });
            navigate('/login');
            return;
        }
        addToCart(product);
        showToast(`Đã thêm "${product.name.slice(0, 20)}..." vào giỏ hàng!`);
    };

    const handleLogout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('refreshToken');
        localStorage.removeItem('userRole');
        localStorage.removeItem('userName');
        window.location.reload();
    };

    const handleContactSubmit = (e) => {
        e.preventDefault();
        if (!contactForm.name || !contactForm.email || !contactForm.message) {
            Swal.fire({
                icon: "info",
                text: "Vui lòng điền đầy đủ thông tin!"
            });
            return;
        }
        setContactSubmitted(true);
        setTimeout(() => {
            setContactSubmitted(false);
            setContactForm({ name: '', email: '', message: '' });
        }, 5000);
    };

    // Helper to assign correct beautiful image based on product name/category
    const getProductImage = (product) => {
        if (product.imageUrls && product.imageUrls.length > 0) {
            const url = product.imageUrls[0];
            return url.startsWith('http') ? url : `${(import.meta.env.VITE_API_BASE_URL || '')}${url}`;
        }
        
        const name = product.name?.toLowerCase() || '';
        const category = product.category?.name?.toLowerCase() || '';
        
        if (name.includes('pro x1') || name.includes('laptop') || name.includes('macbook') || category.includes('laptop') || category.includes('máy tính')) {
            return 'https://lh3.googleusercontent.com/aida-public/AB6AXuDzzkgw3qdK2qx_eejr9Oee4qzRfJoNIb-smYiUqNBNBZ4_KNAbm4HxoqNIyfUqk9pV0qWkdyFf7t7SYsjbbwCkkEN06FQNQBCseQPzXacixmLlO0YB5GVfxd7AR42kwnUufnptDgHCXnHlGXhg3x4QzV7sXZkMAYLQHcoPSLjWbTIWG3W_hrzh4eWEuFkuhEuk_62jmY9dMbWUMr11EIDivHK_RMuJDGpKxGyfr8JRcfcL8i0qv6Ve';
        }
        if (name.includes('mechanical') || name.includes('keyboard') || name.includes('chuột') || name.includes('mouse') || category.includes('peripheral') || category.includes('phụ kiện') || category.includes('gaming')) {
            return 'https://lh3.googleusercontent.com/aida-public/AB6AXuCtePE6a8azsOINxsGhPHJGa7pybuOEtEXGQUTmgTTPhKMBwOS-7FOpWVDKTgo17TVnuDYz0tGqUltaJ9vPZxqJCWCdknYb9x1VpLfsq9eC8Qlc1fG2cjNg5i2klZdqsM6d2QHuBDP_mHVRg-Ley5Dw6z3yNJURrfQ5bnPx_TyxzBc7EuP7b5boquCcOezT3SWcmEEoMElGb7VQSAkNtos5xtMcNxJFn8D0-Oq4x3zH0vgAz5bRpJdx';
        }
        if (name.includes('bud') || name.includes('earbud') || name.includes('headphone') || name.includes('tai nghe') || category.includes('audio') || category.includes('âm thanh')) {
            return 'https://lh3.googleusercontent.com/aida-public/AB6AXujCnwa9YYlaySQHciXXGs5ENqNsPlYyM48pBFebzeWQc7dPKrvdRI9-hr5S5mxpSSR3ZK689MtFW5CFO2yl2XF3_1RCn3iwdnmtwXFQeVUo_FXR0vOQ7FYE_qzsTZb4Q8_zaMowCCLDFa84jAXRV-XqKV2AZPbY2fWUotHuBFbc9Jv95ESTZuet5JJdQVxXrmhm4ItvrDA3BDDkW6wfXdjWtO5ynIppnxJllrxffafcwXaW4XKodrR';
        }
        if (name.includes('watch') || name.includes('smartwatch') || name.includes('đồng hồ') || category.includes('wearable')) {
            return 'https://lh3.googleusercontent.com/aida-public/AB6AXuBn-mFJd_xtHwuNyIDKTNQv3xEkpuUVhvk2ZpiMvjAQf_i2wmaIYp4rCSt-PYezlHvTV9a1Houwtt9tlkJdeQA8TXcUPSi0rjQe5s25vmtFRI_4-QwKd3tpQdd9uQDF9YGL0-wLZUAFHc42YK8SkSixMOVVrNNAXNCvIP8lL7HD_uLINN7SAoT_A5LgtBOK9si2owN3mSzUZQ7Rmi5WBf-sqnjA6maOVREOIVb9wY7GqJrBIJy_zFoL';
        }
        if (name.includes('phone') || name.includes('iphone') || name.includes('samsung') || name.includes('điện thoại') || category.includes('smartphone') || category.includes('mobile')) {
            return 'https://lh3.googleusercontent.com/aida-public/AB6AXuAltOeyhWUN8d_OI3RZgPhmFTnEFiE8btibjVA1UyeS4BMmMyjSKaTmgBimNagFrcF5ixaI_tKAShIux-GWN1Ed-N4cXrCROBioCaBreSt4h4NtC8LD-0H3MX6jv_fs4XT3pt7d0fecPOmOrn9wrTrKkLcAH0eYV75rcouQVMTlc39VoiWaFpa2STxaIe2OkNzeota4rS1mkkwmLFdG16EQo8bXdMVcpc2tss9oN1UsRXklpzD_rStA';
        }
        return 'https://lh3.googleusercontent.com/aida-public/AB6AXuDzzkgw3qdK2qx_eejr9Oee4qzRfJoNIb-smYiUqNBNBZ4_KNAbm4HxoqNIyfUqk9pV0qWkdyFf7t7SYsjbbwCkkEN06FQNQBCseQPzXacixmLlO0YB5GVfxd7AR42kwnUufnptDgHCXnHlGXhg3x4QzV7sXZkMAYLQHcoPSLjWbTIWG3W_hrzh4eWEuFkuhEuk_62jmY9dMbWUMr11EIDivHK_RMuJDGpKxGyfr8JRcfcL8i0qv6Ve';
    };

    // Filter products dynamically
    const filteredProducts = products.filter(product => {
        const matchesSearch = product.name?.toLowerCase().includes(searchTerm.toLowerCase()) || 
                              product.description?.toLowerCase().includes(searchTerm.toLowerCase()) ||
                              product.category?.name?.toLowerCase().includes(searchTerm.toLowerCase());
        
        if (selectedCategory === 'All') return matchesSearch;
        
        const catName = product.category?.name?.toLowerCase() || '';
        const prodName = product.name?.toLowerCase() || '';
        
        if (selectedCategory === 'Laptops') {
            return matchesSearch && (catName.includes('laptop') || catName.includes('máy tính') || prodName.includes('laptop') || prodName.includes('pro x1'));
        }
        if (selectedCategory === 'Gaming') {
            return matchesSearch && (catName.includes('gaming') || catName.includes('peripheral') || catName.includes('phụ kiện') || prodName.includes('keyboard') || prodName.includes('mechanical') || prodName.includes('gear'));
        }
        if (selectedCategory === 'Audio') {
            return matchesSearch && (catName.includes('audio') || catName.includes('âm thanh') || prodName.includes('bud') || prodName.includes('earbud') || prodName.includes('headphone'));
        }
        if (selectedCategory === 'Smartphones') {
            return matchesSearch && (catName.includes('phone') || catName.includes('smartphone') || catName.includes('điện thoại') || prodName.includes('phone') || prodName.includes('nexus'));
        }
        
        return matchesSearch;
    });

    const toggleCategory = (category) => {
        if (selectedCategory === category) {
            setSelectedCategory('All');
        } else {
            setSelectedCategory(category);
            // Smooth scroll to product section
            document.getElementById('shop')?.scrollIntoView({ behavior: 'smooth' });
        }
    };

    return (
        <div className="min-h-screen bg-background text-on-surface flex flex-col relative overflow-x-hidden font-sans">
            {/* Toast Notification */}
            {toast && (
                <div className="fixed top-20 right-4 z-[999] bg-slate-900 border border-emerald-500/20 text-white px-4 py-3 rounded-2xl shadow-2xl flex items-center gap-3 animate-in slide-in-from-right duration-300">
                    <div className="w-6 h-6 bg-emerald-500/20 rounded-full flex items-center justify-center flex-shrink-0">
                        <CheckCircle2 size={14} className="text-emerald-400" />
                    </div>
                    <p className="text-sm font-medium">{toast}</p>
                </div>
            )}
            {/* Header (Shared Component: TopNavBar) */}
            <nav className={`fixed top-0 w-full z-50 bg-surface/80 backdrop-blur-md border-b border-outline-variant/30 transition-all duration-300 ease-in-out ${isScrolled ? 'py-2 shadow-md' : 'py-4 shadow-sm'}`}>
                <div className="max-w-[1440px] mx-auto flex justify-between items-center px-margin-mobile md:px-margin-desktop">
                    {/* Brand Identity */}
                    <Link to="/" className="flex items-center gap-3">
                        <img 
                            alt="iLuminaty Shop Logo" 
                            className="h-[50px] lg:h-[60px] w-auto object-contain transition-all duration-300" 
                            src="/Favicon.png"
                        />
                    </Link>

                    {/* Navigation Links */}
                    <div className="hidden md:flex items-center gap-8 font-medium text-sm text-on-surface-variant">
                        <a 
                            onClick={(e) => { e.preventDefault(); document.getElementById('shop')?.scrollIntoView({ behavior: 'smooth' }); }} 
                            href="#shop" 
                            className={`hover:text-primary transition-colors duration-200 ${activeSection === 'shop' ? 'text-primary font-bold border-b-2 border-primary pb-1' : ''}`}
                        >
                            Sản Phẩm
                        </a>
                        <a href="#about" onClick={(e) => { e.preventDefault(); document.getElementById('about')?.scrollIntoView({ behavior: 'smooth' }); }} className={`hover:text-primary transition-colors duration-200 ${activeSection === 'about' ? 'text-primary font-bold border-b-2 border-primary pb-1' : ''}`}>Giới Thiệu</a>
                        <a href="#contact" onClick={(e) => { e.preventDefault(); document.getElementById('contact')?.scrollIntoView({ behavior: 'smooth' }); }} className={`hover:text-primary transition-colors duration-200 ${activeSection === 'contact' ? 'text-primary font-bold border-b-2 border-primary pb-1' : ''}`}>Liên Hệ</a>
                        <Link to="/recruitment" className="hover:text-primary transition-colors duration-200 text-on-surface-variant hover:text-primary">Tuyển Dụng</Link>
                    </div>

                    {/* Search & Actions */}
                    <div className="flex items-center gap-4 lg:gap-6">
                        {/* Search bar inside header */}
                        <div className="relative hidden lg:block">
                            <input 
                                className="bg-surface-container-low border border-outline-variant/50 rounded-full px-5 py-2 text-sm w-64 focus:outline-none focus:ring-2 focus:ring-primary/20 focus:border-primary transition-all text-on-surface" 
                                placeholder="Tìm kiếm sản phẩm..." 
                                type="text"
                                value={searchTerm}
                                onChange={(e) => setSearchTerm(e.target.value)}
                            />
                            <Search size={16} className="absolute right-4 top-3 text-on-surface-variant" />
                        </div>

                        {/* Cart Icon */}
                        <Link to="/cart" className="relative p-1.5 text-on-surface-variant hover:text-primary transition-colors">
                            <ShoppingCart size={20} />
                            {cartCount > 0 && (
                                <span className="absolute -top-1 -right-1 bg-primary text-white text-[10px] min-w-[18px] h-[18px] flex items-center justify-center rounded-full font-bold px-1">
                                    {cartCount > 99 ? '99+' : cartCount}
                                </span>
                            )}
                        </Link>
                        
                        {/* Notification Bell */}
                        {token && (
                            <div className="relative">
                                <button 
                                    onClick={() => {
                                        setNotificationOpen(!notificationOpen);
                                        if (!notificationOpen) markAsRead();
                                    }}
                                    className="relative p-1.5 text-on-surface-variant hover:text-primary transition-colors"
                                >
                                    <Bell size={20} />
                                    {unreadCount > 0 && (
                                        <span className="absolute -top-1 -right-1 bg-rose-500 text-white text-[10px] min-w-[18px] h-[18px] flex items-center justify-center rounded-full font-bold px-1">
                                            {unreadCount > 99 ? '99+' : unreadCount}
                                        </span>
                                    )}
                                </button>
                                {notificationOpen && (
                                    <div className="absolute top-full right-0 mt-2 w-80 bg-surface border border-outline-variant/30 rounded-2xl shadow-xl z-50 overflow-hidden animate-in fade-in slide-in-from-top-2">
                                        <div className="p-3 border-b border-outline-variant/20 bg-surface-container-lowest">
                                            <h4 className="font-bold text-xs text-on-surface">Thông báo hệ thống</h4>
                                        </div>
                                        <div className="max-h-80 overflow-y-auto">
                                            {notifications.length === 0 ? (
                                                <div className="p-6 text-center text-on-surface-variant text-xs">
                                                    Không có thông báo mới.
                                                </div>
                                            ) : (
                                                notifications.map((notif, idx) => (
                                                    <div key={idx} className="p-4 border-b border-outline-variant/10 hover:bg-surface-container-lowest transition-colors">
                                                        <h5 className="text-[13px] font-bold text-on-surface leading-tight mb-1">{notif.title}</h5>
                                                        <p className="text-xs text-on-surface-variant">{notif.message}</p>
                                                        <span className="text-[10px] text-outline block mt-2">{new Date(notif.date).toLocaleString('vi-VN')}</span>
                                                    </div>
                                                ))
                                            )}
                                        </div>
                                    </div>
                                )}
                            </div>
                        )}

                        {/* Session / Authentication UI */}
                        {token ? (
                            <div className="flex items-center gap-3">
                                <Link to="/profile" className="flex items-center gap-2 text-xs font-medium text-on-surface-variant hover:text-primary transition-colors">
                                    <div className="w-7 h-7 bg-primary/10 rounded-full flex items-center justify-center border border-primary/20">
                                        <User size={13} className="text-primary" />
                                    </div>
                                    <span className="hidden md:inline">{userName}</span>
                                </Link>
                                
                                {(userRole === 'Admin' || userRole === 'Staff') && (
                                    <Link 
                                        to="/admin" 
                                        className="rounded-full bg-primary px-4 py-2 text-xs font-semibold text-white shadow-md hover:bg-primary-container transition-all"
                                    >
                                        Quản Trị
                                    </Link>
                                )}

                                <button 
                                    onClick={handleLogout}
                                    className="p-1.5 text-rose-500 hover:text-rose-600 transition-colors"
                                    title="Đăng xuất"
                                >
                                    <LogOut size={18} />
                                </button>
                            </div>
                        ) : (
                            <div className="hidden sm:flex items-center gap-3">
                                <Link to="/login" className="text-xs font-semibold text-on-surface-variant hover:text-primary transition-colors">
                                    Đăng nhập
                                </Link>
                                <Link 
                                    to="/register" 
                                    className="rounded-full bg-primary text-white px-4 py-2 text-xs font-semibold hover:bg-primary-container hover:shadow-lg transition-all"
                                >
                                    Đăng ký
                                </Link>
                            </div>
                        )}

                        {/* Mobile Menu Toggle Button */}
                        <button 
                            className="md:hidden p-1 text-on-surface-variant hover:text-primary transition-colors"
                            onClick={() => setMobileMenuOpen(!mobileMenuOpen)}
                        >
                            {mobileMenuOpen ? <X size={24} /> : <Menu size={24} />}
                        </button>
                    </div>
                </div>

                {/* Mobile Dropdown Menu */}
                {mobileMenuOpen && (
                    <div className="md:hidden absolute top-full left-0 w-full bg-surface border-b border-outline-variant/30 py-4 px-margin-mobile flex flex-col gap-4 shadow-lg animate-in fade-in slide-in-from-top-5 duration-200">
                        {/* Search Input for Mobile */}
                        <div className="relative">
                            <input 
                                className="w-full bg-surface-container-low border border-outline-variant/50 rounded-full px-5 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-primary/20 focus:border-primary text-on-surface" 
                                placeholder="Tìm kiếm sản phẩm..." 
                                type="text"
                                value={searchTerm}
                                onChange={(e) => setSearchTerm(e.target.value)}
                            />
                            <Search size={16} className="absolute right-4 top-3 text-on-surface-variant" />
                        </div>

                        <div className="flex flex-col gap-3 font-medium text-sm text-on-surface-variant">
                            <a 
                                href="#shop" 
                                onClick={() => { setSelectedCategory('All'); setMobileMenuOpen(false); }} 
                                className="hover:text-primary py-1 border-b border-outline-variant/10"
                            >
                                Sản Phẩm
                            </a>
                            <a href="#about" onClick={() => setMobileMenuOpen(false)} className="hover:text-primary py-1 border-b border-outline-variant/10">Giới Thiệu</a>
                            <a href="#contact" onClick={() => setMobileMenuOpen(false)} className="hover:text-primary py-1 border-b border-outline-variant/10">Liên Hệ</a>
                            <Link to="/recruitment" onClick={() => setMobileMenuOpen(false)} className="hover:text-primary py-1">Tuyển Dụng</Link>
                        </div>

                        {/* Dynamic Cache source inside mobile dropdown */}
                        {source && (
                            <div className="flex items-center gap-1.5 rounded-full border border-primary/20 bg-primary-container/20 px-3 py-1.5 text-xs font-semibold text-primary">
                                {source === 'Redis Cache' ? <Zap size={14} className="text-amber-500 fill-amber-500" /> : <Database size={14} />}
                                <span>Dữ liệu: {source}</span>
                            </div>
                        )}

                        {/* Sign in / Register buttons in mobile menu */}
                        {!token && (
                            <div className="flex gap-4 pt-2 border-t border-outline-variant/20">
                                <Link 
                                    to="/login" 
                                    onClick={() => setMobileMenuOpen(false)}
                                    className="flex-1 text-center py-2.5 rounded-full border border-outline-variant text-xs font-semibold text-on-surface-variant hover:bg-surface-container-low transition-all"
                                >
                                    Đăng nhập
                                </Link>
                                <Link 
                                    to="/register" 
                                    onClick={() => setMobileMenuOpen(false)}
                                    className="flex-1 text-center py-2.5 rounded-full bg-primary text-white text-xs font-semibold hover:bg-primary-container transition-all"
                                >
                                    Đăng ký
                                </Link>
                            </div>
                        )}
                    </div>
                )}
            </nav>

            <main className="pt-16">
                {/* Hero Section */}
                <section className="relative min-h-[85vh] flex items-center overflow-hidden hero-gradient">
                    <div className="absolute inset-0 z-0">
                        <div className="absolute inset-0 bg-gradient-to-r from-background via-background/90 to-transparent z-10"></div>
                        <img 
                            className="w-full h-full object-cover object-center transform scale-105 hover:scale-110 transition-transform duration-[10s] ease-out transition-opacity duration-1000" 
                            alt={banners[currentBannerIndex]?.title || "Hardware technology background"}
                            src={banners.length > 0 ? (banners[currentBannerIndex].imageUrl.startsWith('http') ? banners[currentBannerIndex].imageUrl : `${(import.meta.env.VITE_API_BASE_URL || '')}${banners[currentBannerIndex].imageUrl}`) : "https://lh3.googleusercontent.com/aida/AP1WRLtCzJC5IBSYexQRKC338Kux8lU5df0n6aC3Pl0yaVjleZj7T1OfDJLAiuHJJ3q-qIffvIo6bCL0p-lsUy9hjpFiz-CrHkZgiZD0c-TcCT7d4vuqeF0Rx336-ABqWjiL_oS3zBRXwUxDfNV87Xc45Tk98YTMNre3Tf78nvU26iewryMrdUpDVuDQvo_K5mCWlxJt4cem-6byl1Qq7XRrHSm6Cz9k5pXMNl4_IPI951CMDOnFoqhKxtq_muQ"}
                        />
                    </div>
                    <div className="relative z-20 max-w-[1440px] mx-auto px-margin-mobile md:px-margin-desktop w-full py-12">
                        <div className="max-w-2xl space-y-6">
                            <div className="inline-flex items-center gap-2 px-3 py-1 bg-primary-container text-on-primary-container rounded-full text-xs font-semibold">
                                <Sparkles size={14} className="text-amber-500 fill-amber-500 animate-pulse" />
                                <span>ĐỒNG HÀNH KHỞI TẠO TƯƠNG LAI</span>
                            </div>
                            <h1 className="text-4xl md:text-6xl font-bold leading-tight tracking-tight text-on-surface">
                                {banners.length > 0 ? banners[currentBannerIndex].title : (
                                    <>Khám Phá Công Nghệ <br/><span className="text-primary bg-gradient-to-r from-primary to-primary-container bg-clip-text text-transparent">Kỷ Nguyên Mới</span></>
                                )}
                            </h1>
                            <p className="text-base md:text-lg text-on-surface-variant max-w-lg leading-relaxed">
                                {banners.length > 0 && banners[currentBannerIndex].subtitle ? banners[currentBannerIndex].subtitle : (
                                    "Nâng tầm cuộc sống số của bạn với các thiết bị phần cứng được chế tác tinh xảo. Từ dàn máy gaming đỉnh cao đến hệ sinh thái kết nối di động liền mạch, iLuminaty Shop đem tương lai đến hôm nay."
                                )}
                            </p>
                            <div className="flex flex-wrap gap-4 pt-4">
                                <a 
                                    href={banners.length > 0 && banners[currentBannerIndex].targetUrl ? banners[currentBannerIndex].targetUrl : "#shop"} 
                                    className="bg-primary text-white px-8 py-4 rounded-xl font-semibold text-sm hover:bg-primary-container transition-all shadow-lg shadow-primary/20 hover:shadow-xl hover:shadow-primary/30 flex items-center gap-2 group"
                                >
                                    Mua Sắm Ngay
                                    <ArrowRight size={16} className="group-hover:translate-x-1 transition-transform" />
                                </a>
                                <a 
                                    href="#about"
                                    className="bg-surface-container-lowest border border-outline-variant text-on-surface px-8 py-4 rounded-xl font-semibold text-sm hover:bg-surface-container-low transition-all"
                                >
                                    Tìm Hiểu Thêm
                                </a>
                            </div>
                        </div>
                    </div>
                </section>

                {/* Brand Story Section */}
                <section id="about" className="py-16 max-w-[1440px] mx-auto px-margin-mobile md:px-margin-desktop">
                    <div className="grid grid-cols-1 lg:grid-cols-2 gap-16 items-center">
                        <div className="relative">
                            <div className="aspect-video rounded-2xl overflow-hidden shadow-2xl relative group">
                                <div 
                                    className="w-full h-full bg-cover bg-center group-hover:scale-105 transition-transform duration-700" 
                                    style={{ backgroundImage: `url('https://lh3.googleusercontent.com/aida-public/AB6AXuBs27dS6atukIKCRVOCtSKAaeTuhWP7LNf7lrhs3unQoWBdVvKXerT56uxJg6hhP8McGO5KyKT4AlUk7F5Q965jDLRityfRwkyWODAWqs7W2vBkUD4SSYJ5CdTqWWxYOxClXbBw4kgsZzfd5mQkALjj9cYqDRrTGDX1pEJTcF3iouVPU3fofQRXHQls-3ba5CL02etFOeljdZ0o5avxaRYTnFmL4HwDLxsN--i07j-a9f07h-ISqnaq')` }}
                                ></div>
                                <div className="absolute inset-0 bg-primary/10 mix-blend-overlay"></div>
                            </div>
                            <div className="absolute -bottom-8 -right-8 bg-surface-container-lowest p-6 rounded-2xl shadow-xl border border-outline-variant/30 hidden md:block">
                                <div className="flex items-center gap-4">
                                    <div className="w-12 h-12 bg-primary/10 rounded-full flex items-center justify-center text-primary">
                                        <Award size={24} />
                                    </div>
                                    <div>
                                        <p className="font-bold text-2xl text-primary">15+</p>
                                        <p className="text-xs text-on-surface-variant font-semibold">Năm Kiến Tạo Đổi Mới</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="space-y-6">
                            <span className="text-primary font-bold text-sm tracking-widest uppercase">Sứ Mệnh Của Chúng Tôi</span>
                            <h2 className="text-3xl md:text-4xl font-bold text-on-surface leading-tight">Chế Tác Vượt Trội. <br/>Kiến Tạo Cuộc Sống.</h2>
                            <p className="text-sm md:text-base text-on-surface-variant leading-relaxed">
                                Tại iLuminaty Shop, chúng tôi tin rằng công nghệ phải là sự mở rộng năng lực vô hạn của bạn. Kể từ khi thành lập, chúng tôi luôn bứt phá các rào cản phần cứng thông thường để tạo nên các tuyệt tác cả về thẩm mỹ lẫn hiệu năng tối đa.
                            </p>
                            <div className="grid grid-cols-2 gap-8 pt-4">
                                <div className="space-y-1">
                                    <h3 className="font-bold text-2xl text-on-surface flex items-center gap-1.5">
                                        <CheckCircle2 className="text-emerald-500" size={20} /> 99.9%
                                    </h3>
                                    <p className="text-xs text-on-surface-variant">Độ Tin Cậy Phần Cứng</p>
                                </div>
                                <div className="space-y-1">
                                    <h3 className="font-bold text-2xl text-on-surface flex items-center gap-1.5">
                                        <Truck className="text-primary" size={20} /> 24/7
                                    </h3>
                                    <p className="text-xs text-on-surface-variant">Đồng Hành Hỗ Trợ</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>

                {/* Featured Categories (Bento Grid) */}
                <section className="py-16 bg-surface-container-low">
                    <div className="max-w-[1440px] mx-auto px-margin-mobile md:px-margin-desktop">
                        <div className="flex justify-between items-end mb-12">
                            <div className="space-y-2">
                                <h2 className="text-2xl md:text-3xl font-bold text-on-surface">Danh Mục Mua Sắm</h2>
                                <p className="text-sm text-on-surface-variant">Tìm kiếm công cụ tối ưu cho công việc và giải trí của bạn.</p>
                            </div>
                            <Link 
                                to="/products"
                                className="text-primary font-semibold text-sm flex items-center gap-2 hover:underline"
                            >
                                Xem Tất Cả <ChevronRight size={16} />
                            </Link>
                        </div>
                        
                        <div className="grid grid-cols-1 md:grid-cols-12 gap-gutter h-auto md:h-[600px]">
                            {/* Laptops */}
                            <div 
                                onClick={() => toggleCategory('Laptops')}
                                className={`md:col-span-8 group relative rounded-2xl overflow-hidden cursor-pointer hover-lift border-2 transition-all ${selectedCategory === 'Laptops' ? 'border-primary shadow-lg scale-[0.99]' : 'border-transparent'}`}
                            >
                                <div 
                                    className="w-full h-96 md:h-full bg-cover bg-center group-hover:scale-105 transition-transform duration-500" 
                                    style={{ backgroundImage: `url('https://lh3.googleusercontent.com/aida-public/AB6AXuB6hSI0MjXxtJkkJcOwHAqSTSqQLFLqMl6oWZ97hTY39ucc9vyqUz8vOf8fdRGqAoRq3gRJPSH-vB4PlPlxNVoK0gYuq-3heP-BNLFYLjulBRMEoUtD8a9E9Wv5j0bkQsVoAy3E_JpAS1DM_RSZg81coefXslGzGIxXIsVddAvlNKoF3PnWzhzc2poccXZRQcaVGDkWXRSl70SdEkJUUbppjD4l4UEM5W7eZvXX9ZlpZ14uCCe1A7QZ')` }}
                                ></div>
                                <div className="absolute inset-0 bg-gradient-to-t from-black/80 via-black/30 to-transparent flex flex-col justify-end p-8">
                                    <h3 className="text-white font-bold text-xl md:text-2xl">Laptops & Máy Tính</h3>
                                    <p className="text-white/80 text-sm mt-1">Sức mạnh công nghệ dành cho sáng tạo và làm việc chuyên nghiệp.</p>
                                    <div className="mt-3 flex items-center gap-1.5 text-xs font-semibold text-primary bg-white/10 w-fit px-3 py-1 rounded-full backdrop-blur-md">
                                        Xem Chi Tiết <ArrowUpRight size={14} />
                                    </div>
                                </div>
                            </div>
                            
                            {/* Gaming */}
                            <div 
                                onClick={() => toggleCategory('Gaming')}
                                className={`md:col-span-4 md:row-span-2 group relative rounded-2xl overflow-hidden cursor-pointer hover-lift border-2 transition-all ${selectedCategory === 'Gaming' ? 'border-primary shadow-lg scale-[0.99]' : 'border-transparent'}`}
                            >
                                <div 
                                    className="w-full h-96 md:h-full bg-cover bg-center group-hover:scale-105 transition-transform duration-500" 
                                    style={{ backgroundImage: `url('https://lh3.googleusercontent.com/aida-public/AB6AXuB8FbDXR4wA4l-wWYxA_LFZLmdx6e-oqdjNC81ZRIAZv3DcrdkWenSf7rePViHM0ugI50_3ggw99t1ra_pYmW74svifW1jB3CfXXYel_me-_LCZphDm4zFbjCE3LOnROfJyArAeXqA2Exd-KDjsUd8urn6LnDU82WCjDC4o5qYL9v2eAFGc-HMqBKRdNf1wv6m_Hz7I_GKzB61oLfH6sbaxr9wLAi73KSkfGrSnJ8ScAojCMKdmTGwm')` }}
                                ></div>
                                <div className="absolute inset-0 bg-gradient-to-t from-black/80 via-black/30 to-transparent flex flex-col justify-end p-8">
                                    <h3 className="text-white font-bold text-xl md:text-2xl">Thiết Bị Gaming</h3>
                                    <p className="text-white/80 text-sm mt-1">Làm chủ hoàn toàn không gian thực tế ảo.</p>
                                    <div className="mt-3 flex items-center gap-1.5 text-xs font-semibold text-primary bg-white/10 w-fit px-3 py-1 rounded-full backdrop-blur-md">
                                        Xem Chi Tiết <ArrowUpRight size={14} />
                                    </div>
                                </div>
                            </div>

                            {/* Audio */}
                            <div 
                                onClick={() => toggleCategory('Audio')}
                                className={`md:col-span-4 group relative rounded-2xl overflow-hidden cursor-pointer hover-lift border-2 transition-all ${selectedCategory === 'Audio' ? 'border-primary shadow-lg scale-[0.99]' : 'border-transparent'}`}
                            >
                                <div 
                                    className="w-full h-64 md:h-full bg-cover bg-center group-hover:scale-105 transition-transform duration-500" 
                                    style={{ backgroundImage: `url('https://lh3.googleusercontent.com/aida-public/AB6AXu2tSrDnNvFUgKSReQL72nuFly-Eeuh2wXtP1ljrG7Zjy1sg3nDrxaCUNWj7ZwkcEVTjqCME1D6bJ_m9gTos1XDdWOTq4r-iyYs4lMAKaHjvKp0YrAyjpi4Vy1w3S_V1TusuL87vcV_aUi4RGzFwCemMF5eQAsAS7__lwYO9m18KGer0HeBJXJzWB5k0gGKSv5TE66m5w3aYM0y_0PHECHIF7eK5Tzx3Y32-l1_ygOe5tkKH8MbrtrW')` }}
                                ></div>
                                <div className="absolute inset-0 bg-gradient-to-t from-black/80 via-black/30 to-transparent flex flex-col justify-end p-8">
                                    <h3 className="text-white font-bold text-xl">Âm Thanh Premium</h3>
                                    <p className="text-white/80 text-xs mt-1">Chất lượng âm thanh hoàn mỹ không tạp âm.</p>
                                    <div className="mt-2 flex items-center gap-1.5 text-xs font-semibold text-primary bg-white/10 w-fit px-3 py-1 rounded-full backdrop-blur-md">
                                        Xem Chi Tiết <ArrowUpRight size={14} />
                                    </div>
                                </div>
                            </div>

                            {/* Smartphones */}
                            <div 
                                onClick={() => toggleCategory('Smartphones')}
                                className={`md:col-span-4 group relative rounded-2xl overflow-hidden cursor-pointer hover-lift border-2 transition-all ${selectedCategory === 'Smartphones' ? 'border-primary shadow-lg scale-[0.99]' : 'border-transparent'}`}
                            >
                                <div 
                                    className="w-full h-64 md:h-full bg-cover bg-center group-hover:scale-105 transition-transform duration-500" 
                                    style={{ backgroundImage: `url('https://lh3.googleusercontent.com/aida-public/AB6AXuAltOeyhWUN8d_OI3RZgPhmFTnEFiE8btibjVA1UyeS4BMmMyjSKaTmgBimNagFrcF5ixaI_tKAShIux-GWN1Ed-N4cXrCROBioCaBreSt4h4NtC8LD-0H3MX6jv_fs4XT3pt7d0fecPOmOrn9wrTrKkLcAH0eYV75rcouQVMTlc39VoiWaFpa2STxaIe2OkNzeota4rS1mkkwmLFdG16EQo8bXdMVcpc2tss9oN1UsRXklpzD_rStA')` }}
                                ></div>
                                <div className="absolute inset-0 bg-gradient-to-t from-black/80 via-black/30 to-transparent flex flex-col justify-end p-8">
                                    <h3 className="text-white font-bold text-xl">Smartphones</h3>
                                    <p className="text-white/80 text-xs mt-1">Kết nối không giới hạn mọi lúc mọi nơi.</p>
                                    <div className="mt-2 flex items-center gap-1.5 text-xs font-semibold text-primary bg-white/10 w-fit px-3 py-1 rounded-full backdrop-blur-md">
                                        Xem Chi Tiết <ArrowUpRight size={14} />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>

                {/* Trending Products (Product Catalog) */}
                <section id="shop" className="py-16 max-w-[1440px] mx-auto px-margin-mobile md:px-margin-desktop scroll-mt-24">
                    <div className="text-center mb-16 space-y-4">
                        <h2 className="text-3xl font-bold text-on-surface">Sản Phẩm Đang Thịnh Hành</h2>
                        <p className="text-on-surface-variant max-w-xl mx-auto">
                            Những sản phẩm công nghệ bán chạy và được chuyên gia của chúng tôi đánh giá cao nhất.
                        </p>
                        
                        {/* Search and Filters Status Info */}
                        {(searchTerm || selectedCategory !== 'All') && (
                            <div className="flex flex-wrap items-center justify-center gap-2 pt-2">
                                <span className="text-xs text-on-surface-variant">Bộ lọc đang kích hoạt:</span>
                                {selectedCategory !== 'All' && (
                                    <span className="bg-primary/10 border border-primary/20 text-primary px-3 py-1 rounded-full text-xs font-semibold">
                                        Danh mục: {selectedCategory}
                                    </span>
                                )}
                                {searchTerm && (
                                    <span className="bg-primary/10 border border-primary/20 text-primary px-3 py-1 rounded-full text-xs font-semibold">
                                        Từ khóa: "{searchTerm}"
                                    </span>
                                )}
                                <button 
                                    onClick={() => { setSelectedCategory('All'); setSearchTerm(''); }}
                                    className="text-xs text-rose-500 hover:text-rose-600 font-bold hover:underline"
                                >
                                    Xóa bộ lọc trang chủ
                                </button>
                            </div>
                        )}
                    </div>

                    {/* Loader State */}
                    {loading ? (
                        <div className="flex flex-col justify-center items-center h-80 gap-4">
                            <div className="animate-spin rounded-full h-10 w-10 border-b-2 border-primary"></div>
                            <span className="text-xs font-medium text-on-surface-variant animate-pulse">Đang tải danh sách sản phẩm...</span>
                        </div>
                    ) : error ? (
                        <div className="mt-12 mx-auto max-w-md rounded-3xl border border-error/10 bg-error-container/5 p-6 text-center backdrop-blur-md">
                            <AlertCircle className="mx-auto text-error" size={32} />
                            <h3 className="mt-3 text-lg font-bold text-on-surface">Đã xảy ra lỗi</h3>
                            <p className="mt-2 text-xs text-error">{error}</p>
                            <button
                                onClick={fetchProducts}
                                className="mt-4 inline-flex items-center gap-2 rounded-2xl bg-error text-white px-4 py-2.5 text-xs font-semibold hover:bg-error/95 transition-all"
                            >
                                Thử lại
                            </button>
                        </div>
                    ) : filteredProducts.length === 0 ? (
                        <div className="text-center py-20 bg-surface-container-low rounded-3xl border border-outline-variant/30 max-w-lg mx-auto">
                            <ShoppingBag className="mx-auto text-on-surface-variant opacity-40 mb-4" size={40} />
                            <h3 className="text-lg font-bold text-on-surface">Không tìm thấy sản phẩm nào</h3>
                            <p className="text-xs text-on-surface-variant mt-2">Vui lòng thử lại với từ khóa hoặc danh mục khác.</p>
                            <button 
                                onClick={() => { setSelectedCategory('All'); setSearchTerm(''); }}
                                className="mt-4 px-4 py-2 bg-primary text-white rounded-full text-xs font-semibold hover:bg-primary-container"
                            >
                                Hiển thị tất cả sản phẩm
                            </button>
                        </div>
                    ) : (
                        <>
                        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-gutter">
                            {filteredProducts.slice(0, 8).map((product, index) => {
                                const isAvailable = product.inventory?.availableQuantity > 0;
                                const isNew = index % 3 === 0;
                                const isSale = index % 4 === 1;
                                
                                return (
                                    <Link 
                                        to={`/product/${product.id}`}
                                        key={product.id} 
                                        className="group bg-surface-container-lowest rounded-2xl border border-outline-variant/30 hover-lift overflow-hidden flex flex-col justify-between"
                                    >
                                        <div className="aspect-square relative overflow-hidden bg-surface-container-low p-6 flex items-center justify-center">
                                            <img 
                                                className="w-full h-full object-contain group-hover:scale-110 transition-transform duration-500" 
                                                alt={product.name}
                                                src={getProductImage(product)} 
                                            />
                                            {/* Stock / Promotion Badges */}
                                            <div className="absolute top-4 left-4 flex flex-col gap-2">
                                                {isNew && (
                                                    <span className="bg-primary text-white text-[10px] font-bold px-2 py-1 rounded-full uppercase tracking-wider">
                                                        Mới
                                                    </span>
                                                )}
                                                {isSale && (
                                                    <span className="bg-error text-white text-[10px] font-bold px-2 py-1 rounded-full uppercase tracking-wider">
                                                        Sale
                                                    </span>
                                                )}
                                            </div>
                                            <div className="absolute top-4 right-4">
                                                <span className={`text-[10px] px-2.5 py-1 rounded-full font-bold uppercase ${isAvailable ? 'bg-emerald-500/10 text-emerald-600 border border-emerald-500/20' : 'bg-rose-500/10 text-rose-600 border border-rose-500/20'}`}>
                                                    {isAvailable ? `Còn ${product.inventory.availableQuantity}` : 'Hết hàng'}
                                                </span>
                                            </div>
                                        </div>

                                        <div className="p-6 flex-1 flex flex-col justify-between space-y-4">
                                            <div className="space-y-1">
                                                <div className="flex justify-between items-start">
                                                    <div className="flex items-center gap-1 text-on-surface-variant text-[10px] font-bold uppercase tracking-wider">
                                                        <Tag size={10} /> 
                                                        <span>{product.category?.name || 'Điện Tử'}</span>
                                                    </div>
                                                    <div className="flex items-center text-tertiary">
                                                        <Star size={12} className="fill-amber-500 text-amber-500" />
                                                        <span className="text-xs font-bold ml-1">4.9</span>
                                                    </div>
                                                </div>
                                                <h3 className="font-bold text-base text-on-surface group-hover:text-primary transition-colors duration-200 line-clamp-1">
                                                    {product.name}
                                                </h3>
                                                <p className="text-xs text-on-surface-variant line-clamp-2 leading-relaxed">
                                                    {product.description || 'Không có mô tả cho sản phẩm này.'}
                                                </p>
                                            </div>

                                            <div className="pt-4 border-t border-outline-variant/30 space-y-3">
                                                <div className="flex items-baseline gap-2">
                                                    <span className="text-lg font-extrabold text-primary">
                                                        {product.price.toLocaleString()}đ
                                                    </span>
                                                    {isSale && (
                                                        <span className="text-xs text-on-surface-variant line-through">
                                                            {(product.price * 1.2).toLocaleString()}đ
                                                        </span>
                                                    )}
                                                </div>

                                                <button 
                                                    onClick={(e) => handleAddToCart(e, product)}
                                                    disabled={!isAvailable}
                                                    className="w-full bg-surface-container border border-outline-variant/50 text-on-surface py-3 rounded-xl text-xs font-semibold hover:bg-primary hover:text-white hover:border-primary disabled:opacity-50 disabled:pointer-events-none transition-all flex items-center justify-center gap-2"
                                                >
                                                    <ShoppingCart size={14} /> Thêm Vào Giỏ
                                                </button>
                                            </div>
                                        </div>
                                    </Link>
                                );
                            })}
                        </div>
                        {filteredProducts.length > 0 && (
                            <div className="flex justify-center mt-12">
                                <Link
                                    to="/products"
                                    className="inline-flex items-center gap-2 bg-primary text-white px-8 py-4 rounded-2xl text-sm font-bold hover:bg-primary-container transition-all shadow-lg shadow-primary/25 active:scale-95"
                                >
                                    Xem Tất Cả Sản Phẩm
                                    <ArrowUpRight size={18} />
                                </Link>
                            </div>
                        )}
                        </>
                    )}
                </section>

                {/* Value Props Section */}
                <section className="py-10 border-y border-outline-variant/30 bg-surface-container-lowest">
                    <div className="max-w-[1440px] mx-auto px-margin-mobile md:px-margin-desktop">
                        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-8">
                            <div className="flex items-center gap-4">
                                <div className="w-12 h-12 bg-primary/5 text-primary rounded-full flex items-center justify-center shrink-0">
                                    <Truck size={24} />
                                </div>
                                <div>
                                    <h4 className="font-bold text-sm text-on-surface">Giao Hàng Toàn Quốc</h4>
                                    <p className="text-xs text-on-surface-variant">Miễn phí với đơn hàng trên 2.000.000đ</p>
                                </div>
                            </div>
                            <div className="flex items-center gap-4">
                                <div className="w-12 h-12 bg-primary/5 text-primary rounded-full flex items-center justify-center shrink-0">
                                    <User size={24} />
                                </div>
                                <div>
                                    <h4 className="font-bold text-sm text-on-surface">Đồng Hành 24/7</h4>
                                    <p className="text-xs text-on-surface-variant">Đội ngũ chuyên gia kỹ thuật hỗ trợ</p>
                                </div>
                            </div>
                            <div className="flex items-center gap-4">
                                <div className="w-12 h-12 bg-primary/5 text-primary rounded-full flex items-center justify-center shrink-0">
                                    <ShieldCheck size={24} />
                                </div>
                                <div>
                                    <h4 className="font-bold text-sm text-on-surface">Thanh Toán Bảo Mật</h4>
                                    <p className="text-xs text-on-surface-variant">Giao dịch mã hóa an toàn 100%</p>
                                </div>
                            </div>
                            <div className="flex items-center gap-4">
                                <div className="w-12 h-12 bg-primary/5 text-primary rounded-full flex items-center justify-center shrink-0">
                                    <Award size={24} />
                                </div>
                                <div>
                                    <h4 className="font-bold text-sm text-on-surface">1 Năm Bảo Hành</h4>
                                    <p className="text-xs text-on-surface-variant">Chất lượng được chứng thực tốt nhất</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>

                {/* Contact Section */}
                <section id="contact" className="py-16 max-w-[1440px] mx-auto px-margin-mobile md:px-margin-desktop">
                    <div className="bg-primary-container/20 rounded-3xl p-8 md:p-12 lg:p-20 relative overflow-hidden">
                        <div className="absolute top-0 right-0 w-1/2 h-full opacity-5 pointer-events-none hidden lg:block">
                            <HelpCircle size={400} className="absolute -right-20 -top-20 text-primary" />
                        </div>
                        <div className="grid grid-cols-1 lg:grid-cols-2 gap-12 relative z-10">
                            <div className="space-y-6">
                                <h2 className="text-2xl md:text-3xl font-bold text-primary">Cần Tư Vấn Chuyên Gia?</h2>
                                <p className="text-sm md:text-base text-on-surface-variant leading-relaxed max-w-md">
                                    Các kỹ sư công nghệ của chúng tôi luôn sẵn sàng hỗ trợ bạn thiết lập cấu hình tốt nhất. Hãy gửi liên hệ để nhận tư vấn chuyên sâu 1-1 miễn phí.
                                </p>
                                <div className="space-y-4 pt-4 text-sm text-on-surface-variant">
                                    <div className="flex items-center gap-4">
                                        <Mail size={18} className="text-primary" />
                                        <span className="font-medium">support@luminatech.com</span>
                                    </div>
                                    <div className="flex items-center gap-4">
                                        <Phone size={18} className="text-primary" />
                                        <span className="font-medium">+84 (1800) LUMINA</span>
                                    </div>
                                </div>
                            </div>

                            <div className="bg-surface-container-lowest p-6 md:p-8 rounded-2xl shadow-xl border border-outline-variant/20">
                                {contactSubmitted ? (
                                    <div className="text-center py-12 space-y-4">
                                        <div className="w-16 h-16 bg-emerald-100 text-emerald-600 rounded-full flex items-center justify-center mx-auto">
                                            <CheckCircle2 size={36} />
                                        </div>
                                        <h3 className="text-lg font-bold text-on-surface">Đã gửi tin nhắn thành công!</h3>
                                        <p className="text-xs text-on-surface-variant max-w-xs mx-auto">
                                            Cảm ơn bạn đã liên hệ. Đội ngũ chuyên gia kỹ thuật của iLuminaty Shop sẽ liên hệ lại qua email trong vòng 24 giờ.
                                        </p>
                                    </div>
                                ) : (
                                    <form onSubmit={handleContactSubmit} className="space-y-4">
                                        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                                            <input 
                                                className="w-full bg-surface border border-outline-variant rounded-xl p-4 focus:ring-2 focus:ring-primary focus:border-transparent outline-none text-sm text-on-surface" 
                                                placeholder="Họ và Tên" 
                                                type="text"
                                                required
                                                value={contactForm.name}
                                                onChange={(e) => setContactForm({ ...contactForm, name: e.target.value })}
                                            />
                                            <input 
                                                className="w-full bg-surface border border-outline-variant rounded-xl p-4 focus:ring-2 focus:ring-primary focus:border-transparent outline-none text-sm text-on-surface" 
                                                placeholder="Địa chỉ Email" 
                                                type="email"
                                                required
                                                value={contactForm.email}
                                                onChange={(e) => setContactForm({ ...contactForm, email: e.target.value })}
                                            />
                                        </div>
                                        <textarea 
                                            className="w-full bg-surface border border-outline-variant rounded-xl p-4 focus:ring-2 focus:ring-primary focus:border-transparent outline-none text-sm text-on-surface" 
                                            placeholder="Bạn cần hỗ trợ cấu hình hoặc thắc mắc gì?" 
                                            rows="4"
                                            required
                                            value={contactForm.message}
                                            onChange={(e) => setContactForm({ ...contactForm, message: e.target.value })}
                                        ></textarea>
                                        <button 
                                            className="w-full bg-primary text-white py-4 rounded-xl font-bold text-sm hover:bg-primary-container transition-all shadow-lg hover:shadow-xl shadow-primary/20" 
                                            type="submit"
                                        >
                                            Gửi Tin Nhắn
                                        </button>
                                    </form>
                                )}
                            </div>
                        </div>
                    </div>
                </section>
            </main>

            {/* Footer (Shared Component: Footer) */}
            <footer className="w-full bg-surface-container-lowest border-t border-outline-variant/30 mt-16">
                <div className="max-w-[1440px] mx-auto px-margin-mobile md:px-margin-desktop py-16">
                    <div className="flex flex-col lg:flex-row justify-between gap-12 mb-16">
                        {/* Brand & Newsletter */}
                        <div className="max-w-sm space-y-6">
                            <div className="flex items-center gap-3">
                                <img 
                                    alt="iLuminaty Shop Logo" 
                                    className="h-[60px] w-auto object-contain" 
                                    src="/Favicon.png"
                                />
                            </div>
                            <p className="text-sm text-on-surface-variant leading-relaxed">
                                Đăng ký nhận tin tức để không bỏ lỡ các công nghệ phần cứng đột phá và sự kiện khuyến mãi mới nhất.
                            </p>
                            <div className="flex gap-2">
                                <input 
                                    className="flex-1 bg-surface-container-low border border-outline-variant/50 rounded-xl px-4 py-3 text-xs outline-none focus:border-primary text-on-surface" 
                                    placeholder="Địa chỉ Email của bạn" 
                                    type="email"
                                />
                                <button className="bg-primary text-white px-5 py-3 rounded-xl hover:bg-primary-container text-xs font-semibold transition-all">
                                    Đăng Ký
                                </button>
                            </div>
                        </div>

                        {/* Footer Links Grid */}
                        <div className="grid grid-cols-2 sm:grid-cols-3 gap-12 lg:gap-24">
                            <div className="space-y-4">
                                <h4 className="font-bold text-sm text-on-surface">Danh Mục</h4>
                                <ul className="space-y-2 text-xs text-on-surface-variant">
                                    <li><button onClick={() => toggleCategory('Laptops')} className="hover:text-primary transition-colors">Laptops & PCs</button></li>
                                    <li><button onClick={() => toggleCategory('Audio')} className="hover:text-primary transition-colors">Thiết Bị Âm Thanh</button></li>
                                    <li><button onClick={() => toggleCategory('Gaming')} className="hover:text-primary transition-colors">Thiết Bị Gaming</button></li>
                                    <li><button onClick={() => toggleCategory('Smartphones')} className="hover:text-primary transition-colors">Smartphones</button></li>
                                </ul>
                            </div>
                            <div className="space-y-4">
                                <h4 className="font-bold text-sm text-on-surface">Tuyển Dụng</h4>
                                <ul className="space-y-2 text-xs text-on-surface-variant">
                                    <li><Link to="/recruitment" className="hover:text-primary transition-colors">Vị Trí Đang Tuyển</Link></li>
                                    <li><a className="hover:text-primary transition-colors" href="mailto:hr@iluminaty.com">Gửi CV</a></li>
                                    <li><Link to="/recruitment" className="hover:text-primary transition-colors">Về iLuminaty Shop</Link></li>
                                    <li><a className="hover:text-primary transition-colors" href="#contact">Liên Hệ HR</a></li>
                                </ul>
                            </div>
                            <div className="space-y-4">
                                <h4 className="font-bold text-sm text-on-surface">Mạng Xã Hội</h4>
                                <div className="flex gap-3">
                                    <a className="w-9 h-9 bg-surface-container-low rounded-full flex items-center justify-center hover:bg-primary hover:text-white transition-all text-on-surface-variant" href="#"><Share2 size={16} /></a>
                                    <a className="w-9 h-9 bg-surface-container-low rounded-full flex items-center justify-center hover:bg-primary hover:text-white transition-all text-on-surface-variant" href="#"><Globe size={16} /></a>
                                    <a className="w-9 h-9 bg-surface-container-low rounded-full flex items-center justify-center hover:bg-primary hover:text-white transition-all text-on-surface-variant" href="#"><AtSign size={16} /></a>
                                </div>
                            </div>
                        </div>
                    </div>

                    {/* Copyright and Legal */}
                    <div className="pt-8 border-t border-outline-variant/20 flex flex-col sm:flex-row justify-between items-center gap-4">
                        <p className="text-xs text-on-surface-variant">© 2026 iLuminaty Shop. Mọi quyền được bảo lưu.</p>
                        <div className="flex flex-wrap justify-center gap-6 text-xs text-on-surface-variant">
                            <a className="hover:text-primary transition-colors" href="#">Chính Sách Bảo Mật</a>
                            <a className="hover:text-primary transition-colors" href="#">Điều Khoản Dịch Vụ</a>
                            <a className="hover:text-primary transition-colors" href="#">Chính Sách Cookie</a>
                        </div>
                    </div>
                </div>
            </footer>
        </div>
    );
}
