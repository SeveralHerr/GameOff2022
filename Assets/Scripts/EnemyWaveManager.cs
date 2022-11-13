using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class TestDoctorWave : IWave
{
    private ITimer _timer;

    public EnemyWave Wave { get; set; }

    [Inject]
    private void Construct(TestDoctor.Factory factory, ITimer timer)
    {
        _timer = timer;
        
        Wave = new EnemyWave(factory, _timer);
    }
    
    public void RunWave()
    {
        Wave.RunWave();
    }

    public bool IsWaveCompleted()
    {
        return Wave.IsWaveCompleted();
    }
}

public class FasterBiggerTestDoctorWave : IWave
{
    private ITimer _timer;

    public EnemyWave Wave { get; set; }

    [Inject]
    private void Construct(TestDoctor.Factory factory, ITimer timer)
    {
        _timer = timer;

        Wave = new EnemyWave(factory, _timer)
        {
            MoveSpeed = 50f,
            EnemyMaxCount = 10,
            SpawnRate = 3f,
            SpawnRateIntensity = 0.5f
        };
    }

    public void RunWave()
    {
        Wave.RunWave();
    }
    
    public bool IsWaveCompleted()
    {
        return Wave.IsWaveCompleted();
    }
}

public class EnemyWave
{
    public List<IEnemy> Enemies = new List<IEnemy>();
    public bool IsComplete = false;
    public int EnemyMaxCount { get; set; } = 5;
    public float SpawnRate { get; set; } = 3f;
    public float SpawnRateIntensity { get; set; } = 0.5f;
    public float MoveSpeed { get; set; } = 25f;

    private int _enemyCount = 0;

    private IFactory<IEnemy> _doctorFactory;
    private ITimer _timer;
    
    public  EnemyWave(IFactory<IEnemy> factory, ITimer timer)
    {
        _doctorFactory = factory;
        _timer = timer;
    }

    
    public void RunWave()
    {
        _timer.RunTimer(SpawnRate, () =>
        {
            if(_enemyCount >= EnemyMaxCount)
            {
                return;
            }

            var enemy = _doctorFactory.Create();
            enemy.MoveSpeed = MoveSpeed;
            Enemies.Add(enemy);

            _enemyCount++;
            SpawnRate -= SpawnRateIntensity;
        });

        if (Enemies.Count == 0 && _enemyCount >= EnemyMaxCount)
        {
            IsComplete = true;
            return;
        }
    }

    public bool IsWaveCompleted()
    {
        return IsComplete;
    }
}
public class EnemyWaveManager
{
    
}
