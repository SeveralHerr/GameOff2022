using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TestGrid : MonoBehaviour
{
    //private Grid _grid;
    private AppleShooter.Factory _factory;
    private EnemySpawner _enemySpawner;

    [Inject]
    private void Construct(AppleShooter.Factory factory, EnemySpawner enemySpawner)
    {
        _factory = factory;
        _enemySpawner = enemySpawner;
    }
    // Start is called before the first frame update
    void Start()
    {
       // Grid.Instance.Create(10, 10, 32f, new Vector3(-155, -155, 0));
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    {
        //        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //         //grid.SetValue(mousePosition, 56);
        //        var shooter = _factory.Create();
        //        shooter.transform.position = Grid.Instance.ValidateWorldGridPosition(mousePosition);
                

        //    }
        //}

        
    }
}
