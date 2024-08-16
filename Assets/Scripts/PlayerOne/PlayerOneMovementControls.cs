using System.Collections;
using UnityEngine;

public class ShipTranslate : MonoBehaviour
{
    [SerializeField]
    private Transform myTransform;

    public float speed;
    public float dashSpeed; // Dash speed
    public float dashDuration; // Duration of the dash
    private float dashTime; // Time remaining before the dash ends
    private bool isDashing; // Indicates if the player is currently dashing
    private bool hasDashed;

    [SerializeField]
    private float dashCooldown = 2f;
    public float DashCooldown { get { return dashCooldown; } }

    private float timeRemaining;
    public float TimeRemaining { get { return timeRemaining; } }

    // Define Map Borders
    [SerializeField]
    public Transform mapBorderBL; // Bottom-left corner of the map
    [SerializeField]
    public Transform mapBorderTR; // Top-right corner of the map

    void Update()
    {
        if (IsReady())
        {
            // Check if the right mouse button is pressed
            if (Input.GetMouseButtonDown(1) && !isDashing)
            {
                StartCoroutine(Dash());
            }
        }

        // Calculate movements
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Adjust speed during dash
        float currentSpeed = isDashing ? dashSpeed : speed;

        // Calculate the intended movement
        Vector3 move = new Vector3(moveX, moveY, 0) * currentSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + move;

        // Clamp the position to stay within map borders
        newPosition.x = Mathf.Clamp(newPosition.x, mapBorderBL.position.x, mapBorderTR.position.x);
        newPosition.y = Mathf.Clamp(newPosition.y, mapBorderBL.position.y, mapBorderTR.position.y);

        // Apply new position
        transform.position = newPosition;
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        hasDashed = true;
        dashTime = dashDuration;

        while (dashTime > 0)
        {
            dashTime -= Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }

    private bool IsReady()
    {
        if (timeRemaining > 0 && hasDashed)
        {
            timeRemaining -= Time.deltaTime;
            return false;
        }
        else
        {
            hasDashed = false;
            timeRemaining = dashCooldown;
            return true;
        }
    }
}
