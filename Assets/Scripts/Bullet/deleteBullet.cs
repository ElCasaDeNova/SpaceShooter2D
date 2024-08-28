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
        // Return Bullet in BulletPooler if out of Camera range
        if (IsInvisibleToCamera(mainCamera))
        {
            BulletPooler.Instance.ReturnBullet(gameObject); 
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
