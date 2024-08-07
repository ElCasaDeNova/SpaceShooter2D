using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTranslate : MonoBehaviour
{
    [SerializeField]
    private Transform myTransform;

    public float speed;

    // Définir les limites de la Carte 
    [SerializeField]
    public Transform mapBorderBL;
    [SerializeField]
    public Transform mapBorderTR;

    void Start()
    {
 
    }

    void Update()
    {
        // Calculer le mouvement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Déplacement du personnage
        Vector3 move = new Vector3(moveX, moveY, 0) * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + move;

        // Limiter la position
        newPosition.x = Mathf.Clamp(newPosition.x, mapBorderBL.position.x, mapBorderTR.position.x);
        newPosition.y = Mathf.Clamp(newPosition.y, mapBorderBL.position.y, mapBorderTR.position.y);

        // Appliquer la nouvelle position
        transform.position = newPosition;
    }
}
