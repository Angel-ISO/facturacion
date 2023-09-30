
namespace facturacionAPI.Dtos;

   public class InvoiceDetailDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Subtotal { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
    }