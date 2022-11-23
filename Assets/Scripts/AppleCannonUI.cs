using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AppleCannonUI : MonoBehaviour
{
    public Button AppleButton;
    public Button TreeButton;
    public Button GreenAppleButton;

    public GameObject ApplePrefab;
    public GameObject MouseOverPrefab;
    private AppleShooter.Factory _factory;

    public GameObject GreenApplePrefab;
    public GameObject GreenMouseOverPrefab;
    private GreenAppleShooter.Factory _greenFactory;

    public GameObject TreePrefab;
    public GameObject TreeMouseOverPrefab;
    private TreeResource.Factory _treeFactory;


    public GameObject SelectedPrefab = null;
    public GameObject SelectedMouseOverPrefab = null;
    public IFactory<IPlaceable> SelectedFactory = null;
    public int SelectedCost = 0;


    private ResourceManager _resourceManager;
    private bool isAppleShooterActive = false;




    [Inject]
    private void Construct(AppleShooter.Factory factory, GreenAppleShooter.Factory greenAppleFactory, ResourceManager resourceManager, TreeResource.Factory treeFactory)
    {
        _factory = factory;
        _greenFactory = greenAppleFactory;
        _resourceManager = resourceManager;
        _treeFactory = treeFactory;
    }


    // Update is called once per frame
    void Update()
    {
        if(SelectedPrefab != null)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            SelectedMouseOverPrefab.SetActive(true);
            SelectedMouseOverPrefab.transform.position = Grid.Instance.ValidateWorldGridPosition(mousePosition);

            var sprites = SelectedMouseOverPrefab.GetComponentsInChildren<SpriteRenderer>();

            foreach (var sprite in sprites)
            {
                if(sprite.gameObject.name == "Range")
                {
                    continue;
                }

                if (Grid.Instance.GetValue(mousePosition) == null)
                {
                    if(SelectedPrefab != null && SelectedCost > _resourceManager.Apples)
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

        
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (SelectedPrefab == null)
            {
                var grid_type = Grid.Instance.GetValue(mousePosition)?.GetType();
                if (grid_type == typeof(AppleShooter) || grid_type == typeof(GreenAppleShooter) || grid_type == typeof(TreeResource))
                {
                    Debug.Log("test");
                }
                return;
            }
            

            if (Grid.Instance.GetValue(mousePosition) == null)
            {
                if (_resourceManager.Apples - SelectedCost >= 0)
                {
                    _resourceManager.Apples -= SelectedCost;

                    var shooter = SelectedFactory.Create();
                    shooter.GetGameObject().transform.position = Grid.Instance.ValidateWorldGridPosition(mousePosition);

                    Grid.Instance.SetValue(mousePosition, shooter);
                }
            }
                
        }

        if(Input.GetMouseButtonDown(1))
        {   

            ClearSelection();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AppleButton.onClick.AddListener(() => OnClick());
        GreenAppleButton.onClick.AddListener(() => GreenOnClick());
        TreeButton.onClick.AddListener(() => TreeOnClick());
    }

    private void ClearSelection()
    {
        if (SelectedMouseOverPrefab != null)
        {
            SelectedMouseOverPrefab.SetActive(false);
        }
        SelectedPrefab = null;
        SelectedMouseOverPrefab = null;
        SelectedCost = 0;
        SelectedFactory = null;
        
    }

    private void OnClick()
    {
        ClearSelection();
        SelectedPrefab = ApplePrefab;
        SelectedMouseOverPrefab = MouseOverPrefab;
        SelectedFactory = _factory;
        SelectedCost = 1;
    }

    private void GreenOnClick()
    {
        ClearSelection();
        SelectedPrefab = GreenApplePrefab;
        SelectedMouseOverPrefab = GreenMouseOverPrefab;
        SelectedFactory = _greenFactory;
        SelectedCost = 2;
    }

    private void TreeOnClick()
    {
        ClearSelection();
        SelectedPrefab = TreePrefab;
        SelectedMouseOverPrefab = TreeMouseOverPrefab;
        SelectedFactory = _treeFactory;
        SelectedCost = 2;
    }
}
