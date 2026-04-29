import React, { useState } from 'react';
import api from '../api/axios';
import { UserPlus, Mail, Lock, Phone, MapPin, User } from 'lucide-react';
import { useNavigate, Link } from 'react-router-dom';

function Register() {
    const [formData, setFormData] = useState({
        fullName: '',
        email: '',
        password: '',
        confirmPassword: '',
        phoneNumber: '',
        address: ''
    });
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');

        // Kiểm tra mật khẩu khớp
        if (formData.password !== formData.confirmPassword) {
            setError("Mật khẩu xác nhận không khớp!");
            return;
        }

        try {
            // Gửi sang Backend (Backend cần nhận đúng các trường này)
            await api.post('/Auth/register', {
                fullName: formData.fullName,
                email: formData.email,
                password: formData.password,
                phoneNumber: formData.phoneNumber,
                address: formData.address
            });

            alert("Đăng ký thành công! Hãy đăng nhập.");
            navigate('/login');
        } catch (err) {
            setError(err.response?.data || "Đăng ký thất bại, vui lòng thử lại.");
        }
    };

    return (
        <div className="min-h-screen bg-gray-50 flex items-center justify-center p-4">
            <div className="bg-white p-8 rounded-2xl shadow-xl w-full max-w-md border border-gray-100">
                <div className="text-center mb-8">
                    <div className="bg-blue-100 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4">
                        <UserPlus className="text-blue-600" size={32} />
                    </div>
                    <h2 className="text-2xl font-black text-slate-800">Tạo Tài Khoản</h2>
                    <p className="text-gray-500">Tham gia ECO-SHOP ngay hôm nay</p>
                </div>

                {error && <div className="bg-red-50 text-red-600 p-3 rounded-lg mb-4 text-sm font-medium">{error}</div>}

                <form onSubmit={handleSubmit} className="space-y-4">
                    <div className="relative">
                        <User className="absolute left-3 top-3 text-gray-400" size={18} />
                        <input name="fullName" type="text" placeholder="Họ và tên" required className="w-full pl-10 p-2.5 border rounded-xl focus:ring-2 focus:ring-blue-500 outline-none" onChange={handleChange} />
                    </div>

                    <div className="relative">
                        <Mail className="absolute left-3 top-3 text-gray-400" size={18} />
                        <input name="email" type="email" placeholder="Email" required className="w-full pl-10 p-2.5 border rounded-xl focus:ring-2 focus:ring-blue-500 outline-none" onChange={handleChange} />
                    </div>

                    <div className="grid grid-cols-2 gap-4">
                        <div className="relative">
                            <Lock className="absolute left-3 top-3 text-gray-400" size={18} />
                            <input name="password" type="password" placeholder="Mật khẩu" required className="w-full pl-10 p-2.5 border rounded-xl focus:ring-2 focus:ring-blue-500 outline-none" onChange={handleChange} />
                        </div>
                        <div className="relative">
                            <Lock className="absolute left-3 top-3 text-gray-400" size={18} />
                            <input name="confirmPassword" type="password" placeholder="Xác nhận" required className="w-full pl-10 p-2.5 border rounded-xl focus:ring-2 focus:ring-blue-500 outline-none" onChange={handleChange} />
                        </div>
                    </div>

                    <div className="relative">
                        <Phone className="absolute left-3 top-3 text-gray-400" size={18} />
                        <input name="phoneNumber" type="text" placeholder="Số điện thoại" className="w-full pl-10 p-2.5 border rounded-xl focus:ring-2 focus:ring-blue-500 outline-none" onChange={handleChange} />
                    </div>

                    <div className="relative">
                        <MapPin className="absolute left-3 top-3 text-gray-400" size={18} />
                        <textarea name="address" placeholder="Địa chỉ giao hàng" rows="2" className="w-full pl-10 p-2.5 border rounded-xl focus:ring-2 focus:ring-blue-500 outline-none" onChange={handleChange}></textarea>
                    </div>

                    <button type="submit" className="w-full bg-slate-900 text-white py-3 rounded-xl font-bold hover:bg-blue-600 transition shadow-lg">
                        Đăng Ký Miễn Phí
                    </button>
                </form>

                <p className="text-center mt-6 text-gray-600 text-sm">
                    Đã có tài khoản? <Link to="/login" className="text-blue-600 font-bold hover:underline">Đăng nhập</Link>
                </p>
            </div>
        </div>
    );
}

export default Register;