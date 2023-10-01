using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using facturacionAPI.Dtos;
using Domain.Entities;
using facturacionAPI.Helpers;
using facturacionAPI.Services;
using iText.Layout;
using iText.Kernel.Pdf;
/* using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout; */

namespace facturacionAPI.Controllers;


[ApiVersion("1.0")]
[ApiVersion("1.1")]

 public class InvoiceController : BaseApiController
{

     private readonly IUnitOfWork _unitofwork;
     private readonly IMapper _mapper;
    private readonly PdfInvoiceService _pdfService; 


    public InvoiceController(IUnitOfWork unitOfWork, IMapper mapper,PdfInvoiceService pdfService )
    {
        this._unitofwork = unitOfWork;
        _mapper = mapper;
      _pdfService = pdfService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<InvoiceDto>>> Get()
    {
        var Con = await  _unitofwork.Invoices.GetAllAsync();
        return _mapper.Map<List<InvoiceDto>>(Con);
    }


     [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<InvoiceDto>>> Get11([FromQuery] Params Pparams)
    {
        var pag = await _unitofwork.Invoices.GetAllAsync(Pparams.PageIndex, Pparams.PageSize, Pparams.Search);
        var lstN = _mapper.Map<List<InvoiceDto>>(pag.registros);
        return new Pager<InvoiceDto>(lstN, pag.totalRegistros, Pparams.PageIndex, Pparams.PageSize, Pparams.Search);
    }






[HttpGet("GenerateAllInvoicesPDF/{customerId}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<IActionResult> GenerateAllInvoicesPDF(int customerId)
{
    var customerInvoices = await _unitofwork.Invoices.GetInvoicesByCustomerIdAsync(customerId);

    if (customerInvoices == null || !customerInvoices.Any())
    {
        return NotFound();
    }

    var pdfBytes = _pdfService.GenerateAllInvoicesPdf(customerInvoices);

    return File(pdfBytes, "application/pdf", "facturas.pdf");
}














    /* [HttpGet("{id}/GeneratePDF")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GeneratePDF(int id)
    {
        var invoice = await _unitofwork.Invoices.GetByIdAsync(id);
        if (invoice == null)
        {
            return NotFound();
        }

        var pdfBytes = _pdfService.GenerateInvoicePdf(invoice); 

        return File(pdfBytes, "application/pdf", "factura.pdf");
    } 

 */





    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Invoice>> Post(InvoiceDto invoiceDto){
        var invoice = _mapper.Map<Invoice>(invoiceDto);
        this._unitofwork.Invoices.Add(invoice);
        await _unitofwork.SaveAsync();
        if(invoice == null)
        {
            return BadRequest();
        }
        invoiceDto.Id = invoice.Id;
        return CreatedAtAction(nameof(Post),new {id= invoiceDto.Id}, invoiceDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Invoice>> Put(int id, [FromBody]Invoice product){
        if(product == null)
            return NotFound();
        _unitofwork.Invoices.Update(product);
        await _unitofwork.SaveAsync();
        return product;
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var D = await _unitofwork.Invoices.GetByIdAsync(id);
        if(D == null){
            return NotFound();
        }
        _unitofwork.Invoices.Remove(D);
        await _unitofwork.SaveAsync();
        return NoContent();
    }








/* 


[HttpGet("{id}/GeneratePDF")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<IActionResult> GeneratePDF(int id)
{
    var invoice = await _unitofwork.Invoices.GetByIdAsync(id);
    if (invoice == null)
    {
        return NotFound();
    }

    try
    {
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

        var pdfBytes = memoryStream.ToArray();
        return File(pdfBytes, "application/pdf", "factura.pdf");
    }
    catch (Exception ex)
    {
        // Manejar cualquier error que pueda ocurrir al generar el PDF.
        return BadRequest($"Error al generar el PDF: {ex.Message}");
    }
}

 */










}