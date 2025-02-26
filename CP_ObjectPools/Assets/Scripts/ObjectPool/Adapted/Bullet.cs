using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifeTime = 3f;

    private float _currentLifeTime;

    private void OnEnable()
    {
        _currentLifeTime = _lifeTime;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        _currentLifeTime -= Time.deltaTime;
        if (_currentLifeTime <= 0)
        {
            ReturnToPool();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameObjectPool>().ReturnToPool(gameObject);
    }
}