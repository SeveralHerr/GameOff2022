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
    
    public class Factory : PlaceholderFactory<string, T>
    {
        public T Create()
        {
            return base.Create($"Prefabs/{nameof(T)}");
        }
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

    private GameObject GetClosestEnemy()
    {
        return EnemySpawner.GetClosestEnemy(transform.position, 4000f);
    }
}

public class GreenAppleShooter : Shooter<GreenAppleShooter>, IPlaceable
{
    private AppleProjectile.Factory _factory;
    private ITimer _timer;

    [Inject]
    public void Construct(ITimer timer, AppleProjectile.Factory factory)
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
