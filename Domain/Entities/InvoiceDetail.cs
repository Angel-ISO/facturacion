
namespace Domain.Entities;
public class InvoiceDetail : BaseEntity
{
    public int Quantity { get; set; }
    public int Subtotal { get; set; }

    public int InvoiceId { get; set; }
    public  Invoice Invoice { get; set; }
    
    public int ProductId { get; set; }
    public  Product Product { get; set; }
}