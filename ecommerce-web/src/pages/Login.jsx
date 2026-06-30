import React, { useState } from 'react';
import api from '../api/axios';
import { ShoppingCart, Mail, Lock, AlertCircle, ArrowRight } from 'lucide-react';
import { Link, useNavigate } from 'react-router-dom';

function Login() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        setError(null);

        try {
            const response = await api.post('/Auth/login', { email, password });
            const { accessToken, refreshToken, role, fullName } = response.data;
            
            localStorage.setItem('token', accessToken);
            localStorage.setItem('refreshToken', refreshToken);
            localStorage.setItem('userRole', role);
            localStorage.setItem('userName', fullName);

            if (role === 'Admin' || role === 'Staff') {
                window.location.href = "/admin";
            } else {
                window.location.href = "/";
            }
        } catch (err) {
            setError(err.response?.data?.message || 'Đăng nhập thất bại. Vui lòng kiểm tra lại tài khoản.');
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
                        <ShoppingCart size={24} />
                    </div>
                    <h2 className="text-3xl font-bold tracking-tight text-on-surface">Chào mừng trở lại</h2>
                    <p className="mt-2 text-sm text-on-surface-variant">
                        Chưa có tài khoản?{' '}
                        <Link to="/register" className="font-bold text-primary hover:text-primary-container transition-colors">
                            Đăng ký ngay
                        </Link>
                    </p>
                </div>

                {error && (
                    <div className="flex items-center gap-3 rounded-2xl border border-error/20 bg-error-container p-4 text-sm text-on-error-container">
                        <AlertCircle size={18} className="shrink-0 text-error" />
                        <p>{error}</p>
                    </div>
                )}

                <form onSubmit={handleSubmit} className="space-y-6">
                    <div className="space-y-2">
                        <label className="text-sm font-bold text-on-surface block">Địa chỉ Email</label>
                        <div className="relative">
                            <div className="absolute inset-y-0 left-0 pl-4 flex items-center pointer-events-none text-on-surface-variant">
                                <Mail size={18} />
                            </div>
                            <input
                                type="email"
                                required
                                placeholder="name@example.com"
                                className="w-full pl-11 pr-4 py-3 bg-surface-container-low border border-outline-variant/30 rounded-2xl text-on-surface placeholder:text-outline focus:outline-none focus:border-primary focus:ring-1 focus:ring-primary transition-all"
                                onChange={(e) => setEmail(e.target.value)}
                            />
                        </div>
                    </div>

                    <div className="space-y-2">
                        <div className="flex justify-between items-center">
                            <label className="text-sm font-bold text-on-surface block">Mật khẩu</label>
                        </div>
                        <div className="relative">
                            <div className="absolute inset-y-0 left-0 pl-4 flex items-center pointer-events-none text-on-surface-variant">
                                <Lock size={18} />
                            </div>
                            <input
                                type="password"
                                required
                                placeholder="••••••••"
                                className="w-full pl-11 pr-4 py-3 bg-surface-container-low border border-outline-variant/30 rounded-2xl text-on-surface placeholder:text-outline focus:outline-none focus:border-primary focus:ring-1 focus:ring-primary transition-all"
                                onChange={(e) => setPassword(e.target.value)}
                            />
                        </div>
                    </div>

                    <button
                        type="submit"
                        disabled={loading}
                        className="w-full flex items-center justify-center gap-2 rounded-2xl bg-primary py-4 text-sm font-bold text-on-primary shadow-lg shadow-primary/20 hover:bg-primary-container active:scale-[0.98] disabled:opacity-50 disabled:pointer-events-none transition-all duration-200 hover-lift"
                    >
                        {loading ? 'Đang xác thực...' : 'Đăng Nhập'}
                        {!loading && <ArrowRight size={16} />}
                    </button>
                </form>
            </div>
        </div>
    );
}

export default Login;