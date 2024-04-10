using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity // useble for BaseEntity or derived classes from it can be used as T
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);// to delete an entity by id
    Task<bool> SaveAsync();
    Task<T> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
}

