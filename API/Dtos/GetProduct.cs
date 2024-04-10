namespace API.Dtos;

public class GetProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int ProductTypeId { get; set; }
    public int ProductBrandId { get; set; }

}
