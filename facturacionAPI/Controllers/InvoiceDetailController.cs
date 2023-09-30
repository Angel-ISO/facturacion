using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using facturacionAPI.Dtos;
using Domain.Entities;
using facturacionAPI.Helpers;

namespace facturacionAPI.Controllers;


[ApiVersion("1.0")]
[ApiVersion("1.1")]
 public class InvoiceDetailController : BaseApiController
{

     private readonly IUnitOfWork _unitofwork;
     private readonly IMapper _mapper;

    public InvoiceDetailController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitofwork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<InvoiceDetailDto>>> Get()
    {
        var Con = await  _unitofwork.InvoiceDetails.GetAllAsync();
        return _mapper.Map<List<InvoiceDetailDto>>(Con);
    }


     [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<InvoiceDetailDto>>> Get11([FromQuery] Params Pparams)
    {
        var pag = await _unitofwork.InvoiceDetails.GetAllAsync(Pparams.PageIndex, Pparams.PageSize, Pparams.Search);
        var lstN = _mapper.Map<List<InvoiceDetailDto>>(pag.registros);
        return new Pager<InvoiceDetailDto>(lstN, pag.totalRegistros, Pparams.PageIndex, Pparams.PageSize, Pparams.Search);
    }

  




    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InvoiceDetail>> Post(InvoiceDetailDto invoiceDetailDto){
        var invoiceDetail = _mapper.Map<InvoiceDetail>(invoiceDetailDto);
        this._unitofwork.InvoiceDetails.Add(invoiceDetail);
        await _unitofwork.SaveAsync();
        if(invoiceDetail == null)
        {
            return BadRequest();
        }
        invoiceDetailDto.Id = invoiceDetail.Id;
        return CreatedAtAction(nameof(Post),new {id= invoiceDetailDto.Id}, invoiceDetailDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InvoiceDetail>> Put(int id, [FromBody]InvoiceDetail product){
        if(product == null)
            return NotFound();
        _unitofwork.InvoiceDetails.Update(product);
        await _unitofwork.SaveAsync();
        return product;
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var D = await _unitofwork.InvoiceDetails.GetByIdAsync(id);
        if(D == null){
            return NotFound();
        }
        _unitofwork.InvoiceDetails.Remove(D);
        await _unitofwork.SaveAsync();
        return NoContent();
    }
}