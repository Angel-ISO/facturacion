
namespace facturacionAPI.Dtos;

    public class InvoiceDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Total { get; set; }
        public int CustomerId { get; set; }
    }
