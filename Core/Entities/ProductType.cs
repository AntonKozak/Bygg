using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class ProductType : BaseEntity
{
    [Required]
    public string Name { get; set; } = null!;
}