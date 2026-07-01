import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import api from '../api/axios';
import {
  ArrowLeft, MapPin, Briefcase, Clock, DollarSign, ChevronRight,
  Users, Sparkles, Mail, Building2, Calendar
} from 'lucide-react';

const DEPT_COLORS = {
  'Kinh Doanh': 'bg-blue-500/10 text-blue-400 border-blue-500/20',
  'Công Nghệ': 'bg-violet-500/10 text-violet-400 border-violet-500/20',
  'Marketing': 'bg-pink-500/10 text-pink-400 border-pink-500/20',
  'Vận Hành': 'bg-emerald-500/10 text-emerald-400 border-emerald-500/20',
};

function JobCard({ job, expanded, onToggle }) {
  const deptClass = DEPT_COLORS[job.department] || 'bg-primary/10 text-primary border-primary/20';

  return (
    <article className="bg-surface-container-lowest border border-outline-variant/30 rounded-3xl overflow-hidden hover:border-primary/30 transition-all duration-300">
      <button
        onClick={onToggle}
        className="w-full text-left p-6 md:p-8 space-y-4"
      >
        <div className="flex flex-wrap items-start justify-between gap-4">
          <div className="space-y-3 flex-1">
            <span className={`inline-flex items-center gap-1.5 text-xs font-bold px-3 py-1 rounded-full border ${deptClass}`}>
              <Building2 size={12} />
              {job.department}
            </span>
            <h2 className="text-xl md:text-2xl font-black text-on-surface">{job.title}</h2>
            <div className="flex flex-wrap gap-4 text-sm text-on-surface-variant">
              <span className="flex items-center gap-1.5"><MapPin size={14} className="text-primary" />{job.location}</span>
              <span className="flex items-center gap-1.5"><Briefcase size={14} className="text-primary" />{job.employmentType}</span>
              <span className="flex items-center gap-1.5"><DollarSign size={14} className="text-primary" />{job.salaryRange}</span>
              <span className="flex items-center gap-1.5"><Calendar size={14} className="text-primary" />
                {new Date(job.postedAt).toLocaleDateString('vi-VN')}
              </span>
            </div>
          </div>
          <ChevronRight
            size={20}
            className={`text-on-surface-variant shrink-0 transition-transform duration-300 ${expanded ? 'rotate-90' : ''}`}
          />
        </div>
      </button>

      {expanded && (
        <div className="px-6 md:px-8 pb-8 space-y-6 border-t border-outline-variant/20 pt-6 animate-in fade-in slide-in-from-top-2 duration-300">
          <div>
            <h3 className="text-sm font-bold text-on-surface uppercase tracking-wider mb-2">Mô tả công việc</h3>
            <p className="text-sm text-on-surface-variant leading-relaxed whitespace-pre-line">{job.description}</p>
          </div>
          <div>
            <h3 className="text-sm font-bold text-on-surface uppercase tracking-wider mb-2">Yêu cầu</h3>
            <p className="text-sm text-on-surface-variant leading-relaxed whitespace-pre-line">{job.requirements}</p>
          </div>
          <div>
            <h3 className="text-sm font-bold text-on-surface uppercase tracking-wider mb-2">Quyền lợi</h3>
            <p className="text-sm text-on-surface-variant leading-relaxed whitespace-pre-line">{job.benefits}</p>
          </div>
          <a
            href={`mailto:hr@iluminaty.com?subject=Ứng tuyển: ${encodeURIComponent(job.title)}`}
            className="inline-flex items-center gap-2 bg-primary text-white px-6 py-3 rounded-2xl text-sm font-bold hover:bg-primary-container transition-all shadow-lg shadow-primary/20"
          >
            <Mail size={16} />
            Ứng tuyển ngay
          </a>
        </div>
      )}
    </article>
  );
}

