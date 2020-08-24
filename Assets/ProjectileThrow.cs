using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileThrow : MonoBehaviour
{
    public float speed = 20f;

    public Button fireButton;

    public EventHandler eventHandler;

    public Rigidbody rb;

    public Transform firePoint;
    public GameObject bulletFab;

    private float holdDownStartTime = 0f;

    private float holdDownTime;

    private static float fireTime;

    private bool trueMate;

    private float timedInt;

    private static int ii = 0;

    private float CalculatePower(float holdTime)
    {
        float maxForceHoldDownTime = 2f;

        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxForceHoldDownTime);



        float force = holdTimeNormalized * 500f; //max force metric

        Debug.Log(force.ToString());

        return force;


    }


    void Start()
    {

        eventHandler += testingOnSpacePressed;

       //fireButton.onClick.AddListener()
    }


    private void testingOnSpacePressed(object sender, EventArgs e)
    {
        trueMate = true;

        ii += 1;

        //fireButton.GetComponent<"Butt">

        var j = Instantiate(rb, firePoint.position, firePoint.rotation);

        j.AddForce(firePoint.forward * (holdDownTime * 100) * (25));

        j.freezeRotation = true;

     


        fireTime = Time.time;

        Destroy(gameObject);

    }

    void LateUpdate()
    {

        if (trueMate)
        {




            trueMate = false;
        }

    }

    private void Update()
    {






        //checking if throw a and throw b are within 2 secs of each other

        if (Input.GetMouseButtonDown(0))
        {

            holdDownStartTime = Time.time;

           
        }

        
            if (Input.GetMouseButtonUp(0))
            {

                if ((Time.time - fireTime) < 2)
                {

                    Debug.Log("On cooldown");

                    return;

                }

            holdDownTime = Time.time - holdDownStartTime;

                if (holdDownTime > 0f && holdDownTime < 3f)
                {
                    //Destroy(gameObject);
                    timedInt = Time.time - holdDownStartTime;

                    eventHandler?.Invoke(this, EventArgs.Empty);

                    trueMate = true;

                    timedInt = Time.time;

                }

                else if (holdDownTime >= 3f)
                {


                    holdDownTime = 3f;
                    eventHandler?.Invoke(this, EventArgs.Empty);


                }

            
        }



    }

    private void SpawnWeapon()
    {

        rb.velocity = Vector3.forward;


    }


    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //Enemy enemy = hitInfo.GetComponent<Enemy>();
        //if (enemy != null)
        //{
        //    enemy.TakeDamage(damage);
        //}

        //Instantiate(rb, transform.position, transform.rotation);

        Destroy(gameObject);
    }


}
