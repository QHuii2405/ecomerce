import Swal from "sweetalert2";
import React, { useEffect, useState, useMemo } from 'react';
import { useParams, Link, useNavigate, useLocation } from 'react-router-dom';
import api from '../api/axios';
import { addToCart } from '../api/cartStore';
import { getSuggestedAccessories } from '../utils/productExtras';
import {
  ArrowLeft, ShoppingCart, Zap, Package, Tag, Star,
  Check, AlertCircle, ChevronRight, Truck, ShieldCheck, RotateCcw,
  Plus, Minus, Info, FileText, MessageSquare, Layers, Heart, CornerDownRight, Reply
} from 'lucide-react';

const PRODUCT_IMAGES = {
  laptop:     'https://lh3.googleusercontent.com/aida-public/AB6AXuDzzkgw3qdK2qx_eejr9Oee4qzRfJoNIb-smYiUqNBNBZ4_KNAbm4HxoqNIyfUqk9pV0qWkdyFf7t7SYsjbbwCkkEN06FQNQBCseQPzXacixmLlO0YB5GVfxd7AR42kwnUufnptDgHCXnHlGXhg3x4QzV7sXZkMAYLQHcoPSLjWbTIWG3W_hrzh4eWEuFkuhEuk_62jmY9dMbWUMr11EIDivHK_RMuJDGpKxGyfr8JRcfcL8i0qv6Ve',
  gaming:     'https://lh3.googleusercontent.com/aida-public/AB6AXuCtePE6a8azsOINxsGhPHJGa7pybuOEtEXGQUTmgTTPhKMBwOS-7FOpWVDKTgo17TVnuDYz0tGqUltaJ9vPZxqJCWCdknYb9x1VpLfsq9eC8Qlc1fG2cjNg5i2klZdqsM6d2QHuBDP_mHVRg-Ley5Dw6z3yNJURrfQ5bnPx_TyxzBc7EuP7b5boquCcOezT3SWcmEEoMElGb7VQSAkNtos5xtMcNxJFn8D0-Oq4x3zH0vgAz5bRpJdx',
  audio:      'https://lh3.googleusercontent.com/aida-public/AB6AXujCnwa9YYlaySQHciXXGs5ENqNsPlYyM48pBFebzeWQc7dPKrvdRI9-hr5S5mxpSSR3ZK689MtFW5CFO2yl2XF3_1RCn3iwdnmtwXFQeVUo_FXR0vOQ7FYE_qzsTZb4Q8_zaMowCCLDFa84jAXRV-XqKV2AZPbY2fWUotHuBFbc9Jv95ESTZuet5JJdQVxXrmhm4ItvrDA3BDDkW6wfXdjWtO5ynIppnxJllrxffafcwXaW4XKodrR',
  smartphone: 'https://lh3.googleusercontent.com/aida-public/AB6AXuAltOeyhWUN8d_OI3RZgPhmFTnEFiE8btibjVA1UyeS4BMmMyjSKaTmgBimNagFrcF5ixaI_tKAShIux-GWN1Ed-N4cXrCROBioCaBreSt4h4NtC8LD-0H3MX6jv_fs4XT3pt7d0fecPOmOrn9wrTrKkLcAH0eYV75rcouQVMTlc39VoiWaFpa2STxaIe2OkNzeota4rS1mkkwmLFdG16EQo8bXdMVcpc2tss9oN1UsRXklpzD_rStA',
};

function getProductImage(product) {
  if (product.imageUrls && product.imageUrls.length > 0) {
    return product.imageUrls[0].startsWith('http') ? product.imageUrls[0] : `${import.meta.env.VITE_API_BASE_URL}${product.imageUrls[0]}`;
  }
  const name = (product.name || '').toLowerCase();
  const cat  = (product.category?.name || '').toLowerCase();
  if (cat.includes('laptop') || name.includes('laptop') || name.includes('book') || name.includes('studio') || name.includes('coder')) return PRODUCT_IMAGES.laptop;
  if (cat.includes('gaming') || cat.includes('peripheral')) return PRODUCT_IMAGES.gaming;
  if (cat.includes('audio')) return PRODUCT_IMAGES.audio;
  return PRODUCT_IMAGES.smartphone;
}

