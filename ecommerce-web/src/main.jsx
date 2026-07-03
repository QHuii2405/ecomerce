import React from 'react'
import ReactDOM from 'react-dom/client'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import App from './App'
import Products from './pages/Products'
import Login from './pages/Login'
import Register from './pages/Register'
import Profile from './pages/Profile'
import Cart from './pages/Cart'
import ProductDetail from './pages/ProductDetail'
import Recruitment from './pages/Recruitment'
import Unauthorized from './pages/Unauthorized'
import PaymentResult from './pages/PaymentResult'
import ProtectedRoute from './components/ProtectedRoute'
import AdminLayout from './components/AdminLayout'
import AdminDashboard from './pages/admin/AdminDashboard'
import AdminProducts from './pages/admin/AdminProducts'
import AdminOrders from './pages/admin/AdminOrders'
import AdminCategories from './pages/admin/AdminCategories'
import AdminUsers from './pages/admin/AdminUsers'
import AdminInventory from './pages/admin/AdminInventory'
import AdminVouchers from './pages/admin/AdminVouchers'
import AdminReviews from './pages/admin/AdminReviews'
import AdminReturns from './pages/admin/AdminReturns'
import AdminCms from './pages/admin/AdminCms'
import { NotificationProvider } from './contexts/NotificationContext'
import { GoogleOAuthProvider } from '@react-oauth/google'
import './index.css'

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <GoogleOAuthProvider clientId="868316103534-vghj1eq84lqtma1lvknghmc3ifgb73cg.apps.googleusercontent.com">
      <NotificationProvider>
        <BrowserRouter>
          <Routes>
            {/* Nhánh Route công khai (Public Store) */}
            <Route path="/" element={<App />} />
            <Route path="/products" element={<Products />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/cart" element={<Cart />} />
            <Route path="/product/:id" element={<ProductDetail />} />
            <Route path="/recruitment" element={<Recruitment />} />
            <Route path="/unauthorized" element={<Unauthorized />} />
            <Route path="/payment-result" element={<PaymentResult />} />

            {/* Nhánh Route được bảo vệ dành cho khách hàng đã đăng nhập */}
            <Route element={<ProtectedRoute allowedRoles={['Customer', 'Staff', 'Admin']} />}>
              <Route path="/profile" element={<Profile />} />
            </Route>

            {/* Nhánh Route được bảo vệ dành cho nhân sự quản trị (Admin & Staff) */}
            <Route element={<ProtectedRoute allowedRoles={['Admin', 'Staff']} />}>
              <Route path="/admin" element={<AdminLayout />}>
                <Route index element={<AdminDashboard />} />
                <Route path="products" element={<AdminProducts />} />
                <Route path="orders" element={<AdminOrders />} />
                <Route path="categories" element={<AdminCategories />} />
                <Route path="vouchers" element={<AdminVouchers />} />
                <Route path="inventory" element={<AdminInventory />} />
                <Route path="reviews" element={<AdminReviews />} />
                <Route path="returns" element={<AdminReturns />} />
                <Route path="cms" element={<AdminCms />} />

                {/* Route con chỉ dành riêng cho Admin (Staff truy cập sẽ vào trang Unauthorized) */}
                <Route element={<ProtectedRoute allowedRoles={['Admin']} />}>
                  <Route path="users" element={<AdminUsers />} />
                </Route>
              </Route>
            </Route>
          </Routes>
        </BrowserRouter>
      </NotificationProvider>
    </GoogleOAuthProvider>
  </React.StrictMode>
)
