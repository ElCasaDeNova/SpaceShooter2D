using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Button menuButton;

    void Start()
    {
        menuButton.onClick.AddListener(OnStartButtonClick);
    }

    void OnStartButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}