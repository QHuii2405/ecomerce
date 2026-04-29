import React, { useEffect, useState } from 'react';
import api from '../api/axios';
import { User, Mail, Phone, MapPin, Calendar, ShieldCheck, LogOut } from 'lucide-react';

function Profile() {
    const [userData, setUserData] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchProfile = async () => {
            try {
                const response = await api.get('/Auth/me');
                setUserData(response.data);
            } catch (error) {
                console.error("Lỗi lấy thông tin cá nhân:", error);
            } finally {
                setLoading(false);
            }
        };
        fetchProfile();
    }, []);

    const handleLogout = () => {
        localStorage.removeItem('token');
        window.location.href = '/login';
    };

    if (loading) return <div className="text-center mt-20">Đang tải hồ sơ...</div>;

    return (
        <div className="min-h-screen bg-gray-50 p-4 pb-12">
            <div className="max-w-4xl mx-auto">
                {/* Header Profile */}
                <div className="bg-slate-900 rounded-3xl p-8 text-white mb-6 shadow-xl flex flex-col md:flex-row items-center gap-6">
                    <div className="w-24 h-24 bg-blue-500 rounded-full flex items-center justify-center text-3xl font-black border-4 border-slate-700">
                        {userData?.fullName?.charAt(0).toUpperCase()}
                    </div>
                    <div className="text-center md:text-left flex-1">
                        <h1 className="text-3xl font-bold">{userData?.fullName}</h1>
                        <p className="text-blue-300 flex items-center justify-center md:justify-start gap-2 mt-1">
                            <ShieldCheck size={16} /> {userData?.role} Account
                        </p>
                    </div>
                    <button onClick={handleLogout} className="bg-red-500/20 hover:bg-red-500 text-red-200 px-4 py-2 rounded-xl transition flex items-center gap-2 border border-red-500/50">
                        <LogOut size={18} /> Đăng xuất
                    </button>
                </div>

                <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
                    {/* Cột thông tin chi tiết */}
                    <div className="md:col-span-2 space-y-6">
                        <div className="bg-white p-6 rounded-2xl shadow-sm border border-gray-100">
                            <h3 className="text-lg font-bold mb-4 flex items-center gap-2">
                                <User size={20} className="text-blue-600" /> Thông tin cá nhân
                            </h3>
                            <div className="grid grid-cols-1 gap-4">
                                <div className="flex items-center gap-4 p-3 bg-gray-50 rounded-xl">
                                    <Mail className="text-gray-400" size={20} />
                                    <div>
                                        <p className="text-xs text-gray-500">Email</p>
                                        <p className="font-medium">{userData?.email}</p>
                                    </div>
                                </div>
                                <div className="flex items-center gap-4 p-3 bg-gray-50 rounded-xl">
                                    <Phone className="text-gray-400" size={20} />
                                    <div>
                                        <p className="text-xs text-gray-500">Số điện thoại</p>
                                        <p className="font-medium">{userData?.phoneNumber || "Chưa cập nhật"}</p>
                                    </div>
                                </div>
                                <div className="flex items-center gap-4 p-3 bg-gray-50 rounded-xl">
                                    <MapPin className="text-gray-400" size={20} />
                                    <div>
                                        <p className="text-xs text-gray-500">Địa chỉ giao hàng</p>
                                        <p className="font-medium">{userData?.address || "Chưa cập nhật"}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    {/* Cột phụ */}
                    <div className="space-y-6">
                        <div className="bg-white p-6 rounded-2xl shadow-sm border border-gray-100">
                            <h3 className="text-lg font-bold mb-4 flex items-center gap-2">
                                <Calendar size={20} className="text-blue-600" /> Hoạt động
                            </h3>
                            <p className="text-xs text-gray-500">Ngày tham gia</p>
                            <p className="font-medium mb-4">{new Date(userData?.createdAt).toLocaleDateString('vi-VN')}</p>
                            <div className="pt-4 border-t">
                                <button className="w-full text-blue-600 font-bold text-sm hover:underline">
                                    Chỉnh sửa hồ sơ
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Profile;