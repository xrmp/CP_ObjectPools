using System.Collections.Generic;
using UnityEngine;

public class AutoExpandableGameObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab; // Префаб пули
    [SerializeField] private int _startPoolSize = 10; // Начальный размер пула
    [SerializeField] private int _maxPoolSize = 20; // Максимальный размер пула

    private Queue<GameObject> _availableObjects = new Queue<GameObject>();
    private List<GameObject> _allObjects = new List<GameObject>();

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _startPoolSize; i++)
        {
            CreateNewObject();
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(_prefab, transform);
        obj.SetActive(false);
        _availableObjects.Enqueue(obj);
        _allObjects.Add(obj);
        return obj;
    }

    public GameObject TryGetFromPool()
    {
        if (_availableObjects.Count > 0)
        {
            GameObject obj = _availableObjects.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else if (_allObjects.Count < _maxPoolSize)
        {
            GameObject newObj = CreateNewObject();
            newObj.SetActive(true);
            return newObj;
        }
        else
        {
            Debug.LogWarning("Pool is full, cannot create more objects.");
            return null;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        if (_allObjects.Contains(obj))
        {
            obj.SetActive(false);
            _availableObjects.Enqueue(obj);
        }
        else
        {
            Debug.LogWarning("Object does not belong to this pool.");
        }
    }
}