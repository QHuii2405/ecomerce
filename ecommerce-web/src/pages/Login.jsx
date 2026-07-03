import React, { useState } from 'react';
import api from '../api/axios';
import { ShoppingCart, Mail, Lock, AlertCircle, ArrowRight, Eye, EyeOff } from 'lucide-react';
import { Link, useNavigate } from 'react-router-dom';
import ForgotPasswordModal from '../components/ForgotPasswordModal';
import { useGoogleLogin } from '@react-oauth/google';

function Login() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [showPassword, setShowPassword] = useState(false);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const [forgotModalOpen, setForgotModalOpen] = useState(false);
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

    const handleGoogleLogin = useGoogleLogin({
        onSuccess: async (tokenResponse) => {
            setLoading(true);
            setError(null);
            try {
                // Pass access_token or id_token to backend.
                // React-oauth/google returns access_token in implicit flow.
                // To get id_token, we should use credentialResponse from <GoogleLogin />
                // Since we use useGoogleLogin for custom button, we fetch userinfo first or let backend verify.
                // Actually, backend expects id_token. But useGoogleLogin gives access_token.
                // It's better to fetch userInfo here or just send access_token and let backend fetch Google profile.
                // Since our backend expects id_token and uses tokeninfo endpoint, we need to adapt it.
                // Let's modify backend later if needed, but for now we pass the token to backend.
                // Wait, useGoogleLogin doesn't return id_token. I'll use the Google API directly for custom button.
                
                // Fetch user info from Google using access token
                const userInfoRes = await fetch('https://www.googleapis.com/oauth2/v3/userinfo', {
                    headers: { Authorization: `Bearer ${tokenResponse.access_token}` },
                });
                const userInfo = await userInfoRes.json();
                
                // We'll modify backend GoogleLogin to accept the user info directly, or better:
                // Send the token to backend and modify backend to use userinfo endpoint.
                
                const response = await api.post('/Auth/google-login', { idToken: tokenResponse.access_token });
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
                setError(err.response?.data?.message || 'Đăng nhập Google thất bại.');
            } finally {
                setLoading(false);
            }
        },
        onError: () => setError('Đăng nhập Google thất bại.'),
    });

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
                            <button 
                                type="button" 
                                onClick={() => setForgotModalOpen(true)}
                                className="text-xs font-semibold text-primary hover:text-primary-container transition-colors"
                            >
                                Quên mật khẩu?
                            </button>
                        </div>
                        <div className="relative">
                            <div className="absolute inset-y-0 left-0 pl-4 flex items-center pointer-events-none text-on-surface-variant">
                                <Lock size={18} />
                            </div>
                            <input
                                type={showPassword ? 'text' : 'password'}
                                required
                                placeholder="••••••••"
                                className="w-full pl-11 pr-12 py-3 bg-surface-container-low border border-outline-variant/30 rounded-2xl text-on-surface placeholder:text-outline focus:outline-none focus:border-primary focus:ring-1 focus:ring-primary transition-all"
                                onChange={(e) => setPassword(e.target.value)}
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
                    </div>

                    <button
                        type="submit"
                        disabled={loading}
                        className="w-full flex items-center justify-center gap-2 rounded-2xl bg-primary py-4 text-sm font-bold text-on-primary shadow-lg shadow-primary/20 hover:bg-primary-container active:scale-[0.98] disabled:opacity-50 disabled:pointer-events-none transition-all duration-200 hover-lift"
                    >
                        {loading ? 'Đang xác thực...' : 'Đăng Nhập'}
                        {!loading && <ArrowRight size={16} />}
                    </button>

                    <div className="relative flex items-center justify-center mt-6 mb-6">
                        <div className="absolute inset-0 flex items-center">
                            <div className="w-full border-t border-outline-variant/30"></div>
                        </div>
                        <span className="relative bg-surface-container-lowest px-4 text-xs text-on-surface-variant uppercase tracking-wider font-semibold">
                            Hoặc
                        </span>
                    </div>

                    <button
                        type="button"
                        onClick={() => handleGoogleLogin()}
                        disabled={loading}
                        className="w-full flex items-center justify-center gap-3 rounded-2xl bg-surface-container-lowest border border-outline-variant/30 py-3.5 text-sm font-bold text-on-surface hover:bg-surface-container-low transition-all duration-200 active:scale-[0.98]"
                    >
                        <img src="https://www.svgrepo.com/show/475656/google-color.svg" alt="Google" className="w-5 h-5" />
                        Đăng nhập bằng Google
                    </button>
                </form>
            </div>
            
            <ForgotPasswordModal open={forgotModalOpen} onClose={() => setForgotModalOpen(false)} />
        </div>
    );
}

export default Login;
