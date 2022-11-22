using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    private TestDoctorWave _enemyWave;
    private FasterBiggerTestDoctorWave _fasterBiggerTestDoctorWave;
    private Dictionary<int, IWave> _waves = new Dictionary<int, IWave>();
    private int _currentWave = 0;


    public IFactory<TestDoctor> Create()
    {
        return new TestDoctor.Factory();
    }

    [Inject]
    private void Construct(TestDoctorWave enemyWave, FasterBiggerTestDoctorWave fasterBiggerTestDoctorWave)
    {
        _enemyWave = enemyWave;
        _fasterBiggerTestDoctorWave = fasterBiggerTestDoctorWave;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _waves.Add(0, _enemyWave);
        _waves.Add(1, _fasterBiggerTestDoctorWave);
    }

    // Update is called once per frame
    void Update()
    {
        // no more waves
        if (_currentWave == _waves.Count)
        {
            return;
        }

        if (!_waves[_currentWave].IsWaveCompleted())
        {
            _waves[_currentWave].RunWave();
        }
        else
        {
            _currentWave++;
        }
    }

    public GameObject GetClosestEnemy(Vector3 position, float maxRange)
    {
        if (_currentWave == _waves.Count)
        {
            return null;
        }

        
        if (_waves[_currentWave].Wave.Enemies.Count == 0)
        {
            return null;
        }

        IEnemy closest = null;
        foreach(var enemy in _waves[_currentWave].Wave.Enemies)
        {
            //Debug.Log(Vector3.Distance(position, enemy.transform.position));
            if (Vector3.Distance(position, enemy.Position) < maxRange)
            {
                if(closest == null)
                {
                    closest = enemy;
                }
                else
                {
                    if (Vector3.Distance(position, enemy.Position) < Vector3.Distance(position, closest.Position))
                    {
                        closest = enemy;
                    }
                }
            }
        }

        return closest?.GetGameObject();
    }

    public void RemoveEnemy(TestDoctor enemy)
    {
        _waves[_currentWave].Wave.Enemies.Remove(enemy);
    }
}
