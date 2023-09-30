namespace Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Invoice> Invoices { get; set; }
}