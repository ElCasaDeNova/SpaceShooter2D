using UnityEngine;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour
{
    [SerializeField]
    private Button confirmButton;

    void Start()
    {
        confirmButton.onClick.AddListener(OnStartButtonClick);
    }

    void OnStartButtonClick()
    {
        // TODO
    }
}
