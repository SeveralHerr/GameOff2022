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
    public GameObject mouseOverPrefab;
    private AppleShooter.Factory _factory;
    private bool isAppleShooterActive = false;




    [Inject]
    private void Construct(AppleShooter.Factory factory)
    {
        _factory = factory;

    }


    // Update is called once per frame
    void Update()
    {
        if (isAppleShooterActive)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseOverPrefab.transform.position = Grid.Instance.ValidateWorldGridPosition(mousePosition);
            //new Vector3(mousePosition.x, mousePosition.y, 0);

            var sprites = mouseOverPrefab.GetComponentsInChildren<SpriteRenderer>();

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
            if(!isAppleShooterActive)
            {
                return; 
            }
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Grid.Instance.GetValue(mousePosition) == null)
            {

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
        button.onClick.AddListener(() => OnClick());
    }


    private void OnClick()
    {
        Debug.Log("click");
        isAppleShooterActive = true;
    }
}
