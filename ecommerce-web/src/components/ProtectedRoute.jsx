import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';

export default function ProtectedRoute({ allowedRoles }) {
    const token = localStorage.getItem('token');
    const userRole = localStorage.getItem('userRole');

    if (!token) {
        // Chưa đăng nhập -> chuyển hướng về trang Login
        return <Navigate to="/login" replace />;
    }

    if (allowedRoles && !allowedRoles.includes(userRole)) {
        // Đã đăng nhập nhưng không đủ quyền -> chuyển hướng về trang chủ
        return <Navigate to="/unauthorized" replace />;
    }

    // Hợp lệ -> Cho phép truy cập vào các route con
    return <Outlet />;
}
