using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AppleCannonUI : MonoBehaviour
{
    public Button AppleButton;
    public Button GreenAppleButton;

    public GameObject ApplePrefab;
    public GameObject MouseOverPrefab;
    private AppleShooter.Factory _factory;

    public GameObject GreenApplePrefab;
    public GameObject GreenMouseOverPrefab;
    private GreenAppleShooter.Factory _greenFactory;


    public GameObject SelectedPrefab = null;
    public GameObject SelectedMouseOverPrefab = null;
    public IFactory<IPlaceable> SelectedFactory = null;
    public int SelectedCost = 0;


    private ResourceManager _resourceManager;
    private bool isAppleShooterActive = false;




    [Inject]
    private void Construct(AppleShooter.Factory factory, GreenAppleShooter.Factory greenAppleFactory, ResourceManager resourceManager)
    {
        _factory = factory;
        _greenFactory = greenAppleFactory;
        _resourceManager = resourceManager;
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
                    if(SelectedPrefab != null && SelectedCost > ResourceManager.Apples)
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
            if (SelectedPrefab == null)
            {
                return;
            }
            
            if(SelectedCost > ResourceManager.Apples)
            {
                return; 
            }

            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Grid.Instance.GetValue(mousePosition) == null)
            {
                var shooter = SelectedFactory.Create();
                shooter.GetGameObject().transform.position = Grid.Instance.ValidateWorldGridPosition(mousePosition);

                ResourceManager.Apples -= SelectedCost;

                Grid.Instance.SetValue(mousePosition, shooter);
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            SelectedMouseOverPrefab.SetActive(false);
            SelectedPrefab = null;
            SelectedMouseOverPrefab = null;
            SelectedCost = 0;
            SelectedFactory = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AppleButton.onClick.AddListener(() => OnClick());
        GreenAppleButton.onClick.AddListener(() => GreenOnClick());
    }


    private void OnClick()
    {
        SelectedPrefab = ApplePrefab;
        SelectedMouseOverPrefab = MouseOverPrefab;
        SelectedFactory = _factory;
        SelectedCost = 1;
    }

    private void GreenOnClick()
    {
        SelectedPrefab = GreenApplePrefab;
        SelectedMouseOverPrefab = GreenMouseOverPrefab;
        SelectedFactory = _greenFactory;
        SelectedCost = 2;
    }
}
