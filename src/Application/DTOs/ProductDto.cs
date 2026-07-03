namespace Application.DTOs;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public string Brand { get; set; } = string.Empty;
    public List<string> ImageUrls { get; set; } = new();
    public Dictionary<string, string> Attributes { get; set; } = new();
    public CategoryResponse? Category { get; set; }
    public InventoryResponse? Inventory { get; set; }
    public List<ProductVariantResponse> Variants { get; set; } = new();
    public ReviewSummaryResponse ReviewSummary { get; set; } = new();
}

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? ParentId { get; set; }
}

public class InventoryResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int StockQuantity { get; set; }
    public int ReservedQuantity { get; set; }
    public int AvailableQuantity { get; set; }
}

public class ProductVariantResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Dictionary<string, string> Attributes { get; set; } = new();
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int ReservedQuantity { get; set; }
    public int AvailableQuantity { get; set; }
}

public class ReviewSummaryResponse
{
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
}

public class ProductReviewResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string? AdminReply { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ProductName { get; set; } = string.Empty;
}

public class ProductReviewEligibilityResponse
{
    public bool CanReview { get; set; }
    public string Reason { get; set; } = string.Empty;
    public Guid? EligibleOrderId { get; set; }
}

public class CreateProductReviewRequest
{
    public Guid OrderId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}

public class CreateProductVariantRequest
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Dictionary<string, string> Attributes { get; set; } = new();
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public string Brand { get; set; } = "iLuminaty";
    public List<string> ImageUrls { get; set; } = new();
    public Dictionary<string, string> Attributes { get; set; } = new();
    public int InitialStock { get; set; } // Số lượng nhập kho ban đầu
    public List<CreateProductVariantRequest> Variants { get; set; } = new();
}

public class UpdateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public string Brand { get; set; } = "iLuminaty";
    public List<string> ImageUrls { get; set; } = new();
    public Dictionary<string, string> Attributes { get; set; } = new();
    public int StockQuantity { get; set; }
}

public class UpdateProductVariantRequest
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Dictionary<string, string> Attributes { get; set; } = new();
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
