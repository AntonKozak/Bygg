using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TypeController : BaseController
{
    private readonly IGenericRepository<ProductType> _productTypeRepo;
    public TypeController(IGenericRepository<ProductType> productTypeRepo)
    {
        _productTypeRepo = productTypeRepo;
    }
    [HttpGet]
    public async Task<ActionResult<List<ProductType>>> GetAllProductTypes()
    {
        var productTypes = await _productTypeRepo.ListAllAsync();
        return Ok(productTypes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductType>> GetProductTypeById(int id)
    {
        var productType = await _productTypeRepo.GetByIdAsync(id);
        if (productType == null)
        {
            return NotFound();
        }

        return Ok(productType);
    }

    [HttpPost("add-ProductType")]
    public async Task<ActionResult<ProductType>> AddProductType(ProductType productType)
    {
        var newProductType = await _productTypeRepo.AddAsync(productType);
        return Ok(newProductType);
    }

    [HttpPut("update-ProductType")]
    public async Task<ActionResult<ProductType>> UpdateProductType(ProductType productType)
    {
        await _productTypeRepo.UpdateAsync(productType);
        return Ok(productType);
    }

    [HttpDelete("delete-ProductType/{id}")]
    public async Task<ActionResult<ProductType>> DeleteProductType(int id)
    {
        await _productTypeRepo.DeleteAsync(id);
        return Ok();
    }
}
