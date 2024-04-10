namespace Infrastructure.Data.Configuration;

// This class will be used to bind the Cloudinary settings from the appsettings.json file
public class CloudinarySettings
{
    public string CloudName { get; set; }
    public string ApiKey { get; set; }
    public string ApiSecret { get; set; }
}
