using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using API.Dtos;
using Core.Specifications;
using AutoMapper;

namespace API.Controllers;

public class ProductController : BaseController
{
    private readonly IPhotoService _photoService;
    private readonly IGenericRepository<Product> _productRepo;
    private readonly IMapper _mapper;
    public ProductController(
        IPhotoService photoService,
        IGenericRepository<Product> productRepo,
        IMapper mapper
        )
    {
        _photoService = photoService;
        _productRepo = productRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var spec = new ProductWithTypesAndBrandsAndCategorySpecification();

        IReadOnlyList<Product> products = await _productRepo.ListAsync(spec);

        var productsDtoToReturn = _mapper.Map<IEnumerable<ProductDto>>(products);

        return Ok(productsDtoToReturn);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var creteria = new ProductWithTypesAndBrandsAndCategorySpecification(id);
        var product = await _productRepo.GetEntityWithSpec(creteria);

        var productsDtoToReturn = _mapper.Map<IEnumerable<Product>>(product);

        return Ok(productsDtoToReturn);
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
            ProductId = product.Id, // Assign the product's ID after saving
            PublicId = photoResult.PublicId
        };

        // Update the product entity with the associated photo
        product.Photos.Add(photo);

        // Save the updated product entity with the photo
        await _productRepo.UpdateAsync(product);

        // Map the Product entity to a DTO for response
        // var productDto = new ProductDto
        // {
        //     Id = product.Id,
        //     Name = product.Name,
        //     Description = product.Description,
        //     Price = product.Price,
        //     Category = product.Category.Name,
        //     ProductType = product.ProductType.Name,
        //     ProductBrand = product.ProductBrand.Name,
        //     MainPhotoUrl = photo.Url
        // };


        var creteria = new ProductWithTypesAndBrandsAndCategorySpecification(product.Id);
        var newProduct = await _productRepo.GetEntityWithSpec(creteria);

        return Ok(_mapper.Map<ProductDto>(newProduct));
    }

}
