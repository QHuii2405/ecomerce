import React, { useEffect, useState } from 'react';
import { useSearchParams, Link } from 'react-router-dom';
import { CheckCircle2, XCircle, Loader2 } from 'lucide-react';
import api from '../api/axios';

export default function PaymentResult() {
  const [searchParams] = useSearchParams();
  const [status, setStatus] = useState('loading');
  
  const orderId = searchParams.get('orderId'); 
  const errorCode = searchParams.get('errorCode');
  const resultCode = searchParams.get('resultCode');
  const cancel = searchParams.get('cancel');

  useEffect(() => {
    if (errorCode && errorCode !== '0') {
      setStatus('failed');
      return;
    }
    if (resultCode && resultCode !== '0') {
      setStatus('failed');
      return;
    }
    if (cancel === 'true') {
      setStatus('failed');
      return;
    }
    
    const checkStatus = async () => {
      try {
        let idToCheck = orderId;
        if (!idToCheck) {
            const res = await api.get('/orders/my-orders');
            if (res.data && res.data.length > 0) {
                idToCheck = res.data[0].id;
            }
        }
        
        if (!idToCheck) {
            setStatus('failed');
            return;
        }

        await new Promise(r => setTimeout(r, 2000));

        const res = await api.get(`/payments/${idToCheck}/status`);
        if (res.data?.paymentStatus === 'Paid' || res.data?.status === 'Confirmed') {
          setStatus('success');
        } else {
          // If still pending, treat as failed or pending
          setStatus('failed');
        }
      } catch (err) {
        setStatus('failed');
      }
    };
    
    checkStatus();
  }, [orderId, errorCode, resultCode, cancel]);

  return (
    <div className="min-h-screen bg-background flex items-center justify-center p-4">
      <div className="text-center max-w-md space-y-6">
        {status === 'loading' && (
          <div className="space-y-4">
            <Loader2 size={48} className="mx-auto text-primary animate-spin" />
            <h2 className="text-xl font-bold">Đang kiểm tra giao dịch...</h2>
            <p className="text-sm text-on-surface-variant">Vui lòng không đóng trình duyệt lúc này.</p>
          </div>
        )}
        
        {status === 'success' && (
          <div className="space-y-4">
            <div className="w-20 h-20 bg-emerald-500/10 rounded-full flex items-center justify-center mx-auto border border-emerald-500/20">
              <CheckCircle2 size={40} className="text-emerald-500" />
            </div>
            <h2 className="text-2xl font-bold">Thanh toán thành công!</h2>
            <p className="text-sm text-on-surface-variant">Đơn hàng của bạn đã được xác nhận và đang được chuẩn bị.</p>
            <div className="pt-4 flex gap-3 justify-center">
              <Link to="/profile" className="px-6 py-2.5 bg-primary text-white rounded-xl font-semibold hover:bg-primary-container shadow-lg shadow-primary/20 transition-all">
                Theo dõi đơn hàng
              </Link>
              <Link to="/" className="px-6 py-2.5 border border-outline-variant text-on-surface rounded-xl font-semibold hover:bg-surface-container transition-all">
                Tiếp tục mua sắm
              </Link>
            </div>
          </div>
        )}

        {status === 'failed' && (
          <div className="space-y-4">
            <div className="w-20 h-20 bg-rose-500/10 rounded-full flex items-center justify-center mx-auto border border-rose-500/20">
              <XCircle size={40} className="text-rose-500" />
            </div>
            <h2 className="text-2xl font-bold">Thanh toán thất bại</h2>
            <p className="text-sm text-on-surface-variant">Giao dịch đã bị hủy hoặc có lỗi xảy ra. Vui lòng thử lại.</p>
            <div className="pt-4 flex gap-3 justify-center">
              <Link to="/cart" className="px-6 py-2.5 bg-primary text-white rounded-xl font-semibold hover:bg-primary-container shadow-lg shadow-primary/20 transition-all">
                Về giỏ hàng
              </Link>
              <Link to="/profile" className="px-6 py-2.5 border border-outline-variant text-on-surface rounded-xl font-semibold hover:bg-surface-container transition-all">
                Xem đơn hàng
              </Link>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
