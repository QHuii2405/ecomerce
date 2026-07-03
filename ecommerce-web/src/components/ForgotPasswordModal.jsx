import React, { useState } from 'react';
import api from '../api/axios';
import Swal from 'sweetalert2';
import { X, Mail, Lock, CheckCircle2, ArrowRight } from 'lucide-react';

export default function ForgotPasswordModal({ open, onClose }) {
  const [step, setStep] = useState(1);
  const [email, setEmail] = useState('');
  const [otp, setOtp] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [loading, setLoading] = useState(false);

  if (!open) return null;

  const handleSendOtp = async (e) => {
    e.preventDefault();
    if (!email) return;
    setLoading(true);
    try {
      const res = await api.post('/Auth/forgot-password', { email });
      Swal.fire({ icon: 'success', text: res.data.message });
      setStep(2);
    } catch (err) {
      Swal.fire({ icon: 'error', text: err.response?.data?.message || 'Lỗi gửi yêu cầu' });
    } finally {
      setLoading(false);
    }
  };

  const handleResetPassword = async (e) => {
    e.preventDefault();
    if (!otp || !newPassword) return;
    setLoading(true);
    try {
      const res = await api.post('/Auth/reset-password', { email, otpCode: otp, newPassword });
      Swal.fire({ icon: 'success', text: res.data.message });
      onClose();
      // Reset state
      setStep(1);
      setEmail('');
      setOtp('');
      setNewPassword('');
    } catch (err) {
      Swal.fire({ icon: 'error', text: err.response?.data?.message || 'Lỗi khôi phục mật khẩu' });
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-black/50 backdrop-blur-sm">
      <div className="bg-surface border border-outline-variant/30 rounded-3xl w-full max-w-md shadow-2xl overflow-hidden relative">
        {/* Header */}
        <div className="flex items-center justify-between p-6 border-b border-outline-variant/30">
          <h3 className="font-bold text-on-surface text-xl">Quên mật khẩu</h3>
          <button onClick={onClose} className="text-on-surface-variant hover:text-on-surface transition-colors">
            <X size={24} />
          </button>
        </div>

        {/* Body */}
        <div className="p-6">
          {step === 1 ? (
            <form onSubmit={handleSendOtp} className="space-y-6">
              <p className="text-sm text-on-surface-variant">
                Vui lòng nhập email đã đăng ký. Chúng tôi sẽ gửi một mã OTP gồm 6 chữ số để đặt lại mật khẩu.
              </p>
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
                    className="w-full pl-11 pr-4 py-3 bg-surface-container-low border border-outline-variant/30 rounded-2xl text-on-surface placeholder:text-outline focus:outline-none focus:border-primary transition-all"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                  />
                </div>
              </div>
              <button
                type="submit"
                disabled={loading}
                className="w-full flex items-center justify-center gap-2 rounded-2xl bg-primary py-4 text-sm font-bold text-on-primary shadow-lg shadow-primary/20 hover:bg-primary-container disabled:opacity-50 transition-all"
              >
                {loading ? 'Đang gửi...' : 'Gửi mã xác nhận'}
              </button>
            </form>
          ) : (
            <form onSubmit={handleResetPassword} className="space-y-6">
              <p className="text-sm text-on-surface-variant">
                Mã xác nhận đã được gửi đến <span className="font-bold text-on-surface">{email}</span>
              </p>
              <div className="space-y-2">
                <label className="text-sm font-bold text-on-surface block">Mã OTP (6 số)</label>
                <div className="relative">
                  <div className="absolute inset-y-0 left-0 pl-4 flex items-center pointer-events-none text-on-surface-variant">
                    <CheckCircle2 size={18} />
                  </div>
                  <input
                    type="text"
                    required
                    maxLength={6}
                    placeholder="123456"
                    className="w-full pl-11 pr-4 py-3 bg-surface-container-low border border-outline-variant/30 rounded-2xl text-on-surface placeholder:text-outline focus:outline-none focus:border-primary transition-all font-mono tracking-widest text-lg"
                    value={otp}
                    onChange={(e) => setOtp(e.target.value)}
                  />
                </div>
              </div>
              <div className="space-y-2">
                <label className="text-sm font-bold text-on-surface block">Mật khẩu mới</label>
                <div className="relative">
                  <div className="absolute inset-y-0 left-0 pl-4 flex items-center pointer-events-none text-on-surface-variant">
                    <Lock size={18} />
                  </div>
                  <input
                    type="password"
                    required
                    placeholder="••••••••"
                    className="w-full pl-11 pr-4 py-3 bg-surface-container-low border border-outline-variant/30 rounded-2xl text-on-surface placeholder:text-outline focus:outline-none focus:border-primary transition-all"
                    value={newPassword}
                    onChange={(e) => setNewPassword(e.target.value)}
                  />
                </div>
              </div>
              <button
                type="submit"
                disabled={loading}
                className="w-full flex items-center justify-center gap-2 rounded-2xl bg-primary py-4 text-sm font-bold text-on-primary shadow-lg shadow-primary/20 hover:bg-primary-container disabled:opacity-50 transition-all"
              >
                {loading ? 'Đang đặt lại...' : 'Đặt lại mật khẩu'}
                {!loading && <ArrowRight size={16} />}
              </button>
            </form>
          )}
        </div>
      </div>
    </div>
  );
}
