using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveAcceleration = 5f;
    public float yawSpeed = 100f;
    public float pitchSpeed = 100f;
    public float rollSpeed = 100f;
    public float resistance = 5f;

    Rigidbody rb;

    
    private void start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Turn();
        Thrust();

    }

    void Turn()
    {
        //yawSpeed +=  Time.deltaTime * (Input.GetAxis("Horizontal")*turnAcceleration-yawSpeed*resistance);
        //pitchSpeed += Time.deltaTime * (turnAcceleration * Input.GetAxis("Pitch")-pitchSpeed*resistance);
        //rollSpeed += Time.deltaTime*(rollAcceleration * Input.GetAxis("Roll")-rollSpeed*resistance);
        //transform.Rotate(-pitchSpeed*Time.deltaTime, yawSpeed*Time.deltaTime, -rollSpeed*Time.deltaTime);
        transform.Rotate(-pitchSpeed*Time.deltaTime* Input.GetAxis("Pitch"), yawSpeed*Time.deltaTime* Input.GetAxis("Horizontal"), -rollSpeed*Time.deltaTime* Input.GetAxis("Roll"));
    }

    Vector3 speed;
    void Thrust()
    {
        speed +=  Time.deltaTime * (moveAcceleration *Input.GetAxis("Vertical")*transform.forward-speed.normalized*speed.sqrMagnitude*resistance/100000);
        transform.position += speed * Time.deltaTime;
    }
}    

