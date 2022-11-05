using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    private Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(4, 2, 60f, new Vector3(200, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            {
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                 grid.SetValue(mousePosition, 56);

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grid.SetValue(mousePosition, 0);
        }
    }
}
