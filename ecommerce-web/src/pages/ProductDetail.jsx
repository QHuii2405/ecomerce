import React, { useEffect, useState, useMemo } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import api from '../api/axios';
import { addToCart } from '../api/cartStore';
import { getProductExtras, calcVariantPrice, getSuggestedAccessories } from '../utils/productExtras';
import {
  ArrowLeft, ShoppingCart, Zap, Package, Tag, Star,
  Check, AlertCircle, ChevronRight, Truck, ShieldCheck, RotateCcw,
  Plus, Minus, Info, FileText, MessageSquare, Layers
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

function ColorSwatch({ color, selected, onClick }) {
  const isGradient = color.hex?.startsWith('linear');
  return (
    <button
      onClick={onClick}
      title={color.name}
      className={`relative w-10 h-10 rounded-full border-2 transition-all active:scale-95 ${
        selected ? 'border-primary ring-2 ring-primary/30 scale-110' : 'border-outline-variant/40 hover:border-primary/50'
      }`}
    >
      <span
        className="absolute inset-1 rounded-full"
        style={isGradient ? { background: color.hex } : { backgroundColor: color.hex }}
      />
    </button>
  );
}

function OptionButton({ option, selected, onClick, showPrice }) {
  return (
    <button
      onClick={onClick}
      className={`px-4 py-2.5 rounded-xl text-sm font-semibold border transition-all active:scale-95 ${
        selected
          ? 'border-primary bg-primary/10 text-primary'
          : 'border-outline-variant/40 text-on-surface-variant hover:border-primary/40'
      }`}
    >
      {option.name}
      {showPrice && option.priceAdj > 0 && (
        <span className="block text-[10px] font-normal text-primary mt-0.5">+{option.priceAdj.toLocaleString()}đ</span>
      )}
      {showPrice && option.priceAdj < 0 && (
        <span className="block text-[10px] font-normal text-emerald-500 mt-0.5">{option.priceAdj.toLocaleString()}đ</span>
      )}
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
  const [product, setProduct] = useState(null);
  const [allProducts, setAllProducts] = useState([]);
  const [quantity, setQuantity] = useState(1);
  const [loading, setLoading] = useState(true);
  const [addedToast, setAddedToast] = useState(false);
  const [activeTab, setActiveTab] = useState('info');
  const [selectedColor, setSelectedColor] = useState(null);
  const [selectedSecond, setSelectedSecond] = useState(null);
  const [selectedThird, setSelectedThird] = useState(null);

  const token = localStorage.getItem('token');

  const extras = useMemo(() => product ? getProductExtras(product) : null, [product]);

  useEffect(() => {
    if (extras) {
      setSelectedColor(extras.colors[0]);
      setSelectedSecond(extras.secondAttr.options[0]);
      setSelectedThird(extras.thirdAttr?.options[0] ?? null);
    }
  }, [extras]);

  const currentPrice = useMemo(() => {
    if (!product) return 0;
    return calcVariantPrice(product.price, selectedColor, selectedSecond, selectedThird);
  }, [product, selectedColor, selectedSecond, selectedThird]);

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
      const [productRes, allRes] = await Promise.all([
        api.get(`/products/${id}`),
        api.get('/products'),
      ]);
      setProduct(productRes.data);
      setAllProducts(allRes.data.data || []);
    } catch (err) {
      console.error('ProductDetail fetch error:', err);
      setProduct(null);
    } finally {
      setLoading(false);
    }
  };

  const buildCartItem = () => ({
    ...product,
    price: currentPrice,
    variant: {
      color: selectedColor?.name,
      [extras?.secondAttr?.key]: selectedSecond?.name,
      ...(extras?.thirdAttr ? { [extras.thirdAttr.key]: selectedThird?.name } : {}),
    },
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

  if (!product || !extras) {
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
                src={imgSrc}
                alt={product.name}
                className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-700"
                onError={e => { e.target.src = PRODUCT_IMAGES.laptop; }}
              />
              {selectedColor && (
                <div className="absolute bottom-4 left-4 bg-black/60 backdrop-blur-sm text-white text-xs font-semibold px-3 py-1.5 rounded-full">
                  {selectedColor.name}
                </div>
              )}
              {stockQty <= 0 && (
                <div className="absolute inset-0 bg-black/50 flex items-center justify-center rounded-3xl">
                  <span className="text-white font-bold text-lg">Hết hàng</span>
                </div>
              )}
            </div>
            {/* Color thumbnails */}
            <div className="flex gap-3 justify-center">
              {extras.colors.map(c => (
                <ColorSwatch
                  key={c.name}
                  color={c}
                  selected={selectedColor?.name === c.name}
                  onClick={() => setSelectedColor(c)}
                />
              ))}
            </div>
          </div>

          {/* Info + Variants */}
          <div className="space-y-5">
            <div>
              <span className="inline-flex items-center gap-1.5 text-xs font-semibold text-primary bg-primary/10 border border-primary/20 px-3 py-1 rounded-full mb-3">
                <Tag size={11} />
                {product.category?.name || 'Sản phẩm'}
              </span>
              <h1 className="text-3xl font-black text-on-surface tracking-tight leading-tight">{product.name}</h1>
              <div className="flex items-center gap-3 mt-2">
                <div className="flex items-center gap-1">
                  {[1,2,3,4,5].map(s => (
                    <Star key={s} size={14} className={s <= Math.round(extras.avgRating) ? 'fill-amber-500 text-amber-500' : 'text-on-surface-variant/30'} />
                  ))}
                  <span className="text-sm font-bold text-on-surface ml-1">{extras.avgRating.toFixed(1)}</span>
                </div>
                <span className="text-xs text-on-surface-variant">({extras.reviewCount} đánh giá)</span>
              </div>
            </div>

            <div className="flex items-end gap-3">
              <span className="text-4xl font-black text-primary">{currentPrice.toLocaleString()}đ</span>
              {currentPrice !== Number(product.price) && (
                <span className="text-sm text-on-surface-variant line-through">{Number(product.price).toLocaleString()}đ</span>
              )}
            </div>

            <StockBadge product={product} />

            {/* Color selector */}
            <div className="space-y-2">
              <p className="text-xs font-bold text-on-surface-variant uppercase tracking-wider">
                Màu sắc: <span className="text-on-surface normal-case">{selectedColor?.name}</span>
              </p>
              <div className="flex flex-wrap gap-2">
                {extras.colors.map(c => (
                  <button
                    key={c.name}
                    onClick={() => setSelectedColor(c)}
                    className={`px-3 py-2 rounded-xl text-xs font-semibold border transition-all ${
                      selectedColor?.name === c.name
                        ? 'border-primary bg-primary/10 text-primary'
                        : 'border-outline-variant/40 text-on-surface-variant hover:border-primary/40'
                    }`}
                  >
                    {c.name}
                    {c.priceAdj > 0 && <span className="text-primary ml-1">+{(c.priceAdj / 1000)}K</span>}
                  </button>
                ))}
              </div>
            </div>

            {/* Second attribute (RAM / Gram / Storage...) */}
            <div className="space-y-2">
              <p className="text-xs font-bold text-on-surface-variant uppercase tracking-wider">{extras.secondAttr.label}</p>
              <div className="flex flex-wrap gap-2">
                {extras.secondAttr.options.map(opt => (
                  <OptionButton
                    key={opt.name}
                    option={opt}
                    selected={selectedSecond?.name === opt.name}
                    onClick={() => setSelectedSecond(opt)}
                    showPrice
                  />
                ))}
              </div>
            </div>

            {/* Third attribute if exists */}
            {extras.thirdAttr && (
              <div className="space-y-2">
                <p className="text-xs font-bold text-on-surface-variant uppercase tracking-wider">{extras.thirdAttr.label}</p>
                <div className="flex flex-wrap gap-2">
                  {extras.thirdAttr.options.map(opt => (
                    <OptionButton
                      key={opt.name}
                      option={opt}
                      selected={selectedThird?.name === opt.name}
                      onClick={() => setSelectedThird(opt)}
                      showPrice
                    />
                  ))}
                </div>
              </div>
            )}

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
                <div className="text-center p-3">
                  <p className="text-xs text-on-surface-variant">Màu đã chọn</p>
                  <p className="text-sm font-bold text-on-surface mt-1">{selectedColor?.name}</p>
                </div>
                <div className="text-center p-3">
                  <p className="text-xs text-on-surface-variant">{extras.secondAttr.label}</p>
                  <p className="text-sm font-bold text-on-surface mt-1">{selectedSecond?.name}</p>
                </div>
                {extras.thirdAttr && (
                  <div className="text-center p-3">
                    <p className="text-xs text-on-surface-variant">{extras.thirdAttr.label}</p>
                    <p className="text-sm font-bold text-on-surface mt-1">{selectedThird?.name}</p>
                  </div>
                )}
                <div className="text-center p-3">
                  <p className="text-xs text-on-surface-variant">Giá cấu hình</p>
                  <p className="text-sm font-bold text-primary mt-1">{currentPrice.toLocaleString()}đ</p>
                </div>
              </div>
            </div>
          )}

          {activeTab === 'specs' && (
            <div className="bg-surface-container-low border border-outline-variant/20 rounded-2xl overflow-hidden">
              <table className="w-full text-sm">
                <tbody>
                  {extras.specs.map((spec, i) => (
                    <tr key={spec.label} className={i % 2 === 0 ? 'bg-surface-container-lowest' : ''}>
                      <td className="px-6 py-3 font-semibold text-on-surface w-1/3 border-b border-outline-variant/10">{spec.label}</td>
                      <td className="px-6 py-3 text-on-surface-variant border-b border-outline-variant/10">{spec.value}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}

          {activeTab === 'reviews' && (
            <div className="space-y-4">
              <div className="flex items-center gap-4 bg-surface-container-low border border-outline-variant/20 rounded-2xl p-6">
                <div className="text-center">
                  <p className="text-5xl font-black text-primary">{extras.avgRating.toFixed(1)}</p>
                  <div className="flex justify-center mt-1">
                    {[1,2,3,4,5].map(s => (
                      <Star key={s} size={14} className="fill-amber-500 text-amber-500" />
                    ))}
                  </div>
                  <p className="text-xs text-on-surface-variant mt-1">{extras.reviewCount} đánh giá</p>
                </div>
                <div className="flex-1 space-y-1">
                  {[5,4,3,2,1].map(star => (
                    <div key={star} className="flex items-center gap-2 text-xs">
                      <span className="w-3 text-on-surface-variant">{star}</span>
                      <Star size={10} className="fill-amber-500 text-amber-500" />
                      <div className="flex-1 h-1.5 bg-surface-container rounded-full overflow-hidden">
                        <div className="h-full bg-amber-500 rounded-full" style={{ width: `${star === 5 ? 70 : star === 4 ? 20 : 5}%` }} />
                      </div>
                    </div>
                  ))}
                </div>
              </div>
              {extras.reviews.map(review => (
                <div key={review.id} className="bg-surface-container-low border border-outline-variant/20 rounded-2xl p-5 space-y-2">
                  <div className="flex items-center justify-between">
                    <div className="flex items-center gap-2">
                      <div className="w-8 h-8 bg-primary/10 rounded-full flex items-center justify-center text-xs font-bold text-primary">
                        {review.author[0]}
                      </div>
                      <div>
                        <p className="text-sm font-bold text-on-surface">{review.author}</p>
                        {review.verified && <p className="text-[10px] text-emerald-500 font-semibold">✓ Đã mua hàng</p>}
                      </div>
                    </div>
                    <div className="flex items-center gap-2">
                      <div className="flex">
                        {[1,2,3,4,5].map(s => (
                          <Star key={s} size={12} className={s <= review.rating ? 'fill-amber-500 text-amber-500' : 'text-on-surface-variant/20'} />
                        ))}
                      </div>
                      <span className="text-xs text-on-surface-variant">{review.date}</span>
                    </div>
                  </div>
                  <p className="text-sm text-on-surface-variant leading-relaxed">{review.comment}</p>
                </div>
              ))}
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
