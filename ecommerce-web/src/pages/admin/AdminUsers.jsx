import Swal from "sweetalert2";
import React, { useEffect, useState } from 'react';
import api from '../../api/axios';
import {
  Users, RefreshCw, AlertCircle, Search,
  Shield, User, Briefcase, ChevronDown
} from 'lucide-react';

const ROLES = ['Customer', 'Staff', 'Admin'];

const ROLE_CONFIG = {
  Admin:    { label: 'Admin',    cls: 'text-purple-400 bg-purple-500/10 border-purple-500/20', icon: Shield },
  Staff:    { label: 'Staff',    cls: 'text-blue-400 bg-blue-500/10 border-blue-500/20',       icon: Briefcase },
  Customer: { label: 'Customer', cls: 'text-emerald-400 bg-emerald-500/10 border-emerald-500/20', icon: User },
};

function RoleBadge({ role }) {
  const c = ROLE_CONFIG[role] || ROLE_CONFIG.Customer;
  const Icon = c.icon;
  return (
    <span className={`inline-flex items-center gap-1 text-[10px] font-bold uppercase tracking-wider px-2.5 py-1 rounded-full border ${c.cls}`}>
      <Icon size={10} />{c.label}
    </span>
  );
}

function UserRow({ user, onRoleChange }) {
  const [updating, setUpdating] = useState(false);
  const [dropdownOpen, setDropdownOpen] = useState(false);

  const handleRoleChange = async (newRole) => {
    if (newRole === user.role) { setDropdownOpen(false); return; }
    if (!(await Swal.fire({
      title: "X�c nh?n",
      text: `Đổi quyền của "${user.fullName}" thành ${newRole}?`,
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "�?ng �",
      cancelButtonText: "H?y"
    })).isConfirmed) return;
    setUpdating(true);
    setDropdownOpen(false);
    try {
      await api.post('/auth/update-role', { email: user.email, newRole });
      onRoleChange(user.id, newRole);
    } catch (err) {
      Swal.fire({
        icon: "info",
        text: 'Lỗi: ' + (err.response?.data?.message || 'Không thể cập nhật quyền')
      });
    } finally {
      setUpdating(false);
    }
  };

  const formattedDate = new Date(user.createdAt).toLocaleDateString('vi-VN', {
    day: '2-digit', month: '2-digit', year: 'numeric'
  });

  const avatarLetter = user.fullName?.[0]?.toUpperCase() || 'U';
  const roleConfig = ROLE_CONFIG[user.role] || ROLE_CONFIG.Customer;

  return (
    <div className="grid grid-cols-12 gap-4 items-center px-5 py-4 hover:bg-white/5 transition-colors">
      {/* User info */}
      <div className="col-span-5 flex items-center gap-3 min-w-0">
        <div className={`w-9 h-9 rounded-full flex items-center justify-center text-sm font-bold flex-shrink-0 ${
          user.role === 'Admin' ? 'bg-purple-50 text-purple-600' :
          user.role === 'Staff' ? 'bg-blue-50 text-blue-600' :
          'bg-emerald-50 text-emerald-600'
        }`}>
          {avatarLetter}
        </div>
        <div className="min-w-0">
          <p className="text-sm font-semibold text-on-surface truncate">{user.fullName}</p>
          <p className="text-[10px] text-on-surface-variant truncate">{user.email}</p>
        </div>
      </div>

      {/* Phone */}
      <div className="col-span-2">
        <p className="text-xs text-on-surface-variant">{user.phoneNumber || '—'}</p>
      </div>

      {/* Role */}
      <div className="col-span-2">
        <RoleBadge role={user.role} />
      </div>

      {/* Joined */}
      <div className="col-span-2">
        <p className="text-xs text-on-surface-variant">{formattedDate}</p>
      </div>

      {/* Change Role */}
      <div className="col-span-1 relative">
        <button
          onClick={() => setDropdownOpen(!dropdownOpen)}
          disabled={updating}
          className="flex items-center gap-1 px-2.5 py-1.5 bg-surface-container-lowest border border-outline-variant/30 text-on-surface-variant rounded-xl text-xs hover:bg-surface-container-low hover:text-on-surface transition-all disabled:opacity-50"
        >
          {updating ? (
            <div className="animate-spin rounded-full h-3 w-3 border-b-2 border-on-surface-variant" />
          ) : (
            <>Phân quyền <ChevronDown size={10} /></>
          )}
        </button>

        {dropdownOpen && (
          <div className="absolute right-0 top-8 z-20 bg-surface border border-outline-variant/30 rounded-xl shadow-xl overflow-hidden w-32">
            {ROLES.map(role => (
              <button
                key={role}
                onClick={() => handleRoleChange(role)}
                className={`w-full flex items-center gap-2 px-3 py-2 text-xs hover:bg-surface-container-low transition-colors ${
                  user.role === role ? 'text-primary bg-primary/10' : 'text-on-surface'
                }`}
              >
                {React.createElement(ROLE_CONFIG[role].icon, { size: 12 })}
                {role}
                {user.role === role && ' ✓'}
              </button>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}

export default function AdminUsers() {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [search, setSearch] = useState('');
  const [roleFilter, setRoleFilter] = useState('All');

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    setLoading(true);
    setError(null);
    try {
      const res = await api.get('/auth/users');
      setUsers(res.data || []);
    } catch (err) {
      setError('Chỉ Admin mới có quyền xem trang này. ' + (err.response?.data?.message || ''));
    } finally {
      setLoading(false);
    }
  };

  const handleRoleChange = (userId, newRole) => {
    setUsers(prev => prev.map(u => u.id === userId ? { ...u, role: newRole } : u));
  };

  const filteredUsers = users.filter(u => {
    const matchRole = roleFilter === 'All' || u.role === roleFilter;
    const matchSearch = !search ||
      u.fullName?.toLowerCase().includes(search.toLowerCase()) ||
      u.email?.toLowerCase().includes(search.toLowerCase());
    return matchRole && matchSearch;
  });

  const countByRole = (role) => users.filter(u => role === 'All' ? true : u.role === role).length;

  return (
    <div className="space-y-6">
      {/* Header */}
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <div>
          <h1 className="text-3xl font-bold text-on-surface tracking-tight">Quản Lý Nhân Sự</h1>
          <p className="text-on-surface-variant mt-1">Xem danh sách người dùng và phân quyền hệ thống (chỉ Admin).</p>
        </div>
        <button onClick={fetchUsers} className="flex items-center gap-2 px-4 py-2.5 bg-surface border border-outline-variant/30 text-on-surface-variant rounded-xl text-sm hover:bg-surface-container-low transition-all self-start">
          <RefreshCw size={14} className={loading ? 'animate-spin' : ''} />
          Làm mới
        </button>
      </div>

      {/* Stats */}
      <div className="grid grid-cols-4 gap-4">
        {['All', 'Admin', 'Staff', 'Customer'].map(role => {
          const config = role === 'All' ? null : ROLE_CONFIG[role];
          const Icon = config?.icon || Users;
          return (
            <button
              key={role}
              onClick={() => setRoleFilter(role)}
              className={`p-4 rounded-2xl border text-left transition-all ${
                roleFilter === role
                  ? 'bg-primary/10 border-primary/30'
                  : 'bg-surface border-outline-variant/30 hover:border-primary/20'
              }`}
            >
              <div className="flex items-center justify-between mb-2">
                <Icon size={16} className={roleFilter === role ? 'text-primary' : 'text-outline'} />
                <span className="text-xl font-extrabold text-on-surface">{countByRole(role)}</span>
              </div>
              <p className="text-[10px] text-outline font-semibold uppercase tracking-wider">{role === 'All' ? 'Tổng cộng' : role}</p>
            </button>
          );
        })}
      </div>

      {/* Search */}
      <div className="relative max-w-sm">
        <Search size={14} className="absolute left-4 top-1/2 -translate-y-1/2 text-outline" />
        <input
          className="w-full bg-surface border border-outline-variant/30 text-on-surface text-sm rounded-xl pl-10 pr-4 py-2.5 focus:outline-none focus:border-primary transition-all placeholder:text-outline"
          placeholder="Tìm theo tên hoặc email..."
          value={search}
          onChange={e => setSearch(e.target.value)}
        />
      </div>

      {/* Error */}
      {error && (
        <div className="flex items-center gap-3 p-4 bg-rose-500/10 border border-rose-500/20 rounded-2xl text-rose-400">
          <AlertCircle size={18} /><span className="text-sm">{error}</span>
        </div>
      )}

      {/* Users Table */}
      {loading ? (
        <div className="space-y-3">
          {[1, 2, 3, 4].map(i => (
            <div key={i} className="backdrop-blur-md bg-surface border border-outline-variant/30 rounded-2xl p-4 animate-pulse">
              <div className="flex items-center gap-4">
                <div className="w-9 h-9 bg-surface-container rounded-full flex-shrink-0" />
                <div className="flex-1 space-y-1.5">
                  <div className="h-3 bg-surface-container rounded w-36" />
                  <div className="h-2 bg-surface-container rounded w-48" />
                </div>
              </div>
            </div>
          ))}
        </div>
      ) : filteredUsers.length === 0 ? (
        <div className="backdrop-blur-md bg-surface border border-outline-variant/30 p-12 rounded-3xl text-center space-y-4 shadow-sm">
          <div className="h-16 w-16 bg-surface-container border border-outline-variant/20 text-outline rounded-3xl flex items-center justify-center mx-auto">
            <Users size={28} />
          </div>
          <h3 className="text-lg font-bold text-on-surface">Không tìm thấy người dùng</h3>
        </div>
      ) : (
        <div className="backdrop-blur-md bg-surface border border-outline-variant/30 rounded-2xl overflow-hidden shadow-sm">
          {/* Table Header */}
          <div className="grid grid-cols-12 gap-4 px-5 py-3 border-b border-outline-variant/30 text-[10px] font-bold uppercase tracking-widest text-outline bg-surface-container-lowest">
            <div className="col-span-5">Người dùng</div>
            <div className="col-span-2">Điện thoại</div>
            <div className="col-span-2">Vai trò</div>
            <div className="col-span-2">Ngày tạo</div>
            <div className="col-span-1 text-center">Hành động</div>
          </div>
          <div className="divide-y divide-outline-variant/30">
            {filteredUsers.map(user => (
              <UserRow key={user.id} user={user} onRoleChange={handleRoleChange} />
            ))}
          </div>
          <div className="px-5 py-3 border-t border-outline-variant/30 text-center bg-surface-container-lowest">
            <span className="text-xs text-outline">Hiển thị {filteredUsers.length} / {users.length} người dùng</span>
          </div>
        </div>
      )}
    </div>
  );
}