function StockBadge({ product }) {
  const qty = product.inventory?.availableQuantity ?? product.inventory?.stockQuantity ?? 0;
  if (qty <= 0)  return <span className="text-rose-400 text-sm font-semibold flex items-center gap-1"><AlertCircle size={14} /> Hết hàng</span>;
  if (qty < 5)   return <span className="text-amber-400 text-sm font-semibold">Sắp hết — chỉ còn {qty} sản phẩm</span>;
  return <span className="text-emerald-400 text-sm font-semibold flex items-center gap-1"><Check size={14} /> Còn hàng ({qty})</span>;
}

function ColorSwatch({ colorName, selected, onClick }) {
  const hexMap = {
    'Black': '#000000', 'White': '#ffffff', 'Silver': '#c0c0c0', 'Midnight': '#191970',
    'Natural Titanium': '#b0b0b0', 'Blue Titanium': '#2a4b7c', 'Titanium Black': '#1a1a1a', 'Titanium Yellow': '#f5d547',
    'Đen Midnight': '#191970', 'Bạc Titan': '#b0b0b0', 'Trắng Ngọc': '#ffffff', 'Xanh Aurora': '#2a4b7c',
    'Đen Obsidian': '#000000', 'Bạc Platinum': '#e5e4e2', 'Vàng Champagne': '#f7e7ce',
    'Đen Matte': '#28282b', 'Trắng Snow': '#fffafa', 'Hồng Sakura': '#ffb7c5', 'RGB Limited': 'linear-gradient(45deg, red, blue, green)'
  };
  const hex = hexMap[colorName] || '#999999';
  const isGradient = hex.startsWith('linear');
  return (
    <button
      onClick={onClick}
      title={colorName}
      className={`relative w-10 h-10 rounded-full border-2 transition-all active:scale-95 ${
        selected ? 'border-primary ring-2 ring-primary/30 scale-110' : 'border-outline-variant/40 hover:border-primary/50'
      }`}
    >
      <span
        className="absolute inset-1 rounded-full"
        style={isGradient ? { background: hex } : { backgroundColor: hex }}
      />
    </button>
  );
}

function OptionButton({ value, selected, onClick }) {
  return (
    <button
      onClick={onClick}
      className={`px-4 py-2.5 rounded-xl text-sm font-semibold border transition-all active:scale-95 ${
        selected
          ? 'border-primary bg-primary/10 text-primary'
          : 'border-outline-variant/40 text-on-surface-variant hover:border-primary/40'
      }`}
    >
      {value}
    </button>
  );
}

const TABS = [
  { id: 'info', label: 'Thông tin', icon: Info },
  { id: 'specs', label: 'Thông số', icon: FileText },
  { id: 'reviews', label: 'Đánh giá', icon: MessageSquare },
];

