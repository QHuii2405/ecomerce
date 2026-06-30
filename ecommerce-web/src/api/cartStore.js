// Hàm lấy giỏ hàng từ LocalStorage
export const getCart = () => {
    const cart = localStorage.getItem('cart');
    return cart ? JSON.parse(cart) : [];
};

// Hàm thêm sản phẩm vào giỏ
export const addToCart = (product) => {
    let cart = getCart();
    const index = cart.findIndex(item => item.id === product.id);

    if (index !== -1) {
        cart[index].quantity += 1;
    } else {
        cart.push({ ...product, quantity: 1 });
    }
    localStorage.setItem('cart', JSON.stringify(cart));
    // Dispatch event để Nav cập nhật số lượng giỏ hàng
    window.dispatchEvent(new Event('cart-updated'));
};

// Hàm xóa một sản phẩm
export const removeFromCart = (productId) => {
    let cart = getCart().filter(item => item.id !== productId);
    localStorage.setItem('cart', JSON.stringify(cart));
    window.dispatchEvent(new Event('cart-updated'));
};

// Hàm cập nhật số lượng
export const updateQuantity = (productId, quantity) => {
    let cart = getCart();
    const index = cart.findIndex(item => item.id === productId);
    if (index !== -1) {
        cart[index].quantity = quantity;
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