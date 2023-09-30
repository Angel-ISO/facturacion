namespace Domain.Entities;

 public class Invoice : BaseEntity
{
    public DateTime Date { get; set; }
    public int Total { get; set; }
    public int CustomerId { get; set; }
    public  Customer Customer { get; set; }
    public  ICollection<InvoiceDetail> Details { get; set; }
}

