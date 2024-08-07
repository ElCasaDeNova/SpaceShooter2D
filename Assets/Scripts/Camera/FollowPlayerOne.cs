using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Le vaisseau que la caméra doit suivre
    public float smoothSpeed = 0.125f; // Vitesse de lissage du mouvement
    public Vector3 offset; // Décalage de la caméra par rapport au vaisseau

    void LateUpdate()
    {
        // Calculer la position désirée
        Vector3 desiredPosition = target.position + offset;
        // Lissage du mouvement pour éviter des secousses
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Appliquer la position lissée à la caméra
        transform.position = smoothedPosition;
    }
}
