using UnityEngine;
using Zenject;

public interface IEnemy
{
    public float MoveSpeed { get; set; }
    public Vector3 Position { get; set; }

    public GameObject GetGameObject();

    //public IFactory<TestDoctor> Create();

    public HealthBehavior HealthBehavior { get; set; }
}
