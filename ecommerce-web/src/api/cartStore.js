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
};

// Hàm xóa toàn bộ giỏ hàng
export const clearCart = () => {
    localStorage.removeItem('cart');
};