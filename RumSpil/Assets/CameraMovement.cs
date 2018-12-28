using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private ShipMovement target;
    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Awake()
    {
        offset = new Vector3(-6,3.5f,0);
    }

    // Update is called once per frame
    private Vector3 acc = Vector3.zero;
    void LateUpdate()
    {   
        Vector3 desiredPosition = target.transform.position+offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref target.speed, 0.5f);

    }
}
