using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public partial class Product : BaseEntity
{
    [Required]
    public string Name { get; set; } = null!;
    public string Description { get; set; }
    public decimal Price { get; set; }
    public AvailabilityStatus? AvailabilityStatus { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
    public ProductType ProductType { get; set; }
    public int ProductTypeId { get; set; }
    public ProductBrand ProductBrand { get; set; }
    public int ProductBrandId { get; set; }
    public ICollection<Photo> Photos { get; set; } = new List<Photo>();// cascade deleting photos when product is deleted
}

public enum AvailabilityStatus
{
    Available,
    ComingSoon,
    OutOfStock
}
