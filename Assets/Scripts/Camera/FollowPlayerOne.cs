using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Le vaisseau que la caméra doit suivre
    public float smoothSpeed = 0.125f; // Vitesse de lissage du mouvement avec valeur par défaut
    public Vector3 offset; // Décalage de la caméra par rapport au vaisseau

    // Définir les limites de la carte pour la caméra
    public Transform topRightLimit;
    public Transform bottomLeftLimit;

    private Camera cam;

    void Start()
    {
        // Obtenir la caméra
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        // Calculer la position désirée
        Vector3 desiredPosition = target.position + offset;

        // Calculer la taille de la vue de la caméra
        float camHeight = 2f * cam.orthographicSize; // La hauteur de la caméra
        float camWidth = camHeight * cam.aspect; // La largeur de la caméra

        // Limiter la position de la caméra en tenant compte de la taille de la caméra
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, bottomLeftLimit.position.x + camWidth / 2, topRightLimit.position.x - camWidth / 2);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, bottomLeftLimit.position.y + camHeight / 2, topRightLimit.position.y - camHeight / 2);

        // Lissage du mouvement pour éviter des secousses
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Appliquer la position lissée à la caméra
        transform.position = smoothedPosition;
    }
}
