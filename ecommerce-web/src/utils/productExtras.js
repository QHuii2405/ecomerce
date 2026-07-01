/**
 * Sinh thuộc tính biến thể, thông số, đánh giá và phụ kiện gợi ý theo danh mục sản phẩm.
 */

const COLOR_PALETTES = {
  default: [
    { name: 'Đen Midnight', hex: '#1a1a2e', priceAdj: 0 },
    { name: 'Bạc Titan', hex: '#c0c0c0', priceAdj: 500_000 },
    { name: 'Trắng Ngọc', hex: '#f5f5f5', priceAdj: 300_000 },
    { name: 'Xanh Aurora', hex: '#3b82f6', priceAdj: 800_000 },
  ],
  gaming: [
    { name: 'Đen Matte', hex: '#111111', priceAdj: 0 },
    { name: 'Trắng Snow', hex: '#eeeeee', priceAdj: 200_000 },
    { name: 'Hồng Sakura', hex: '#f472b6', priceAdj: 350_000 },
    { name: 'RGB Limited', hex: 'linear-gradient(90deg,#f00,#0f0,#00f)', priceAdj: 500_000 },
  ],
  audio: [
    { name: 'Đen Obsidian', hex: '#0f0f0f', priceAdj: 0 },
    { name: 'Vàng Champagne', hex: '#d4af37', priceAdj: 600_000 },
    { name: 'Bạc Platinum', hex: '#e5e5e5', priceAdj: 400_000 },
  ],
};

function getCategoryKey(product) {
  const cat = (product.category?.name || '').toLowerCase();
  const name = (product.name || '').toLowerCase();
  if (cat.includes('gaming') || name.includes('mouse') || name.includes('keyboard') || name.includes('headset')) return 'gaming';
  if (cat.includes('audio') || name.includes('sonic') || name.includes('mic') || name.includes('speaker')) return 'audio';
  if (cat.includes('laptop') || cat.includes('smart')) return 'default';
  return 'default';
}

function getSecondAttribute(product) {
  const cat = (product.category?.name || '').toLowerCase();
  const name = (product.name || '').toLowerCase();

  if (name.includes('mouse') || name.includes('chuột')) {
    return {
      label: 'Trọng lượng',
      key: 'weight',
      options: [
        { name: '58g', priceAdj: 0 },
        { name: '65g', priceAdj: -200_000 },
        { name: '72g', priceAdj: -400_000 },
      ],
    };
  }
  if (cat.includes('laptop') || name.includes('book') || name.includes('laptop')) {
    return {
      label: 'RAM',
      key: 'ram',
      options: [
        { name: '16GB', priceAdj: 0 },
        { name: '32GB', priceAdj: 3_000_000 },
        { name: '64GB', priceAdj: 8_000_000 },
      ],
    };
  }
  if (cat.includes('smart') || name.includes('phone') || name.includes('tablet')) {
    return {
      label: 'Bộ nhớ',
      key: 'storage',
      options: [
        { name: '128GB', priceAdj: 0 },
        { name: '256GB', priceAdj: 2_000_000 },
        { name: '512GB', priceAdj: 4_500_000 },
        { name: '1TB', priceAdj: 8_000_000 },
      ],
    };
  }
  if (cat.includes('audio')) {
    return {
      label: 'Phiên bản',
      key: 'edition',
      options: [
        { name: 'Standard', priceAdj: 0 },
        { name: 'Pro', priceAdj: 1_500_000 },
        { name: 'Studio', priceAdj: 3_000_000 },
      ],
    };
  }
  return {
    label: 'Cấu hình',
    key: 'config',
    options: [
      { name: 'Tiêu chuẩn', priceAdj: 0 },
      { name: 'Nâng cao', priceAdj: 1_000_000 },
      { name: 'Premium', priceAdj: 2_500_000 },
    ],
  };
}

function getThirdAttribute(product) {
  const cat = (product.category?.name || '').toLowerCase();
  if (cat.includes('laptop')) {
    return {
      label: 'Ổ cứng',
      key: 'ssd',
      options: [
        { name: '512GB SSD', priceAdj: 0 },
        { name: '1TB SSD', priceAdj: 2_500_000 },
        { name: '2TB SSD', priceAdj: 5_000_000 },
      ],
    };
  }
  if (cat.includes('gaming') && !(product.name || '').toLowerCase().includes('mouse')) {
    return {
      label: 'Switch',
      key: 'switch',
      options: [
        { name: 'Red Linear', priceAdj: 0 },
        { name: 'Brown Tactile', priceAdj: 200_000 },
        { name: 'Blue Clicky', priceAdj: 200_000 },
      ],
    };
  }
  return null;
}

