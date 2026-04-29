using Domain.Common;

namespace Domain.Entities;

public class Inventory : BaseEntity
{
    public Guid ProductId { get; set; }

    // Số lượng thực tế trong kho [cite: 95]
    public int StockQuantity { get; set; }

    // Số lượng đang được giữ cho các đơn hàng chưa hoàn tất [cite: 96]
    public int ReservedQuantity { get; set; }

    // Số lượng thực tế có thể bán = Stock - Reserved [cite: 97]
    public int AvailableQuantity => StockQuantity - ReservedQuantity;

    public virtual Product? Product { get; set; }
}