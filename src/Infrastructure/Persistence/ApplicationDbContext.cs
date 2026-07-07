using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Khai báo các bảng dữ liệu 
    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Inventory> Inventories => Set<Inventory>();
    public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
    public DbSet<ProductReview> ProductReviews => Set<ProductReview>();
    public DbSet<PaymentTransaction> PaymentTransactions => Set<PaymentTransaction>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<JobPosting> JobPostings => Set<JobPosting>();
    public DbSet<GoodsReceipt> GoodsReceipts => Set<GoodsReceipt>();
    public DbSet<GoodsReceiptDetail> GoodsReceiptDetails => Set<GoodsReceiptDetail>();
    public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();
    public DbSet<Voucher> Vouchers => Set<Voucher>();
    
    // CMS
    public DbSet<Banner> Banners => Set<Banner>();
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
    
    // Return & Refund
    public DbSet<ReturnRequest> ReturnRequests => Set<ReturnRequest>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Cấu hình các Entity thông qua IEntityTypeConfiguration<T> (SOLID)
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
