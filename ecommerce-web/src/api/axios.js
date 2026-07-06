import axios from 'axios';

const api = axios.create({
    baseURL: (import.meta.env.VITE_API_BASE_URL || '') + '/api',
});

// Tự động đính kèm Access Token vào Header mỗi khi gửi request
api.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('token');
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => Promise.reject(error)
);

// Interceptor xử lý phản hồi lỗi (Đặc biệt là lỗi 401 do hết hạn Access Token)
api.interceptors.response.use(
    (response) => response,
    async (error) => {
        const originalRequest = error.config;
        
        // Nếu phản hồi trả về mã 401 (Unauthorized) và request này chưa được thử lại
        if (error.response?.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;
            
            const accessToken = localStorage.getItem('token');
            const refreshToken = localStorage.getItem('refreshToken');
            
            // Nếu có đầy đủ cả 2 mã thông báo
            if (accessToken && refreshToken) {
                try {
                    // Gọi API refresh-token qua một instance axios độc lập để tránh lặp vô tận
                    const response = await axios.post((import.meta.env.VITE_API_BASE_URL || '') + '/api/auth/refresh-token', {
                        accessToken: accessToken,
                        refreshToken: refreshToken
                    });
                    
                    const { accessToken: newAccessToken, refreshToken: newRefreshToken } = response.data;
                    
                    // Lưu lại cặp mã thông báo mới vào LocalStorage
                    localStorage.setItem('token', newAccessToken);
                    localStorage.setItem('refreshToken', newRefreshToken);
                    
                    // Cập nhật lại Header Authorization cho request bị lỗi ban đầu và gửi lại
                    originalRequest.headers.Authorization = `Bearer ${newAccessToken}`;
                    return api(originalRequest);
                } catch (refreshError) {
                    // Nếu làm mới token thất bại (ví dụ: Refresh Token hết hạn hoặc bị thu hồi)
                    console.error("Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại.", refreshError);
                    
                    // Xóa sạch thông tin cũ và buộc người dùng đăng nhập lại
                    localStorage.removeItem('token');
                    localStorage.removeItem('refreshToken');
                    localStorage.removeItem('userRole');
                    localStorage.removeItem('userName');
                    
                    // Phát sự kiện logout để App component cập nhật trạng thái nếu cần
                    window.dispatchEvent(new Event('auth-logout'));
                    window.location.href = '/login';
                }
            }
        }
        
        return Promise.reject(error);
    }
);

export default api;