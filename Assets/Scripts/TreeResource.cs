using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TreeResource : MonoBehaviour, IPlaceable
{
    private ResourceManager _resourceManager;

    [Inject]
    private void Construct(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;
    }
    private void Start()
    {
        _resourceManager.AppleMultiplier += 1;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public class Factory : PlaceholderFactory<string, TreeResource>, IFactory<TreeResource>
    {
        public TreeResource Create()
        {
            return base.Create($"Prefabs/{nameof(TreeResource)}");
        }
    }
}
