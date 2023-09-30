using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using facturacionAPI.Dtos;
using Domain.Entities;
using facturacionAPI.Helpers;
using facturacionAPI.Services;

namespace facturacionAPI.Controllers;


[ApiVersion("1.0")]
[ApiVersion("1.1")]

 public class InvoiceController : BaseApiController
{

     private readonly IUnitOfWork _unitofwork;
     private readonly IMapper _mapper;
    private readonly PdfInvoiceService _pdfService; 


    public InvoiceController(IUnitOfWork unitOfWork, IMapper mapper, PdfInvoiceService pdfService)
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

        var pdfBytes = _pdfService.GenerateInvoicePdf(invoice); 

        return File(pdfBytes, "application/pdf", "factura.pdf");
    }

  




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
}