using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TestGrid : MonoBehaviour
{
    private Grid grid;
    private AppleShooter.Factory _factory;


    [Inject]
    private void Construct(AppleShooter.Factory factory)
    {
        _factory = factory;
    }
    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(10, 10, 32f, new Vector3(-155, -155, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            {
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                 //grid.SetValue(mousePosition, 56);
                var shooter = _factory.Create();
                shooter.transform.position = grid.ValidateWorldGridPosition(mousePosition);
                

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //grid.SetValue(mousePosition, 0);
        }
    }
}
