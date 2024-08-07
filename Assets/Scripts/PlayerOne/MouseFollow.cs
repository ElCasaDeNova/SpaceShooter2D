using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public Texture2D cursorTexture; // Texture pour le viseur personnalisé

    private Vector3 mousePosition;

    void Start()
    {
        // Masquer le curseur système
        Cursor.visible = false;
    }

    void Update()
    {
        // Récupérer la position de la souris en coordonnées du monde
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculer la direction du vaisseau vers la souris
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Appliquer la rotation en fonction de la direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Soustraire 90 degrés car le vaisseau pointe par défaut vers le haut
    }

    // S'applique à la GUI
    void OnGUI()
    {
        // Afficher le viseur personnalisé à la position de la souris
        if (cursorTexture != null)
        {
            Vector3 cursorPos = Input.mousePosition;
            cursorPos.y = Screen.height - cursorPos.y; // Inverser l'axe Y
            GUI.DrawTexture(new Rect(cursorPos.x - cursorTexture.width / 2, cursorPos.y - cursorTexture.height / 2, cursorTexture.width, cursorTexture.height), cursorTexture);
        }
    }
}
