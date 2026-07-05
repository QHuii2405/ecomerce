import Swal from "sweetalert2";
import React, { useEffect, useState } from 'react';
import api from '../api/axios';
import { Link, useNavigate } from 'react-router-dom';
import { addToCart, getCartCount } from '../api/cartStore';
import {
    Search, ShoppingCart, Tag, Star, ArrowLeft, Filter, AlertCircle, ShoppingBag, X, CheckCircle2, Heart
} from 'lucide-react';

export default function Products() {
    const navigate = useNavigate();
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [wishlist, setWishlist] = useState([]);

    const [searchTerm, setSearchTerm] = useState('');
    const [selectedCategory, setSelectedCategory] = useState('All');
    const [selectedBrand, setSelectedBrand] = useState('All');
    const [selectedPrice, setSelectedPrice] = useState('All');
    const [brandOptions, setBrandOptions] = useState([]);
    const [cartCount, setCartCount] = useState(getCartCount());
    const [toast, setToast] = useState(null);

    const token = localStorage.getItem('token');

    const showToast = (message) => {
        setToast(message);
        setTimeout(() => setToast(null), 2500);
    };

    useEffect(() => {
        const updateCount = () => setCartCount(getCartCount());
        window.addEventListener('cart-updated', updateCount);
        window.scrollTo(0, 0);
        return () => window.removeEventListener('cart-updated', updateCount);
    }, []);

    useEffect(() => {
        fetchProducts();
    }, [selectedCategory, selectedBrand, selectedPrice, searchTerm]);

    useEffect(() => {
        fetchBrands();
        setSelectedBrand('All');
        if (token) fetchWishlist();
    }, [selectedCategory, token]);

    const fetchWishlist = async () => {
        try {
            const response = await api.get('/Wishlist');
            setWishlist((response.data.data || []).map(p => p.id));
        } catch (err) {}
    };

    const handleToggleWishlist = async (e, productId) => {
        e.preventDefault();
        e.stopPropagation();
        if (!token) {
            Swal.fire({
                icon: "info",
                text: "Vui lòng đăng nhập để sử dụng tính năng yêu thích!"
            });
            navigate('/login');
            return;
        }
        try {
            await api.post(`/Wishlist/${productId}/toggle`);
            if (wishlist.includes(productId)) {
                setWishlist(wishlist.filter(id => id !== productId));
                showToast("Đã bỏ khỏi danh sách yêu thích");
            } else {
                setWishlist([...wishlist, productId]);
                showToast("Đã thêm vào danh sách yêu thích");
            }
        } catch (err) {
            showToast("Có lỗi xảy ra");
        }
    };

    const getPriceRange = () => {
        if (selectedPrice === '<5M') return { maxPrice: 4999999 };
        if (selectedPrice === '5M-20M') return { minPrice: 5000000, maxPrice: 20000000 };
        if (selectedPrice === '>20M') return { minPrice: 20000001 };
        return {};
    };

    const fetchProducts = async () => {
        try {
            setLoading(true);
            const params = {
                ...(selectedCategory !== 'All' ? { category: selectedCategory } : {}),
                ...(selectedBrand !== 'All' ? { brand: selectedBrand } : {}),
                ...(searchTerm.trim() ? { search: searchTerm.trim() } : {}),
                ...getPriceRange()
            };
            const response = await api.get('/Products', { params });
            setProducts(response.data.data || []);
            setError(null);
        } catch (err) {
            setError("Không thể tải danh sách sản phẩm.");
        } finally {
            setLoading(false);
        }
    };

    const fetchBrands = async () => {
        try {
            const params = selectedCategory !== 'All' ? { category: selectedCategory } : {};
            const response = await api.get('/Products/brands', { params });
            setBrandOptions(response.data.data || []);
        } catch {
            setBrandOptions([]);
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

    const getProductImage = (product) => {
        if (product.imageUrls && product.imageUrls.length > 0) {
            const url = product.imageUrls[0];
            return url.startsWith('http') ? url : `${import.meta.env.VITE_API_BASE_URL}${url}`;
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
        return 'https://lh3.googleusercontent.com/aida-public/AB6AXuAltOeyhWUN8d_OI3RZgPhmFTnEFiE8btibjVA1UyeS4BMmMyjSKaTmgBimNagFrcF5ixaI_tKAShIux-GWN1Ed-N4cXrCROBioCaBreSt4h4NtC8LD-0H3MX6jv_fs4XT3pt7d0fecPOmOrn9wrTrKkLcAH0eYV75rcouQVMTlc39VoiWaFpa2STxaIe2OkNzeota4rS1mkkwmLFdG16EQo8bXdMVcpc2tss9oN1UsRXklpzD_rStA';
    };

    const filteredProducts = products;

    return (
        <div className="min-h-screen bg-background text-on-surface flex flex-col font-sans">
            {toast && (
                <div className="fixed top-20 right-4 z-[999] bg-slate-900 border border-emerald-500/20 text-white px-4 py-3 rounded-2xl shadow-2xl flex items-center gap-3 animate-in slide-in-from-right duration-300">
                    <div className="w-6 h-6 bg-emerald-500/20 rounded-full flex items-center justify-center flex-shrink-0">
                        <CheckCircle2 size={14} className="text-emerald-400" />
                    </div>
                    <p className="text-sm font-medium">{toast}</p>
                </div>
            )}
            
            {/* Header */}
            <nav className="sticky top-0 z-40 bg-surface/80 backdrop-blur-md border-b border-outline-variant/30 py-4 shadow-sm">
                <div className="max-w-[1440px] mx-auto px-4 md:px-12 flex items-center justify-between">
                    <div className="flex items-center gap-4">
                        <Link to="/" className="p-2 -ml-2 text-on-surface-variant hover:text-primary transition-colors">
                            <ArrowLeft size={20} />
                        </Link>
                        <span className="font-bold text-lg hidden sm:block">Tất Cả Sản Phẩm</span>
                    </div>
                    
                    <div className="flex-1 max-w-xl px-4 relative">
                        <input 
                            type="text" 
                            placeholder="Tìm kiếm sản phẩm..." 
                            value={searchTerm}
                            onChange={(e) => setSearchTerm(e.target.value)}
                            className="w-full bg-surface-container-low border border-outline-variant/50 rounded-full pl-5 pr-10 py-2.5 text-sm focus:outline-none focus:ring-2 focus:ring-primary/20 focus:border-primary text-on-surface"
                        />
                        <Search size={16} className="absolute right-8 top-3.5 text-on-surface-variant" />
                    </div>

                    <Link to="/cart" className="relative p-2 text-on-surface-variant hover:text-primary transition-colors">
                        <ShoppingCart size={22} />
                        {cartCount > 0 && (
                            <span className="absolute 0 right-0 bg-primary text-white text-[10px] min-w-[18px] h-[18px] flex items-center justify-center rounded-full font-bold px-1">
                                {cartCount > 99 ? '99+' : cartCount}
                            </span>
                        )}
                    </Link>
                </div>
            </nav>

            <main className="max-w-[1440px] mx-auto px-4 md:px-12 py-8 w-full flex flex-col md:flex-row gap-8 flex-1">
                {/* Filters Sidebar */}
                <aside className="w-full md:w-64 flex-shrink-0 space-y-6">
                    <div className="bg-surface-container-lowest border border-outline-variant/30 rounded-2xl p-5 sticky top-24">
                        <div className="flex items-center gap-2 mb-4">
                            <Filter size={18} className="text-primary" />
                            <h3 className="font-bold text-on-surface">Bộ Lọc</h3>
                        </div>
                        
                        <div className="space-y-5">
                            <div>
                                <p className="text-xs font-bold text-on-surface-variant uppercase tracking-wider mb-3">Danh Mục</p>
                                <div className="space-y-2">
                                    {['All', 'Laptops', 'Gaming', 'Audio', 'Smartphones'].map(cat => (
                                        <label key={cat} className="flex items-center gap-2 text-sm cursor-pointer hover:text-primary transition-colors">
                                            <input 
                                                type="radio" 
                                                name="category" 
                                                checked={selectedCategory === cat}
                                                onChange={() => setSelectedCategory(cat)}
                                                className="accent-primary"
                                            />
                                            {cat === 'All' ? 'Tất cả' : cat}
                                        </label>
                                    ))}
                                </div>
                            </div>
                            <div className="border-t border-outline-variant/20 pt-4">
                                <p className="text-xs font-bold text-on-surface-variant uppercase tracking-wider mb-3">Hãng</p>
                                <div className="space-y-2 max-h-48 overflow-y-auto pr-1">
                                    {['All', ...brandOptions].map(brand => (
                                        <label key={brand} className="flex items-center gap-2 text-sm cursor-pointer hover:text-primary transition-colors">
                                            <input
                                                type="radio"
                                                name="brand"
                                                checked={selectedBrand === brand}
                                                onChange={() => setSelectedBrand(brand)}
                                                className="accent-primary"
                                            />
                                            {brand === 'All' ? 'Tất cả hãng' : brand}
                                        </label>
                                    ))}
                                </div>
                            </div>

                            <div className="border-t border-outline-variant/20 pt-4">
                                <p className="text-xs font-bold text-on-surface-variant uppercase tracking-wider mb-3">Mức Giá</p>
                                <div className="space-y-2">
                                    {[
                                        { id: 'All', label: 'Mọi mức giá' },
                                        { id: '<5M', label: 'Dưới 5 triệu' },
                                        { id: '5M-20M', label: 'Từ 5 - 20 triệu' },
                                        { id: '>20M', label: 'Trên 20 triệu' }
                                    ].map(price => (
                                        <label key={price.id} className="flex items-center gap-2 text-sm cursor-pointer hover:text-primary transition-colors">
                                            <input 
                                                type="radio" 
                                                name="price" 
                                                checked={selectedPrice === price.id}
                                                onChange={() => setSelectedPrice(price.id)}
                                                className="accent-primary"
                                            />
                                            {price.label}
                                        </label>
                                    ))}
                                </div>
                            </div>
                            
                            {(selectedCategory !== 'All' || selectedBrand !== 'All' || selectedPrice !== 'All' || searchTerm) && (
                                <button 
                                    onClick={() => { setSelectedCategory('All'); setSelectedBrand('All'); setSelectedPrice('All'); setSearchTerm(''); }}
                                    className="w-full flex items-center justify-center gap-2 py-2 mt-4 text-xs font-bold text-rose-500 bg-rose-500/10 rounded-xl hover:bg-rose-500/20 transition-colors"
                                >
                                    <X size={14} /> Xóa bộ lọc
                                </button>
                            )}
                        </div>
                    </div>
                </aside>

                {/* Product Grid */}
                <div className="flex-1">
                    {loading ? (
                        <div className="flex flex-col justify-center items-center h-64 gap-4">
                            <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-primary"></div>
                            <span className="text-xs font-medium text-on-surface-variant animate-pulse">Đang tải danh sách sản phẩm...</span>
                        </div>
                    ) : error ? (
                        <div className="mt-12 mx-auto max-w-md rounded-3xl border border-error/10 bg-error-container/5 p-6 text-center">
                            <AlertCircle className="mx-auto text-error mb-3" size={32} />
                            <h3 className="text-lg font-bold text-on-surface">Đã xảy ra lỗi</h3>
                            <p className="text-xs text-error mt-2">{error}</p>
                        </div>
                    ) : filteredProducts.length === 0 ? (
                        <div className="text-center py-20 bg-surface-container-lowest rounded-3xl border border-dashed border-outline-variant/30 max-w-lg mx-auto">
                            <ShoppingBag className="mx-auto text-on-surface-variant opacity-40 mb-4" size={40} />
                            <h3 className="text-lg font-bold text-on-surface">Không tìm thấy sản phẩm</h3>
                            <p className="text-xs text-on-surface-variant mt-2">Vui lòng thử lại với từ khóa hoặc bộ lọc khác.</p>
                        </div>
                    ) : (
                        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
                            {filteredProducts.map((product, index) => {
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
                                            <div className="absolute top-4 left-4 flex flex-col gap-2">
                                                {isNew && <span className="bg-primary text-white text-[10px] font-bold px-2 py-1 rounded-full uppercase tracking-wider">Mới</span>}
                                                {isSale && <span className="bg-error text-white text-[10px] font-bold px-2 py-1 rounded-full uppercase tracking-wider">Sale</span>}
                                            </div>
                                            <div className="absolute top-4 right-4 flex flex-col gap-2">
                                                <button 
                                                    onClick={(e) => handleToggleWishlist(e, product.id)}
                                                    className={`p-2 rounded-full backdrop-blur-md border shadow-sm transition-all ${wishlist.includes(product.id) ? 'bg-rose-500/10 border-rose-500/20 text-rose-500' : 'bg-white/80 border-outline-variant/30 text-on-surface-variant hover:text-rose-500'}`}
                                                >
                                                    <Heart size={16} className={wishlist.includes(product.id) ? "fill-rose-500" : ""} />
                                                </button>
                                                <span className={`text-[10px] px-2.5 py-1 rounded-full font-bold uppercase text-center ${isAvailable ? 'bg-emerald-500/10 text-emerald-600 border border-emerald-500/20' : 'bg-rose-500/10 text-rose-600 border border-rose-500/20'}`}>
                                                    {isAvailable ? `Còn ${product.inventory.availableQuantity}` : 'Hết hàng'}
                                                </span>
                                            </div>
                                        </div>

                                        <div className="p-5 flex-1 flex flex-col justify-between space-y-4">
                                            <div className="space-y-1">
                                                <div className="flex justify-between items-start">
                                                    <div className="flex items-center gap-1 text-on-surface-variant text-[10px] font-bold uppercase tracking-wider">
                                                        <Tag size={10} /> 
                                                        <span>{product.brand || product.category?.name || 'Điện Tử'}</span>
                                                    </div>
                                                    <div className="flex items-center text-tertiary">
                                                        <Star size={12} className="fill-amber-500 text-amber-500" />
                                                        <span className="text-xs font-bold ml-1">4.9</span>
                                                    </div>
                                                </div>
                                                <h3 className="font-bold text-sm text-on-surface group-hover:text-primary transition-colors duration-200 line-clamp-1">
                                                    {product.name}
                                                </h3>
                                            </div>

                                            <div className="pt-3 border-t border-outline-variant/30 space-y-3">
                                                <div className="flex items-baseline gap-2">
                                                    <span className="text-base font-extrabold text-primary">
                                                        {product.price.toLocaleString()}đ
                                                    </span>
                                                    {isSale && (
                                                        <span className="text-[10px] text-on-surface-variant line-through">
                                                            {(product.price * 1.2).toLocaleString()}đ
                                                        </span>
                                                    )}
                                                </div>

                                                <button 
                                                    onClick={(e) => handleAddToCart(e, product)}
                                                    disabled={!isAvailable}
                                                    className="w-full bg-surface-container border border-outline-variant/50 text-on-surface py-2.5 rounded-xl text-xs font-semibold hover:bg-primary hover:text-white hover:border-primary disabled:opacity-50 disabled:pointer-events-none transition-all flex items-center justify-center gap-2"
                                                >
                                                    <ShoppingCart size={14} /> Thêm Vào Giỏ
                                                </button>
                                            </div>
                                        </div>
                                    </Link>
                                );
                            })}
                        </div>
                    )}
                </div>
            </main>
        </div>
    );
}
