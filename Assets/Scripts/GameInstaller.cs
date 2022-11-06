using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        Container.BindFactory<string, AppleProjectile, AppleProjectile.Factory>().FromFactory<PrefabResourceFactory<AppleProjectile>>();
        //Container.Bind<AppleShooter>().AsSingle();

        Container.BindFactory<string, TestDoctor, TestDoctor.Factory>().FromFactory<PrefabResourceFactory<TestDoctor>>();
        Container.BindFactory<string, AppleShooter, AppleShooter.Factory>().FromFactory<PrefabResourceFactory<AppleShooter>>();

        Container.BindFactory<string, GreenAppleShooter, GreenAppleShooter.Factory>().FromFactory<PrefabResourceFactory<GreenAppleShooter>>();
        Container.BindFactory<string, GreenAppleProjectile, GreenAppleProjectile.Factory>().FromFactory<PrefabResourceFactory<GreenAppleProjectile>>();


        //Container.BindFactoryCustomInterface< GreenAppleProjectile, GreenAppleProjectile.Factory, IFactory<GreenAppleProjectile>();

        Container.Bind<ITimer>().To<Timer>().AsTransient();

    }
}
