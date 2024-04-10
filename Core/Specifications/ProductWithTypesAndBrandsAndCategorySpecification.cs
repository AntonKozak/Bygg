using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public class ProductWithTypesAndBrandsAndCategorySpecification : BaseSpecification<Product>
{
    public ProductWithTypesAndBrandsAndCategorySpecification()
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
        AddInclude(x => x.Category);
    }

    public ProductWithTypesAndBrandsAndCategorySpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
        AddInclude(x => x.Category);
    }


}
