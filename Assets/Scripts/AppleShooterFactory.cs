using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AppleShooterFactory : MonoBehaviour
{
    public class Factory : PlaceholderFactory<string, AppleShooterFactory>
    {
        public AppleShooterFactory Create()
        {
            return base.Create($"Prefabs/Apple");
        }
    }

    public GameObject doctor;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

    public Vector2 aimDirection;
    public Vector3 initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        // initialPosition = transform.position;
        aimDirection = new Vector3(1, 1,0);//(Vector2)(doctor.transform.position - transform.position).normalized;
        

        //var parentPos = transform.parent.position;
        //transform.SetParent(null);
        //transform.position = parentPos;
    }

    // Update is called once per frame
    void Update()
    {
        //
        rb.MovePosition(rb.position + aimDirection * 3);
    }

    private void LateUpdate()
    {
        //this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.transform.parent.rotation.z * -1.0f);
    }
}
