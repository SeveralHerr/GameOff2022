using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AppleShooter : MonoBehaviour, IPlaceable
{
    private AppleShooterFactory.Factory _factory;
    private ITimer _timer;

    public GameObject _projectileSpawnPosition;
    public GameObject doctor;
    public GameObject Barrel;

    [Inject]
    public void Construct(ITimer timer, AppleShooterFactory.Factory factory)
    {
        _timer = timer;
        _factory = factory;
    }


    public class Factory : PlaceholderFactory<string, AppleShooter>
    {
        public AppleShooter Create()
        {
            return base.Create($"Prefabs/{nameof(AppleShooter)}");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        var enemy = GetClosestEnemy();
        if (enemy == null)
        {
            return;

        }

        var aimDirection = (enemy.transform.position - Barrel.transform.position).normalized;
        var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        Barrel.transform.eulerAngles = new Vector3(0, 0, angle);
        
        
        _timer.RunTimer(3.1f, () =>
       {


           var projectile = _factory.Create();
           projectile.Setup(_projectileSpawnPosition.transform.position, enemy.transform.position);

       });
    }

    private GameObject GetClosestEnemy()
    {
        return EnemySpawner.GetClosestEnemy(transform.position, 4000f);
    }
}


