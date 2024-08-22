using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    // Called during intilization
    void Awake()
    {
        if (FindObjectsOfType<BackGroundMusic>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
