using UnityEngine;

public class GenerateCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.None;
    }

}
