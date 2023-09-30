using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using facturacionAPI.Dtos;
using Domain.Entities;
using facturacionAPI.Helpers;

namespace facturacionAPI.Controllers;


[ApiVersion("1.0")]
[ApiVersion("1.1")]
 public class CustomerController : BaseApiController
{

     private readonly IUnitOfWork _unitofwork;
     private readonly IMapper _mapper;

    public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitofwork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> Get()
    {
        var Con = await  _unitofwork.Customers.GetAllAsync();
        return _mapper.Map<List<CustomerDto>>(Con);
    }


     [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CustomerDto>>> Get11([FromQuery] Params Pparams)
    {
        var pag = await _unitofwork.Customers.GetAllAsync(Pparams.PageIndex, Pparams.PageSize, Pparams.Search);
        var lstN = _mapper.Map<List<CustomerDto>>(pag.registros);
        return new Pager<CustomerDto>(lstN, pag.totalRegistros, Pparams.PageIndex, Pparams.PageSize, Pparams.Search);
    }

  


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Customer>> Post(CustomerDto customerDto){
        var customer = _mapper.Map<Customer>(customerDto);
        this._unitofwork.Customers.Add(customer);
        await _unitofwork.SaveAsync();
        if(customer == null)
        {
            return BadRequest();
        }
        customerDto.Id = customer.Id;
        return CreatedAtAction(nameof(Post),new {id= customerDto.Id}, customerDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Customer>> Put(int id, [FromBody]Customer product){
        if(product == null)
            return NotFound();
        _unitofwork.Customers.Update(product);
        await _unitofwork.SaveAsync();
        return product;
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var D = await _unitofwork.Customers.GetByIdAsync(id);
        if(D == null){
            return NotFound();
        }
        _unitofwork.Customers.Remove(D);
        await _unitofwork.SaveAsync();
        return NoContent();
    }
}