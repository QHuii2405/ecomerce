import React, { useState } from 'react';
import { Bot, Send, X, MessageCircle, Loader2 } from 'lucide-react';
import { Link } from 'react-router-dom';
import api from '../api/axios';

export default function ChatbotWidget() {
  const [open, setOpen] = useState(false);
  const [message, setMessage] = useState('');
  const [loading, setLoading] = useState(false);
  const [messages, setMessages] = useState([
    { role: 'assistant', text: 'Xin chào! Mình có thể tư vấn sản phẩm theo nhu cầu, hãng, ngân sách và tồn kho hiện tại.' }
  ]);
  const [suggestedProducts, setSuggestedProducts] = useState([]);

  const sendMessage = async (text = message) => {
    if (!text.trim() || loading) return;
    const userText = text.trim();
    setMessage('');
    setMessages(prev => [...prev, { role: 'user', text: userText }]);
    setLoading(true);

    try {
      const res = await api.post('/chat', { message: userText });
      setMessages(prev => [...prev, { role: 'assistant', text: res.data.reply }]);
      setSuggestedProducts(res.data.suggestedProducts || []);
    } catch {
      setMessages(prev => [...prev, { role: 'assistant', text: 'Mình chưa kết nối được chatbot. Vui lòng thử lại sau.' }]);
    } finally {
      setLoading(false);
    }
  };

  const quickPrompts = ['Laptop dưới 30 triệu', 'Điện thoại Samsung còn hàng', 'Tai nghe chống ồn', 'Tư vấn gaming gear'];

  return (
    <div className="fixed bottom-5 right-5 z-[1000] font-sans">
      {open && (
        <div className="mb-4 w-[calc(100vw-2.5rem)] max-w-sm h-[560px] bg-surface-container-lowest border border-outline-variant/30 rounded-3xl shadow-2xl overflow-hidden flex flex-col">
          <div className="bg-primary text-white p-4 flex items-center justify-between">
            <div className="flex items-center gap-2">
              <div className="w-9 h-9 bg-white/20 rounded-2xl flex items-center justify-center"><Bot size={20} /></div>
              <div>
                <p className="font-bold text-sm">AI tư vấn mua hàng</p>
                <p className="text-[10px] text-white/80">Gemini + dữ liệu cửa hàng</p>
              </div>
            </div>
            <button onClick={() => setOpen(false)} className="p-1 hover:bg-white/10 rounded-lg"><X size={18} /></button>
          </div>

          <div className="flex-1 overflow-y-auto p-4 space-y-3 bg-background/50">
            {messages.map((m, idx) => (
              <div key={idx} className={`flex ${m.role === 'user' ? 'justify-end' : 'justify-start'}`}>
                <div className={`max-w-[82%] rounded-2xl px-3 py-2 text-xs leading-relaxed ${m.role === 'user' ? 'bg-primary text-white' : 'bg-surface-container-low border border-outline-variant/20 text-on-surface'}`}>
                  {m.text}
                </div>
              </div>
            ))}
            {loading && <div className="flex items-center gap-2 text-xs text-on-surface-variant"><Loader2 size={14} className="animate-spin" /> Đang tư vấn...</div>}

            {suggestedProducts.length > 0 && (
              <div className="space-y-2 pt-2">
                <p className="text-[10px] font-bold uppercase text-on-surface-variant">Sản phẩm gợi ý</p>
                {suggestedProducts.slice(0, 3).map(p => (
                  <Link key={p.id} to={`/product/${p.id}`} onClick={() => setOpen(false)} className="block bg-surface-container-lowest border border-outline-variant/20 rounded-2xl p-3 hover:border-primary/40 transition-all">
                    <p className="text-xs font-bold text-on-surface line-clamp-1">{p.name}</p>
                    <p className="text-[10px] text-on-surface-variant">{p.brand} • {p.category?.name}</p>
                    <p className="text-xs font-black text-primary mt-1">{Number(p.price).toLocaleString()}đ</p>
                  </Link>
                ))}
              </div>
            )}
          </div>

          <div className="p-3 border-t border-outline-variant/20 space-y-3">
            <div className="flex flex-wrap gap-1.5">
              {quickPrompts.map(prompt => (
                <button key={prompt} onClick={() => sendMessage(prompt)} className="text-[10px] px-2 py-1 rounded-full bg-primary/10 text-primary border border-primary/20 hover:bg-primary/20">
                  {prompt}
                </button>
              ))}
            </div>
            <form onSubmit={(e) => { e.preventDefault(); sendMessage(); }} className="flex gap-2">
              <input value={message} onChange={e => setMessage(e.target.value)} placeholder="Nhập nhu cầu của bạn..." className="flex-1 bg-surface-container-low border border-outline-variant/30 rounded-2xl px-3 py-2 text-xs outline-none focus:border-primary" />
              <button type="submit" disabled={loading} className="w-10 h-10 bg-primary text-white rounded-2xl flex items-center justify-center disabled:opacity-50"><Send size={16} /></button>
            </form>
          </div>
        </div>
      )}
      <button onClick={() => setOpen(prev => !prev)} className="w-14 h-14 bg-primary text-white rounded-full shadow-2xl shadow-primary/30 flex items-center justify-center hover:bg-primary-container active:scale-95 transition-all">
        {open ? <X size={24} /> : <MessageCircle size={24} />}
      </button>
    </div>
  );
}
