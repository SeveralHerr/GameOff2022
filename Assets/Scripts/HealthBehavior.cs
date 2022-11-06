using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehavior : MonoBehaviour
{
    public float Health = 3;
    public float MaxHealth = 3;

    public HealthBar HealthBar;

    public void Start()
    {
        HealthBar.SetMaxHealth(MaxHealth);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if(Health <= 0)
        {
            EnemySpawner.Enemies.Remove(gameObject.GetComponent<TestDoctor>());
            Destroy(gameObject);
        }
    }
}
