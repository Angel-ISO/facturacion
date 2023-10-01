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
                    Table table = new Table(1).UseAllAvailableWidth();
                    Cell cell = new Cell();
                    table.AddCell(cell);

                    Image logo = new Image(ImageDataFactory.Create(@"C:\Users\Angel Ortega\Documents\facturacion\Media\Logo_Exito_colombia.png"))
                        .SetWidth(100);
                    cell.Add(logo);

                    cell.Add(new Paragraph("Número de factura: " + invoice.Id));
                    cell.Add(new Paragraph("Fecha: " + invoice.Date));
                    cell.Add(new Paragraph("Detalles de los productos:"));

                    foreach (var detail in invoice.Details)
                    {
                        if (detail.Product != null)
                        {
                            cell.Add(new Paragraph("Producto: " + detail.Product.Name));
                            cell.Add(new Paragraph("Cantidad: " + detail.Quantity));
                            cell.Add(new Paragraph("Precio unitario: " + detail.Product.Price.ToString("C")));
                            cell.Add(new Paragraph("Subtotal: " + (detail.Quantity * detail.Product.Price).ToString("C")));
                        }
                    }

                    var totalAmount = invoice.Details?.Sum(detail => detail.Quantity * detail.Product.Price) ?? 0;
                    cell.Add(new Paragraph("Total: " + totalAmount.ToString("C")));

                    document.Add(table);
                    document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                }
            }

            document.Close();
            pdfWriter.Close();

            return memoryStream.ToArray();
        }
    }
}
