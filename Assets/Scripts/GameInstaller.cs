using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public EnemySpawner enemySpawner;
    public ResourceManager resourceManager;
    public GridManager gridManager;
    public InputManager inputManager;

    public override void InstallBindings()
    {
        Container.BindFactory<string, AppleProjectile, AppleProjectile.Factory>().FromFactory<PrefabResourceFactory<AppleProjectile>>();
        //Container.Bind<AppleShooter>().AsSingle();

        Container.BindFactory<string, TestDoctor, TestDoctor.Factory>().FromFactory<PrefabResourceFactory<TestDoctor>>();
        Container.BindFactory<string, HealDoctor, HealDoctor.Factory>().FromFactory<PrefabResourceFactory<HealDoctor>>();
        Container.BindFactory<string, AppleShooter, AppleShooter.Factory>().FromFactory<PrefabResourceFactory<AppleShooter>>();

        Container.BindFactory<string, TreeResource, TreeResource.Factory>().FromFactory<PrefabResourceFactory<TreeResource>>();

        Container.BindFactory<string, GreenAppleShooter, GreenAppleShooter.Factory>().FromFactory<PrefabResourceFactory<GreenAppleShooter>>();
        Container.BindFactory<string, GreenAppleProjectile, GreenAppleProjectile.Factory>().FromFactory<PrefabResourceFactory<GreenAppleProjectile>>();
        Container.Bind<TestDoctorWave>().ToSelf().AsTransient();
        Container.Bind<FasterBiggerTestDoctorWave>().ToSelf().AsTransient();
        Container.Bind<Wave3>().ToSelf().AsTransient();
        Container.Bind<Wave1>().ToSelf().AsTransient();
        Container.Bind<Wave2>().ToSelf().AsTransient();
        //Container.Bind<InputManager>().ToSelf().AsSingle();
        //Instantiate(Resources.Load("Prefabs/EnemySpawner"), Vector3.zero, Quaternion.identity);

        Container.BindInstance<EnemySpawner>(enemySpawner).AsSingle();
        Container.BindInstance<ResourceManager>(resourceManager).AsSingle();
        Container.BindInstance<GridManager>(gridManager).AsSingle();

        Container.BindInstance<InputManager>(inputManager).AsSingle();



        //Container.BindFactoryCustomInterface< GreenAppleProjectile, GreenAppleProjectile.Factory, IFactory<GreenAppleProjectile>();

        Container.Bind<ITimer>().To<Timer>().AsTransient();

    }
}
