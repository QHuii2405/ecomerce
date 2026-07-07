using Domain.Entities;
using Infrastructure.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class JobPostingConfiguration : IEntityTypeConfiguration<JobPosting>
{
    public void Configure(EntityTypeBuilder<JobPosting> builder)
    {
        builder.HasData(JobPostingSeedData.GetJobPostings());
    }
}

public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
{
    public void Configure(EntityTypeBuilder<ProductReview> builder)
    {
        builder.HasIndex(r => new { r.ProductId, r.UserId }).IsUnique();

        builder.HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId);

        builder.HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(r => r.Order)
            .WithMany(o => o.Reviews)
            .HasForeignKey(r => r.OrderId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasOne(i => i.ProductVariant)
            .WithMany()
            .HasForeignKey(i => i.ProductVariantId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

public class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
{
    public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
    {
        builder.HasOne(p => p.Order)
            .WithMany(o => o.PaymentTransactions)
            .HasForeignKey(p => p.OrderId);
    }
}

public class GoodsReceiptConfiguration : IEntityTypeConfiguration<GoodsReceipt>
{
    public void Configure(EntityTypeBuilder<GoodsReceipt> builder)
    {
        builder.HasOne(r => r.CreatedByUser)
            .WithMany()
            .HasForeignKey(r => r.CreatedByUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(r => r.ApprovedByUser)
            .WithMany()
            .HasForeignKey(r => r.ApprovedByUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(r => r.TotalAmount)
            .HasColumnType("decimal(18,2)");
    }
}

public class GoodsReceiptDetailConfiguration : IEntityTypeConfiguration<GoodsReceiptDetail>
{
    public void Configure(EntityTypeBuilder<GoodsReceiptDetail> builder)
    {
        builder.HasOne(d => d.GoodsReceipt)
            .WithMany(r => r.Details)
            .HasForeignKey(d => d.GoodsReceiptId);

        builder.Property(d => d.ImportPrice)
            .HasColumnType("decimal(18,2)");
    }
}

public class ReturnRequestConfiguration : IEntityTypeConfiguration<ReturnRequest>
{
    public void Configure(EntityTypeBuilder<ReturnRequest> builder)
    {
        builder.HasOne(r => r.Order)
            .WithMany()
            .HasForeignKey(r => r.OrderId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

public class WishlistItemConfiguration : IEntityTypeConfiguration<WishlistItem>
{
    public void Configure(EntityTypeBuilder<WishlistItem> builder)
    {
        builder.HasIndex(w => new { w.UserId, w.ProductId }).IsUnique();
    }
}

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne(o => o.Voucher)
            .WithMany()
            .HasForeignKey(o => o.VoucherId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.HasIndex(v => v.Code).IsUnique();
        
        builder.Property(v => v.DiscountValue).HasColumnType("decimal(18,2)");
        builder.Property(v => v.MinOrderValue).HasColumnType("decimal(18,2)");
        builder.Property(v => v.MaxDiscountValue).HasColumnType("decimal(18,2)");
    }
}
