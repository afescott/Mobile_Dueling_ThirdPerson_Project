using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformToMordor : MonoBehaviour
{
    private float accelx;
    public Transform target;
    private float accely;
    private float accelz;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        accelx = Input.acceleration.x;
        accely = Input.acceleration.y;
        accelz = Input.acceleration.z;
        target.Rotate(accelx * Time.deltaTime, 
            accely * Time.deltaTime,
            accelz * Time.deltaTime);
    }
}
