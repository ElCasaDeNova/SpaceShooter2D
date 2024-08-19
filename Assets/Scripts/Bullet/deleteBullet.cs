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
        // Retourne la balle au pool si elle est hors de la vue
        if (IsInvisibleToCamera(mainCamera))
        {
            BulletPooler.Instance.ReturnBullet(gameObject); // Retourne la balle au pool
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
