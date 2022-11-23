using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InputManager : MonoBehaviour
{
    public SelectableObject SelectedObject;
    private IFactory<IPlaceable> SelectedFactory;

    public List<SelectableObject> SelectableObjects;


    private GridManager _gridManager;
    private AppleShooter.Factory _factory;
    private GreenAppleShooter.Factory _greenFactory;
    private TreeResource.Factory _treeFactory;


    [Inject]
    private void Construct(GridManager gridManager, AppleShooter.Factory factory, GreenAppleShooter.Factory greenAppleFactory, TreeResource.Factory treeFactory)
    {
        _gridManager = gridManager;
        _factory = factory;
        _greenFactory = greenAppleFactory;
        _treeFactory = treeFactory;
    }

    //private void Start()
    //{
    //    foreach(var obj in SelectableObjects)
    //    {
    //        foreach(var button in obj.Buttons)
    //        {
    //            button.onClick.AddListener(() => OnClick(obj));
    //        }
    //    }
    //}

    public void OnLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log($"Tile: {_gridManager.GetSelectableObject(mousePosition)?.Type}"); 

            if(SelectedObject != null)
            {
                if (_gridManager.IsOccupied(mousePosition))
                {
                    return;
                }
                
                var shooter = SelectedFactory.Create();
                shooter.GetGameObject().transform.position = _gridManager.GetCenterOfTile(mousePosition);

                var newObj = SetupSelectedPlaceableObject(shooter);

                _gridManager.SetTile(mousePosition, newObj);

                ClearSelected();
            }

            else if (_gridManager.IsOccupied(mousePosition) && SelectedObject == null && _gridManager.GetSelectableObject(mousePosition)?.Type == SelectableType.Placeable)
            {
                Debug.Log($"Tile: {_gridManager.GetSelectableObject(mousePosition)?.Type}");

                var tile = _gridManager.GetSelectableObject(mousePosition);
                var test = tile.GetComponentInChildren<Turret>();

                test.UpgradeButton.gameObject.SetActive(true);
                test.DestroyButton.gameObject.SetActive(true);
                //test.MainButton.gameObject.SetActive(false);

               // foreach (var button in tile.Buttons)
                //{
                //    if (button.gameObject.tag == "AppleCannonUI")
                //    {
                //        button.gameObject.SetActive(true);
                //    }
                //    else if (button.gameObject.tag == Tags.DestroySign.ToString())
                //    {
                //        button.gameObject.SetActive(true);
                //    }
                //    else
                //    {
                //        button.gameObject.SetActive(false);
                //    }
                //}
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            ClearSelected();
        }
    }

    private SelectableObject SetupSelectedPlaceableObject(IPlaceable placeable)
    {
        var newSelectableObject = (SelectableObject)ScriptableObject.CreateInstance(nameof(SelectableObject));
        newSelectableObject.Type = SelectableType.Placeable;
        newSelectableObject.Prefab = placeable.GetGameObject();
        newSelectableObject.Buttons = new List<Button>();
  

        var buttons = newSelectableObject.Prefab.gameObject.GetComponentsInChildren<Button>();
        foreach (var button in buttons)
        {
            newSelectableObject.Buttons.Add(button);
        }

        newSelectableObject.Buttons.Add(newSelectableObject.Prefab.gameObject.GetComponent<Button>());

        AddSelectableObject(newSelectableObject);

        return newSelectableObject;
    }


    private void OnClick(SelectableObject obj)
    {
        //if (obj.Type == SelectableType.UI)
        //{
        //    OnTurrentClick();
        //}

        if (obj.Type == SelectableType.TreeResource)
        {
            OnFactoryClick(_treeFactory, obj);
        }

        if (obj.Type == SelectableType.AppleShooter)
        {
            OnFactoryClick(_factory, obj);
        }

        if (obj.Type == SelectableType.GreenAppleShooter)
        {
            OnFactoryClick(_greenFactory, obj);
        }
    }

    public void OnFactoryClick(IFactory<IPlaceable> factory, SelectableObject obj)
    {
        //var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //var shooter = factory.Create();
        //shooter.GetGameObject().transform.position = _gridManager.GetCenterOfTile(mousePosition);

        //_gridManager.SetTile(mousePosition, obj);

        SelectedFactory = factory;
        SelectedObject = obj;

        SelectedObject.MouseOverPrefab.SetActive(true);
    }

    public void UpdateMouseover()
    {
        if (SelectedObject == null)
        {
            return;
        }

        if(SelectedObject?.MouseOverPrefab != null)
        {
            HandleMouseoverColors();
        }

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SelectedObject.MouseOverPrefab.transform.position = _gridManager.GetCenterOfTile(mousePosition);
    }

    private void ClearSelected()
    {
        SelectedObject?.MouseOverPrefab?.SetActive(false);
        SelectedObject = null;
        SelectedFactory = null;
    }

    public void OnTurrentClick(SelectableObject obj)
    {
        // upgrade / destroy 

    }

    private void HandleMouseoverColors()
    {
        var sprites = SelectedObject.MouseOverPrefab.GetComponentsInChildren<SpriteRenderer>();

        foreach (var sprite in sprites)
        {
            if (sprite.gameObject.name == "Range")
            {
                continue;
            }

            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!_gridManager.IsOccupied(mousePosition))
            {
                if (SelectedObject.Prefab != null )//&& SelectedCost > _resourceManager.Apples)
                {
                    sprite.color = Color.red;
                }
                else
                {
                    sprite.color = Color.green;
                }
            }
            else
            {
                sprite.color = Color.red;
            }
        }
    }


    public void AddSelectableObject(SelectableObject selectableObject, Vector3 position)
    {
        foreach (var button in selectableObject.Buttons)
        {
            button.onClick.AddListener(() => OnClick(selectableObject));
        }

        _gridManager.SetTile(position, selectableObject);
        //SelectableObjects.Add(selectableObject);
    }

    public void AddSelectableObject(SelectableObject selectableObject)
    {

        foreach (var button in selectableObject.Buttons)
        {
            button.onClick.AddListener(() => OnClick(selectableObject));
        }

        //SelectableObjects.Add(selectableObject);
    }

    public void RemoveSelectableObject(Vector3 position, SelectableObject selectableObject)
    {
        foreach (var button in selectableObject.Buttons)
        {
            button.onClick.RemoveAllListeners();
        }

        _gridManager.ClearTile(position);
        //SelectableObjects.Remove(selectableObject);
    }
}