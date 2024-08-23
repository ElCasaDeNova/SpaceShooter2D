using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The following Ship
    public float smoothSpeed = 0.125f;
    public Vector3 offset; // Gap between Cam and Player speed

    // Define Map Borders
    public Transform topRightLimit;
    public Transform bottomLeftLimit;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        // Calculate desired position
        Vector3 desiredPosition = target.position + offset;

        // Calculate Height and Width of the camera
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Limit the Camera position based on its height, width and the Map borders
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, bottomLeftLimit.position.x + camWidth / 2, topRightLimit.position.x - camWidth / 2);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, bottomLeftLimit.position.y + camHeight / 2, topRightLimit.position.y - camHeight / 2);

        // Smoothing the movement to avoid shaking
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply smooth movements
        transform.position = smoothedPosition;
    }
}
