using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AppleShooter : Shooter<AppleShooter>, IPlaceable
{
    private AppleProjectile.Factory _factory;
    private ITimer _timer;
    
    [Inject]
    public void Construct(ITimer timer, AppleProjectile.Factory factory)
    {
        _timer = timer;
        _factory = factory;
    }

  

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        PointToEnemy();
        _timer.RunTimer(3.1f, () =>
        {
            if (TargetEnemy == null)
            {
                return;
            }

            var projectile = _factory.Create();
            projectile.Setup(ProjectileSpawnPosition.transform.position, TargetEnemy.transform.position,
                1f, 150f);
        });
    }



}


