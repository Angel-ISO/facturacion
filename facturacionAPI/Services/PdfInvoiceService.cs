using Domain.Entities;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace facturacionAPI.Services
{
    public class PdfInvoiceService
    {
    

   public byte[] GenerateAllInvoicesPdf(IEnumerable<Invoice> invoices)
{
    using var memoryStream = new MemoryStream();
    var pdfWriter = new PdfWriter(memoryStream);
    var pdfDocument = new PdfDocument(pdfWriter);
    var document = new Document(pdfDocument);

    PageSize a4 = PageSize.A4;
    pdfDocument.SetDefaultPageSize(a4);

    foreach (var invoice in invoices)
    {
        if (invoice.Details != null)
        {
            Image logo = new Image(ImageDataFactory.Create(@"C:\Users\Angel Ortega\Documents\facturacion\Media\Logo_Exito_colombia.png"))
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetWidth(100);
            document.Add(logo);

            document.Add(new Paragraph("Número de factura: " + invoice.Id));
            document.Add(new Paragraph("Fecha de expedicion: " + invoice.Date));
            document.Add(new Paragraph("Detalle venta: " ));

            if (invoice.Customer != null)
            {
                document.Add(new Paragraph("Cliente: " + invoice.Customer.Name));
            }

            Table table = new Table(4);
            table.SetWidth(UnitValue.CreatePercentValue(100)); 

            table.AddHeaderCell("Producto");
            table.AddHeaderCell("Cantidad");
            table.AddHeaderCell("Precio unitario");
            table.AddHeaderCell("Subtotal");

            foreach (var detail in invoice.Details)
            {
                if (detail.Product != null)
                {
                    table.AddCell(detail.Product.Name);
                    table.AddCell(detail.Quantity.ToString());
                    table.AddCell(detail.Product.Price.ToString("C"));
                    table.AddCell((detail.Quantity * detail.Product.Price).ToString("C"));
                }
            }

            document.Add(table);

            var totalAmount = invoice.Details?.Sum(detail => detail.Quantity * detail.Product.Price) ?? 0;

            Paragraph total = new Paragraph("Total a pagar: " + totalAmount.ToString("C"))
                .SetBold();

            document.Add(total);

            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
        }
    }

    document.Close();
    pdfWriter.Close();

    return memoryStream.ToArray();
}


    }
}
  