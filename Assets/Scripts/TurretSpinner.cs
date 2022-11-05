using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject.SpaceFighter;
using static UnityEngine.GraphicsBuffer;
using static Zenject.CheatSheet;

public class TurretSpinner : MonoBehaviour
{
    public GameObject doctor; 

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var aimDirection = (doctor.transform.position - transform.position).normalized;
        var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
