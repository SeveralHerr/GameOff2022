using UnityEngine;
using Zenject;

public class Projectile<T> : MonoBehaviour
{
    private Vector3 _targetPosition;
    private Vector3 _spawnPosition;

    public float MoveSpeed = 350f;

    public class Factory : PlaceholderFactory<string, T>, IFactory<T>
    {
        public T Create()
        {
            
            return base.Create($"Prefabs/{typeof(T).FullName}");
        }
    }

    public void Setup(Vector3 spawnPosition, Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        _spawnPosition = spawnPosition;

        transform.position = _spawnPosition;
    }

    public void MoveTowardsTarget()
    {
        var moveDirection = (_targetPosition - _spawnPosition).normalized;

        var movespeed = 500f;

        transform.position += moveDirection * movespeed * Time.deltaTime;
    }
}
