using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPdfService
    {
        byte[] GenerateInvoicePdf(Order order);
    }
}
