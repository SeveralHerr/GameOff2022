using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

public class Shooter<T> : MonoBehaviour, IPlaceable
{
    public GameObject ProjectileSpawnPosition;
    public GameObject Barrel;
    public GameObject TargetEnemy;

    private EnemySpawner _enemySpawner;

    [Inject]
    private void Construct(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
    }

    public void PointToEnemy()
    {
        TargetEnemy = GetClosestEnemy();
        if (TargetEnemy == null)
        {
            return;
        }

        var aimDirection = (TargetEnemy.transform.position - Barrel.transform.position).normalized;
        var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        Barrel.transform.eulerAngles = new Vector3(0, 0, angle);
    }


    public class Factory : PlaceholderFactory<string, T>
    {
        public T Create()
        {
            return base.Create($"Prefabs/{typeof(T).FullName}");
        }
    }
    private GameObject GetClosestEnemy()
    {
        return _enemySpawner.GetClosestEnemy(transform.position, 4000f);
    }
}

public class GreenAppleShooter : Shooter<GreenAppleShooter>, IPlaceable
{
    private GreenAppleProjectile.Factory _factory;
    private ITimer _timer;

    [Inject]
    public void Construct(ITimer timer, GreenAppleProjectile.Factory factory)
    {
        _timer = timer;
        _factory = factory;
    }



    
    // Update is called once per frame
    void Update()
    {
        PointToEnemy();
        _timer.RunTimer(3.1f, () =>
        {
            if(TargetEnemy == null)
            {
                return;
            }
            
            var projectile = _factory.Create();
            projectile.Setup(ProjectileSpawnPosition.transform.position, TargetEnemy.transform.position);
        });
    }
}
