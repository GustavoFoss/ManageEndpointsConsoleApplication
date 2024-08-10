// BaseService.cs
using System.Collections.Generic;
using System.Linq;
namespace manage_endpoints.Service;

public class BaseService<T>
{
    protected readonly List<T> _items = new List<T>();

    public void AddItem(T item)
    {
        if (_items.Contains(item))
        {
            throw new InvalidOperationException("Item already exists.");
        }

        _items.Add(item);
    }

    public T FindItemByCondition(Func<T, bool> predicate)
    {
        return _items.SingleOrDefault(predicate);
    }

    public List<T> GetAllItems()
    {
        return _items.ToList();
    }
}