using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 10f;
    public float turnSpeed = 20f;
    public float rollSpeed = 50f;
    Rigidbody rb;


    private void start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Thrust();
        Turn();
        
    }
    
    void Turn()
    {
        float yaw = turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal");//("Mouse X") 
        float pitch = turnSpeed * Time.deltaTime * Input.GetAxis("Pitch"); //("Mouse Y") kan bruges hvis musen skal styre rotation
        float roll = rollSpeed * Time.deltaTime * Input.GetAxis("Roll");
        transform.Rotate(-pitch,yaw,-roll);
                

    }

    void Thrust()
    {
        transform.position += transform.forward * speed * Time.deltaTime * Input.GetAxis("Vertical");
    }
   
                       
}
