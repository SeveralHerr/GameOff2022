using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HealthBehavior : MonoBehaviour
{
    public float Health = 3;
    public float MaxHealth = 3;

    public HealthBar HealthBar;

    private EnemySpawner _enemySpawner;

    [Inject]
    private void Construct(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
    }

    
    public void Start()
    {
        HealthBar.SetMaxHealth(MaxHealth);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        HealthBar.SetHealth(Health);

        if (Health <= 0)
        {
            _enemySpawner.RemoveEnemy(gameObject.GetComponent<TestDoctor>());
            Destroy(gameObject);
        }
    }
}
