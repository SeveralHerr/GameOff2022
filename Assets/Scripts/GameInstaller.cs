using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public EnemySpawner enemySpawner;
    public override void InstallBindings()
    {
        Container.BindFactory<string, AppleProjectile, AppleProjectile.Factory>().FromFactory<PrefabResourceFactory<AppleProjectile>>();
        //Container.Bind<AppleShooter>().AsSingle();

        Container.BindFactory<string, TestDoctor, TestDoctor.Factory>().FromFactory<PrefabResourceFactory<TestDoctor>>();
        Container.BindFactory<string, AppleShooter, AppleShooter.Factory>().FromFactory<PrefabResourceFactory<AppleShooter>>();

        Container.BindFactory<string, GreenAppleShooter, GreenAppleShooter.Factory>().FromFactory<PrefabResourceFactory<GreenAppleShooter>>();
        Container.BindFactory<string, GreenAppleProjectile, GreenAppleProjectile.Factory>().FromFactory<PrefabResourceFactory<GreenAppleProjectile>>();
        Container.Bind<TestDoctorWave>().ToSelf().AsTransient();
        //Instantiate(Resources.Load("Prefabs/EnemySpawner"), Vector3.zero, Quaternion.identity);

        Container.BindInstance<EnemySpawner>(enemySpawner).AsSingle();



        //Container.BindFactoryCustomInterface< GreenAppleProjectile, GreenAppleProjectile.Factory, IFactory<GreenAppleProjectile>();

        Container.Bind<ITimer>().To<Timer>().AsTransient();

    }
}
