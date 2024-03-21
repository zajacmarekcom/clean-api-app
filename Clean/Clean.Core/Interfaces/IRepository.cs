namespace Clean.Core.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetById(Guid id);
    Task<IReadOnlyList<T>> ListAll();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task Save();
}