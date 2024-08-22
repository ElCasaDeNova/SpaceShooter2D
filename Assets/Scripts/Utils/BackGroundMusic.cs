using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    // Méthode appelée au moment où l'objet est initialisé
    void Awake()
    {
        // Vérifie si cet objet est déjà existant dans la scène
        if (FindObjectsOfType<BackGroundMusic>().Length > 1)
        {
            // Si un autre objet de ce type existe déjà, détruisez cet objet pour éviter les duplications
            Destroy(gameObject);
        }
        else
        {
            // Empêche cet objet d'être détruit lors du chargement d'une nouvelle scène
            DontDestroyOnLoad(gameObject);
        }
    }
}
