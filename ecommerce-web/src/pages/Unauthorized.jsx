import React from 'react';
import { Link } from 'react-router-dom';
import { ShieldAlert, ArrowLeft } from 'lucide-react';

export default function Unauthorized() {
    return (
        <div className="min-h-screen flex items-center justify-center bg-slate-950 px-4 text-center relative overflow-hidden">
            {/* Ambient glows */}
            <div className="absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 w-96 h-96 bg-rose-500/5 rounded-full blur-3xl pointer-events-none" />

            <div className="max-w-md w-full backdrop-blur-md bg-white/5 border border-white/5 p-8 rounded-3xl shadow-2xl space-y-6 relative z-10">
                <div className="h-16 w-16 bg-rose-500/10 border border-rose-500/10 text-rose-400 rounded-3xl flex items-center justify-center mx-auto">
                    <ShieldAlert size={32} />
                </div>
                
                <div className="space-y-2">
                    <h2 className="text-2xl font-bold text-white">Từ chối truy cập</h2>
                    <p className="text-slate-400 text-sm leading-relaxed">
                        Bạn không có quyền hạn cần thiết để truy cập vào phân hệ này. Khu vực này chỉ dành riêng cho nhân viên điều hành hoặc quản trị viên.
                    </p>
                </div>

                <div className="pt-4">
                    <Link
                        to="/"
                        className="inline-flex items-center justify-center gap-2 rounded-2xl bg-indigo-600 px-5 py-3 text-sm font-semibold text-white shadow-lg shadow-indigo-600/20 hover:bg-indigo-500 active:scale-95 transition-all duration-200"
                    >
                        <ArrowLeft size={16} />
                        Quay lại trang chủ
                    </Link>
                </div>
            </div>
        </div>
    );
}
