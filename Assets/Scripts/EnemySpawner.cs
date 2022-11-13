using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    private TestDoctorWave _enemyWave;
    private Dictionary<int, IWave> _waves = new Dictionary<int, IWave>();
    private int _currentWave = 0;

    [Inject]
    private void Construct(TestDoctorWave enemyWave)
    {
        _enemyWave = enemyWave;
    }

    // Start is called before the first frame update
    void Start()
    {
        _waves.Add(0, _enemyWave);
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
        if (_enemyWave.Wave.Enemies.Count == 0)
        {
            return null;
        }

        IEnemy closest = null;
        foreach(var enemy in _enemyWave.Wave.Enemies)
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
        _enemyWave.Wave.Enemies.Remove(enemy);
    }
}