function buildSpecs(product) {
  const cat = (product.category?.name || '').toLowerCase();
  const base = [
    { label: 'Thương hiệu', value: 'iLuminaty' },
    { label: 'Model', value: product.name },
    { label: 'Danh mục', value: product.category?.name || 'Điện tử' },
    { label: 'Bảo hành', value: '12 tháng chính hãng' },
    { label: 'Xuất xứ', value: 'Chính hãng quốc tế' },
  ];

  if (cat.includes('laptop')) {
    return [
      ...base,
      { label: 'Màn hình', value: '14-16" OLED/QHD 120Hz' },
      { label: 'CPU', value: 'Intel Core Ultra / AMD Ryzen 7' },
      { label: 'GPU', value: 'Intel Arc / NVIDIA RTX' },
      { label: 'Kết nối', value: 'Wi-Fi 7, Thunderbolt 4, USB-C' },
      { label: 'Pin', value: 'Lên đến 20 giờ' },
      { label: 'Trọng lượng', value: '0.9 - 1.5 kg' },
    ];
  }
  if (cat.includes('gaming')) {
    return [
      ...base,
      { label: 'Kết nối', value: 'USB-C / Bluetooth 5.3 / 2.4GHz' },
      { label: 'Đèn LED', value: 'RGB per-key / Aura Sync' },
      { label: 'Tương thích', value: 'Windows, macOS, PS5, Xbox' },
      { label: 'Phần mềm', value: 'iLuminaty Control Center' },
    ];
  }
  if (cat.includes('audio')) {
    return [
      ...base,
      { label: 'Driver', value: 'Dynamic 11mm / Planar 50mm' },
      { label: 'Tần số', value: '20Hz - 40kHz' },
      { label: 'Chống ồn', value: 'ANC thế hệ 3' },
      { label: 'Pin', value: '28 - 36 giờ' },
      { label: 'Codec', value: 'LDAC, aptX HD, AAC' },
    ];
  }
  return [
    ...base,
    { label: 'Màn hình', value: 'AMOLED 120Hz' },
    { label: 'Chip', value: 'Snapdragon 8 Elite' },
    { label: 'Camera', value: '200MP AI Triple' },
    { label: 'Sạc', value: '100W sạc nhanh' },
    { label: 'Chống nước', value: 'IP68' },
  ];
}

function buildReviews(product) {
  const names = ['Minh Tuấn', 'Lan Anh', 'Hoàng Đức', 'Thu Hà', 'Quốc Bảo'];
  const comments = [
    `Rất hài lòng với ${product.name}. Chất lượng hoàn thiện tuyệt vời, đúng như mô tả.`,
    'Giao hàng nhanh, đóng gói cẩn thận. Sản phẩm chính hãng, có tem bảo hành đầy đủ.',
    'Giá tốt so với thị trường. Nhân viên tư vấn nhiệt tình, hỗ trợ setup máy miễn phí.',
    'Dùng được 2 tuần, mọi thứ hoạt động mượt mà. Sẽ mua thêm phụ kiện tại shop.',
    'Đánh giá cao về thiết kế và hiệu năng. Phù hợp cho công việc và giải trí.',
  ];
  return names.map((name, i) => ({
    id: i + 1,
    author: name,
    rating: 5 - (i % 2),
    date: `2026-0${6 - i}-1${i + 2}`,
    comment: comments[i],
    verified: true,
  }));
}

export function getProductExtras(product) {
  const catKey = getCategoryKey(product);
  const colors = COLOR_PALETTES[catKey] || COLOR_PALETTES.default;
  const secondAttr = getSecondAttribute(product);
  const thirdAttr = getThirdAttribute(product);

  return {
    colors,
    secondAttr,
    thirdAttr,
    specs: buildSpecs(product),
    reviews: buildReviews(product),
    avgRating: 4.7 + (product.name.length % 3) * 0.1,
    reviewCount: 24 + (product.name.length % 20),
  };
}

export function calcVariantPrice(basePrice, color, second, third) {
  let price = Number(basePrice) || 0;
  if (color) price += color.priceAdj || 0;
  if (second) price += second.priceAdj || 0;
  if (third) price += third.priceAdj || 0;
  return price;
}

export function getSuggestedAccessories(product, allProducts = []) {
  const catId = product.categoryId;
  const accessories = allProducts.filter(p =>
    p.id !== product.id &&
    p.categoryId === catId &&
    p.inventory?.availableQuantity > 0
  ).slice(0, 4);

  if (accessories.length >= 2) return accessories;

  const extras = allProducts.filter(p =>
    p.id !== product.id &&
    p.categoryId !== catId &&
    p.inventory?.availableQuantity > 0
  ).slice(0, 4 - accessories.length);

  return [...accessories, ...extras].slice(0, 4);
}
