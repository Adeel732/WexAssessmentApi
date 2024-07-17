using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public abstract class Repository<T> : IRepository<T>
{
    protected readonly Dictionary<int, T> data = new Dictionary<int, T>();

    public Task<IEnumerable<T>> GetAllAsync()
    {
        return Task.FromResult(data.Values.AsEnumerable());
    }

    public Task<T> GetByIdAsync(int id)
    {
        data.TryGetValue(id, out T entity);
        return Task.FromResult(entity);
    }

    public Task AddAsync(T entity)
    {
        var id = data.Count + 1;
        (entity as dynamic).Id = id;
        data[id] = entity;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(T entity)
    {
        var id = (int)(entity as dynamic).Id;
        data[id] = entity;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        data.Remove(id);
        return Task.CompletedTask;
    }
}
