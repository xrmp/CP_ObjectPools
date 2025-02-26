using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab; // Префаб пули
    [SerializeField] private int _poolSize = 10; // Размер пула

    private Queue<GameObject> _pool = new Queue<GameObject>();

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_prefab, transform);
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public GameObject TryGetFromPool()
    {
        if (_pool.Count > 0)
        {
            GameObject obj = _pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Debug.LogWarning("No objects available in the pool.");
            return null;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        if (_pool.Count < _poolSize)
        {
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
        else
        {
            Debug.LogWarning("Pool is full, cannot return object.");
        }
    }
}