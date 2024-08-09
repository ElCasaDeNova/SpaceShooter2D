using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    public float bulletLifeTime;

    private float timeSinceShoot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceShoot += Time.deltaTime;

        if (timeSinceShoot >= bulletLifeTime)
        {
            Destroy(bullet);
        }
    }
}
