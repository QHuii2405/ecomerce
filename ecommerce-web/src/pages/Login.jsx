import React, { useState } from 'react';
import api from '../api/axios';

function Login() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await api.post('/Auth/login', { email, password });
            localStorage.setItem('token', response.data.token);
            alert("Đăng nhập thành công! Token đã được lưu.");
            window.location.href = "/"; // Quay lại trang chủ
        } catch (error) {
            alert("Đăng nhập thất bại: " + error.response?.data);
        }
    };

    return (
        <div className="min-h-screen flex items-center justify-center bg-gray-100">
            <form onSubmit={handleSubmit} className="bg-white p-8 rounded-lg shadow-md w-96">
                <h2 className="text-2xl font-bold mb-6 text-center text-gray-800">Đăng Nhập</h2>
                <input
                    type="email" placeholder="Email"
                    className="w-full p-2 mb-4 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                    onChange={(e) => setEmail(e.target.value)}
                />
                <input
                    type="password" placeholder="Mật khẩu"
                    className="w-full p-2 mb-6 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
                    onChange={(e) => setPassword(e.target.value)}
                />
                <button type="submit" className="w-full bg-blue-600 text-white py-2 rounded font-bold hover:bg-blue-700">
                    Xác Nhận
                </button>
            </form>
        </div>
    );
}

export default Login;