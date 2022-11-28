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

        Wave = new EnemyWave(factory, _timer)
        {
            MoveSpeed = 35f,
            EnemyMaxCount = 10,
            SpawnRate = 2.5f,
            Health = 3
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
           // SpawnRateIntensity = 0.5f,
            Health = 5
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

public class Wave3 : IWave
{
    private ITimer _timer;

    public EnemyWave Wave { get; set; }

    [Inject]
    private void Construct(HealDoctor.Factory factory, ITimer timer)
    {
        _timer = timer;

        Wave = new EnemyWave(factory, _timer)
        {
            MoveSpeed = 50f,
            EnemyMaxCount = 25,
            SpawnRate = 2f,
            //SpawnRateIntensity = 0.5f,
            Health = 5
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

public class Wave1 : IWave
{
    private ITimer _timer;

    public EnemyWave Wave { get; set; }

    [Inject]
    private void Construct(TestDoctor.Factory factory, ITimer timer)
    {
        _timer = timer;

        Wave = new EnemyWave(factory, _timer)
        {
            MoveSpeed = 25f,
            EnemyMaxCount = 3,
            SpawnRate = 4f,
            //SpawnRateIntensity = 0.5f,
            Health = 1
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

public class Wave6 : IWave
{
    private ITimer _timer;

    public EnemyWave Wave { get; set; }

    [Inject]
    private void Construct(TestDoctor.Factory factory, ITimer timer)
    {
        _timer = timer;

        Wave = new EnemyWave(factory, _timer)
        {
            MoveSpeed = 30f,
            EnemyMaxCount = 30,
            SpawnRate = 2f,
            //SpawnRateIntensity = 0.5f,
            Health = 3
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

public class Wave7 : IWave
{
    private ITimer _timer;

    public EnemyWave Wave { get; set; }

    [Inject]
    private void Construct(TestDoctor.Factory factory, ITimer timer)
    {
        _timer = timer;

        Wave = new EnemyWave(factory, _timer)
        {
            MoveSpeed = 30f,
            EnemyMaxCount = 300,
            SpawnRate = 2f,
            //SpawnRateIntensity = 0.5f,
            Health = 4
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

public class WinWave : IWave
{
    private ITimer _timer;

    public EnemyWave Wave { get; set; }

    [Inject]
    private void Construct(TestDoctor.Factory factory, ITimer timer)
    {
        _timer = timer;

        Wave = new EnemyWave(factory, _timer)
        {
            MoveSpeed = 30f,
            EnemyMaxCount = 300,
            SpawnRate = 2f,
            //SpawnRateIntensity = 0.5f,
            Health = 4
        };
    }

    public void RunWave()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene");
        //Wave.RunWave();
    }

    public bool IsWaveCompleted()
    {
        return Wave.IsWaveCompleted();
    }
}

public class Wave2 : IWave
{
    private ITimer _timer;

    public EnemyWave Wave { get; set; }

    [Inject]
    private void Construct(HealDoctor.Factory factory, ITimer timer)
    {
        _timer = timer;

        Wave = new EnemyWave(factory, _timer)
        {
            MoveSpeed = 25f,
            EnemyMaxCount = 4,
            SpawnRate = 3f,
            //SpawnRateIntensity = 0.5f,
            Health = 3
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
    public float SpawnRateIntensity { get; set; } = 0f;
    public float MoveSpeed { get; set; } = 25f;

    public float Health { get; set; } = 3f;

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
            enemy.HealthBehavior.SetMaxHealth(Health);
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
