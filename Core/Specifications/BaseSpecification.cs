using System.Linq.Expressions;

namespace Core.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification()
    {
    }
    //Constructor that takes a criteria like id or name for example
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria { get; }
    //It is gonna have a list of includes that we want to apply to our query so we can include related entities in our query
    //include.(p => p.ProductType) for example
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
    //Adding a include to our list of includes
    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
}
