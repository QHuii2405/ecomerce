import React, { useEffect, useState } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import api from '../api/axios';
import { addToCart } from '../api/cartStore';
import {
  ArrowLeft, ShoppingCart, Zap, Package, Tag, Star,
  Check, AlertCircle, ChevronRight, Truck, ShieldCheck, RotateCcw,
  Plus, Minus, Heart, Share2, Info
} from 'lucide-react';

const PRODUCT_IMAGES = {
  laptop:     'https://lh3.googleusercontent.com/aida-public/AB6AXuDzzkgw3qdK2qx_eejr9Oee4qzRfJoNIb-smYiUqNBNBZ4_KNAbm4HxoqNIyfUqk9pV0qWkdyFf7t7SYsjbbwCkkEN06FQNQBCseQPzXacixmLlO0YB5GVfxd7AR42kwnUufnptDgHCXnHlGXhg3x4QzV7sXZkMAYLQHcoPSLjWbTIWG3W_hrzh4eWEuFkuhEuk_62jmY9dMbWUMr11EIDivHK_RMuJDGpKxGyfr8JRcfcL8i0qv6Ve',
  gaming:     'https://lh3.googleusercontent.com/aida-public/AB6AXuCtePE6a8azsOINxsGhPHJGa7pybuOEtEXGQUTmgTTPhKMBwOS-7FOpWVDKTgo17TVnuDYz0tGqUltaJ9vPZxqJCWCdknYb9x1VpLfsq9eC8Qlc1fG2cjNg5i2klZdqsM6d2QHuBDP_mHVRg-Ley5Dw6z3yNJURrfQ5bnPx_TyxzBc7EuP7b5boquCcOezT3SWcmEEoMElGb7VQSAkNtos5xtMcNxJFn8D0-Oq4x3zH0vgAz5bRpJdx',
  audio:      'https://lh3.googleusercontent.com/aida-public/AB6AXujCnwa9YYlaySQHciXXGs5ENqNsPlYyM48pBFebzeWQc7dPKrvdRI9-hr5S5mxpSSR3ZK689MtFW5CFO2yl2XF3_1RCn3iwdnmtwXFQeVUo_FXR0vOQ7FYE_qzsTZb4Q8_zaMowCCLDFa84jAXRV-XqKV2AZPbY2fWUotHuBFbc9Jv95ESTZuet5JJdQVxXrmhm4ItvrDA3BDDkW6wfXdjWtO5ynIppnxJllrxffafcwXaW4XKodrR',
  smartphone: 'https://lh3.googleusercontent.com/aida-public/AB6AXuAltOeyhWUN8d_OI3RZgPhmFTnEFiE8btibjVA1UyeS4BMmMyjSKaTmgBimNagFrcF5ixaI_tKAShIux-GWN1Ed-N4cXrCROBioCaBreSt4h4NtC8LD-0H3MX6jv_fs4XT3pt7d0fecPOmOrn9wrTrKkLcAH0eYV75rcouQVMTlc39VoiWaFpa2STxaIe2OkNzeota4rS1mkkwmLFdG16EQo8bXdMVcpc2tss9oN1UsRXklpzD_rStA',
};

