using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
    // This method is gonna take in a query and a specification and it's gonna apply the criteria and the includes to that query
    /// <summary>
    /// Helper class for evaluating specifications on IQueryable data sets.
    /// </summary>
    /// <typeparam name="T">The type of entities being queried.</typeparam>
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        /// <summary>
        /// Applies the criteria and includes defined in a specification to an IQueryable query.
        /// </summary>
        /// <param name="inputQuery">The original query to be modified.</param>
        /// <param name="spec">The specification containing criteria and includes to be applied.</param>
        /// <returns>An IQueryable object representing the modified query with applied criteria and includes.</returns>
        var query = inputQuery;
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);// p => p.ProductTypeId == id for example
        }

        if (spec.Includes != null && spec.Includes.Any())
        {
            //current represents the entity that we are including and include is the expression that we are gonna include
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return query;
    }
}
