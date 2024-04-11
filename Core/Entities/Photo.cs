using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Photo : BaseEntity
{
    public string Url { get; set; }
    public bool IsMain { get; set; } = false;
    public string PublicId { get; set; } // used for deleting photo from cloudinary
    public int ProductId { get; set; }  // Foreign key
    [ForeignKey("ProductId")]
    public Product Product { get; set; }  // Navigation property
}