using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    private TestDoctor.Factory _factory;
    private ITimer _timer;

    public static List<TestDoctor> Enemies = new List<TestDoctor>();

    



    [Inject]
    private void Construct(TestDoctor.Factory factory, ITimer timer)
    {
        _factory = factory;
        _timer = timer;
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _timer.RunTimer(3f, () => {
            var enemy = _factory.Create();
            Enemies.Add(enemy);
            }
        );
    }

    public static GameObject GetClosestEnemy(Vector3 position, float maxRange)
    {
        if (Enemies.Count == 0)
        {
            return null;
        }

        TestDoctor closest = null;
        foreach(var enemy in Enemies)
        {
            //Debug.Log(Vector3.Distance(position, enemy.transform.position));
            if (Vector3.Distance(position, enemy.transform.position) < maxRange)
            {
                if(closest == null)
                {
                    closest = enemy;
                }
                else
                {
                    if (Vector3.Distance(position, enemy.transform.position) < Vector3.Distance(position, closest.transform.position))
                    {
                        closest = enemy;
                    }
                }
            }
        }

        return closest?.gameObject;
    }
}
