using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AppleShooterFactory : MonoBehaviour
{
    public Vector3 _targetPosition;
    private Vector3 _spawnPosition;
    
    public class Factory : PlaceholderFactory<string, AppleShooterFactory>
    {
        public AppleShooterFactory Create()
        {
            return base.Create($"Prefabs/Apple");
        }
    }

    void Start()
    {
        
    }
    
    public void Setup(Vector3 spawnPosition, Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        _spawnPosition = spawnPosition;

        transform.position = _spawnPosition;
    }

    void Update()
    {
        var moveDirection = (_targetPosition - _spawnPosition).normalized;

        var movespeed = 500f;

        transform.position += moveDirection * movespeed * Time.deltaTime;

        //var destroySelfDistance = 1f;
        //if (Vector3.Distance(transform.position, _targetPosition) < destroySelfDistance)
        //{
        //    Destroy(gameObject);
        //}
    }
}
