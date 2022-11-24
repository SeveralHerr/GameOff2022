using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static UnityEngine.GraphicsBuffer;

public class InputManager : MonoBehaviour
{
    public SelectableObject SelectedObject;
    private IFactory<IPlaceable> SelectedFactory;

    [SerializeField]
    private SelectableObject OpenUpgradeWindow;

    public List<SelectableObject> SelectableObjects;


    private GridManager _gridManager;
    private AppleShooter.Factory _factory;
    private GreenAppleShooter.Factory _greenFactory;
    private TreeResource.Factory _treeFactory;

    public Button TestButton;


    [Inject]
    private void Construct(GridManager gridManager, AppleShooter.Factory factory, GreenAppleShooter.Factory greenAppleFactory, TreeResource.Factory treeFactory)
    {
        _gridManager = gridManager;
        _factory = factory;
        _greenFactory = greenAppleFactory;
        _treeFactory = treeFactory;
    }

    public void OnLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClearWindows();
            
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
                


                var tileSelectableObject = _gridManager.GetSelectableObject(mousePosition);
                var turret = tileSelectableObject.Prefab.GetComponentInChildren<Turret>();

                OpenUpgradeWindow = tileSelectableObject;

                turret.UpgradeButton.gameObject.SetActive(true);
                turret.DestroyButton.gameObject.SetActive(true);

               // TestButton = turrent.UpgradeButton;
                //TestButton.onClick.AddListener(() => Debug.Log("test"));

                var test = turret.DestroyButton.onClick.GetPersistentEventCount();

                Debug.Log(test);


            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            ClearWindows();
            ClearSelected();
        }
    }


    private void ClearWindows()
    {
        if (OpenUpgradeWindow == null)
        {
            return;
        }

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //if (_gridManager.)
        
        var turret = OpenUpgradeWindow.Prefab.GetComponentInChildren<Turret>();
        turret.UpgradeButton.gameObject.SetActive(false);
        turret.DestroyButton.gameObject.SetActive(false);
    }
    
    private SelectableObject SetupSelectedPlaceableObject(IPlaceable placeable)
    {
        var newSelectableObject = (SelectableObject)ScriptableObject.CreateInstance(nameof(SelectableObject));
        newSelectableObject.Type = SelectableType.Placeable;
        newSelectableObject.Prefab = placeable.GetGameObject();
        newSelectableObject.Buttons = new List<Button>();
  

        var buttons = newSelectableObject.Prefab.gameObject.GetComponentInChildren<Turret>();

        newSelectableObject.Buttons.Add(buttons.DestroyButton);
        newSelectableObject.Buttons.Add(buttons.UpgradeButton);
        newSelectableObject.Buttons.Add(buttons.MainButton);

        AddSelectableObject(newSelectableObject);

        return newSelectableObject;
    }


    private void OnClick(SelectableObject obj)
    {
        if (obj.Type == SelectableType.Placeable)
        {
            OnTurrentClick(obj);
        }

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

        Debug.Log($"{obj.Prefab.name} was clicked");

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

        //_gridManager.ClearTile(position);
        //SelectableObjects.Remove(selectableObject);
    }
}