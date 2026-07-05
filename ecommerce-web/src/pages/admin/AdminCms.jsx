import React, { useState, useEffect } from "react";
import api from "../../api/axios";
import { FolderTree, Plus, Edit, Trash2, Image, FileText } from "lucide-react";
import Swal from "sweetalert2";

export default function AdminCms() {
    const [banners, setBanners] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchBanners();
    }, []);

    const fetchBanners = async () => {
        try {
            setLoading(true);
            const res = await api.get("/Cms/banners?onlyActive=false");
            setBanners(res.data.data);
        } catch (error) {
            console.error(error);
            Swal.fire("Lỗi", "Không thể lấy danh sách banner", "error");
        } finally {
            setLoading(false);
        }
    };

    const handleCreateBanner = async () => {
        const { value: formValues } = await Swal.fire({
            title: "Thêm Banner",
            html:
                '<input id="swal-title" class="swal2-input" placeholder="Tiêu đề">' +
                '<input id="swal-subtitle" class="swal2-input" placeholder="Nội dung mô tả phụ (Subtitle)">' +
                '<input id="swal-image" class="swal2-input" placeholder="URL Hình ảnh (bắt buộc)">' +
                '<input id="swal-target" class="swal2-input" placeholder="URL Đích (tùy chọn)">' +
                '<input id="swal-order" type="number" class="swal2-input" placeholder="Thứ tự hiển thị" value="0">',
            focusConfirm: false,
            showCancelButton: true,
            preConfirm: () => {
                const title = document.getElementById("swal-title").value;
                const image = document.getElementById("swal-image").value;
                if (!title || !image) {
                    Swal.showValidationMessage("Vui lòng nhập tiêu đề và URL hình ảnh");
                    return false;
                }
                return {
                    title: title,
                    subtitle: document.getElementById("swal-subtitle").value,
                    imageUrl: image,
                    targetUrl: document.getElementById("swal-target").value,
                    displayOrder: parseInt(document.getElementById("swal-order").value) || 0,
                    isActive: true
                };
            }
        });

        if (formValues) {
            try {
                await api.post("/Cms/banners", formValues);
                Swal.fire("Thành công", "Đã thêm banner mới", "success");
                fetchBanners();
            } catch (error) {
                Swal.fire("Lỗi", "Không thể thêm banner", "error");
            }
        }
    };

    const handleDeleteBanner = async (id) => {
        const result = await Swal.fire({
            title: "Xác nhận xóa?",
            text: "Banner sẽ bị xóa vĩnh viễn!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Xóa",
            cancelButtonText: "Hủy"
        });

        if (result.isConfirmed) {
            try {
                await api.delete(`/Cms/banners/${id}`);
                Swal.fire("Đã xóa!", "Banner đã bị xóa.", "success");
                fetchBanners();
            } catch (error) {
                Swal.fire("Lỗi", "Không thể xóa banner", "error");
            }
        }
    };

    const toggleBannerActive = async (banner) => {
        try {
            await api.put(`/Cms/banners/${banner.id}`, {
                ...banner,
                isActive: !banner.isActive
            });
            fetchBanners();
        } catch (error) {
            Swal.fire("Lỗi", "Không thể cập nhật trạng thái", "error");
        }
    };

    return (
        <div className="space-y-6">
            <div className="flex justify-between items-center">
                <h1 className="text-2xl font-bold flex items-center gap-2">
                    <FolderTree className="text-primary" /> Quản Lý Nội Dung
                </h1>
            </div>

            {/* Banners Section */}
            <div className="bg-surface rounded-2xl border border-outline-variant/30 overflow-hidden shadow-sm">
                <div className="p-4 border-b border-outline-variant/30 flex justify-between items-center bg-surface-container-lowest">
                    <h2 className="font-bold text-lg flex items-center gap-2">
                        <Image className="text-primary" size={20} /> Banners Trang Chủ
                    </h2>
                    <button 
                        onClick={handleCreateBanner}
                        className="flex items-center gap-2 bg-primary text-white px-4 py-2 rounded-xl text-sm font-semibold hover:bg-primary-container transition-all"
                    >
                        <Plus size={16} /> Thêm Banner
                    </button>
                </div>
                <div className="p-4 grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                    {loading ? (
                        <p className="text-on-surface-variant">Đang tải...</p>
                    ) : banners.length === 0 ? (
                        <p className="text-on-surface-variant">Chưa có banner nào.</p>
                    ) : (
                        banners.map(banner => (
                            <div key={banner.id} className="border border-outline-variant/30 rounded-xl overflow-hidden relative group">
                                <img src={banner.imageUrl.startsWith('http') ? banner.imageUrl : `${import.meta.env.VITE_API_BASE_URL}${banner.imageUrl}`} alt={banner.title} className="w-full h-32 object-cover" />
                                <div className="p-3 bg-surface-container-lowest">
                                    <h3 className="font-bold text-sm truncate">{banner.title}</h3>
                                    {banner.subtitle && <p className="text-xs text-on-surface-variant truncate">{banner.subtitle}</p>}
                                    <div className="flex justify-between items-center mt-2">
                                        <label className="flex items-center gap-2 text-xs cursor-pointer">
                                            <input 
                                                type="checkbox" 
                                                checked={banner.isActive} 
                                                onChange={() => toggleBannerActive(banner)}
                                                className="accent-primary"
                                            />
                                            Hiển thị
                                        </label>
                                        <button 
                                            onClick={() => handleDeleteBanner(banner.id)}
                                            className="text-rose-500 hover:text-rose-600 p-1 bg-rose-500/10 rounded-lg"
                                        >
                                            <Trash2 size={14} />
                                        </button>
                                    </div>
                                </div>
                            </div>
                        ))
                    )}
                </div>
            </div>

            {/* Blogs Section (Placeholder for future) */}
            <div className="bg-surface rounded-2xl border border-outline-variant/30 overflow-hidden shadow-sm p-4">
                <div className="flex justify-between items-center mb-4">
                    <h2 className="font-bold text-lg flex items-center gap-2">
                        <FileText className="text-primary" size={20} /> Quản Lý Blog / Tin tức
                    </h2>
                    <button 
                        onClick={() => Swal.fire("Thông báo", "Tính năng quản lý Blog đang được phát triển.", "info")}
                        className="flex items-center gap-2 bg-surface-container text-on-surface px-4 py-2 rounded-xl text-sm font-semibold hover:bg-surface-container-high transition-all border border-outline-variant/30"
                    >
                        <Plus size={16} /> Viết Bài Mới
                    </button>
                </div>
                <div className="text-center py-8 text-on-surface-variant border-2 border-dashed border-outline-variant/30 rounded-xl">
                    Chưa có bài viết nào.
                </div>
            </div>
        </div>
    );
}
