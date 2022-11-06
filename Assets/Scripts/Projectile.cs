using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Projectile<T> : MonoBehaviour
{
    private Vector3 _targetPosition;
    private Vector3 _spawnPosition;

    private BoxCollider2D boxCollider2D;

    public float MoveSpeed = 350f;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var healthBehavior = collision.gameObject.GetComponent<HealthBehavior>();
        if (healthBehavior != null)
        {
            healthBehavior.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
