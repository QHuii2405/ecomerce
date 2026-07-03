import React, { useState, useEffect } from "react";
import api from "../../api/axios";
import { Package, Check, X, Eye, FileImage } from "lucide-react";
import Swal from "sweetalert2";

export default function AdminReturns() {
    const [returns, setReturns] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchReturns();
    }, []);

    const fetchReturns = async () => {
        try {
            setLoading(true);
            const res = await api.get("/Return");
            setReturns(res.data.data);
        } catch (error) {
            console.error(error);
            Swal.fire("Lỗi", "Không thể lấy danh sách hoàn trả", "error");
        } finally {
            setLoading(false);
        }
    };

    const handleProcess = async (id, isApprove) => {
        const { value: note } = await Swal.fire({
            title: isApprove ? "Duyệt yêu cầu hoàn trả" : "Từ chối yêu cầu",
            input: "textarea",
            inputLabel: "Ghi chú (Tùy chọn)",
            inputPlaceholder: "Nhập ghi chú cho khách hàng...",
            showCancelButton: true,
            confirmButtonText: "Xác nhận",
            cancelButtonText: "Hủy"
        });

        if (note !== undefined) {
            try {
                await api.post(`/Return/${id}/process`, {
                    approve: isApprove,
                    adminNote: note
                });
                Swal.fire("Thành công", "Đã xử lý yêu cầu", "success");
                fetchReturns();
            } catch (error) {
                Swal.fire("Lỗi", error.response?.data?.message || "Không thể xử lý yêu cầu", "error");
            }
        }
    };

    const handleViewImages = (imageUrls) => {
        if (!imageUrls || imageUrls.length === 0) return;
        const html = `<div style="display:flex; flex-wrap:wrap; gap:10px; justify-content:center;">
            ${imageUrls.map(url => `<img src="http://localhost:5092${url}" style="max-width:100px; max-height:100px; object-fit:cover; border-radius:8px;" />`).join("")}
        </div>`;
        Swal.fire({
            title: "Ảnh đính kèm",
            html: html,
            width: 600
        });
    };

    return (
        <div className="space-y-6">
            <div className="flex justify-between items-center">
                <h1 className="text-2xl font-bold flex items-center gap-2">
                    <Package className="text-primary" /> Quản Lý Hoàn Trả
                </h1>
            </div>

            <div className="bg-surface rounded-2xl border border-outline-variant/30 overflow-hidden shadow-sm">
                <div className="overflow-x-auto">
                    <table className="w-full text-sm text-left">
                        <thead className="bg-surface-container-low text-on-surface-variant font-medium border-b border-outline-variant/30">
                            <tr>
                                <th className="px-4 py-3">Mã Yêu Cầu</th>
                                <th className="px-4 py-3">Mã Đơn Hàng</th>
                                <th className="px-4 py-3">Lý Do</th>
                                <th className="px-4 py-3">Minh Chứng</th>
                                <th className="px-4 py-3">Trạng Thái</th>
                                <th className="px-4 py-3">Ngày Yêu Cầu</th>
                                <th className="px-4 py-3">Thao Tác</th>
                            </tr>
                        </thead>
                        <tbody className="divide-y divide-outline-variant/20">
                            {loading ? (
                                <tr>
                                    <td colSpan="7" className="px-4 py-8 text-center text-on-surface-variant">
                                        Đang tải dữ liệu...
                                    </td>
                                </tr>
                            ) : returns.length === 0 ? (
                                <tr>
                                    <td colSpan="7" className="px-4 py-8 text-center text-on-surface-variant">
                                        Không có yêu cầu hoàn trả nào.
                                    </td>
                                </tr>
                            ) : (
                                returns.map((req) => (
                                    <tr key={req.id} className="hover:bg-surface-container-lowest transition-colors">
                                        <td className="px-4 py-3 font-medium text-xs">...{req.id.substring(req.id.length - 8)}</td>
                                        <td className="px-4 py-3 font-medium text-xs">...{req.orderId.substring(req.orderId.length - 8)}</td>
                                        <td className="px-4 py-3 max-w-[200px] truncate" title={req.reason}>{req.reason}</td>
                                        <td className="px-4 py-3">
                                            {req.imageUrls && req.imageUrls.length > 0 ? (
                                                <button onClick={() => handleViewImages(req.imageUrls)} className="text-primary hover:underline flex items-center gap-1 text-xs">
                                                    <FileImage size={14} /> Xem ảnh ({req.imageUrls.length})
                                                </button>
                                            ) : (
                                                <span className="text-xs text-on-surface-variant">Không có</span>
                                            )}
                                        </td>
                                        <td className="px-4 py-3">
                                            <span className={`px-2 py-1 rounded-full text-[10px] font-bold ${
                                                req.status === 'Pending' ? 'bg-amber-100 text-amber-700' :
                                                req.status === 'Approved' ? 'bg-blue-100 text-blue-700' :
                                                req.status === 'Refunded' ? 'bg-emerald-100 text-emerald-700' :
                                                'bg-rose-100 text-rose-700'
                                            }`}>
                                                {req.status === 'Pending' ? 'Chờ xử lý' : req.status === 'Approved' ? 'Đã duyệt' : req.status === 'Refunded' ? 'Đã hoàn tiền' : 'Đã từ chối'}
                                            </span>
                                        </td>
                                        <td className="px-4 py-3 text-xs">{new Date(req.createdAt).toLocaleDateString("vi-VN")}</td>
                                        <td className="px-4 py-3">
                                            {req.status === 'Pending' && (
                                                <div className="flex gap-2">
                                                    <button 
                                                        onClick={() => handleProcess(req.id, true)}
                                                        className="p-1.5 bg-emerald-500/10 text-emerald-600 rounded-lg hover:bg-emerald-500/20"
                                                        title="Duyệt"
                                                    >
                                                        <Check size={16} />
                                                    </button>
                                                    <button 
                                                        onClick={() => handleProcess(req.id, false)}
                                                        className="p-1.5 bg-rose-500/10 text-rose-600 rounded-lg hover:bg-rose-500/20"
                                                        title="Từ chối"
                                                    >
                                                        <X size={16} />
                                                    </button>
                                                </div>
                                            )}
                                        </td>
                                    </tr>
                                ))
                            )}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
}