export default function Recruitment() {
  const [jobs, setJobs] = useState([]);
  const [loading, setLoading] = useState(true);
  const [expandedId, setExpandedId] = useState(null);

  useEffect(() => {
    window.scrollTo(0, 0);
    api.get('/jobs')
      .then(res => setJobs(res.data.data || []))
      .catch(() => setJobs([]))
      .finally(() => setLoading(false));
  }, []);

  return (
    <div className="min-h-screen bg-background text-on-surface font-sans">
      <nav className="sticky top-0 z-40 bg-surface/90 backdrop-blur-md border-b border-outline-variant/30 py-4 px-4 md:px-12">
        <div className="max-w-[1440px] mx-auto flex items-center justify-between">
          <Link to="/" className="flex items-center gap-2 text-sm text-on-surface-variant hover:text-primary transition-colors">
            <ArrowLeft size={16} />
            Trang chủ
          </Link>
          <Link to="/" className="font-bold text-lg text-primary">iLuminaty Shop</Link>
        </div>
      </nav>

      {/* Hero */}
      <header className="relative overflow-hidden bg-gradient-to-br from-primary/10 via-background to-violet-500/5 border-b border-outline-variant/20">
        <div className="max-w-[1440px] mx-auto px-4 md:px-12 py-16 md:py-24">
          <div className="max-w-2xl space-y-6">
            <div className="inline-flex items-center gap-2 bg-primary/10 border border-primary/20 text-primary px-4 py-1.5 rounded-full text-xs font-bold uppercase tracking-wider">
              <Sparkles size={14} />
              Cơ hội nghề nghiệp
            </div>
            <h1 className="text-4xl md:text-5xl font-black text-on-surface tracking-tight">
              Tuyển Dụng tại iLuminaty Shop
            </h1>
            <p className="text-on-surface-variant text-lg leading-relaxed">
              Gia nhập đội ngũ đam mê công nghệ. Chúng tôi đang tìm kiếm những tài năng xuất sắc
              để cùng xây dựng trải nghiệm mua sắm điện tử hàng đầu Việt Nam.
            </p>
            <div className="flex flex-wrap gap-6 pt-2">
              {[
                { icon: Users, label: '50+ nhân viên' },
                { icon: MapPin, label: 'TP. Hồ Chí Minh' },
                { icon: Clock, label: 'Làm việc linh hoạt' },
              ].map((item, i) => (
                <div key={i} className="flex items-center gap-2 text-sm text-on-surface-variant">
                  <item.icon size={16} className="text-primary" />
                  {item.label}
                </div>
              ))}
            </div>
          </div>
        </div>
      </header>

      <main className="max-w-[1440px] mx-auto px-4 md:px-12 py-12 md:py-16">
        <div className="flex items-center justify-between mb-8">
          <h2 className="text-2xl font-black text-on-surface">
            Vị trí đang tuyển
            {!loading && <span className="text-primary ml-2">({jobs.length})</span>}
          </h2>
        </div>

        {loading ? (
          <div className="space-y-4">
            {[1, 2, 3].map(i => (
              <div key={i} className="h-40 bg-surface-container-low rounded-3xl animate-pulse" />
            ))}
          </div>
        ) : jobs.length === 0 ? (
          <div className="text-center py-20 bg-surface-container-low rounded-3xl border border-outline-variant/30">
            <Briefcase size={48} className="mx-auto text-on-surface-variant opacity-30 mb-4" />
            <p className="text-on-surface-variant">Hiện chưa có vị trí tuyển dụng. Vui lòng quay lại sau.</p>
          </div>
        ) : (
          <div className="space-y-4">
            {jobs.map(job => (
              <JobCard
                key={job.id}
                job={job}
                expanded={expandedId === job.id}
                onToggle={() => setExpandedId(expandedId === job.id ? null : job.id)}
              />
            ))}
          </div>
        )}

        {/* CTA */}
        <section className="mt-16 bg-surface-container-low border border-outline-variant/30 rounded-3xl p-8 md:p-12 text-center space-y-4">
          <h3 className="text-xl font-black text-on-surface">Không thấy vị trí phù hợp?</h3>
          <p className="text-sm text-on-surface-variant max-w-md mx-auto">
            Gửi CV của bạn về hr@iluminaty.com — chúng tôi luôn tìm kiếm những tài năng tiềm năng.
          </p>
          <a
            href="mailto:hr@iluminaty.com"
            className="inline-flex items-center gap-2 border border-primary/40 text-primary px-6 py-3 rounded-2xl text-sm font-semibold hover:bg-primary/5 transition-all"
          >
            <Mail size={16} />
            Gửi hồ sơ tự do
          </a>
        </section>
      </main>
    </div>
  );
}
