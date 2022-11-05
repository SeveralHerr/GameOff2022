using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TestDoctor : MonoBehaviour
{
    private Vector3 _targetPosition;
    private Vector3 _spawnPosition;
    private Rigidbody2D rb;
    
    public class Factory : PlaceholderFactory<string, TestDoctor>
    {
        public TestDoctor Create()
        {
            return base.Create($"Prefabs/{nameof(TestDoctor)}");
        }
    }

    public void Setup(Vector3 spawnPosition)
    {
        _spawnPosition = spawnPosition;

        transform.position = _spawnPosition;
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        var direction = new Vector2(-1, 0);
        rb.MovePosition(rb.position + direction * 3);

        //var moveDirection = (_targetPosition - transform.position).normalized;

        //var movespeed = 5f;

        //transform.position += moveDirection * movespeed * Time.deltaTime;
    }
}
