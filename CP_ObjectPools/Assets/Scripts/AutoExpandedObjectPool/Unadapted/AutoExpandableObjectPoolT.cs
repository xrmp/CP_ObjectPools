using System;
using System.Collections.Generic;

public class AutoExpandableObjectPool<T> where T : new()
{
    private readonly Queue<T> _availableObjects = new Queue<T>();
    private readonly List<T> _allObjects = new List<T>();
    private readonly int _maxPoolSize;
    private readonly int _startPoolSize;

    public AutoExpandableObjectPool(int startPoolSize, int maxPoolSize)
    {
        if (startPoolSize > maxPoolSize)
        {
            throw new ArgumentException("startPoolSize cannot be greater than maxPoolSize.");
        }

        _maxPoolSize = maxPoolSize;
        _startPoolSize = startPoolSize;
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _startPoolSize; i++)
        {
            T obj = new T();
            _availableObjects.Enqueue(obj);
            _allObjects.Add(obj);
        }
    }

    public T TryGetFromPool()
    {
        if (_availableObjects.Count > 0)
        {
            return _availableObjects.Dequeue();
        }
        else if (_allObjects.Count < _maxPoolSize)
        {
            T newObj = new T();
            _allObjects.Add(newObj);
            return newObj;
        }
        else
        {
            throw new InvalidOperationException("No objects available in the pool and pool has reached its maximum size.");
        }
    }

    public void ReturnToPool(T obj)
    {
        if (_allObjects.Contains(obj))
        {
            if (!_availableObjects.Contains(obj))
            {
                _availableObjects.Enqueue(obj);
            }
            else
            {
                throw new InvalidOperationException("Object is already in the available pool.");
            }
        }
        else
        {
            throw new InvalidOperationException("Object does not belong to this pool.");
        }
    }

    public int AvailableObjectsCount => _availableObjects.Count;
    public int TotalObjectsCount => _allObjects.Count;
}