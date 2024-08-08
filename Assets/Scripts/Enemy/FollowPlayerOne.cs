using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerOne : MonoBehaviour
{
    [SerializeField]
    private Transform myTransform;

    [SerializeField]
    private Transform playerOne;

    private Vector3 playerPosition;

    void Start()
    {

    }

    void Update()
    {
        // Récupérer la position du vaisseau en coordonnées du monde
        playerPosition = playerOne.position;

        // Calculer la direction de l'ennemis vers le vaisseau
        Vector3 direction = (playerPosition - transform.position).normalized;

        // Appliquer la rotation en fonction de la direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90 )); // Ajouter 90 degr�s car le croiseur pointe par d�faut vers le bas
    }
}