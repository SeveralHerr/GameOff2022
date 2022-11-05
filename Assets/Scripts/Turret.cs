using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Vector3 shootFromPosition;
    public GameObject apple;
    
    private void Awake()
    {
       // shootFromPosition = transform.Find("ShootFromPosition").position;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(apple);
    }
}
