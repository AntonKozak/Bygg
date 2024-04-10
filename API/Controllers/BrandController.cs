using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BrandController : BaseController
    {
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;

        public BrandController(IGenericRepository<ProductBrand> productBrandRepo)
        {
            _productBrandRepo = productBrandRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductBrand>>> GetAllProductBrands()
        {
            var productBrands = await _productBrandRepo.ListAllAsync();
            return Ok(productBrands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrandById(int id)
        {
            var productBrand = await _productBrandRepo.GetByIdAsync(id);
            if (productBrand == null)
            {
                return NotFound();
            }

            return Ok(productBrand);
        }

        [HttpPost("add-ProductBrand")]
        public async Task<ActionResult<ProductBrand>> AddProductBrand(ProductBrand productBrand)
        {
            var newProductBrand = await _productBrandRepo.AddAsync(productBrand);
            return Ok(newProductBrand);
        }

        [HttpPut("update-ProductBrand")]
        public async Task<ActionResult<ProductBrand>> UpdateProductBrand(ProductBrand productBrand)
        {
            await _productBrandRepo.UpdateAsync(productBrand);
            return Ok(productBrand);
        }

        [HttpDelete("delete-ProductBrand/{id}")]
        public async Task<ActionResult<ProductBrand>> DeleteProductBrand(int id)
        {
            await _productBrandRepo.DeleteAsync(id);
            return Ok();
        }
    }
}
