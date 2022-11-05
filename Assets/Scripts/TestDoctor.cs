using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class TestDoctor : MonoBehaviour
{
    private Vector2 _targetPosition;
    private Rigidbody2D rb;

    private int path = 0;
    private Vector3 offset = new Vector3(0, 15, 0);
    
    public class Factory : PlaceholderFactory<string, TestDoctor>
    {
        public TestDoctor Create()
        {
            return base.Create($"Prefabs/{nameof(TestDoctor)}");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        transform.position = Grid.Instance.GetEnemyPathWorldPosition(0, new Vector3(100, 0, 0) + offset);
        _targetPosition = Grid.Instance.GetEnemyPathWorldPosition(0, offset);
    }
    
    // Update is called once per frame
    void Update()
    {
        var targetPosition = Grid.Instance.GetEnemyPathWorldPosition(path, offset);
        var isEndOfPath = path == Grid.Instance.EnemyPath.Length-1;
        if (Vector3.Distance(transform.position, targetPosition) < 35f && !isEndOfPath)
        {
            _targetPosition = targetPosition;
            path++;
        }
        else if(isEndOfPath)
        {
            var lastPath = Grid.Instance.EnemyPath.Length - 1;
            _targetPosition = Grid.Instance.GetEnemyPathWorldPosition(lastPath, new Vector3(-100, 0, 0) + offset);
        }

        var moveDirection = (_targetPosition - (Vector2)transform.position).normalized;

        var movespeed = 25f;

        transform.position += (Vector3) moveDirection * movespeed * Time.deltaTime;
    }
}
