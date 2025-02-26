using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObjectPool _bulletPool;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = _bulletPool.TryGetFromPool();
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
            }
        }
    }
}