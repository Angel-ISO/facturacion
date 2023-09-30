using Domain.Entities;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace facturacionAPI.Services
{
    public class PdfInvoiceService{


  public byte[] GenerateInvoicePdf(Invoice invoice)
{
    if (invoice == null)
    {
        return new byte[0]; 
    }

    using var memoryStream = new MemoryStream();
    var pdfWriter = new PdfWriter(memoryStream);
    var pdfDocument = new PdfDocument(pdfWriter);
    var document = new Document(pdfDocument);

    document.Add(new Paragraph("Su Factura"));
    document.Add(new Paragraph("Número de factura: " + invoice.Id));
    document.Add(new Paragraph("Fecha: " + invoice.Date));

    if (invoice.Details != null)
    {
        document.Add(new Paragraph("Detalles de los productos:"));
        foreach (var detail in invoice.Details)
        {
            if (detail.Product != null) 
            {
                document.Add(new Paragraph("Producto: " + detail.Product.Name));
                document.Add(new Paragraph("Cantidad: " + detail.Quantity));
                document.Add(new Paragraph("Precio unitario: " + detail.Product.Price.ToString("C")));
                document.Add(new Paragraph("Subtotal: " + (detail.Quantity * detail.Product.Price).ToString("C")));
            }
        }
    }

    var totalAmount = invoice.Details?.Sum(detail => detail.Quantity * detail.Product.Price) ?? 0;
    document.Add(new Paragraph("Total: " + totalAmount.ToString("C")));

    document.Close();
    pdfWriter.Close();

    return memoryStream.ToArray();
}



    }
}
 