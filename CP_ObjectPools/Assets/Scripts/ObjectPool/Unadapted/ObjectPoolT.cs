using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : new()
{
    private readonly Queue<T> _pool = new Queue<T>();
    private readonly int _poolSize;

    public ObjectPool(int poolSize)
    {
        _poolSize = poolSize;
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            _pool.Enqueue(new T());
        }
    }

    public T TryGetFromPool()
    {
        if (_pool.Count > 0)
        {
            return _pool.Dequeue();
        }
        else
        {
            throw new InvalidOperationException("No objects available in the pool.");
        }
    }

    public void ReturnToPool(T obj)
    {
        if (_pool.Count < _poolSize)
        {
            _pool.Enqueue(obj);
        }
        else
        {
            throw new InvalidOperationException("Pool is full, cannot return object.");
        }
    }
}