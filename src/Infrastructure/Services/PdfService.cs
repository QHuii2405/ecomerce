using Application.Interfaces;
using Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;

namespace Infrastructure.Services
{
    public class PdfService : IPdfService
    {
        public PdfService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] GenerateInvoicePdf(Order order)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily(Fonts.Arial));

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(x => ComposeContent(x, order));
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Trang ");
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
            });

            return document.GeneratePdf();
        }

        void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text("HÓA ĐƠN BÁN HÀNG").FontSize(20).SemiBold().FontColor(Colors.Blue.Darken2);
                    column.Item().Text("Cửa hàng iLuminaty Shop").FontSize(14);
                    column.Item().Text("Email: hotaro2405@gmail.com");
                    column.Item().Text("Hotline: 0123 456 789");
                });
            });
        }

        void ComposeContent(IContainer container, Order order)
        {
            container.PaddingVertical(1, Unit.Centimetre).Column(column =>
            {
                column.Spacing(20);

                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new AddressComponent("Thông tin khách hàng", order));
                    row.ConstantItem(50);
                    row.RelativeItem().Component(new OrderDetailsComponent(order));
                });

                column.Item().Element(x => ComposeTable(x, order));

                var totalAmount = order.TotalAmount - order.DiscountAmount;
                column.Item().AlignRight().Text($"Tổng thanh toán: {totalAmount:N0} VNĐ").FontSize(14).SemiBold();
            });
        }

        void ComposeTable(IContainer container, Order order)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(30);
                    columns.RelativeColumn(3);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Sản phẩm");
                    header.Cell().Element(CellStyle).AlignRight().Text("Đơn giá");
                    header.Cell().Element(CellStyle).AlignRight().Text("Số lượng");
                    header.Cell().Element(CellStyle).AlignRight().Text("Thành tiền");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                var i = 1;
                foreach (var item in order.OrderItems)
                {
                    var totalPrice = item.UnitPrice * item.Quantity;

                    table.Cell().Element(CellStyle).Text(i.ToString());
                    table.Cell().Element(CellStyle).Text($"Sản phẩm ID: {item.ProductId}"); // Ideally, name should be passed
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.UnitPrice:N0} đ");
                    table.Cell().Element(CellStyle).AlignRight().Text(item.Quantity.ToString());
                    table.Cell().Element(CellStyle).AlignRight().Text($"{totalPrice:N0} đ");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                    i++;
                }
            });
        }

        public class AddressComponent : IComponent
        {
            private string Title { get; }
            private Order Order { get; }

            public AddressComponent(string title, Order order)
            {
                Title = title;
                Order = order;
            }

            public void Compose(IContainer container)
            {
                container.Column(column =>
                {
                    column.Spacing(2);
                    column.Item().BorderBottom(1).PaddingBottom(5).Text(Title).SemiBold();
                    column.Item().Text(Order.RecipientName ?? "Khách hàng");
                    column.Item().Text(Order.RecipientPhone ?? "");
                    column.Item().Text(Order.ShippingAddress ?? "");
                });
            }
        }

        public class OrderDetailsComponent : IComponent
        {
            private Order Order { get; }

            public OrderDetailsComponent(Order order)
            {
                Order = order;
            }

            public void Compose(IContainer container)
            {
                container.Column(column =>
                {
                    column.Spacing(2);
                    column.Item().BorderBottom(1).PaddingBottom(5).Text("Thông tin đơn hàng").SemiBold();
                    column.Item().Text($"Mã ĐH: #{Order.Id}");
                    column.Item().Text($"Ngày đặt: {Order.CreatedAt:dd/MM/yyyy HH:mm}");
                    column.Item().Text($"Phương thức TT: {Order.PaymentMethod}");
                });
            }
        }
    }
}
