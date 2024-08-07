using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShipTranslate : MonoBehaviour
{
    [SerializeField]
    private Transform myTransform;

    public float speed;

    void Start()
    {

    }

    // Déplace le personnage sur les axes X et Y en fonction de la vitesse donnée et des touches enfoncées
    void Update()
    {
        // Aller vers le Haut
        if (Input.GetAxisRaw("Vertical") ==1)
        {
            // Déplace le personnage vers le haut en fonction de la vitesse et du temps écoulé depuis la dernière frame
            transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
        }

        // Aller vers le Bas
        if (Input.GetAxisRaw("Vertical") == -1)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
        }

        // Aller vers la Droite
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);
        }

        // Aller vers la Gauche
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
        }
    }
}
