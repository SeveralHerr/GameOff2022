using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class DoctorMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var direction = new Vector2(-1, 0);
        rb.MovePosition(rb.position + direction * 3);
    }
}
