using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class HealDoctor : MonoBehaviour, IEnemy
    

{
    private ResourceManager _resourceManager;
    private EnemySpawner _enemySpawner;
    private Vector2 _targetPosition;

    private int path = 0;
    private Vector3 offset = new Vector3(0, 15, 0);

    public float MoveSpeed { get; set; } = 25f;
    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();


        transform.position = Grid.Instance.GetEnemyPathWorldPosition(0, new Vector3(100, 0, 0) + offset);
        _targetPosition = Grid.Instance.GetEnemyPathWorldPosition(0, offset);
    }

    public class Factory : PlaceholderFactory<string, HealDoctor>, IFactory<HealDoctor>
    {
        public HealDoctor Create()
        {
            return base.Create($"Prefabs/{nameof(HealDoctor)}");
        }
    }

    public IFactory<HealDoctor> Create()
    {
        return new HealDoctor.Factory();
    }
    [Inject]
    public void Construct(ResourceManager resourceManager, EnemySpawner enemySpawner)
    {
        _resourceManager = resourceManager;
        _enemySpawner = enemySpawner;
    }

    // Update is called once per frame
    void Update()
    {
        var targetPosition = Grid.Instance.GetEnemyPathWorldPosition(path, offset);
        var isEndOfPath = path == Grid.Instance.EnemyPath.Length - 1;
        if (Vector3.Distance(transform.position, targetPosition) < 35f && !isEndOfPath)
        {
            _targetPosition = targetPosition;
            path++;
        }
        else if (isEndOfPath)
        {
            var lastPath = Grid.Instance.EnemyPath.Length - 1;
            _targetPosition = Grid.Instance.GetEnemyPathWorldPosition(lastPath, new Vector3(-100, 0, 0) + offset);
        }

        var moveDirection = (_targetPosition - (Vector2)transform.position).normalized;


        transform.position += (Vector3)moveDirection * MoveSpeed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Square")
        {
            _resourceManager.Health -= 1;
            _enemySpawner.RemoveEnemy(gameObject.GetComponent<TestDoctor>());
            Destroy(gameObject);
        }
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }

}
