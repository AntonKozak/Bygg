using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Interfaces;
using Infrastructure.Data.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Data;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;
    public PhotoService(IOptions<CloudinarySettings> config)
    {   // This is the constructor that will be used to inject the Cloudinary settings
        var acc = new Account
        (
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
        );
        _cloudinary = new Cloudinary(acc);
    }
    public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();

        if (file == null || file.Length == 0)
        {
            // Handle null or empty file
            return uploadResult;
        }

        using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
            Folder = "ebyggshop"
        };

        uploadResult = await _cloudinary.UploadAsync(uploadParams);
        return uploadResult;
    }

    public async Task<DeletionResult> DeletePhotoAsync(string publicId)
    {
        var deletionParams = new DeletionParams(publicId);

        return await _cloudinary.DestroyAsync(deletionParams);
    }
}
