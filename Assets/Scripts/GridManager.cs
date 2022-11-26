using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[System.Serializable]
public class TileProperty
{
    public int X;
    public int Y;

    public SelectableObject SelectableObject;
    
    public bool Occupied = false;
    public bool IsPath = false;
}

public class GridManager : MonoBehaviour
{
    [SerializeField]
    public TileGrid TileGrid;
    public Tilemap Tilemap;

    private void Start()
    {
        TileGrid.TileProperties = new GridFactory().Create(TileGrid, Tilemap);

        //var pos = GetWorldPosition(2,2);
        //CreateWorldText($"2,2", null, GetCenterOfTile(pos), 100, TextAlignment.Center, TextAnchor.MiddleCenter, Color.white, 99);
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * TileGrid.CellSize + TileGrid.OriginPosition;
    }

    public Vector3 GetCenterOfTile(Vector3 position)
    {
        var cellPosition = Tilemap.WorldToCell(position);
        return Tilemap.GetCellCenterWorld(cellPosition);
    }

    //public Vector3 GetTile(Vector3 position)
    //{
    //    var pos = Tilemap.WorldToCell(position);
    //    return (pos.x, pos.y);
    //}

    public void SetTile(int x, int y, SelectableObject selectableObject = null)
    {
        TileGrid.TileProperties[(x, y)].Occupied = true;
        TileGrid.TileProperties[(x, y)].SelectableObject = selectableObject;
    }

    public void SetTile(Vector3 position, SelectableObject selectableObject = null)
    {
        var cellPosition = Tilemap.WorldToCell(position);
        var x = cellPosition.x;
        var y = cellPosition.y;
       
        TileGrid.TileProperties[(x, y)].Occupied = true;
        TileGrid.TileProperties[(x, y)].SelectableObject = selectableObject;
    }

    public void ClearTile(Vector3 position)
    {
        var cellPosition = Tilemap.WorldToCell(position);
        var x = cellPosition.x;
        var y = cellPosition.y;
        TileGrid.TileProperties[(x, y)].Occupied = false;
        TileGrid.TileProperties[(x, y)].SelectableObject = null;
    }

    public bool IsOccupied(Vector3 position)
    {
        var cellPosition = Tilemap.WorldToCell(position);

        if (!TileGrid.TileProperties.ContainsKey((cellPosition.x, cellPosition.y)))
        {
            return false;
        }

        var tileProperty = TileGrid.TileProperties[(cellPosition.x, cellPosition.y)];
        return tileProperty.Occupied;
    }
    
    public SelectableObject GetSelectableObject(Vector3 position)
    {
        var cellPosition = Tilemap.WorldToCell(position);
        var tileProperty = TileGrid.TileProperties[(cellPosition.x, cellPosition.y)];
        return tileProperty.SelectableObject;
    }
    //public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, TextAlignment textAlignment = TextAlignment.Center, TextAnchor textAnchor = TextAnchor.MiddleCenter, Color? color = null, int sortingOrder = 0)
    //{
    //    if (color == null)
    //    {
    //        color = Color.white;
    //    }
    //    return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    //}

    //public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    //{
    //    var gameObject = new GameObject("World_Text", typeof(TextMesh));
    //    var transform = gameObject.transform;
    //    transform.SetParent(parent, false);
    //    transform.localPosition = localPosition;

    //    var textMesh = gameObject.GetComponent<TextMesh>();
    //    textMesh.anchor = textAnchor;
    //    textMesh.alignment = textAlignment;
    //    textMesh.text = text;
    //    textMesh.fontSize = fontSize;
    //    textMesh.color = color;
    //    textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

    //    return textMesh;
    //}

}


public class GridFactory
{
    public Dictionary<(int x, int y), TileProperty> Create(TileGrid tileGridProperties, Tilemap tilemap)
    {
        tileGridProperties.TileProperties = new Dictionary<(int x, int y), TileProperty>();

        var dictionary = new Dictionary<(int x, int y), TileProperty>();
           
        var startCell = tilemap.cellBounds.min;
        for (int x = startCell.x; x < tileGridProperties.Width; x++)
        {
            for (int y = startCell.y; y < tileGridProperties.Height; y++)
            {
                var occupied = false;
                var tile = tilemap.GetTile(new Vector3Int(x, y, 0));

                if (tile != null)
                {
                    occupied = true;
                }
                
                dictionary.Add((x, y), new TileProperty
                {
                    Occupied = occupied
                });
            }
        }
        
        return dictionary;
    }
}