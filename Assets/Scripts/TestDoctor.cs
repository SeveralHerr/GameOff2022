using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class TestDoctor : MonoBehaviour
{
    private Vector2 _targetPosition;
    private Vector3 _spawnPosition;
    private Rigidbody2D rb;

    private int path = 0;
    
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
        var worldPos = Grid.Instance.GetWorldPosition(Grid.Instance.EnemyPath[path].x, Grid.Instance.EnemyPath[path].y);
        _spawnPosition = new Vector2(worldPos.x + 100, worldPos.y + 15);
        var pos = (Vector2)Grid.Instance.GetWorldPosition(Grid.Instance.EnemyPath[path].x, Grid.Instance.EnemyPath[path].y);
        _targetPosition = new Vector2(pos.x, pos.y + 15);
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
        //Debug.Log(Grid.Instance.GetWorldPosition(Grid.Instance.EnemyPath[path].x, Grid.Instance.EnemyPath[path].y));
        //Debug.Log(Vector3.Distance(transform.position, Grid.Instance.GetWorldPosition(Grid.Instance.EnemyPath[path].x, Grid.Instance.EnemyPath[path].y)));
        var worldPos = Grid.Instance.GetWorldPosition(Grid.Instance.EnemyPath[path].x, Grid.Instance.EnemyPath[path].y);
        if (Vector3.Distance(transform.position, new Vector2(worldPos.x, worldPos.y + 15)) < 35f && path < 9)
        {
            var pos = (Vector2)Grid.Instance.GetWorldPosition(Grid.Instance.EnemyPath[path].x, Grid.Instance.EnemyPath[path].y);
            _targetPosition = new Vector2(pos.x, pos.y + 15);
            path++;

        }



        //rb.MovePosition(rb.position + _targetPosition);

        var moveDirection = (_targetPosition - (Vector2)transform.position).normalized;

        var movespeed = 15f;

        transform.position += (Vector3) moveDirection * movespeed * Time.deltaTime;
    }
}
