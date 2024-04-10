using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using API.Dtos;
using Core.Specifications;
using CloudinaryDotNet.Actions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class ProductController : BaseController
{
    private readonly IPhotoService _photoService;
    private readonly IGenericRepository<Product> _productRepo;
    public ProductController(
        IPhotoService photoService,
        IGenericRepository<Product> productRepo
        )
    {
        _photoService = photoService;
        _productRepo = productRepo;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var spec = new ProductWithTypesAndBrandsAndCategorySpecification();

        var products = await _productRepo.ListAsync(spec);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetProduct>> GetProduct(int id)
    {
        var creteria = new ProductWithTypesAndBrandsAndCategorySpecification(id);
        var product = await _productRepo.GetEntityWithSpec(creteria);


        return Ok(product);
    }

    [HttpPost("add-product-with-photo")]
    public async Task<ActionResult<ProductDto>> AddProductWithPhoto([FromForm] AddProductDtoWithPhoto productWithPhotoDto)
    {
        // Map DTO to Product entity
        var product = new Product
        {
            Name = productWithPhotoDto.Name,
            Description = productWithPhotoDto.Description,
            Price = productWithPhotoDto.Price,
            CategoryId = productWithPhotoDto.CategoryId,
            ProductTypeId = productWithPhotoDto.ProductTypeId,
            ProductBrandId = productWithPhotoDto.ProductBrandId
        };

        // Save the product entity to get the ID
        await _productRepo.AddAsync(product);

        // Upload the photo
        var photoResult = await _photoService.AddPhotoAsync(productWithPhotoDto.PhotoFile);

        if (photoResult.Error != null)
        {
            return BadRequest(photoResult.Error.Message);
        }

        // Create Photo entity
        var photo = new Photo
        {
            Url = photoResult.SecureUrl.AbsoluteUri,
            IsMain = true,
            ProductId = product.Id // Assign the product's ID after saving
        };

        // Update the product entity with the associated photo
        product.Photos.Add(photo);

        // Save the updated product entity with the photo
        await _productRepo.UpdateAsync(product);

        // Map the Product entity to a DTO for response
        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Category = product.Category.Name,
            ProductType = product.ProductType.Name,
            ProductBrand = product.ProductBrand.Name,
            PictureUrl = photo.Url
        };

        return Ok(productDto);
    }

}
