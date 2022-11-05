using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        Container.BindFactory<string, AppleShooterFactory, AppleShooterFactory.Factory>().FromFactory<PrefabResourceFactory<AppleShooterFactory>>();
        //Container.Bind<AppleShooter>().AsSingle();

        Container.BindFactory<string, TestDoctor, TestDoctor.Factory>().FromFactory<PrefabResourceFactory<TestDoctor>>();
        Container.BindFactory<string, AppleShooter, AppleShooter.Factory>().FromFactory<PrefabResourceFactory<AppleShooter>>();

        Container.Bind<ITimer>().To<Timer>().AsTransient();

    }
}
