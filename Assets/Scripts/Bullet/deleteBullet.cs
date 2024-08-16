using UnityEngine;

public class DeleteBullet : MonoBehaviour
{
    private Camera mainCamera;
    private Renderer bulletRenderer;

    private void Start()
    {
        // Find the main camera in the scene
        mainCamera = Camera.main;
        // Get the Renderer component from this bullet
        bulletRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Destroy Bullet if out of vision
        if (IsInvisibleToCamera(mainCamera))
        {
            Destroy(gameObject); // Destroy the bullet itself
        }
    }

    private bool IsInvisibleToCamera(Camera camera)
    {
        // Calculate the camera's frustum planes
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        // Test if the object's bounds are outside the frustum planes
        return !GeometryUtility.TestPlanesAABB(planes, bulletRenderer.bounds);
    }
}
