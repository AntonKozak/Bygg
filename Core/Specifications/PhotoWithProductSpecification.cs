using Core.Entities;

namespace Core.Specifications;

public class PhotoWithProductSpecification : BaseSpecification<Photo>
{
    public PhotoWithProductSpecification()
    {
        AddInclude(x => x.Product);

    }
}
