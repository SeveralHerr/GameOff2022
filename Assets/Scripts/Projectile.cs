using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Projectile<T> : MonoBehaviour
{
    private Vector3 _targetPosition;
    private Vector3 _spawnPosition;

    private BoxCollider2D boxCollider2D;
    public ITimer Timer;

    public float Damage { get; set; } = 1f;

    public float MoveSpeed { get; set; } = 100f;

    [Inject]
    public void Construct(ITimer timer)
    {
        Timer = timer;
    }

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

    public void Setup(Vector3 spawnPosition, Vector3 targetPosition, float damage, float speed)
    {
        _targetPosition = targetPosition;
        _spawnPosition = spawnPosition;
        Damage = damage;
        MoveSpeed = speed;

        transform.position = _spawnPosition;
    }

    public void MoveTowardsTarget()
    {
        var moveDirection = (_targetPosition - _spawnPosition).normalized;


        transform.position += moveDirection * MoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var healthBehavior = collision.gameObject.GetComponent<HealthBehavior>();
        if (healthBehavior != null)
        {
            healthBehavior.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
