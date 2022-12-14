using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GridTile
{
    //public int X;
    //public int Y;\

    public IPlaceable PlaceableObject;
}

[Serializable]
public class Position
{
    
    public int x;
    public int y;
}

public class NotPlaceable : IPlaceable
{
    public GameObject GetGameObject()
    {
        throw new NotImplementedException();
    }
}


public class Grid : SingletonMonobehavior<Grid>
{
    public  int _width;
    public  int _height;
    
    private GridTile[,] _gridArray;
    public float _cellSize;
    private TextMesh[,] _debugTextArray;
    public Vector3 _originPosition;
    public  (int x, int y)[] EnemyPath = new (int x, int y)[18];

    public List<Position> NotPlaceableTiles;// = new List<(int x, int y)>();


    public void Start()
    {
        _gridArray = new GridTile[_width, _height];
        _debugTextArray = new TextMesh[_width, _height];
        

        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                _gridArray[x, y] = new GridTile();
                if(NotPlaceableTiles.Any(s => s.x == x && s.y == y))
                {
                    _gridArray[x, y].PlaceableObject = new NotPlaceable();
                }

                //_debugTextArray[x, y] = CreateWorldText($"{x},{y}", null, GetWorldPosition(x, y) + new Vector3(_cellSize, _cellSize) * 0.5f, 100, TextAlignment.Center, TextAnchor.MiddleCenter, Color.white, 99);
               // Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }

        EnemyPath[0] = (16, 4);
        EnemyPath[1] = (15, 4);
        EnemyPath[2] = (14, 4);
        EnemyPath[3] = (13, 4);
        EnemyPath[4] = (12, 4);
        EnemyPath[5] = (11, 4);
        EnemyPath[6] = (10, 4);
        EnemyPath[7] = (9, 4);
        EnemyPath[8] = (8, 4);
        EnemyPath[9] = (8, 5);
        EnemyPath[10] = (7, 5);
        EnemyPath[11] = (6, 5);
        EnemyPath[12] = (5, 5);
        EnemyPath[13] = (4, 5);
        EnemyPath[14] = (3, 5);
        EnemyPath[15] = (2, 5);
        EnemyPath[16] = (1, 5);
        EnemyPath[17] = (0, 5);
    }
    public void Create(int width, int height, float cellSize, Vector3 originPosition)
    {
        _width = width;
        _height = height;
        _gridArray = new GridTile[width, height];
        _debugTextArray = new TextMesh[_width, _height];
        _cellSize = cellSize;
        _originPosition = originPosition;
        Debug.Log($"Grid created with width { width} and height { height}");

        
        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                _gridArray[x, y] = new GridTile();
                _debugTextArray[x, y] =  CreateWorldText("T", null, GetWorldPosition(x, y) + new Vector3(_cellSize, _cellSize) * 0.5f, 200, TextAlignment.Center, TextAnchor.MiddleCenter, Color.white, 0);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.white, 100f);

        //SetValue(2, 1, 56);

        EnemyPath[0]=(9, 4);
        EnemyPath[1]=(8, 4);
        EnemyPath[2]=(7, 4);
        EnemyPath[3]=(6, 4);
        EnemyPath[4]=(5, 4);
        EnemyPath[5] =(4, 4);
        EnemyPath[6]=(4, 5);
        EnemyPath[7]=(3, 5);
        EnemyPath[8]=(2, 5);
        EnemyPath[9]=(1, 5);
        EnemyPath[10]=(0, 5);
    }

    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, TextAlignment textAlignment = TextAlignment.Center, TextAnchor textAnchor = TextAnchor.MiddleCenter, Color? color = null, int sortingOrder = 0)
    {
        if (color == null)
        {
            color = Color.white;
        }
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }

    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        var gameObject = new GameObject("World_Text", typeof(TextMesh));
        var transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;

        var textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

        return textMesh;
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * _cellSize + _originPosition;
    }

    public Vector3 GetWorldPosition2(int x, int y)
    {
        return new Vector3(x, y) * _cellSize + _originPosition;
    }

    public (int x, int y) GetXY(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition - _originPosition ).x/ _cellSize);
        int y = Mathf.FloorToInt((worldPosition - _originPosition).y / _cellSize);
        return (x, y);
    }
    
    public void SetValue(int x, int y, IPlaceable value)
    {
        if(x >= 0 && y >= 0 && x < _width && y < _height)
        {
            _gridArray[x, y].PlaceableObject = value;
            //_debugTextArray[x, y].text = _gridArray[x, y].ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, IPlaceable value)
    {
        var pos = GetXY(worldPosition);
        SetValue(pos.x, pos.y, value);
    }

    public IPlaceable GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            return _gridArray[x, y].PlaceableObject;
        }
        else
        {
            return null;
        }
    }

    public IPlaceable GetValue(Vector3 worldPosition)
    {
        var pos = GetXY(worldPosition);
        return GetValue(pos.x, pos.y);
    }

    public Vector3 ValidateWorldGridPosition(Vector3 worldPosition)
    {
        var pos = GetXY(worldPosition);
        return GetWorldPosition(pos.x, pos.y) + new Vector3(_cellSize / 2, _cellSize / 2);
    }

    public Vector3 GetEnemyPathWorldPosition(int path, Vector3 offset)
    {
        var worldPos = GetWorldPosition(EnemyPath[path].x, EnemyPath[path].y);
        return worldPos + offset;
    }

    public Vector3 GetEnemyPathWorldPosition(int path)
    {
        return GetEnemyPathWorldPosition(path, Vector3.zero);
    }
}
