using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications;

public class PhotoWithProductSpecification : BaseSpecification<Photo>
{
    public PhotoWithProductSpecification()
    {
        AddInclude(x => x.Product);
    }
}
