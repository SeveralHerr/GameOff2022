using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AppleCannonUI : MonoBehaviour
{
    public Button button;
    public GameObject prefab;
    public GameObject appleMouseoverPrefab;
    public GameObject greenAppleMouseoverPrefab;
    private AppleShooter.Factory _factory;
    private GreenAppleShooter.Factory _greenAppleFactory;
    private bool isAppleShooterActive = false;


    private IPlaceable SelectedObject;

    [Inject]
    private void Construct(AppleShooter.Factory factory, GreenAppleShooter.Factory greenAppleFactory)
    {
        _factory = factory;
        _greenAppleFactory = greenAppleFactory;
    }


    // Update is called once per frame
    void Update()
    {
        if (isAppleShooterActive)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            appleMouseoverPrefab.transform.position = Grid.Instance.ValidateWorldGridPosition(mousePosition);
            //new Vector3(mousePosition.x, mousePosition.y, 0);

            var sprites = appleMouseoverPrefab.GetComponentsInChildren<SpriteRenderer>();

            foreach (var sprite in sprites)
            {
                if(Grid.Instance.GetValue(mousePosition) == null)
                {
                    sprite.color = Color.green;
                }
                else
                {
                    sprite.color = Color.red;
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(SelectedObject == null)
            {
                return;
            }

            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Grid.Instance.GetValue(mousePosition) == null)
            {
                SelectedObject.
                //grid.SetValue(mousePosition, 56);
                var shooter = _factory.Create();
                shooter.transform.position = Grid.Instance.ValidateWorldGridPosition(mousePosition);

                Grid.Instance.SetValue(mousePosition, shooter);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //button.onClick.AddListener(() => OnClick());
        var buttons = gameObject.GetComponentsInChildren<Button>();
        
        foreach(var button in buttons)
        {
            button.onClick.AddListener(() => OnClick(button.gameObject));
        }
    }


    private void OnClick(GameObject obj)
    {
        SelectedObject = obj.GetComponentInChildren<IPlaceable>();
    }
}
