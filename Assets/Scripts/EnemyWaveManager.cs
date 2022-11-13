using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IWave
{
    public void RunWave();
    public bool IsWaveCompleted();
}
public class TestDoctorWave : IWave
{
    private ITimer _timer;

    public EnemyWave Wave;

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

public class EnemyWave
{
    public List<IEnemy> Enemies = new List<IEnemy>();
    public bool IsComplete = false;
    public int EnemyMaxCount { get; set; } = 5;
    public float SpawnRate { get; set; } = 3f;

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
                IsComplete = true;
                return;
            }

            var enemy = _doctorFactory.Create();
            enemy.MoveSpeed = 25f;
            Enemies.Add(enemy);

            _enemyCount++;
            SpawnRate -= 0.5f;
        });
    }

    public bool IsWaveCompleted()
    {
        return IsComplete;
    }
}
public class EnemyWaveManager
{
    
}