export default function ProductDetail() {
  const { id } = useParams();
  const navigate = useNavigate();
  const location = useLocation();
  const [product, setProduct] = useState(null);
  const [allProducts, setAllProducts] = useState([]);
  const [quantity, setQuantity] = useState(1);
  const [loading, setLoading] = useState(true);
  const [addedToast, setAddedToast] = useState(false);
  const [activeTab, setActiveTab] = useState('info');
  const [selectedAttributes, setSelectedAttributes] = useState({});
  const [reviews, setReviews] = useState([]);
  const [reviewEligibility, setReviewEligibility] = useState(null);
  const [reviewForm, setReviewForm] = useState({ rating: 5, comment: '' });
  const [submittingReview, setSubmittingReview] = useState(false);
  const [activeImageIndex, setActiveImageIndex] = useState(0);
  
  const [isFavorited, setIsFavorited] = useState(false);

  const token = localStorage.getItem('token');
  let isAdmin = false;
  if (token) {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const role = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || payload.role;
      isAdmin = role === 'Admin' || role === 'Staff';
    } catch(e) {}
  }

  useEffect(() => {
    const params = new URLSearchParams(location.search);
    const tab = params.get('tab');
    if (tab) {
      setActiveTab(tab);
      setTimeout(() => {
        window.scrollBy({ top: 800, behavior: 'smooth' });
      }, 500);
    }
  }, [location]);

  const optionGroups = useMemo(() => {
    if (!product || !product.variants) return {};
    const groups = {};
    product.variants.forEach(v => {
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
  }, [product]);

  useEffect(() => {
    if (product && product.variants && product.variants.length > 0) {
      const defaultVariant = product.variants.find(v => v.availableQuantity > 0) || product.variants[0];
      setSelectedAttributes(defaultVariant.attributes || {});
    }
  }, [product]);

  const handleAttrChange = (key, val) => {
    setSelectedAttributes(prev => {
      const next = { ...prev, [key]: val };
      const exists = product.variants.find(v => v.attributes && Object.entries(next).every(([k, v2]) => v.attributes[k] === v2));
      if (!exists) {
        const fallback = product.variants.find(v => v.attributes && v.attributes[key] === val);
        if (fallback) return fallback.attributes;
      }
      return next;
    });
  };

  const selectedVariant = useMemo(() => {
    if (!product || !product.variants || product.variants.length === 0) return null;
    return product.variants.find(v => {
      if (!v.attributes) return false;
      return Object.entries(selectedAttributes).every(([k, val]) => v.attributes[k] === val);
    });
  }, [product, selectedAttributes]);

  const currentPrice = selectedVariant ? selectedVariant.price : (product ? product.price : 0);

  const related = useMemo(() => {
    if (!product) return [];
    return allProducts.filter(p => String(p.id) !== String(id) && p.categoryId === product.categoryId).slice(0, 4);
  }, [product, allProducts, id]);

  const accessories = useMemo(() => {
    if (!product) return [];
    return getSuggestedAccessories(product, allProducts);
  }, [product, allProducts]);

  useEffect(() => {
    window.scrollTo(0, 0);
    fetchProduct();
  }, [id]);

  const fetchProduct = async () => {
    setLoading(true);
    try {
      const [productRes, allRes, reviewsRes] = await Promise.all([
        api.get(`/products/${id}`),
        api.get('/products'),
        api.get(`/products/${id}/reviews`).catch(() => ({ data: [] }))
      ]);
      setProduct(productRes.data);
      setAllProducts(allRes.data.data || []);
      setReviews(reviewsRes.data || []);

      if (token) {
        api.get(`/products/${id}/reviews/eligibility`)
           .then(res => setReviewEligibility(res.data))
           .catch(() => setReviewEligibility(null));
           
        api.get(`/Wishlist/${id}/check`)
           .then(res => setIsFavorited(res.data.isFavorited))
           .catch(() => setIsFavorited(false));
      }
    } catch (err) {
      console.error('ProductDetail fetch error:', err);
      setProduct(null);
    } finally {
      setLoading(false);
    }
  };

  const handleSubmitReview = async (e) => {
    e.preventDefault();
    if (!reviewEligibility?.canReview || !reviewEligibility?.eligibleOrderId) return;
    if (!reviewForm.comment.trim()) {
      Swal.fire({
        icon: "info",
        text: "Vui lòng nhập nội dung đánh giá!"
      });
      return;
    }

    setSubmittingReview(true);
    try {
      await api.post(`/products/${id}/reviews`, {
        orderId: reviewEligibility.eligibleOrderId,
        rating: reviewForm.rating,
        comment: reviewForm.comment
      });
      const reviewsRes = await api.get(`/products/${id}/reviews`);
      setReviews(reviewsRes.data || []);
      const eligRes = await api.get(`/products/${id}/reviews/eligibility`);
      setReviewEligibility(eligRes.data);
      setReviewForm({ rating: 5, comment: '' });
      Swal.fire({
        icon: "info",
        text: "Cảm ơn bạn đã đánh giá sản phẩm!"
      });
    } catch (err) {
      Swal.fire({
        icon: "info",
        text: err.response?.data?.message || "Lỗi gửi đánh giá"
      });
    } finally {
      setSubmittingReview(false);
    }
  };

  const handleAdminReply = async (review) => {
    const { value: replyText } = await Swal.fire({
      title: 'Trả lời đánh giá',
      input: 'textarea',
      inputLabel: `Khách hàng: ${review.userName}`,
      inputPlaceholder: 'Nhập câu trả lời của bạn...',
      inputValue: review.adminReply || '',
      showCancelButton: true,
      confirmButtonText: 'Gửi trả lời',
      cancelButtonText: 'Hủy',
      inputValidator: (value) => {
        if (!value.trim()) {
          return 'Bạn cần nhập nội dung trả lời!';
        }
      }
    });

    if (replyText) {
      try {
        await api.post(`/admin/reviews/${review.id}/reply`, { reply: replyText });
        Swal.fire('Thành công!', 'Đã gửi câu trả lời.', 'success');
        // Refresh reviews
        const reviewsRes = await api.get(`/products/${id}/reviews`);
        setReviews(reviewsRes.data || []);
      } catch (err) {
        console.error(err);
        Swal.fire('Lỗi!', err.response?.data?.message || 'Không thể gửi câu trả lời.', 'error');
      }
    }
  };

  const buildCartItem = () => ({
    ...product,
    price: currentPrice,
    variantId: selectedVariant?.id,
    variantName: selectedVariant?.name,
    variantAttributes: selectedVariant?.attributes,
    variants: product.variants,
  });

  const handleAddToCart = () => {
    if (!token) { navigate('/login'); return; }
    const item = buildCartItem();
    for (let i = 0; i < quantity; i++) addToCart(item);
    setAddedToast(true);
    setTimeout(() => setAddedToast(false), 2500);
  };

  const handleBuyNow = () => {
    if (!token) { navigate('/login'); return; }
    const item = buildCartItem();
    for (let i = 0; i < quantity; i++) addToCart(item);
    navigate('/cart');
  };

  const handleToggleWishlist = async () => {
    if (!token) {
        Swal.fire({
          icon: "info",
          text: "Vui lòng đăng nhập để lưu sản phẩm!"
        });
        navigate('/login');
        return;
    }
    try {
        await api.post(`/Wishlist/${id}/toggle`);
        setIsFavorited(!isFavorited);
    } catch (err) {
        Swal.fire({
          icon: "info",
          text: "Có lỗi xảy ra khi lưu sản phẩm"
        });
    }
  };

  if (loading) {
    return (
      <div className="min-h-screen bg-background text-on-surface font-sans">
        <nav className="sticky top-0 z-40 bg-surface/80 backdrop-blur-md border-b border-outline-variant/30 py-4 px-4 md:px-12">
          <div className="max-w-[1440px] mx-auto">
            <div className="h-4 bg-surface-container-low rounded w-48 animate-pulse" />
          </div>
        </nav>
        <div className="max-w-[1440px] mx-auto px-4 md:px-12 py-10">
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-12">
            <div className="aspect-square bg-surface-container-low rounded-3xl animate-pulse" />
            <div className="space-y-4">
              {[1,2,3,4,5].map(i => (
                <div key={i} className="h-6 bg-surface-container-low rounded animate-pulse" style={{width: `${80 - i*10}%`}} />
              ))}
            </div>
          </div>
        </div>
      </div>
    );
  }

  if (!product) {
    return (
      <div className="min-h-screen bg-background flex items-center justify-center font-sans">
        <div className="text-center space-y-4">
          <Package size={48} className="mx-auto text-on-surface-variant opacity-30" />
          <h2 className="text-xl font-bold text-on-surface">Không tìm thấy sản phẩm</h2>
          <Link to="/" className="text-primary hover:underline text-sm">← Quay lại cửa hàng</Link>
        </div>
      </div>
    );
  }

  const stockQty = selectedVariant ? selectedVariant.availableQuantity : (product.inventory?.availableQuantity ?? product.inventory?.stockQuantity ?? 0);
  const imgSrc = getProductImage(product);
  
  // Lấy color từ selectedAttributes để highlight image nếu cần
  const selectedColorName = selectedAttributes['color'] || selectedAttributes['Màu'] || selectedAttributes['Màu sắc'];

  return (
    <div className="min-h-screen bg-background text-on-surface font-sans">
      {addedToast && (
        <div className="fixed top-20 right-4 z-[999] bg-surface-container border border-emerald-500/20 text-on-surface px-4 py-3 rounded-2xl shadow-2xl flex items-center gap-3">
          <Check size={16} className="text-emerald-400" />
          <p className="text-sm font-medium">Đã thêm {quantity} sản phẩm vào giỏ hàng!</p>
        </div>
      )}

      <nav className="sticky top-0 z-40 bg-surface/90 backdrop-blur-md border-b border-outline-variant/30 py-4 px-4 md:px-12">
        <div className="max-w-[1440px] mx-auto flex items-center gap-2 text-sm text-on-surface-variant">
          <Link to="/" className="hover:text-primary transition-colors flex items-center gap-1">
            <ArrowLeft size={16} /> Trang chủ
          </Link>
          <ChevronRight size={14} />
          <span>{product.category?.name || 'Sản phẩm'}</span>
          <ChevronRight size={14} />
          <span className="text-on-surface font-medium truncate max-w-[200px]">{product.name}</span>
        </div>
      </nav>

      <main className="max-w-[1440px] mx-auto px-4 md:px-12 py-10">
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-12 mb-12">
          {/* Image */}
          <div className="space-y-4">
            <div className="aspect-square bg-surface-container-low rounded-3xl overflow-hidden border border-outline-variant/20 flex items-center justify-center relative group">
              <img
                src={product.imageUrls && product.imageUrls.length > 0 ? `${import.meta.env.VITE_API_BASE_URL}${product.imageUrls[activeImageIndex]}` : imgSrc}
                alt={product.name}
                className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-700"
                onError={e => { e.target.src = PRODUCT_IMAGES.laptop; }}
              />
              <button 
                onClick={handleToggleWishlist}
                className={`absolute top-4 right-4 p-3 rounded-full backdrop-blur-md border shadow-sm transition-all ${isFavorited ? 'bg-rose-500/10 border-rose-500/20 text-rose-500' : 'bg-white/80 border-outline-variant/30 text-on-surface-variant hover:text-rose-500 hover:scale-110'}`}
              >
                <Heart size={20} className={isFavorited ? "fill-rose-500" : ""} />
              </button>
              {selectedColorName && (
                <div className="absolute bottom-4 left-4 bg-black/60 backdrop-blur-sm text-white text-xs font-semibold px-3 py-1.5 rounded-full">
                  {selectedColorName}
                </div>
              )}
              {stockQty <= 0 && (
                <div className="absolute inset-0 bg-black/50 flex items-center justify-center rounded-3xl">
                  <span className="text-white font-bold text-lg">Hết hàng</span>
                </div>
              )}
            </div>

            {/* Thumbnails */}
            {product.imageUrls && product.imageUrls.length > 1 && (
              <div className="flex gap-3 overflow-x-auto custom-scrollbar pb-2">
                {product.imageUrls.map((url, idx) => (
                  <button
                    key={idx}
                    onClick={() => setActiveImageIndex(idx)}
                    className={`w-20 h-20 rounded-xl overflow-hidden border-2 flex-shrink-0 transition-all ${
                      activeImageIndex === idx ? 'border-primary scale-105 ring-2 ring-primary/30' : 'border-outline-variant/30 opacity-70 hover:opacity-100 hover:border-primary/50'
                    }`}
                  >
                    <img src={`${import.meta.env.VITE_API_BASE_URL}${url}`} alt="" className="w-full h-full object-cover" />
                  </button>
                ))}
              </div>
            )}

            {/* Color thumbnails */}
            {optionGroups['color'] && (
              <div className="flex gap-3 justify-center">
                {optionGroups['color'].map(val => (
                  <ColorSwatch
                    key={val}
                    colorName={val}
                    selected={selectedAttributes['color'] === val}
                    onClick={() => handleAttrChange('color', val)}
                  />
                ))}
              </div>
            )}
          </div>

          {/* Info + Variants */}
          <div className="space-y-5">
            <div>
              <span className="inline-flex items-center gap-1.5 text-xs font-semibold text-primary bg-primary/10 border border-primary/20 px-3 py-1 rounded-full mb-3">
                <Tag size={11} />
                {product.category?.name || 'Sản phẩm'}
              </span>
              <div className="flex items-center gap-3 mt-2">
                <div className="flex items-center gap-1">
                  {[1,2,3,4,5].map(s => (
                    <Star key={s} size={14} className={s <= Math.round(product.reviewSummary?.averageRating || 0) ? 'fill-amber-500 text-amber-500' : 'text-on-surface-variant/30'} />
                  ))}
                  <span className="text-sm font-bold text-on-surface ml-1">{(product.reviewSummary?.averageRating || 0).toFixed(1)}</span>
                </div>
                <span className="text-xs text-on-surface-variant">({product.reviewSummary?.reviewCount || 0} đánh giá)</span>
              </div>
            </div>

            <div className="flex items-end gap-3">
              <span className="text-4xl font-black text-primary">{currentPrice.toLocaleString()}đ</span>
              {currentPrice !== Number(product.price) && (
                <span className="text-sm text-on-surface-variant line-through">{Number(product.price).toLocaleString()}đ</span>
              )}
            </div>

            <StockBadge product={{ inventory: { availableQuantity: stockQty } }} />

            {/* Attributes selector */}
            {Object.entries(optionGroups).map(([attrKey, values]) => {
              const isColor = attrKey.toLowerCase().includes('color') || attrKey.toLowerCase() === 'màu';
              return (
                <div key={attrKey} className="space-y-2">
                  <p className="text-xs font-bold text-on-surface-variant uppercase tracking-wider">
                    {attrKey}: <span className="text-on-surface normal-case">{selectedAttributes[attrKey]}</span>
                  </p>
                  <div className="flex flex-wrap gap-2">
                    {values.map(val => {
                      const selected = selectedAttributes[attrKey] === val;
                      const onClick = () => handleAttrChange(attrKey, val);
                      
                      if (isColor) {
                        return <ColorSwatch key={val} colorName={val} selected={selected} onClick={onClick} />;
                      }
                      return <OptionButton key={val} value={val} selected={selected} onClick={onClick} />;
                    })}
                  </div>
                </div>
              );
            })}

            {/* Quantity */}
            {stockQty > 0 && (
              <div className="space-y-2">
                <p className="text-xs font-bold text-on-surface-variant uppercase tracking-wider">Số lượng</p>
                <div className="flex items-center gap-3">
                  <button onClick={() => setQuantity(q => Math.max(1, q - 1))} className="w-10 h-10 border border-outline-variant/40 rounded-xl flex items-center justify-center hover:bg-surface-container active:scale-95">
                    <Minus size={16} />
                  </button>
                  <span className="w-12 text-center font-bold text-lg">{quantity}</span>
                  <button onClick={() => setQuantity(q => Math.min(stockQty, q + 1))} className="w-10 h-10 border border-outline-variant/40 rounded-xl flex items-center justify-center hover:bg-surface-container active:scale-95">
                    <Plus size={16} />
                  </button>
                  <span className="text-xs text-on-surface-variant">/ {stockQty} có sẵn</span>
                </div>
              </div>
            )}

            <div className="flex gap-3 pt-2">
              <button onClick={handleAddToCart} disabled={stockQty <= 0} className="flex-1 flex items-center justify-center gap-2 py-4 border border-primary/40 text-primary rounded-2xl text-sm font-semibold hover:bg-primary/5 disabled:opacity-40 disabled:cursor-not-allowed active:scale-95 transition-all">
                <ShoppingCart size={18} /> Thêm vào giỏ
              </button>
              <button onClick={handleBuyNow} disabled={stockQty <= 0} className="flex-1 flex items-center justify-center gap-2 py-4 bg-primary text-white rounded-2xl text-sm font-bold hover:bg-primary-container disabled:opacity-40 disabled:cursor-not-allowed active:scale-95 shadow-lg shadow-primary/25 transition-all">
                <Zap size={18} /> Mua ngay
              </button>
            </div>

            <div className="grid grid-cols-3 gap-3">
              {[
                { icon: Truck, label: 'Miễn phí vận chuyển', sub: 'Đơn từ 500K' },
                { icon: ShieldCheck, label: 'Bảo hành chính hãng', sub: '12 tháng' },
                { icon: RotateCcw, label: 'Đổi trả dễ dàng', sub: '30 ngày' },
              ].map((b, i) => (
                <div key={i} className="flex flex-col items-center gap-2 p-3 bg-surface-container-low border border-outline-variant/20 rounded-2xl text-center">
                  <b.icon size={18} className="text-primary" />
                  <p className="text-[10px] font-bold leading-tight">{b.label}</p>
                  <p className="text-[9px] text-on-surface-variant">{b.sub}</p>
                </div>
              ))}
            </div>
          </div>
        </div>

        {/* Tabs: Info / Specs / Reviews */}
        <section className="mb-16">
          <div className="flex gap-1 border-b border-outline-variant/30 mb-6 overflow-x-auto">
            {TABS.map(tab => (
              <button
                key={tab.id}
                onClick={() => setActiveTab(tab.id)}
                className={`flex items-center gap-2 px-5 py-3 text-sm font-semibold border-b-2 transition-colors whitespace-nowrap ${
                  activeTab === tab.id
                    ? 'border-primary text-primary'
                    : 'border-transparent text-on-surface-variant hover:text-on-surface'
                }`}
              >
                <tab.icon size={16} />
                {tab.label}
              </button>
            ))}
          </div>

          {activeTab === 'info' && (
            <div className="bg-surface-container-low border border-outline-variant/20 rounded-2xl p-6 space-y-4">
              <h3 className="font-bold text-on-surface flex items-center gap-2"><Info size={18} className="text-primary" /> Mô tả sản phẩm</h3>
              <p className="text-sm text-on-surface-variant leading-relaxed">{product.description || 'Chưa có mô tả chi tiết.'}</p>
              <div className="grid grid-cols-2 md:grid-cols-4 gap-3 pt-4 border-t border-outline-variant/20">
                {Object.entries(selectedAttributes).map(([key, val]) => (
                  <div key={key} className="text-center p-3">
                    <p className="text-xs text-on-surface-variant uppercase">{key}</p>
                    <p className="text-sm font-bold text-on-surface mt-1">{val}</p>
                  </div>
                ))}
                <div className="text-center p-3">
                  <p className="text-xs text-on-surface-variant uppercase">Giá</p>
                  <p className="text-sm font-bold text-primary mt-1">{currentPrice.toLocaleString()}đ</p>
                </div>
              </div>
            </div>
          )}

          {activeTab === 'specs' && (
            <div className="bg-surface-container-low border border-outline-variant/20 rounded-2xl overflow-hidden p-6">
              <h3 className="font-bold text-on-surface flex items-center gap-2 mb-4"><Tag size={18} className="text-primary" /> Thông số kỹ thuật</h3>
              
              {!product.attributes || Object.keys(product.attributes).length === 0 ? (
                <div className="text-center py-8 text-sm text-on-surface-variant">Thông số kỹ thuật đang cập nhật.</div>
              ) : (
                <div className="border border-outline-variant/30 rounded-xl overflow-hidden">
                  <table className="w-full text-sm text-left">
                    <tbody>
                      {Object.entries(product.attributes).map(([key, val], idx) => (
                        <tr key={key} className={idx % 2 === 0 ? 'bg-surface-container-lowest' : 'bg-transparent'}>
                          <th className="py-3 px-4 font-bold text-on-surface-variant w-1/3 border-b border-outline-variant/20">{key}</th>
                          <td className="py-3 px-4 text-on-surface border-b border-outline-variant/20">{val}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              )}
            </div>
          )}

          {activeTab === 'reviews' && (
            <div className="space-y-6">
              {reviewEligibility?.canReview && (
                <div className="bg-surface-container-low border border-outline-variant/20 rounded-2xl p-6 space-y-4">
                  <h3 className="font-bold text-lg text-on-surface">Viết đánh giá của bạn</h3>
                  <form onSubmit={handleSubmitReview} className="space-y-4">
                    <div className="flex items-center gap-2">
                      <span className="text-sm font-semibold text-on-surface-variant">Đánh giá sao:</span>
                      <div className="flex items-center gap-1">
                        {[1, 2, 3, 4, 5].map(star => (
                          <button type="button" key={star} onClick={() => setReviewForm(prev => ({ ...prev, rating: star }))}>
                            <Star size={24} className={star <= reviewForm.rating ? "fill-amber-500 text-amber-500" : "text-on-surface-variant/30"} />
                          </button>
                        ))}
                      </div>
                    </div>
                    <textarea
                      placeholder="Chia sẻ cảm nhận của bạn về sản phẩm này..."
                      rows={4}
                      value={reviewForm.comment}
                      onChange={e => setReviewForm(prev => ({ ...prev, comment: e.target.value }))}
                      className="w-full bg-surface-container-lowest border border-outline-variant/30 rounded-xl p-3 text-sm focus:border-primary focus:ring-1 focus:ring-primary outline-none"
                    />
                    <button type="submit" disabled={submittingReview} className="px-6 py-2 bg-primary text-white rounded-xl text-sm font-semibold hover:bg-primary-container active:scale-95 transition-all disabled:opacity-50">
                      {submittingReview ? 'Đang gửi...' : 'Gửi đánh giá'}
                    </button>
                  </form>
                </div>
              )}
              
              {!reviewEligibility?.canReview && reviewEligibility?.reason && token && (
                <div className="p-4 bg-surface-container-lowest border border-outline-variant/20 rounded-xl text-sm text-on-surface-variant text-center">
                  {reviewEligibility.reason}
                </div>
              )}

              {!token && (
                <div className="p-4 bg-surface-container-lowest border border-outline-variant/20 rounded-xl text-sm text-on-surface-variant text-center">
                  Vui lòng <Link to="/login" className="text-primary hover:underline">đăng nhập</Link> để đánh giá sản phẩm.
                </div>
              )}

              <div className="space-y-4">
                <h3 className="font-bold text-lg text-on-surface flex items-center gap-2"><MessageSquare size={20} className="text-primary"/> Đánh giá từ khách hàng ({reviews.length})</h3>
                {reviews.length === 0 ? (
                  <p className="text-sm text-on-surface-variant italic">Chưa có đánh giá nào cho sản phẩm này.</p>
                ) : (
                  reviews.map(review => (
                    <div key={review.id} className="bg-surface-container-lowest border border-outline-variant/20 rounded-2xl p-5 space-y-3">
                      <div className="flex items-center justify-between">
                        <div className="flex items-center gap-3">
                          <div className="w-10 h-10 bg-primary/10 rounded-full flex items-center justify-center text-sm font-bold text-primary">
                            {review.userName[0]}
                          </div>
                          <div>
                            <p className="text-sm font-bold text-on-surface">{review.userName}</p>
                            <div className="flex items-center gap-2">
                              <div className="flex">
                                {[1, 2, 3, 4, 5].map(s => (
                                  <Star key={s} size={12} className={s <= review.rating ? "fill-amber-500 text-amber-500" : "text-on-surface-variant/20"} />
                                ))}
                              </div>
                              <span className="text-[10px] text-on-surface-variant">{new Date(review.createdAt).toLocaleDateString('vi-VN')}</span>
                            </div>
                          </div>
                        </div>
                        <div className="flex items-center gap-2">
                          {isAdmin && (
                            <button onClick={() => handleAdminReply(review)} className="p-1.5 text-blue-500 bg-blue-500/10 hover:bg-blue-500 hover:text-white rounded transition-colors" title="Trả lời khách hàng">
                              <Reply size={14} />
                            </button>
                          )}
                          <span className="text-[10px] font-semibold text-emerald-500 bg-emerald-500/10 px-2 py-1 rounded flex items-center gap-1">
                            <ShieldCheck size={12}/> Đã mua hàng
                          </span>
                        </div>
                      </div>
                      <p className="text-sm text-on-surface-variant leading-relaxed">{review.comment}</p>
                      {review.adminReply && (
                        <div className="mt-2 bg-primary/5 p-3 rounded-xl border border-primary/10 flex gap-2">
                          <CornerDownRight size={16} className="text-primary shrink-0 mt-0.5" />
                          <div>
                            <p className="text-xs font-bold text-primary">Phản hồi từ Admin:</p>
                            <p className="text-xs text-on-surface-variant mt-1">{review.adminReply}</p>
                          </div>
                        </div>
                      )}
                    </div>
                  ))
                )}
              </div>
            </div>
          )}
        </section>

        {/* Suggested accessories */}
        {accessories.length > 0 && (
          <section className="mb-16 space-y-6">
            <div className="flex items-center gap-2">
              <Layers size={20} className="text-primary" />
              <h2 className="text-xl font-black text-on-surface">Sản phẩm gợi ý kèm theo</h2>
            </div>
            <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
              {accessories.map(p => (
                <Link key={p.id} to={`/product/${p.id}`} className="group bg-surface-container-lowest border border-outline-variant/30 rounded-2xl overflow-hidden hover:border-primary/30 transition-all">
                  <div className="aspect-square bg-surface-container-low overflow-hidden">
                    <img src={getProductImage(p)} alt={p.name} className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-500" />
                  </div>
                  <div className="p-3 space-y-1">
                    <p className="text-xs font-bold truncate">{p.name}</p>
                    <p className="text-sm font-black text-primary">{Number(p.price).toLocaleString()}đ</p>
                  </div>
                </Link>
              ))}
            </div>
          </section>
        )}

        {/* Related products */}
        {related.length > 0 && (
          <section className="space-y-6">
            <div className="flex items-center justify-between">
              <h2 className="text-xl font-black text-on-surface">Sản phẩm liên quan</h2>
              <Link to="/products" className="text-sm text-primary font-semibold hover:underline">Xem tất cả</Link>
            </div>
            <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
              {related.map(p => (
                <Link key={p.id} to={`/product/${p.id}`} className="group bg-surface-container-lowest border border-outline-variant/30 rounded-2xl overflow-hidden hover:border-primary/30 transition-all">
                  <div className="aspect-square bg-surface-container-low overflow-hidden">
                    <img src={getProductImage(p)} alt={p.name} className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-500" />
                  </div>
                  <div className="p-3 space-y-1">
                    <p className="text-xs font-bold truncate">{p.name}</p>
                    <p className="text-sm font-black text-primary">{Number(p.price).toLocaleString()}đ</p>
                  </div>
                </Link>
              ))}
            </div>
          </section>
        )}
      </main>
    </div>
  );
}
