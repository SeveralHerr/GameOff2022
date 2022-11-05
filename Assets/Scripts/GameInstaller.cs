using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        Container.BindFactory<string, AppleShooterFactory, AppleShooterFactory.Factory>().FromFactory<PrefabResourceFactory<AppleShooterFactory>>();
        Container.Bind<AppleShooter>().AsSingle();

        Container.Bind<ITimer>().To<Timer>().AsTransient();

    }
}
