using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 speed = Vector3.zero;
    private float acceleration = 30;
    private float maxSpeed = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    private Vector3 stepsize;
    private Vector3 speedAdd;
    void Update()
    {
        stepsize = Vector3.zero;
        if (Input.GetKey("w"))
            stepsize.x += 1;
        if (Input.GetKey("a"))
            stepsize.z += 1;
        if (Input.GetKey("s"))
            stepsize.x -= 1;
        if (Input.GetKey("d"))
            stepsize.z -= 1;
        
        speed += stepsize.normalized*acceleration*Time.deltaTime;
        transform.position += speed*Time.deltaTime;
    }
}
