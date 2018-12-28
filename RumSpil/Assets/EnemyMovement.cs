using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 1;
    // Start is called before the first frame update
    Transform target;
    void Awake()
    {
        target = FindObjectOfType<PlayerInput>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.right*Time.deltaTime*30f);
       Move();
    }
    void Move(){

        Vector3 up = transform.position+transform.up*40;
        Vector3 down = transform.position-transform.up*40;
        Vector3 left = transform.position-transform.right*40;
        Vector3 right = transform.position+transform.right*40;
        Debug.DrawRay(up,transform.forward*140);
        Debug.DrawRay(down,transform.forward*140,Color.cyan);
        Debug.DrawRay(left,transform.forward*140);
        Debug.DrawRay(right,transform.forward*140);
        Vector3 adjustment = Vector3.zero;

        if (Physics.Raycast(down,transform.forward,140))
        {
            Debug.Log("He");
            adjustment += Vector3.left;
        }
            
        else if (Physics.Raycast(up,transform.forward,140))
            adjustment += Vector3.right;

        if (Physics.Raycast(right,transform.forward,140))
            adjustment += Vector3.down;
        else if (Physics.Raycast(left,transform.forward,140))
            adjustment += Vector3.up;
            
        if (adjustment == Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(target.position-transform.position,Vector3.up),Time.deltaTime);
            
        }
        else
        {
            
            transform.Rotate(adjustment*Time.deltaTime*30f);
        }
            
        transform.position += transform.forward*speed*Time.deltaTime;
    }
}
