using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using CloudinaryDotNet.Actions;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PhotoController : BaseController
{
    private readonly IPhotoService _photoService;
    private readonly IGenericRepository<Product> _productRepo;
    private readonly IGenericRepository<Photo> _photoRepo;
    private readonly IMapper _mapper;

    public PhotoController(IPhotoService photoService,
    IGenericRepository<Product> productRepo,
    IGenericRepository<Photo> photoRepo,
    IMapper mapper)
    {
        _photoRepo = photoRepo;
        _productRepo = productRepo;
        _photoService = photoService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<PhotoDto>>> GetPhotos()
    {
        // var spec = new PhotoWithProductSpecification();
        // var photos = await _photoRepo.ListAsync(spec);
        var photos = await _photoRepo.ListAllAsync();

        return Ok(_mapper.Map<List<PhotoDto>>(photos));
        // return Ok(photos);
    }

    [HttpPost("add-photo/{productId}")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file, int productId)
    {
        // Retrieve the product by its ID

        // var product = await _productRepo.GetByIdAsync(productId);
        var creteria = new ProductWithTypesAndBrandsAndCategorySpecification(productId);
        var product = await _productRepo.GetEntityWithSpec(creteria);

        if (product == null)
        {
            return NotFound("Product not found");
        }

        // Upload the photo
        var photoResult = await _photoService.AddPhotoAsync(file);

        if (photoResult.Error != null)
        {
            return BadRequest(photoResult.Error.Message);
        }

        // Create a new Photo entity
        var photo = new Photo
        {
            Url = photoResult.SecureUrl.AbsoluteUri,
            PublicId = photoResult.PublicId,
            IsMain = product.Photos.Count == 0
        };

        product.Photos.Add(photo);
        if (

        await _productRepo.SaveAsync()
        )
        {
            return new PhotoDto
            {
                Id = photo.Id,
                Url = photo.Url,
                IsMain = photo.IsMain,
            };
        }


        // Map the Photo entity to a DTO for response

        return BadRequest("Problem adding photo");
    }

    [HttpDelete("delete-photo/{photoId}/{productId}")]
    public async Task<ActionResult> DeletePhoto(int photoId, int productId)
    {
        var product = await _productRepo.GetByIdAsync(productId);
        var photo = await _photoRepo.GetByIdAsync(photoId);

        if (photo == null)
        {
            return NotFound();
        }

        if (photo.IsMain)
        {
            return BadRequest("You cannot delete the main photo");
        }

        if (photo.PublicId != null)
        {
            var result = await _photoService.DeletePhotoAsync(photo.PublicId);

            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }
        }

        await _photoService.DeletePhotoAsync(photo.PublicId);

        product.Photos.Remove(photo);

        if (await _productRepo.SaveAsync())
        {
            return Ok();
        }

        return BadRequest("Failed to delete the photo");
    }


}