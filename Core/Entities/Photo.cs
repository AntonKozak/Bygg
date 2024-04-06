namespace Core.Entities;

public class Photo : BaseEntity
{
    public string Url { get; set; }
    public int ProductId { get; set; }  // Foreign key
    public Product Product { get; set; }  // Navigation property
    public ICollection<Photo> Photos { get; set; }
}