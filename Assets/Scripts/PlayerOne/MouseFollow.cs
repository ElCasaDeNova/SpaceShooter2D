using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public Texture2D cursorTexture;

    private Vector3 mousePosition;

    void Start()
    {
        // Hide System cursor
        Cursor.visible = false;
    }

    void Update()
    {
#if UNITY_EDITOR

        if (Cursor.visible)
        {
            Cursor.visible = false;
        }
#endif

        // Get mouse position from world point
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate direction from player to mouse position
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Apply rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Subtract 90 degres because of player spawned direction
    }

    // Used for GUI
    void OnGUI()
    {
        // Apply Cursor texture on mouse position
        if (cursorTexture != null)
        {
            Vector3 cursorPos = Input.mousePosition;
            cursorPos.y = Screen.height - cursorPos.y; // Inverse axe Y
            GUI.DrawTexture(new Rect(cursorPos.x - cursorTexture.width / 2, cursorPos.y - cursorTexture.height / 2, cursorTexture.width, cursorTexture.height), cursorTexture);
        }
    }
}
