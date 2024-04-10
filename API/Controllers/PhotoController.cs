using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
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

    public PhotoController(IPhotoService photoService,
    IGenericRepository<Product> productRepo,
    IGenericRepository<Photo> photoRepo)
    {
        _photoRepo = photoRepo;
        _productRepo = productRepo;
        _photoService = photoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PhotoDto>>> GetPhotos()
    {
        var spec = new PhotoWithProductSpecification();
        var photos = await _photoRepo.ListAsync(spec);
        return Ok(photos.Select(photo => new PhotoDto
        {
            Id = photo.Id,
            Url = photo.Url,
            IsMain = photo.IsMain,
        }).ToList());
    }

    [HttpPost("add-photo/{productId}")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(int productId, [FromForm] IFormFile file)
    {
        // Retrieve the product by its ID
        var product = await _productRepo.GetByIdAsync(productId);

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
        };

        if (product.Photos.Count == 0)
        {
            photo.IsMain = true;
        }

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




}
