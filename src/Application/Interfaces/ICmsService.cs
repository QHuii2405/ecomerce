using Application.DTOs;

namespace Application.Interfaces;

public interface ICmsService
{
    // Banners
    Task<List<BannerDto>> GetBannersAsync(bool onlyActive = true);
    Task<BannerDto> CreateBannerAsync(CreateBannerRequest request);
    Task<BannerDto> UpdateBannerAsync(Guid id, CreateBannerRequest request);
    Task DeleteBannerAsync(Guid id);

    // BlogPosts
    Task<List<BlogPostDto>> GetBlogPostsAsync(bool onlyPublished = true);
    Task<BlogPostDto> GetBlogPostBySlugAsync(string slug);
    Task<BlogPostDto> GetBlogPostByIdAsync(Guid id);
    Task<BlogPostDto> CreateBlogPostAsync(CreateBlogPostRequest request, Guid authorId);
    Task<BlogPostDto> UpdateBlogPostAsync(Guid id, CreateBlogPostRequest request);
    Task DeleteBlogPostAsync(Guid id);
}
