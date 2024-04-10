using System.Linq.Expressions;

namespace Core.Specifications;

public interface ISpecification<T>
{
    //Criteria that we want to apply to our query in the form of an expression
    Expression<Func<T, bool>> Criteria { get; }
    //List of includes that we want to apply to our query
    List<Expression<Func<T, object>>> Includes { get; }
}
