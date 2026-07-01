import React, { useState } from 'react';
import api from '../api/axios';
import { UserPlus, Mail, Lock, Phone, MapPin, User, ArrowRight, AlertCircle, Eye, EyeOff } from 'lucide-react';
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
    const [showPassword, setShowPassword] = useState(false);
    const [showConfirmPassword, setShowConfirmPassword] = useState(false);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');

        if (formData.password !== formData.confirmPassword) {
            setError("Mật khẩu xác nhận không khớp!");
            return;
        }

        setLoading(true);
        try {
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
            setError(err.response?.data?.message || err.response?.data || "Đăng ký thất bại, vui lòng thử lại.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="min-h-screen flex items-center justify-center bg-surface px-4 py-12 relative overflow-hidden">
            {/* Background Decorations */}
            <div className="absolute top-1/4 left-1/4 -translate-x-1/2 -translate-y-1/2 w-96 h-96 bg-primary/5 rounded-full blur-3xl" />
            <div className="absolute bottom-1/4 right-1/4 translate-x-1/2 translate-y-1/2 w-96 h-96 bg-tertiary/5 rounded-full blur-3xl" />

            <div className="w-full max-w-md space-y-8 bg-surface-container-lowest border border-outline-variant/30 p-8 rounded-3xl shadow-xl relative z-10">
                <div className="text-center">
                    <div className="inline-flex h-12 w-12 items-center justify-center rounded-2xl bg-primary-container text-on-primary-container shadow-lg shadow-primary/20 mb-4">
                        <UserPlus size={24} />
                    </div>
                    <h2 className="text-3xl font-bold tracking-tight text-on-surface">Tạo Tài Khoản</h2>
                    <p className="mt-2 text-sm text-on-surface-variant">
                        Tham gia <span className="font-bold text-primary">iLuminaty Shop</span> ngay hôm nay
                    </p>
                </div>

                {error && (
                    <div className="flex items-center gap-3 rounded-2xl border border-error/20 bg-error-container p-4 text-sm text-on-error-container">
                        <AlertCircle size={18} className="shrink-0 text-error" />
                        <p>{error}</p>
                    </div>
                )}

                <form onSubmit={handleSubmit} className="space-y-4">
                    <div className="relative">
                        <User className="absolute left-4 top-3.5 text-on-surface-variant" size={18} />
                        <input name="fullName" type="text" placeholder="Họ và tên" required className="w-full pl-11 pr-4 py-3 bg-surface-container-low border border-outline-variant/30 rounded-2xl text-on-surface placeholder:text-outline focus:outline-none focus:border-primary focus:ring-1 focus:ring-primary transition-all" onChange={handleChange} />
                    </div>

                    <div className="relative">
                        <Mail className="absolute left-4 top-3.5 text-on-surface-variant" size={18} />
                        <input name="email" type="email" placeholder="Email" required className="w-full pl-11 pr-4 py-3 bg-surface-container-low border border-outline-variant/30 rounded-2xl text-on-surface placeholder:text-outline focus:outline-none focus:border-primary focus:ring-1 focus:ring-primary transition-all" onChange={handleChange} />
                    </div>

                    <div className="grid grid-cols-2 gap-4">
                        <div className="relative">
                            <Lock className="absolute left-4 top-3.5 text-on-surface-variant" size={18} />
                            <input
                                name="password"
                                type={showPassword ? 'text' : 'password'}
                                placeholder="Mật khẩu"
                                required
                                className="w-full pl-11 pr-12 py-3 bg-surface-container-low border border-outline-variant/30 rounded-2xl text-on-surface placeholder:text-outline focus:outline-none focus:border-primary focus:ring-1 focus:ring-primary transition-all"
                                onChange={handleChange}
                            />
                            <button
                                type="button"
                                onClick={() => setShowPassword((prev) => !prev)}
                                className="absolute inset-y-0 right-0 px-4 flex items-center text-on-surface-variant hover:text-primary transition-colors"
                                aria-label={showPassword ? 'Ẩn mật khẩu' : 'Hiện mật khẩu'}
                                title={showPassword ? 'Ẩn mật khẩu' : 'Hiện mật khẩu'}
                            >
                                {showPassword ? <EyeOff size={18} /> : <Eye size={18} />}
                            </button>
                        </div>
                        <div className="relative">
                            <Lock className="absolute left-4 top-3.5 text-on-surface-variant" size={18} />
                            <input
                                name="confirmPassword"
                                type={showConfirmPassword ? 'text' : 'password'}
                                placeholder="Xác nhận"
                                required
                                className="w-full pl-11 pr-12 py-3 bg-surface-container-low border border-outline-variant/30 rounded-2xl text-on-surface placeholder:text-outline focus:outline-none focus:border-primary focus:ring-1 focus:ring-primary transition-all"
                                onChange={handleChange}
                            />
                            <button
                                type="button"
                                onClick={() => setShowConfirmPassword((prev) => !prev)}
                                className="absolute inset-y-0 right-0 px-4 flex items-center text-on-surface-variant hover:text-primary transition-colors"
                                aria-label={showConfirmPassword ? 'Ẩn mật khẩu xác nhận' : 'Hiện mật khẩu xác nhận'}
                                title={showConfirmPassword ? 'Ẩn mật khẩu xác nhận' : 'Hiện mật khẩu xác nhận'}
                            >
                                {showConfirmPassword ? <EyeOff size={18} /> : <Eye size={18} />}
                            </button>
                        </div>
                    </div>

                    <div className="relative">
                        <Phone className="absolute left-4 top-3.5 text-on-surface-variant" size={18} />
                        <input name="phoneNumber" type="text" placeholder="Số điện thoại" className="w-full pl-11 pr-4 py-3 bg-surface-container-low border border-outline-variant/30 rounded-2xl text-on-surface placeholder:text-outline focus:outline-none focus:border-primary focus:ring-1 focus:ring-primary transition-all" onChange={handleChange} />
                    </div>

                    <div className="relative">
                        <MapPin className="absolute left-4 top-3.5 text-on-surface-variant" size={18} />
                        <textarea name="address" placeholder="Địa chỉ giao hàng" rows="2" className="w-full pl-11 pr-4 py-3 bg-surface-container-low border border-outline-variant/30 rounded-2xl text-on-surface placeholder:text-outline focus:outline-none focus:border-primary focus:ring-1 focus:ring-primary transition-all resize-none" onChange={handleChange}></textarea>
                    </div>

                    <button 
                        type="submit" 
                        disabled={loading}
                        className="w-full flex items-center justify-center gap-2 rounded-2xl bg-primary py-4 text-sm font-bold text-on-primary shadow-lg shadow-primary/20 hover:bg-primary-container active:scale-[0.98] disabled:opacity-50 disabled:pointer-events-none transition-all duration-200 hover-lift mt-2"
                    >
                        {loading ? 'Đang xử lý...' : 'Đăng Ký Miễn Phí'}
                        {!loading && <ArrowRight size={16} />}
                    </button>
                </form>

                <p className="text-center mt-6 text-sm text-on-surface-variant">
                    Đã có tài khoản?{' '}
                    <Link to="/login" className="font-bold text-primary hover:text-primary-container transition-colors">
                        Đăng nhập
                    </Link>
                </p>
            </div>
        </div>
    );
}

export default Register;