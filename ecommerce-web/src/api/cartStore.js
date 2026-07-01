// Hàm lấy giỏ hàng từ LocalStorage
export const getCart = () => {
    const cart = localStorage.getItem('cart');
    return cart ? JSON.parse(cart) : [];
};

const normalizeVariant = (variant = {}) => Object.keys(variant)
    .sort()
    .reduce((acc, key) => ({ ...acc, [key]: variant[key] }), {});

export const getCartItemKey = (item) => item.cartItemKey || `${item.id}-${JSON.stringify(normalizeVariant(item.variant))}`;

// Hàm thêm sản phẩm vào giỏ
export const addToCart = (product) => {
    let cart = getCart();
    const productKey = getCartItemKey(product);
    const index = cart.findIndex(item => getCartItemKey(item) === productKey);

    if (index !== -1) {
        cart[index].quantity += 1;
    } else {
        cart.push({ ...product, cartItemKey: productKey, quantity: 1 });
    }
    localStorage.setItem('cart', JSON.stringify(cart));
    // Dispatch event để Nav cập nhật số lượng giỏ hàng
    window.dispatchEvent(new Event('cart-updated'));
};

// Hàm xóa một sản phẩm
export const removeFromCart = (cartItemKey) => {
    let cart = getCart().filter(item => getCartItemKey(item) !== cartItemKey);
    localStorage.setItem('cart', JSON.stringify(cart));
    window.dispatchEvent(new Event('cart-updated'));
};

// Hàm cập nhật số lượng
export const updateQuantity = (cartItemKey, quantity) => {
    let cart = getCart();
    const index = cart.findIndex(item => getCartItemKey(item) === cartItemKey);
    if (index !== -1) {
        cart[index].quantity = quantity;
        localStorage.setItem('cart', JSON.stringify(cart));
        window.dispatchEvent(new Event('cart-updated'));
    }
};

export const updateCartItemVariant = (cartItemKey, updates) => {
    let cart = getCart();
    const index = cart.findIndex(item => getCartItemKey(item) === cartItemKey);
    if (index !== -1) {
        cart[index].variant = { ...(cart[index].variant || {}), ...updates };
        cart[index].cartItemKey = `${cart[index].id}-${JSON.stringify(normalizeVariant(cart[index].variant))}`;
        localStorage.setItem('cart', JSON.stringify(cart));
        window.dispatchEvent(new Event('cart-updated'));
    }
};

// Hàm xóa toàn bộ giỏ hàng
export const clearCart = () => {
    localStorage.removeItem('cart');
    window.dispatchEvent(new Event('cart-updated'));
};

// Lấy tổng số item trong giỏ
export const getCartCount = () => {
    return getCart().reduce((total, item) => total + item.quantity, 0);
};