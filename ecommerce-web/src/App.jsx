import React, { useEffect, useState } from 'react';
import api from './api/axios';
import { ShoppingCart, Package, Tag, Database, Zap } from 'lucide-react';
import { Link } from 'react-router-dom';
import Register from './pages/Register'
import { addToCart } from './api/cartStore';

function App() {
  const [products, setProducts] = useState([]);
  const [source, setSource] = useState(''); // Để hiển thị dữ liệu từ SQL hay Redis
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchProducts();
  }, []);

  const fetchProducts = async () => {
    try {
      setLoading(true);
      const response = await api.get('/Products');
      // Dựa vào cấu trúc trả về của Backend: { data: [...], source: "..." }
      setProducts(response.data.data);
      setSource(response.data.source);
    } catch (error) {
      console.error("Lỗi lấy sản phẩm:", error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-gray-50">
      {/* Navbar đơn giản */}
      <nav className="bg-slate-900 text-white p-4 shadow-lg sticky top-0 z-10">
        <div className="container mx-auto flex justify-between items-center">
          <h1 className="text-2xl font-black tracking-tighter flex items-center gap-2">
            <ShoppingCart className="text-blue-400" /> ECO-SHOP
          </h1>
          <div className="flex gap-6 items-center">
            <Link to="/login" className="hover:text-blue-400 transition">Đăng nhập</Link>
            <div className="bg-blue-600 px-3 py-1 rounded-full text-xs font-bold flex items-center gap-1">
              {source === 'Redis Cache' ? <Zap size={14} /> : <Database size={14} />}
              {source}
            </div>
          </div>
        </div>
      </nav>

      {/* Hero Section */}
      <header className="bg-white border-b p-8 mb-8">
        <div className="container mx-auto text-center">
          <h2 className="text-4xl font-bold text-slate-800">Danh Sách Sản Phẩm</h2>
          <p className="text-gray-500 mt-2">Dữ liệu được tối ưu hóa bằng Redis Caching</p>
        </div>
      </header>

      {/* Main Content */}
      <main className="container mx-auto p-4">
        {loading ? (
          <div className="flex justify-center items-center h-64">
            <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600"></div>
          </div>
        ) : (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
            {products.map((product) => (
              <div key={product.id} className="bg-white rounded-2xl shadow-sm hover:shadow-xl transition-all border border-gray-100 overflow-hidden group">
                <div className="h-48 bg-gray-200 flex items-center justify-center">
                  <Package size={48} className="text-gray-400 group-hover:scale-110 transition-transform" />
                </div>
                <div className="p-6">
                  <div className="flex items-center gap-2 text-blue-600 text-xs font-bold mb-2 uppercase">
                    <Tag size={12} /> {product.categoryId ? 'Category' : 'No Category'}
                  </div>
                  <h3 className="text-lg font-bold text-slate-800 mb-1">{product.name}</h3>
                  <p className="text-gray-500 text-sm mb-4 line-clamp-2">{product.description}</p>
                  <div className="flex justify-between items-center">
                    <span className="text-xl font-black text-slate-900">
                      {product.price.toLocaleString()}đ
                    </span>
                    <span className={`text-xs px-2 py-1 rounded ${product.inventory?.availableQuantity > 0 ? 'bg-green-100 text-green-700' : 'bg-red-100 text-red-700'}`}>
                      Kho: {product.inventory?.availableQuantity ?? 0}
                    </span>
                  </div>
                  <button
                    onClick={() => {
                      addToCart(product);
                      alert("Đã thêm vào giỏ!");
                    }}
                    className="..."
                  >
                    Thêm vào giỏ
                  </button>
                </div>
              </div>
            ))}
          </div>
        )}
      </main>
    </div>
  );
}

export default App;