using UnityEngine;
using UnityEngine.UI;

public class ClickToStart : MonoBehaviour
{
    private Button Button;

    private void Start()
    {
        Button = GetComponentInChildren<Button>();
        Button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
