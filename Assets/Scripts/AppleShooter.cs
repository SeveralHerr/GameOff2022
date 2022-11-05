using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AppleShooter : MonoBehaviour
{
    private AppleShooterFactory.Factory _factory;
    private ITimer _timer;

    [Inject]
    public void Construct(ITimer timer, AppleShooterFactory.Factory factory)
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
        
       _timer.RunTimer(3f, () => _factory.Create());
    }
}


