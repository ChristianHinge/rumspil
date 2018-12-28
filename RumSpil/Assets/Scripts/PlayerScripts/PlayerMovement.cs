using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float turnSpeed = 20f;
    public float rollSpeed = 50f;
    public float boostSpeed = 5f;
   

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
        float yaw = turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float pitch = turnSpeed * Time.deltaTime * Input.GetAxis("Pitch");
        float roll = rollSpeed * Time.deltaTime * Input.GetAxis("Roll");
        transform.Rotate(-pitch, yaw, -roll);
    }

    void Thrust()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
    }
}    