function getProductImage(product) {
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

export default function ProductDetail() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [product, setProduct] = useState(null);
  const [related, setRelated] = useState([]);
  const [quantity, setQuantity] = useState(1);
  const [loading, setLoading] = useState(true);
  const [addedToast, setAddedToast] = useState(false);

  const token = localStorage.getItem('token');

  useEffect(() => {
    window.scrollTo(0, 0);
    fetchProduct();
  }, [id]);

  const fetchProduct = async () => {
    setLoading(true);
    try {
      const res = await api.get(`/products/${id}`);
      setProduct(res.data);
      // Fetch related (same category)
      const allRes = await api.get('/products');
      const all = allRes.data.data || [];
      setRelated(all.filter(p => p.categoryId === res.data.categoryId && p.id !== id).slice(0, 4));
    } catch (err) {
      console.error('ProductDetail fetch error:', err);
    } finally {
      setLoading(false);
    }
  };

  const handleAddToCart = () => {
    if (!token) { navigate('/login'); return; }
    for (let i = 0; i < quantity; i++) addToCart(product);
    setAddedToast(true);
    setTimeout(() => setAddedToast(false), 2500);
  };

  const handleBuyNow = () => {
    if (!token) { navigate('/login'); return; }
    for (let i = 0; i < quantity; i++) addToCart(product);
    navigate('/cart');
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

  const stockQty = product.inventory?.availableQuantity ?? product.inventory?.stockQuantity ?? 0;
  const imgSrc = getProductImage(product);

  return (
    <div className="min-h-screen bg-background text-on-surface font-sans">
      {/* Toast */}
      {addedToast && (
        <div className="fixed top-20 right-4 z-[999] bg-surface-container border border-emerald-500/20 text-on-surface px-4 py-3 rounded-2xl shadow-2xl flex items-center gap-3 animate-in slide-in-from-right duration-300">
          <Check size={16} className="text-emerald-400" />
          <p className="text-sm font-medium">Đã thêm {quantity} sản phẩm vào giỏ hàng!</p>
        </div>
      )}

      {/* Nav */}
      <nav className="sticky top-0 z-40 bg-surface/90 backdrop-blur-md border-b border-outline-variant/30 py-4 px-4 md:px-12">
        <div className="max-w-[1440px] mx-auto flex items-center gap-2 text-sm text-on-surface-variant">
          <Link to="/" className="hover:text-primary transition-colors flex items-center gap-1">
            <ArrowLeft size={16} /> Trang chủ
          </Link>
          <ChevronRight size={14} />
          <span className="text-on-surface-variant">{product.category?.name}</span>
          <ChevronRight size={14} />
          <span className="text-on-surface font-medium truncate max-w-[200px]">{product.name}</span>
        </div>
      </nav>

      <main className="max-w-[1440px] mx-auto px-4 md:px-12 py-10">
        {/* Product Hero */}
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-12 mb-16">
          {/* Image */}
          <div className="space-y-4">
            <div className="aspect-square bg-surface-container-low rounded-3xl overflow-hidden border border-outline-variant/20 flex items-center justify-center relative group">
              <img
                src={imgSrc}
                alt={product.name}
                className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-700"
                onError={e => { e.target.src = PRODUCT_IMAGES.laptop; }}
              />
              {stockQty <= 5 && stockQty > 0 && (
                <div className="absolute top-4 left-4 bg-amber-500 text-white text-[10px] font-bold px-2 py-1 rounded-full uppercase tracking-wider">
                  Sắp hết hàng
                </div>
              )}
              {stockQty <= 0 && (
                <div className="absolute inset-0 bg-black/50 flex items-center justify-center rounded-3xl">
                  <span className="text-white font-bold text-lg">Hết hàng</span>
                </div>
              )}
            </div>
          </div>

          {/* Info */}
          <div className="space-y-6">
            {/* Category + name */}
            <div>
              <Link
                to="/"
                className="inline-flex items-center gap-1.5 text-xs font-semibold text-primary bg-primary/10 border border-primary/20 px-3 py-1 rounded-full mb-3 hover:bg-primary/20 transition-colors"
              >
                <Tag size={11} />
                {product.category?.name || 'Sản phẩm'}
              </Link>
              <h1 className="text-3xl font-black text-on-surface tracking-tight leading-tight">{product.name}</h1>
            </div>

            {/* Price */}
            <div className="flex items-end gap-3">
              <span className="text-4xl font-black text-primary">{Number(product.price).toLocaleString()}đ</span>
            </div>

            {/* Stock */}
            <StockBadge product={product} />

            {/* Description */}
            <div className="bg-surface-container-low border border-outline-variant/20 rounded-2xl p-4">
              <p className="text-sm text-on-surface-variant leading-relaxed">{product.description || 'Chưa có mô tả chi tiết.'}</p>
            </div>

            {/* Quantity selector */}
            {stockQty > 0 && (
              <div className="space-y-3">
                <p className="text-xs font-bold text-on-surface-variant uppercase tracking-wider">Số lượng</p>
                <div className="flex items-center gap-3">
                  <button
                    onClick={() => setQuantity(q => Math.max(1, q - 1))}
                    className="w-10 h-10 border border-outline-variant/40 rounded-xl flex items-center justify-center text-on-surface hover:bg-surface-container transition-colors active:scale-95"
                  >
                    <Minus size={16} />
                  </button>
                  <span className="w-12 text-center font-bold text-lg text-on-surface">{quantity}</span>
                  <button
                    onClick={() => setQuantity(q => Math.min(stockQty, q + 1))}
                    className="w-10 h-10 border border-outline-variant/40 rounded-xl flex items-center justify-center text-on-surface hover:bg-surface-container transition-colors active:scale-95"
                  >
                    <Plus size={16} />
                  </button>
                  <span className="text-xs text-on-surface-variant">/ {stockQty} có sẵn</span>
                </div>
              </div>
            )}

            {/* CTA Buttons */}
            <div className="flex gap-3 pt-2">
              <button
                onClick={handleAddToCart}
                disabled={stockQty <= 0}
                className="flex-1 flex items-center justify-center gap-2 py-4 border border-primary/40 text-primary rounded-2xl text-sm font-semibold hover:bg-primary/5 transition-all active:scale-95 disabled:opacity-40 disabled:cursor-not-allowed"
              >
                <ShoppingCart size={18} />
                Thêm vào giỏ
              </button>
              <button
                onClick={handleBuyNow}
                disabled={stockQty <= 0}
                className="flex-1 flex items-center justify-center gap-2 py-4 bg-primary text-white rounded-2xl text-sm font-bold hover:bg-primary-container transition-all active:scale-95 shadow-lg shadow-primary/25 disabled:opacity-40 disabled:cursor-not-allowed"
              >
                <Zap size={18} />
                Mua ngay
              </button>
            </div>

            {/* Trust badges */}
            <div className="grid grid-cols-3 gap-3 pt-2">
              {[
                { icon: Truck, label: 'Miễn phí vận chuyển', sub: 'Đơn từ 500K' },
                { icon: ShieldCheck, label: 'Bảo hành chính hãng', sub: '12 tháng' },
                { icon: RotateCcw, label: 'Đổi trả dễ dàng', sub: '30 ngày' },
              ].map((b, i) => (
                <div key={i} className="flex flex-col items-center gap-2 p-3 bg-surface-container-low border border-outline-variant/20 rounded-2xl text-center">
                  <b.icon size={18} className="text-primary" />
                  <p className="text-[10px] font-bold text-on-surface leading-tight">{b.label}</p>
                  <p className="text-[9px] text-on-surface-variant">{b.sub}</p>
                </div>
              ))}
            </div>
          </div>
        </div>

        {/* Related Products */}
        {related.length > 0 && (
          <section className="space-y-6">
            <div className="flex items-center justify-between">
              <h2 className="text-xl font-black text-on-surface">Sản phẩm liên quan</h2>
              <Link to="/" className="text-sm text-primary font-semibold hover:underline">Xem tất cả</Link>
            </div>
            <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
              {related.map(p => (
                <Link
                  key={p.id}
                  to={`/product/${p.id}`}
                  className="group bg-surface-container-lowest border border-outline-variant/30 rounded-2xl overflow-hidden hover:border-primary/30 hover:shadow-lg hover:shadow-primary/10 transition-all duration-300"
                >
                  <div className="aspect-square bg-surface-container-low overflow-hidden">
                    <img
                      src={getProductImage(p)}
                      alt={p.name}
                      className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-500"
                    />
                  </div>
                  <div className="p-3 space-y-1">
                    <p className="text-xs font-bold text-on-surface truncate">{p.name}</p>
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
