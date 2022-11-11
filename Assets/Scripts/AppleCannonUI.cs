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



    private bool isAppleShooterActive = false;




    [Inject]
    private void Construct(AppleShooter.Factory factory, GreenAppleShooter.Factory greenAppleFactory)
    {
        _factory = factory;
        _greenFactory = greenAppleFactory;
    }


    // Update is called once per frame
    void Update()
    {
        //if (isAppleShooterActive)
        //{
        //    var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //    MouseOverPrefab.SetActive(true);
        //    MouseOverPrefab.transform.position = Grid.Instance.ValidateWorldGridPosition(mousePosition);
        //    //new Vector3(mousePosition.x, mousePosition.y, 0);

        //    var sprites = MouseOverPrefab.GetComponentsInChildren<SpriteRenderer>();

        //    foreach (var sprite in sprites)
        //    {
        //        if (Grid.Instance.GetValue(mousePosition) == null)
        //        {
        //            sprite.color = Color.green;
        //        }
        //        else
        //        {
        //            sprite.color = Color.red;
        //        }
        //    }
        //}

        if(SelectedPrefab != null)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            MouseOverPrefab.SetActive(true);
            MouseOverPrefab.transform.position = Grid.Instance.ValidateWorldGridPosition(mousePosition);

            var sprites = MouseOverPrefab.GetComponentsInChildren<SpriteRenderer>();

            foreach (var sprite in sprites)
            {
                if (Grid.Instance.GetValue(mousePosition) == null)
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
            if (SelectedPrefab == null)
            {
                return;
            }
            
            
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Grid.Instance.GetValue(mousePosition) == null)
            {
                if(SelectedPrefab == ApplePrefab)
                {
                    var shooter = _factory.Create();
                    shooter.transform.position = Grid.Instance.ValidateWorldGridPosition(mousePosition);

                    Grid.Instance.SetValue(mousePosition, shooter);
                }
                else if (SelectedPrefab == GreenApplePrefab)
                {
                    var shooter = _greenFactory.Create();
                    shooter.transform.position = Grid.Instance.ValidateWorldGridPosition(mousePosition);

                    Grid.Instance.SetValue(mousePosition, shooter);
                }
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            MouseOverPrefab.SetActive(false);
            SelectedPrefab = null;
            MouseOverPrefab = null;
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
    }

    private void GreenOnClick()
    {
        SelectedPrefab = GreenApplePrefab;
        SelectedMouseOverPrefab = GreenMouseOverPrefab;
    }
}
