using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private Button Button;

    private void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
