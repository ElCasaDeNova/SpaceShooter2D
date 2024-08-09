using UnityEngine;

public class CollidingScript : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object in collision has the player's tag
        /*if (other.CompareTag(playerTag))
        {
            // If it's the player's ship, do nothing
            return;
        }*/

        // deal with collision
        Debug.Log("Collision Trigger with " + other.gameObject.name); //Will later deal with damage when set

        //Delete the bullet
        Destroy(bullet);
    }
}
