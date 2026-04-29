import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5092/api', // Đảm bảo đúng Port của Backend bạn
});

// Tự động đính kèm Token vào Header mỗi khi gọi API
api.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default api;