using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTranslate : MonoBehaviour
{
    [SerializeField]
    private Transform myTransform;

    public float speed;

    // Define Map Borders
    [SerializeField]
    public Transform mapBorderBL;
    [SerializeField]
    public Transform mapBorderTR;

    void Update()
    {
        // Calculate movements
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Move the player
        Vector3 move = new Vector3(moveX, moveY, 0) * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + move;

        // Limit the position
        newPosition.x = Mathf.Clamp(newPosition.x, mapBorderBL.position.x, mapBorderTR.position.x);
        newPosition.y = Mathf.Clamp(newPosition.y, mapBorderBL.position.y, mapBorderTR.position.y);

        // Apply new position
        transform.position = newPosition;
    }
}
