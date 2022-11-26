using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class ResourceManager : MonoBehaviour
{
    public TextMeshProUGUI AppleResource;
    public  int Apples { get; set; } = 1;

    public TextMeshProUGUI HealthResource;
    public int Health { get; set; } = 10;
    public  int AppleMultiplier { get; set; } = 1;

    private ITimer _timer;

    [Inject]
    private void Construct(ITimer timer)
    {
        _timer = timer;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _timer.RunTimer(5f, () => Apples += AppleMultiplier);
        AppleResource.text = $"Apples: {Apples}";

        HealthResource.text = $"Health: {Health}";

        if (Health <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }
}
