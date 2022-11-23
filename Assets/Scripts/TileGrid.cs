using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "so_TileGrid", menuName = "ScriptableObjects/TileGrid")]
public class TileGrid : ScriptableObject
{
    public int Width;
    public int Height;
    public int OriginX;
    public int OriginY;

    [SerializeField]
    public Vector3 OriginPosition;
    public float CellSize = 32f;

    public int Count => Width * Height;

    [SerializeField]
    public  Dictionary<(int x, int y), TileProperty> TileProperties;
}

[CreateAssetMenu(fileName = "so_EnemyPath", menuName = "ScriptableObjects/EnemyPath")]
public class EnemyPath : ScriptableObject
{
    public List<(int x, int y)> Path;


}

