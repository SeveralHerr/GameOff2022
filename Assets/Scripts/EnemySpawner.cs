using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    private TestDoctorWave _enemyWave;
    private FasterBiggerTestDoctorWave _fasterBiggerTestDoctorWave;
    private Wave3 _wave3;
    private Wave2 _wave2;
    private Wave1 _wave1;
    private Wave6 _wave6;
    private Wave7 _wave7;
    private WinWave _winWave;
    private Dictionary<int, IWave> _waves = new Dictionary<int, IWave>();
    private int _currentWave = 0;


    [Inject]
    private void Construct(TestDoctorWave enemyWave, FasterBiggerTestDoctorWave fasterBiggerTestDoctorWave,
        Wave3 wave3, Wave2 wave2, Wave1 wave1, Wave6 wave6, Wave7 wave7, WinWave winWave)
    {
        _enemyWave = enemyWave;
        _fasterBiggerTestDoctorWave = fasterBiggerTestDoctorWave;
        _wave3 = wave3;
        _wave2 = wave2;
        _wave1 = wave1;
        _wave6 = wave6;
        _wave7 = wave7;
        _winWave = winWave;
    }

    // Start is called before the first frame update
    void Start()
    {
        _waves.Add(0, _wave1);
        _waves.Add(1, _wave2);
        _waves.Add(2, _enemyWave);
        _waves.Add(3, _fasterBiggerTestDoctorWave);
        _waves.Add(4, _wave3);
        _waves.Add(5, _wave6);
        _waves.Add(6, _wave7);
        _waves.Add(7, _winWave);
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
            if (enemy != null && Vector3.Distance(position, enemy.Position) < maxRange)
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

    public void RemoveEnemy(IEnemy enemy)
    {
        _waves[_currentWave].Wave.Enemies.Remove(enemy);
    }
}
