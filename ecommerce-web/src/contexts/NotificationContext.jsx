import React, { createContext, useContext, useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';
import Swal from 'sweetalert2';

const NotificationContext = createContext();

export function NotificationProvider({ children }) {
    const [notifications, setNotifications] = useState([]);
    const [unreadCount, setUnreadCount] = useState(0);

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (!token) return;

        const connection = new signalR.HubConnectionBuilder()
            .withUrl(import.meta.env.VITE_API_BASE_URL + "/hubs/notifications", {
                accessTokenFactory: () => token,
                withCredentials: true
            })
            .withAutomaticReconnect()
            .build();

        connection.on("ReceiveNotification", (notification) => {
            // notification có định dạng { title, message, date }
            setNotifications(prev => [notification, ...prev]);
            setUnreadCount(prev => prev + 1);
            
            // Hiện Toast notification
            Swal.fire({
                toast: true,
                position: 'top-end',
                icon: 'info',
                title: notification.title,
                text: notification.message,
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true
            });
        });

        connection.start()
            .then(() => console.log("Connected to NotificationHub"))
            .catch(err => console.error("Error connecting to NotificationHub:", err));

        return () => {
            connection.stop();
        };
    }, []);

    const markAsRead = () => setUnreadCount(0);

    return (
        <NotificationContext.Provider value={{ notifications, unreadCount, markAsRead }}>
            {children}
        </NotificationContext.Provider>
    );
}

export const useNotification = () => useContext(NotificationContext);
