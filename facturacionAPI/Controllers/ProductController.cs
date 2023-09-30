using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using facturacionAPI.Dtos;
using Domain.Entities;
using facturacionAPI.Helpers;

namespace facturacionAPI.Controllers;


[ApiVersion("1.0")]
[ApiVersion("1.1")]
 public class ProductController : BaseApiController
{

     private readonly IUnitOfWork _unitofwork;
     private readonly IMapper _mapper;

    public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitofwork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
    {
        var Con = await  _unitofwork.Products.GetAllAsync();
        return _mapper.Map<List<ProductDto>>(Con);
    }


     [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ProductDto>>> Get11([FromQuery] Params Pparams)
    {
        var pag = await _unitofwork.Products.GetAllAsync(Pparams.PageIndex, Pparams.PageSize, Pparams.Search);
        var lstN = _mapper.Map<List<ProductDto>>(pag.registros);
        return new Pager<ProductDto>(lstN, pag.totalRegistros, Pparams.PageIndex, Pparams.PageSize, Pparams.Search);
    }


  





    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> Post(ProductDto productDto){
        var product = _mapper.Map<Product>(productDto);
        this._unitofwork.Products.Add(product);
        await _unitofwork.SaveAsync();
        if(product == null)
        {
            return BadRequest();
        }
        productDto.Id = product.Id;
        return CreatedAtAction(nameof(Post),new {id= productDto.Id}, productDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> Put(int id, [FromBody]Product product){
        if(product == null)
            return NotFound();
        _unitofwork.Products.Update(product);
        await _unitofwork.SaveAsync();
        return product;
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var D = await _unitofwork.Products.GetByIdAsync(id);
        if(D == null){
            return NotFound();
        }
        _unitofwork.Products.Remove(D);
        await _unitofwork.SaveAsync();
        return NoContent();
    }
}