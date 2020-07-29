using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public const float MAX_FORCE = 500f;

    public Transform firePoint;
    public GameObject bulletFab;

    public GameObject player;

    public void Fire(float force)
    {


    }

    // Start is called before the first frame update
    void Update()
    {



     

        //var k = BulletFab;

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();


        }

    }

    void Shoot()
    {
        //firePoint.rotation = new Quaternion(0.0f, -0.1f, 0.0f, -0.7f  );

      



    }
}
