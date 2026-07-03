using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class CmsService : ICmsService
{
    private readonly IUnitOfWork _unitOfWork;

    public CmsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<BannerDto>> GetBannersAsync(bool onlyActive = true)
    {
        var banners = await _unitOfWork.Repository<Banner>().GetAllAsync();
        if (onlyActive)
            banners = banners.Where(b => b.IsActive);

        return banners.OrderBy(b => b.DisplayOrder).Select(b => new BannerDto
        {
            Id = b.Id,
            Title = b.Title,
            ImageUrl = b.ImageUrl,
            TargetUrl = b.TargetUrl,
            IsActive = b.IsActive,
            DisplayOrder = b.DisplayOrder,
            CreatedAt = b.CreatedAt
        }).ToList();
    }

    public async Task<BannerDto> CreateBannerAsync(CreateBannerRequest request)
    {
        var banner = new Banner
        {
            Title = request.Title,
            ImageUrl = request.ImageUrl,
            TargetUrl = request.TargetUrl,
            IsActive = request.IsActive,
            DisplayOrder = request.DisplayOrder,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Repository<Banner>().AddAsync(banner);
        await _unitOfWork.SaveChangesAsync();

        return new BannerDto
        {
            Id = banner.Id,
            Title = banner.Title,
            ImageUrl = banner.ImageUrl,
            TargetUrl = banner.TargetUrl,
            IsActive = banner.IsActive,
            DisplayOrder = banner.DisplayOrder,
            CreatedAt = banner.CreatedAt
        };
    }

    public async Task<BannerDto> UpdateBannerAsync(Guid id, CreateBannerRequest request)
    {
        var banner = await _unitOfWork.Repository<Banner>().GetByIdAsync(id);
        if (banner == null) throw new Exception("Banner not found");

        banner.Title = request.Title;
        banner.ImageUrl = request.ImageUrl;
        banner.TargetUrl = request.TargetUrl;
        banner.IsActive = request.IsActive;
        banner.DisplayOrder = request.DisplayOrder;
        banner.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Repository<Banner>().Update(banner);
        await _unitOfWork.SaveChangesAsync();

        return new BannerDto
        {
            Id = banner.Id,
            Title = banner.Title,
            ImageUrl = banner.ImageUrl,
            TargetUrl = banner.TargetUrl,
            IsActive = banner.IsActive,
            DisplayOrder = banner.DisplayOrder,
            CreatedAt = banner.CreatedAt
        };
    }

    public async Task DeleteBannerAsync(Guid id)
    {
        var banner = await _unitOfWork.Repository<Banner>().GetByIdAsync(id);
        if (banner != null)
        {
            _unitOfWork.Repository<Banner>().Delete(banner);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<List<BlogPostDto>> GetBlogPostsAsync(bool onlyPublished = true)
    {
        var posts = await _unitOfWork.Repository<BlogPost>().GetAllAsync(includeProperties: "Author");
        if (onlyPublished)
            posts = posts.Where(b => b.IsPublished);

        return posts.OrderByDescending(b => b.CreatedAt).Select(b => new BlogPostDto
        {
            Id = b.Id,
            Title = b.Title,
            Slug = b.Slug,
            Content = b.Content,
            ThumbnailUrl = b.ThumbnailUrl,
            AuthorId = b.AuthorId,
            AuthorName = b.Author?.FullName ?? "Admin",
            IsPublished = b.IsPublished,
            CreatedAt = b.CreatedAt
        }).ToList();
    }

    public async Task<BlogPostDto> GetBlogPostBySlugAsync(string slug)
    {
        var blogs = await _unitOfWork.Repository<BlogPost>().FindAsync(x => x.Slug == slug, includeProperties: "Author");
        var b = blogs.FirstOrDefault();
        if (b == null) throw new Exception("Blog not found");

        return new BlogPostDto
        {
            Id = b.Id,
            Title = b.Title,
            Slug = b.Slug,
            Content = b.Content,
            ThumbnailUrl = b.ThumbnailUrl,
            AuthorId = b.AuthorId,
            AuthorName = b.Author?.FullName ?? "Admin",
            IsPublished = b.IsPublished,
            CreatedAt = b.CreatedAt
        };
    }

    public async Task<BlogPostDto> GetBlogPostByIdAsync(Guid id)
    {
        var blogs = await _unitOfWork.Repository<BlogPost>().FindAsync(x => x.Id == id, includeProperties: "Author");
        var b = blogs.FirstOrDefault();
        if (b == null) throw new Exception("Blog not found");

        return new BlogPostDto
        {
            Id = b.Id,
            Title = b.Title,
            Slug = b.Slug,
            Content = b.Content,
            ThumbnailUrl = b.ThumbnailUrl,
            AuthorId = b.AuthorId,
            AuthorName = b.Author?.FullName ?? "Admin",
            IsPublished = b.IsPublished,
            CreatedAt = b.CreatedAt
        };
    }

    public async Task<BlogPostDto> CreateBlogPostAsync(CreateBlogPostRequest request, Guid authorId)
    {
        var blog = new BlogPost
        {
            Title = request.Title,
            Slug = request.Slug,
            Content = request.Content,
            ThumbnailUrl = request.ThumbnailUrl,
            AuthorId = authorId,
            IsPublished = request.IsPublished,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Repository<BlogPost>().AddAsync(blog);
        await _unitOfWork.SaveChangesAsync();

        var author = await _unitOfWork.Users.GetByIdAsync(authorId);

        return new BlogPostDto
        {
            Id = blog.Id,
            Title = blog.Title,
            Slug = blog.Slug,
            Content = blog.Content,
            ThumbnailUrl = blog.ThumbnailUrl,
            AuthorId = blog.AuthorId,
            AuthorName = author?.FullName ?? "Admin",
            IsPublished = blog.IsPublished,
            CreatedAt = blog.CreatedAt
        };
    }

    public async Task<BlogPostDto> UpdateBlogPostAsync(Guid id, CreateBlogPostRequest request)
    {
        var blogs = await _unitOfWork.Repository<BlogPost>().FindAsync(x => x.Id == id, includeProperties: "Author");
        var blog = blogs.FirstOrDefault();
        if (blog == null) throw new Exception("Blog not found");

        blog.Title = request.Title;
        blog.Slug = request.Slug;
        blog.Content = request.Content;
        blog.ThumbnailUrl = request.ThumbnailUrl;
        blog.IsPublished = request.IsPublished;
        blog.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Repository<BlogPost>().Update(blog);
        await _unitOfWork.SaveChangesAsync();

        return new BlogPostDto
        {
            Id = blog.Id,
            Title = blog.Title,
            Slug = blog.Slug,
            Content = blog.Content,
            ThumbnailUrl = blog.ThumbnailUrl,
            AuthorId = blog.AuthorId,
            AuthorName = blog.Author?.FullName ?? "Admin",
            IsPublished = blog.IsPublished,
            CreatedAt = blog.CreatedAt
        };
    }

    public async Task DeleteBlogPostAsync(Guid id)
    {
        var blog = await _unitOfWork.Repository<BlogPost>().GetByIdAsync(id);
        if (blog != null)
        {
            _unitOfWork.Repository<BlogPost>().Delete(blog);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
